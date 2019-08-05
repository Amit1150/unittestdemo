using MediaStorage.Common;
using MediaStorage.Common.ViewModels.User;
using MediaStorage.Data;
using MediaStorage.Data.Entities;
using MediaStorage.Data.Read;
using MediaStorage.Data.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaStorage.Service
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public List<UserViewModel> GetAllUsers()
        {
            return userRepository.userReadRepository.GetAllUsers();
        }

        public UserPostViewModel GetUserById(Guid id)
        {
            User user = userRepository.userReadRepository.GetUserById(id);
            return user == null ? null : new UserPostViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Mail = user.Mail
            };
        }

        public ServiceResult Login(LoginViewModel model)
        {
            ServiceResult result = new ServiceResult();
            var user = userRepository.userReadRepository.GetByUserIdPassword(model.Username, model.Password);
            if (user == null)
                result.SetFailure("User not found.");
            else
                result.SetSuccess("Login successful.");

            return result;
        }

        public ServiceResult AddUser(UserPostViewModel entity)
        {
            bool isUpdated = false;
            var password = CreateRandomPassword();

            if (userRepository.userReadRepository.GetByUserName(entity.Username) != null)
                return new ServiceResult(false, "Username already exist.");
            if (userRepository.userReadRepository.GetByUserByEmail(entity.Mail) != null)
                return new ServiceResult(false, "Mail already exist.");

            isUpdated = userRepository.userWriteRepository.AddUser(new User
            {
                Username = entity.Username,
                Mail = entity.Mail,
                Password = password,
                IsActive = entity.IsActive
            });

            MailSender ms = new MailSender();
            //ms.Send("example@gmail.com", "Added" + entity.Username, "Welcome ! " + entity.Username).Wait();

            ServiceResult result = new ServiceResult();

            if (!isUpdated)
            {
                result.SetFailure("Error while deleting User.");
            }
            else
            {
                result.SetSuccess("User deleted successfully.");
            }
            return result;
        }

        public ServiceResult RemoveUser(Guid id)
        {
            bool isUpdated = false;
            var user = userRepository.userReadRepository.GetUserById(id);
            if (user != null)
                isUpdated = userRepository.userWriteRepository.DeleteUser(user);

            ServiceResult result = new ServiceResult();

            if (!isUpdated)
            {
                result.SetFailure("Error while deleting User.");
            }
            else
            {
                result.SetSuccess("User deleted successfully.");
            }
            return result;
        }

        private string CreateRandomPassword()
        {
            string charachters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPRSTUVWXYZ0123456789*!+-_.";
            Random random = new Random();
            string password = string.Empty;

            for (int i = 0; i < 16; i++)
            {
                int randomIndex = random.Next(0, charachters.Length);
                password += charachters[randomIndex];
            }

            return password;
        }
    }
}
