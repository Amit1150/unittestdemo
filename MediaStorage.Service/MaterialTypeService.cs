using MediaStorage.Common;
using MediaStorage.Common.ViewModels.MaterialType;
using MediaStorage.Data.Read;
using MediaStorage.Data.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MediaStorage.Common.Constants;

namespace MediaStorage.Service
{
    public class MaterialTypeService : IMaterialTypeService
    {
        public MaterialTypeReadRepository materialTypeReadRepository = new MaterialTypeReadRepository();
        public MaterialTypeWriteRepository materialTypeWriteRepository = new MaterialTypeWriteRepository();
        private ILogger logger;

        public MaterialTypeService(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task<List<MaterialTypeViewModel>> GetAllMaterialTypes()
        {
            List<MaterialTypeViewModel> materialTypes = await GetAllMaterials();
            if (materialTypes == null || !materialTypes.Any())
            {
                logger.Error(NoRecordsExists);
                throw new ResourceNotFoundException(NoRecordsExists);
            }
            return materialTypes;
        }

        public async Task<List<CustomSelectListItem>> GetMaterialTypesAsSelectListItem(int? categoryId)
        {
            List<CustomSelectListItem> materialTypes = await GetMaterialTypeAsSelectListItem(categoryId);

            if (materialTypes == null || !materialTypes.Any())
            {
                logger.Error(NoRecordsExists);
                throw new ResourceNotFoundException(NoRecordsExists);
            }

            if (!materialTypes.Any(x => x.Selected))
            {
                logger.Error(SelectedSubCategoriesDoesNotExistErrorMessage);
                throw new Exception(SelectedSubCategoriesDoesNotExistErrorMessage);
            }

            return materialTypes;
        }

        public async Task<MaterialTypeViewModel> GetMaterialTypeById(int id)
        {
            MaterialTypeViewModel materialType = await GetMaterialById(id);

            if (materialType == null)
            {
                logger.Error(NoRecordsExists);
                throw new ResourceNotFoundException(NoRecordsExists);
            }
            return materialType;
        }

        public async Task<ServiceResult> AddMaterialType(MaterialTypeViewModel entity)
        {
            int id = await AddMaterial(entity);
            ServiceResult result = new ServiceResult() { Id = id };
            if (id < 0)
            {
                result.SetFailure("Error while inserting material.");
            }
            else
            {
                result.SetSuccess("Material added successfully.");
            }
            return result;
        }

        public async Task<ServiceResult> UpdateLibrary(MaterialTypeViewModel entity)
        {
            bool isUpdated = await UpdateMaterialType(entity);
            ServiceResult result = new ServiceResult();
            if (!isUpdated)
            {
                result.SetFailure("Error while material library.");
            }
            else
            {
                result.SetSuccess("Material updated successfully.");
            }
            return result;
        }

        public async Task<ServiceResult> RemoveLibrary(int id)
        {
            bool isUpdated = await RemoveMaterialType(id);
            ServiceResult result = new ServiceResult();
            if (!isUpdated)
            {
                result.SetFailure("Error while deleting material.");
            }
            else
            {
                result.SetSuccess("Library deleted successfully.");
            }
            return result;
        }

        protected virtual async Task<List<MaterialTypeViewModel>> GetAllMaterials()
        {
            return await materialTypeReadRepository.GetAllMaterials();
        }

        protected virtual async Task<List<CustomSelectListItem>> GetMaterialTypeAsSelectListItem(int? categoryId)
        {
            return await materialTypeReadRepository.GetMaterialTypesAsSelectListItem(categoryId);
        }

        protected virtual async Task<MaterialTypeViewModel> GetMaterialById(int id)
        {
            return await materialTypeReadRepository.GetMaterialTypeById(id);
        }

        protected virtual async Task<int> AddMaterial(MaterialTypeViewModel entity)
        {
            return await materialTypeWriteRepository.AddMaterialType(entity);
        }

        protected virtual async Task<bool> UpdateMaterialType(MaterialTypeViewModel entity)
        {
            return await materialTypeWriteRepository.UpdateMaterialType(entity);
        }

        protected virtual async Task<bool> RemoveMaterialType(int id)
        {
            return await materialTypeWriteRepository.RemoveMaterialType(id);
        }
    }
}
