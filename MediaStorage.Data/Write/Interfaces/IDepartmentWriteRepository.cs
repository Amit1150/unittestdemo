using System.Threading.Tasks;
using MediaStorage.Common.ViewModels.Department;

namespace MediaStorage.Data.Write
{
    public interface IDepartmentWriteRepository
    {
        Task<int> AddDepartment(DepartmentViewModel entity);
        Task<bool> DeleteDepartment(int id);
        Task<bool> UpdateDepartment(DepartmentViewModel entity);
    }
}