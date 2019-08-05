using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaStorage.Host
{
    using MediaStorage.Common;
    using MediaStorage.Data;
    using MediaStorage.Data.Read;
    using MediaStorage.Data.Repository;
    using MediaStorage.Data.Write;
    using MediaStorage.Service;
    using System.Data.Entity;
    using Unity;
    using Unity.Injection;
    using Unity.Lifetime;

    class Program
    {
        static void Main(string[] args)
        {

            var container = RegisterDI();

            var departmentService = container.Resolve<DepartmentService>();
            var libraryService = container.Resolve<LibraryService>();
            var materialTypeService = container.Resolve<MaterialTypeService>();
            var menuService = container.Resolve<MenuService>();
            var tagService = container.Resolve<TagService>();
            var userService = container.Resolve<UserService>();

            menuService.GetAllMenus();
            var lib = libraryService.AddLibrary(new Common.ViewModels.Library.LibraryViewModel { Name = "TestLiblary" }).Result;
            var dep = departmentService.AddDepartment(new Common.ViewModels.Department.DepartmentViewModel { Name = "Test", LibraryId = lib.Id }).Result;
            var mtype = materialTypeService.AddMaterialType(new Common.ViewModels.MaterialType.MaterialTypeViewModel { Name = "TestMaterialType" }).Result;
            var menu = menuService.AddMenu(new Common.ViewModels.Menu.MenuViewModel { Name = "Test Menu", Description = "Demo" });
            var tegData = tagService.AddTag(new Common.ViewModels.Tag.TagViewModel { Name = "TestTag" });
            var userData = userService.AddUser(new Common.ViewModels.User.UserPostViewModel { Username = "TestUSer", IsActive = true, Mail = "a@b.com" });

            

        }

        private static UnityContainer RegisterDI()
        {
            var container = new UnityContainer();

            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));

            container.RegisterType<ICategoryReadRepository, CategoryReadRepository>();
            container.RegisterType<IDepartmentReadRepository, DepartmentReadRepository>();
            container.RegisterType<ILibraryReadRepository, LibraryReadRepository>();
            container.RegisterType<IUserReadRepository, UserReadRepository>();
            container.RegisterType<IDepartmentRepository, DepartmentRepository>();
            container.RegisterType<ILibraryRepository, LibraryRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ICategoryWriteRepository, CategoryWriteRepository>();
            container.RegisterType<IDepartmentWriteRepository, DepartmentWriteRepository>();
            container.RegisterType<ILibraryWriteRepository, LibraryWriteRepository>();
            container.RegisterType<IUserWriteRepository, UserWriteRepository>();

            container.RegisterType<ILogger, Logger>();
            container.RegisterType<IConfigurationProvider, ConfigurationProvider>();

            container.RegisterType<IDepartmentService, DepartmentService>();
            container.RegisterType<ILibraryService, LibraryService>();
            container.RegisterType<IMaterialTypeService, MaterialTypeService>();
            container.RegisterType<IMenuService, MenuService>();
            container.RegisterType<ITagService, TagService>();
            container.RegisterType<IUserService, UserService>();

            container.RegisterType<IMediaContext, MediaContext>(new PerResolveLifetimeManager());

            return container;
        }

    }
}
