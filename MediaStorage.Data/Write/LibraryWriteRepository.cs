using MediaStorage.Common.ViewModels.Library;
using MediaStorage.Data.Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MediaStorage.Data.Write
{
    public class LibraryWriteRepository : ILibraryWriteRepository
    {
        private readonly IMediaContext mediaContext;
        public LibraryWriteRepository(IMediaContext mediaContext)
        {
            this.mediaContext = mediaContext;
        }
        
        public async Task<int> AddLibrary(LibraryViewModel entity)
        {
            var result = 0;
            using (var transaction = mediaContext.Database.BeginTransaction())
            {
                try
                {
                    var library = new Library
                    {
                        Name = entity.Name
                    };
                    mediaContext.Libraries.Add(library);
                    await mediaContext.SaveChangesAsync();
                    result = library.Id;
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




        public async Task<bool> UpdateLibrary(LibraryViewModel entity)
        {
            var result = false;
            using (var transaction = mediaContext.Database.BeginTransaction())
            {
                try
                {
                    var library = await mediaContext.Libraries.FirstOrDefaultAsync(x => x.Id == entity.Id);
                    library.Name = entity.Name;

                    mediaContext.Libraries.Add(library);
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

        public async Task<bool> DeleteLibrary(int id)
        {
            var result = false;
            using (var transaction = mediaContext.Database.BeginTransaction())
            {
                try
                {
                    var library = await mediaContext.Libraries.FirstOrDefaultAsync(x => x.Id == id);
                    library.IsDeleted = true;
                    mediaContext.Libraries.Add(library);
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
