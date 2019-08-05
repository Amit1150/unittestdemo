using System.Collections.Generic;
using System.Threading.Tasks;
using MediaStorage.Common;
using MediaStorage.Common.ViewModels.MaterialType;

namespace MediaStorage.Service
{
    public interface IMaterialTypeService
    {
        Task<ServiceResult> AddMaterialType(MaterialTypeViewModel entity);
        Task<List<MaterialTypeViewModel>> GetAllMaterialTypes();
        Task<MaterialTypeViewModel> GetMaterialTypeById(int id);
        Task<List<CustomSelectListItem>> GetMaterialTypesAsSelectListItem(int? categoryId);
        Task<ServiceResult> RemoveLibrary(int id);
        Task<ServiceResult> UpdateLibrary(MaterialTypeViewModel entity);
    }
}