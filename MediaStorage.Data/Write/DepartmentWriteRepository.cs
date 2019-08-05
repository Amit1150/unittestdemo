using MediaStorage.Common.ViewModels.Department;
using MediaStorage.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStorage.Data.Write
{
    public class DepartmentWriteRepository : IDepartmentWriteRepository
    {
        private readonly IMediaContext mediaContext;
        public DepartmentWriteRepository(IMediaContext mediaContext)
        {
            this.mediaContext = mediaContext;
        }

        public async Task<int> AddDepartment(DepartmentViewModel entity)
        {
            var result = 0;
            using (var transaction = mediaContext.Database.BeginTransaction())
            {
                try
                {
                    var department = new Department
                    {
                        Name = entity.Name,
                        LibraryId = entity.LibraryId
                    };
                    mediaContext.Departments.Add(department);
                    await mediaContext.SaveChangesAsync();
                    result = department.Id;
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


        public async Task<bool> UpdateDepartment(DepartmentViewModel entity)
        {
            var result = false;
            using (var transaction = mediaContext.Database.BeginTransaction())
            {
                try
                {
                    var department = await mediaContext.Departments.FirstOrDefaultAsync(x => x.Id == entity.Id);
                    department.LibraryId = entity.LibraryId;
                    department.Name = entity.Name;

                    mediaContext.Departments.Add(department);
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

        public async Task<bool> DeleteDepartment(int id)
        {
            var result = false;
            using (var transaction = mediaContext.Database.BeginTransaction())
            {
                try
                {
                    var department = await mediaContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
                    department.IsDeleted = true;
                    mediaContext.Departments.Add(department);
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
