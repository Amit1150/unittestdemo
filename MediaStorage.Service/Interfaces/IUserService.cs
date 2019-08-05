using System;
using System.Collections.Generic;
using MediaStorage.Common;
using MediaStorage.Common.ViewModels.User;

namespace MediaStorage.Service
{
    public interface IUserService
    {
        ServiceResult AddUser(UserPostViewModel entity);
        List<UserViewModel> GetAllUsers();
        UserPostViewModel GetUserById(Guid id);
        ServiceResult Login(LoginViewModel model);
        ServiceResult RemoveUser(Guid id);
    }
}