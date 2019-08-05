using MediaStorage.Common;
using MediaStorage.Common.ViewModels.User;
using MediaStorage.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStorage.Data.Read
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly IMediaContext mediaContext;
        public UserReadRepository(IMediaContext mediaContext)
        {
            this.mediaContext = mediaContext;
        }

        public List<UserViewModel> GetAllUsers()
        {
            return mediaContext
                   .Users
                   .Select(s => new UserViewModel
                   {
                       Id = s.Id.ToString(),
                       Username = s.Username,
                       Mail = s.Mail,
                       IsActive = s.IsActive
                   }).ToList();
        }

        public User GetUserById(Guid userId)
        {
            return mediaContext.Users.Where(x => !x.IsDeleted).FirstOrDefault();
        }

        public User GetByUserIdPassword(string userName, string password)
        {
            return mediaContext.Users.Where(x => !x.IsDeleted && x.Username == userName && x.Password == password).FirstOrDefault();
        }
        public User GetByUserName(string userName)
        {
            return mediaContext
                     .Users
                     .Where(x => !x.IsDeleted && x.Username == userName).FirstOrDefault();
        }
        public User GetByUserByEmail(string email)
        {
            return mediaContext
                   .Users
                   .Where(x => !x.IsDeleted && x.Mail == email).FirstOrDefault();
        }
    }
}
