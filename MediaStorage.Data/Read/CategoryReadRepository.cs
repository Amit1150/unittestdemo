using MediaStorage.Common;
using MediaStorage.Common.ViewModels.Category;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStorage.Data.Read
{
    public class CategoryReadRepository : ICategoryReadRepository
    {
        private readonly IMediaContext mediaContext;
        public CategoryReadRepository(IMediaContext mediaContext)
        {
            this.mediaContext = mediaContext;
        }

        public async Task<List<CategoryListViewModel>> GetAllCategories()
        {
            return await mediaContext
                    .Categories
                    .Where(x => !x.IsDeleted)
                    .Select(s => new CategoryListViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        ParentCategory = s.ParentCategory.Name,
                        MaterialType = s.MaterialType.Name
                    }).ToListAsync();
        }


        public async Task<List<CustomSelectListItem>> GetSubCategoriesAsSelectListItem(int id)
        {
            return await mediaContext.Categories
                   .Select(s => new CustomSelectListItem
                   {
                       Value = s.Id.ToString(),
                       Text = s.Name,
                       Selected = s.SubCategories.Any(w => w.Id == id)
                   }).ToListAsync();
        }

        public async Task<CategoryViewModel> GetCategoryById(int id)
        {
            return await mediaContext.Categories.Select(category => new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId,
                MaterialTypeId = category.MaterialTypeId
            }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CategoryViewModel>> GetCategoriesByMaterialTypeId(int materialTypeId)
        {
            return await mediaContext.Categories.Where(x => x.MaterialTypeId == materialTypeId)
                    .Select(category => new CategoryViewModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        ParentCategoryId = category.ParentCategoryId,
                        MaterialTypeId = category.MaterialTypeId
                    }).ToListAsync();
        }


    }
}
