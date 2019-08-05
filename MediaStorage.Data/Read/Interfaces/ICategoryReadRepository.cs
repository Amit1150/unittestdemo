using System.Collections.Generic;
using System.Threading.Tasks;
using MediaStorage.Common;
using MediaStorage.Common.ViewModels.Category;

namespace MediaStorage.Data.Read
{
    public interface ICategoryReadRepository
    {
        Task<List<CategoryListViewModel>> GetAllCategories();
        Task<List<CategoryViewModel>> GetCategoriesByMaterialTypeId(int materialTypeId);
        Task<CategoryViewModel> GetCategoryById(int id);
        Task<List<CustomSelectListItem>> GetSubCategoriesAsSelectListItem(int id);
    }
}