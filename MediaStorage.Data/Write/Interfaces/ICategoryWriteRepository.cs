using System.Threading.Tasks;
using MediaStorage.Common.ViewModels.Category;

namespace MediaStorage.Data.Write
{
    public interface ICategoryWriteRepository
    {
        Task<int> AddCategory(CategoryViewModel entity);
        Task<bool> DeleteCategory(int id);
        Task<bool> UpdateCategory(CategoryViewModel entity);
    }
}