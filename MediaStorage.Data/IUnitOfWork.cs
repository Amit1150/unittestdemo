using System.Threading.Tasks;

namespace MediaStorage.Data
{
    public interface IUnitOfWork
    {
        int Commit();
        void Dispose();
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}