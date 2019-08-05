using MediaStorage.Data.Entities;

namespace MediaStorage.Data.Read
{
    public interface IUserWriteRepository
    {
        bool AddUser(User user);
        bool DeleteUser(User user);
    }
}