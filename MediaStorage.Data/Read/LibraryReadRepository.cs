using MediaStorage.Common;
using MediaStorage.Common.ViewModels.Library;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStorage.Data.Read
{
    public class LibraryReadRepository : ILibraryReadRepository
    {
        private readonly IMediaContext mediaContext;
        public LibraryReadRepository(IMediaContext mediaContext)
        {
            this.mediaContext = mediaContext;
        }

        public async Task<List<LibraryViewModel>> GetAllLibraries()
        {
            using (var mediaContext = new MediaContext())
            {
                return await mediaContext
                    .Libraries
                    .Where(x => !x.IsDeleted)
                    .Select(s => new LibraryViewModel
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToListAsync();
            }
        }

        public async Task<List<CustomSelectListItem>> GetLibrariesAsSelectListItem(int? departmentId)
        {
            return await mediaContext
                    .Libraries
                    .Where(x => !x.IsDeleted)
                    .Select(s => new CustomSelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name,
                        Selected = departmentId.HasValue ? s.Departments.Any(w => w.Id == departmentId) : false
                    }).ToListAsync();
        }

        public async Task<LibraryViewModel> GetLibraryById(int id)
        {
            return await mediaContext
                    .Libraries
                    .Where(x => !x.IsDeleted)
                    .Select(library => new LibraryViewModel
                    {
                        Id = library.Id,
                        Name = library.Name
                    }).FirstOrDefaultAsync();
        }

    }
}
