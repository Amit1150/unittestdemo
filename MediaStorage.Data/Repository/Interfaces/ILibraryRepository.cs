using MediaStorage.Data.Read;
using MediaStorage.Data.Write;

namespace MediaStorage.Data.Repository
{
    public interface ILibraryRepository
    {
        ILibraryReadRepository LibraryReadRepository { get; set; }
        ILibraryWriteRepository LibraryWriteRepository { get; set; }
    }
}