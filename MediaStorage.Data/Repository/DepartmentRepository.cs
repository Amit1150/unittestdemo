using MediaStorage.Data.Read;
using MediaStorage.Data.Write;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStorage.Data.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public IDepartmentReadRepository DepartmentReadRepository { get; set; }
        public IDepartmentWriteRepository DepartmentWriteRepository { get; set; }

        public DepartmentRepository(IDepartmentReadRepository departmentReadRepository, IDepartmentWriteRepository departmentWriteRepository)
        {
            DepartmentReadRepository = departmentReadRepository;
            DepartmentWriteRepository = departmentWriteRepository;
        }

        
    }
}
