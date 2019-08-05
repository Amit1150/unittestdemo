using MediaStorage.Common.ViewModels.Category;
using MediaStorage.Data.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStorage.Data.Write
{
    public class CategoryWriteRepository : ICategoryWriteRepository
    {
        private readonly IMediaContext mediaContext;
        public CategoryWriteRepository(IMediaContext mediaContext)
        {
            this.mediaContext = mediaContext;
        }

        public async Task<int> AddCategory(CategoryViewModel entity)
        {
            var result = 0;
            using (var transaction = mediaContext.Database.BeginTransaction())
            {
                try
                {
                    var category = new Category
                    {
                        Name = entity.Name,
                        ParentCategoryId = entity.ParentCategoryId,
                        MaterialTypeId = entity.MaterialTypeId
                    };
                    mediaContext.Categories.Add(category);
                    await mediaContext.SaveChangesAsync();
                    result = category.Id;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return result;
        }


        public async Task<bool> DeleteCategory(int id)
        {
            var result = false;
            using (var transaction = mediaContext.Database.BeginTransaction())
            {
                try
                {
                    Category category = await mediaContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
                    category.IsDeleted = true;
                    mediaContext.Categories.Add(category);
                    await mediaContext.SaveChangesAsync();
                    result = true;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return result;
        }

        public async Task<bool> UpdateCategory(CategoryViewModel entity)
        {
            var result = false;
            using (var transaction = mediaContext.Database.BeginTransaction())
            {
                try
                {
                    var category = new Category
                    {
                        Name = entity.Name,
                        ParentCategoryId = entity.ParentCategoryId,
                        MaterialTypeId = entity.MaterialTypeId,
                        Id = entity.Id.Value
                    };
                    mediaContext.Categories.Add(category);
                    await mediaContext.SaveChangesAsync();
                    result = true;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return result;
        }
    }
}
