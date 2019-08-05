using System.Threading.Tasks;
using MediaStorage.Common.ViewModels.Library;

namespace MediaStorage.Data.Write
{
    public interface ILibraryWriteRepository
    {
        Task<int> AddLibrary(LibraryViewModel entity);
        Task<bool> DeleteLibrary(int id);
        Task<bool> UpdateLibrary(LibraryViewModel entity);
    }
}