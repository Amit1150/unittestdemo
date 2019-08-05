using MediaStorage.Data.Read;
using MediaStorage.Data.Write;

namespace MediaStorage.Data.Repository
{
    public interface IDepartmentRepository
    {
        IDepartmentReadRepository DepartmentReadRepository { get; set; }
        IDepartmentWriteRepository DepartmentWriteRepository { get; set; }
    }
}