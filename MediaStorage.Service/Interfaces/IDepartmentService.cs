using System.Collections.Generic;
using System.Threading.Tasks;
using MediaStorage.Common;
using MediaStorage.Common.ViewModels.Department;

namespace MediaStorage.Service
{
    public interface IDepartmentService
    {
        Task<ServiceResult> AddDepartment(DepartmentViewModel entity);
        Task<List<DepartmentListViewModel>> GetAllDepartments();
        Task<DepartmentViewModel> GetDepartmentById(int id);
        Task<List<DepartmentListViewModel>> GetDepartmentsByLibraryId(int libraryId);
        Task<bool> HasDepartmentsByLibraryId(int libraryId);
        Task<ServiceResult> RemoveDepartment(int id);
        Task<ServiceResult> UpdateDepartment(DepartmentViewModel entity);
    }
}