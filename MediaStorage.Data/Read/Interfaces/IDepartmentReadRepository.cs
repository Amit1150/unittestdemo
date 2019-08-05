using System.Collections.Generic;
using System.Threading.Tasks;
using MediaStorage.Common.ViewModels.Department;

namespace MediaStorage.Data.Read
{
    public interface IDepartmentReadRepository
    {
        Task<List<DepartmentListViewModel>> GetAllDepartments();
        Task<DepartmentViewModel> GetDepartmentById(int departmentId);
        Task<List<DepartmentListViewModel>> GetDepartmentsByLibraryId(int libraryId);
    }
}