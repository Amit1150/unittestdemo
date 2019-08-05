using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using MediaStorage.Data.Entities;

namespace MediaStorage.Data
{
    public interface IMediaContext : IDisposable
    {
        Database Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry Entry(object entity);

        Task<int> SaveChangesAsync();

        int SaveChanges();


        DbSet<Category> Categories { get; set; }
        DbSet<Department> Departments { get; set; }
        DbSet<Lending> Lendings { get; set; }
        DbSet<Library> Libraries { get; set; }
        DbSet<MaterialPropertyItem> MaterialPropertyItems { get; set; }
        DbSet<Material> Materials { get; set; }
        DbSet<MaterialTypeProperty> MaterialTypeProperties { get; set; }
        DbSet<MaterialType> MaterialTypes { get; set; }
        DbSet<Member> Members { get; set; }
        DbSet<MenuItem> MenuItems { get; set; }
        DbSet<Menu> Menus { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<Stock> Stocks { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<User> Users { get; set; }
    }
}