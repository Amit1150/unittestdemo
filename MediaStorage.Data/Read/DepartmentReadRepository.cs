namespace MediaStorage.Data.Read
{
    using MediaStorage.Common.ViewModels.Department;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class DepartmentReadRepository : IDepartmentReadRepository
    {
        private readonly IMediaContext mediaContext;
        public DepartmentReadRepository(IMediaContext mediaContext)
        {
            this.mediaContext = mediaContext;
        }

        public async Task<List<DepartmentListViewModel>> GetAllDepartments()
        {
            return await mediaContext.Departments
                .Select(s => new DepartmentListViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    LibraryName = s.Library.Name
                }).ToListAsync();
        }

        public async Task<List<DepartmentListViewModel>> GetDepartmentsByLibraryId(int libraryId)
        {
            return await mediaContext.Departments
                    .Where(x => x.LibraryId == libraryId)
                .Select(s => new DepartmentListViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    LibraryName = s.Library.Name
                }).ToListAsync();
        }

        public async Task<DepartmentViewModel> GetDepartmentById(int departmentId)
        {
            return await mediaContext.Departments
                    .Where(x => x.Id == departmentId)
                .Select(s => new DepartmentViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    LibraryId = s.Library.Id
                }).FirstOrDefaultAsync();
        }
        
    }
}
