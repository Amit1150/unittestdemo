using System.Collections.Generic;
using System.Threading.Tasks;
using MediaStorage.Common;
using MediaStorage.Common.ViewModels.Library;

namespace MediaStorage.Service
{
    public interface ILibraryService
    {
        Task<ServiceResult> AddLibrary(LibraryViewModel entity);
        Task<List<LibraryViewModel>> GetAllLibraries();
        Task<List<CustomSelectListItem>> GetLibrariesAsSelectListItem(int? departmentId);
        Task<LibraryViewModel> GetLibraryById(int id);
        Task<ServiceResult> RemoveLibrary(int id);
        Task<ServiceResult> UpdateLibrary(LibraryViewModel entity);
    }
}