using MediaStorage.Data.Read;

namespace MediaStorage.Data.Repository
{    public class UserRepository : IUserRepository
    {
        public IUserReadRepository userReadRepository { get; set; }
        public IUserWriteRepository userWriteRepository { get; set; }

        public UserRepository(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository)
        {
            this.userReadRepository = userReadRepository;
            this.userWriteRepository = userWriteRepository;
        }
    }
}
