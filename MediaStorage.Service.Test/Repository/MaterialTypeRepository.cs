using MediaStorage.Common;
using MediaStorage.Common.ViewModels.MaterialType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStorage.Service.Test.Repository
{
    public class MaterialTypeRepository : MaterialTypeService
    {
        private readonly List<MaterialTypeViewModel> materialTypeList;
        private readonly List<CustomSelectListItem> selectListItems;
        private readonly MaterialTypeViewModel materialTypeViewModel;
        private readonly bool isCompleted;

        public MaterialTypeRepository(ILogger logger, List<MaterialTypeViewModel> data) 
            : base(logger)
        {
            materialTypeList = data;
        }

        public MaterialTypeRepository(ILogger logger, List<CustomSelectListItem> data)
            : base(logger)
        {
            selectListItems = data;
        }

        public MaterialTypeRepository(ILogger logger, MaterialTypeViewModel data)
            : base(logger)
        {
            materialTypeViewModel = data;
        }

        public MaterialTypeRepository(ILogger logger, bool isCompleted)
            : base(logger)
        {
            this.isCompleted = isCompleted;
        }

        protected override async Task<List<MaterialTypeViewModel>> GetAllMaterials()
        {
            return materialTypeList;
        }

        protected override async Task<List<CustomSelectListItem>> GetMaterialTypeAsSelectListItem(int? categoryId)
        {
            return selectListItems;
        }

        protected override async Task<MaterialTypeViewModel> GetMaterialById(int id)
        {
            return materialTypeViewModel;
        }

        protected override async Task<int> AddMaterial(MaterialTypeViewModel entity)
        {
            return isCompleted ? 1 : -1;
        }

        protected override async Task<bool> UpdateMaterialType(MaterialTypeViewModel entity)
        {
            return isCompleted;
        }

        protected override async Task<bool> RemoveMaterialType(int id)
        {
            return isCompleted;
        }
    }
}
