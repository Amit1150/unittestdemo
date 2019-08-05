using System;
using System.Collections.Generic;
using MediaStorage.Common.ViewModels.User;
using MediaStorage.Data.Entities;

namespace MediaStorage.Data.Read
{
    public interface IUserReadRepository
    {
        List<UserViewModel> GetAllUsers();
        User GetByUserByEmail(string email);
        User GetByUserIdPassword(string userName, string password);
        User GetByUserName(string userName);
        User GetUserById(Guid userId);
    }
}