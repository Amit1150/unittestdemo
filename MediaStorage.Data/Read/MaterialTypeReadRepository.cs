using MediaStorage.Common;
using MediaStorage.Common.ViewModels.MaterialType;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStorage.Data.Read
{
    public class MaterialTypeReadRepository
    {

        public async Task<List<MaterialTypeViewModel>> GetAllMaterials()
        {
            using (var mediaContext = new MediaContext())
            {
                return await mediaContext
                    .MaterialTypes
                    .Where(x => !x.IsDeleted)
                    .Select(s => new MaterialTypeViewModel
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToListAsync();
            }
        }

        public async Task<List<CustomSelectListItem>> GetMaterialTypesAsSelectListItem(int? categoryId)
        {
            using (var mediaContext = new MediaContext())
            {
                return await mediaContext
                    .MaterialTypes
                    .Where(x => !x.IsDeleted)
                    .Select(s => new CustomSelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name,
                        Selected = categoryId.HasValue ? s.Categories.Any(w => w.Id == categoryId) : false
                    }).ToListAsync();
            }
        }

        public async Task<MaterialTypeViewModel> GetMaterialTypeById(int id)
        {
            using (var mediaContext = new MediaContext())
            {
                return await mediaContext
                    .Libraries
                    .Where(x => !x.IsDeleted)
                    .Select(material => new MaterialTypeViewModel
                    {
                        Id = material.Id,
                        Name = material.Name
                    }).FirstOrDefaultAsync();
            }
        }

    }
}
