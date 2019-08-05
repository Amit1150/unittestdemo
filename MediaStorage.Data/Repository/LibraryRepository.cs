using MediaStorage.Data.Read;
using MediaStorage.Data.Write;

namespace MediaStorage.Data.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        public ILibraryReadRepository LibraryReadRepository { get; set; }
        public ILibraryWriteRepository LibraryWriteRepository { get; set; }

        public LibraryRepository(ILibraryReadRepository libraryReadRepository, ILibraryWriteRepository libraryWriteRepository)
        {
            LibraryReadRepository = libraryReadRepository;
            LibraryWriteRepository = libraryWriteRepository;
        }
    }
}
