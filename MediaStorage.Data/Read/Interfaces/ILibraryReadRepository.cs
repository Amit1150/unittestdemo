using System.Collections.Generic;
using System.Threading.Tasks;
using MediaStorage.Common;
using MediaStorage.Common.ViewModels.Library;

namespace MediaStorage.Data.Read
{
    public interface ILibraryReadRepository
    {
        Task<List<LibraryViewModel>> GetAllLibraries();
        Task<List<CustomSelectListItem>> GetLibrariesAsSelectListItem(int? departmentId);
        Task<LibraryViewModel> GetLibraryById(int id);
    }
}