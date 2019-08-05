using MediaStorage.Common.ViewModels.MaterialType;
using MediaStorage.Data.Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MediaStorage.Data.Write
{
    public class MaterialTypeWriteRepository
    {
        public async Task<int> AddMaterialType(MaterialTypeViewModel entity)
        {
            var result = 0;
            using (var mediaContext = new MediaContext())
            {
                using (var transaction = mediaContext.Database.BeginTransaction())
                {
                    try
                    {
                        var materialType = new MaterialType
                        {
                            Name = entity.Name
                        };
                        mediaContext.MaterialTypes.Add(materialType);
                        await mediaContext.SaveChangesAsync();
                        result = materialType.Id;
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            return result;
        }




        public async Task<bool> UpdateMaterialType(MaterialTypeViewModel entity)
        {
            var result = false;
            using (var mediaContext = new MediaContext())
            {
                using (var transaction = mediaContext.Database.BeginTransaction())
                {
                    try
                    {
                        var materialType = await mediaContext.MaterialTypes.FirstOrDefaultAsync(x => x.Id == entity.Id);
                        materialType.Name = entity.Name;

                        mediaContext.MaterialTypes.Add(materialType);
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
            }
            return result;
        }

        public async Task<bool> RemoveMaterialType(int id)
        {
            var result = false;
            using (var mediaContext = new MediaContext())
            {
                using (var transaction = mediaContext.Database.BeginTransaction())
                {
                    try
                    {
                        var materialType = await mediaContext.MaterialTypes.FirstOrDefaultAsync(x => x.Id == id);
                        materialType.IsDeleted = true;
                        mediaContext.MaterialTypes.Add(materialType);
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
            }
            return result;
        }



    }
}
