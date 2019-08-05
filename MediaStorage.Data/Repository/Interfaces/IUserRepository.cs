using MediaStorage.Data.Read;

namespace MediaStorage.Data.Repository
{
    public interface IUserRepository
    {
        IUserReadRepository userReadRepository { get; set; }
        IUserWriteRepository userWriteRepository { get; set; }
    }
}