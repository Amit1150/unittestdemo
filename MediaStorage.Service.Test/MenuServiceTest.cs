using MediaStorage.Common;
using MediaStorage.Common.ViewModels.Menu;
using MediaStorage.Common.ViewModels.Tag;
using MediaStorage.Data;
using MediaStorage.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MediaStorage.Service.Test
{
    [TestClass]
    public class MenuServiceTest
    {
        private IUnitOfWork uow;
        private IRepository<Menu> menuRepository;
        private IRepository<MenuItem> menuItemRepository;
        private IMenuService menuService;
        private IConfigurationProvider configurationProvider;

        public MenuServiceTest()
        {
            this.uow = NSubstitute.Substitute.For<IUnitOfWork>();
            this.menuRepository = NSubstitute.Substitute.For<IRepository<Menu>>();
            this.menuItemRepository = NSubstitute.Substitute.For<IRepository<MenuItem>>();
            this.configurationProvider = NSubstitute.Substitute.For<IConfigurationProvider>();
        }

        [TestInitialize]
        public void TestSetup()
        {
            menuService = new MenuService(this.uow, this.menuRepository, this.menuItemRepository, this.configurationProvider);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            menuRepository.ClearReceivedCalls();
            menuItemRepository.ClearReceivedCalls();
        }

        [TestMethod]
        public void GetAllMenus_ShouldReturnAllMenu()
        {
            // Arrange
            IQueryable<Menu> menus = new List<Menu>()
            {
                new Menu()
                {
                    Id = 1,

                    Name = "PHP"
                },
                new Menu()
                {
                    Id = 1,
                    Name = "JAVA"
                }
            }.AsQueryable();

            this.configurationProvider.GetAppSetting(Arg.Any<string>()).Returns("true");
            this.menuRepository.GetAll().Returns(menus);

            // Act
            var result = this.menuService.GetAllMenus();

            // Assert
            this.menuRepository.Received(1).GetAll();
            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void GetAllMenus_WithCanGetAllMenusFalse_ShouldReturnAllMenu()
        {
            // Arrange
            IQueryable<Menu> menus = new List<Menu>()
            {
                new Menu()
                {
                    Id = 1,

                    Name = "PHP"
                },
                new Menu()
                {
                    Id = 1,
                    Name = "JAVA"
                }
            }.AsQueryable();

            this.configurationProvider.GetAppSetting(Arg.Any<string>()).Returns("false");
            this.menuRepository.GetAll().Returns(menus);

            // Act
            var result = this.menuService.GetAllMenus();

            // Assert
            this.menuRepository.Received(1).GetAll();
            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void GetAllMenus_WithCanGetAllMenusFalse_ShouldReturnNull()
        {
            // Arrange
            this.configurationProvider.GetAppSetting(Arg.Any<string>()).Returns("123");

            // Act
            var result = this.menuService.GetAllMenus();

            // Assert
            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void GetAllMenusBySelectListItem_ShouldReturnAllMenu()
        {
            // Arrange
            IQueryable<Menu> menus = new List<Menu>()
            {
                new Menu()
                {
                    Id = 1,

                    Name = "PHP"
                },
                new Menu()
                {
                    Id = 1,
                    Name = "JAVA"
                }
            }.AsQueryable();

            this.menuRepository.GetAll().Returns(menus);

            // Act
            var result = this.menuService.GetAllMenusBySelectListItem(null);

            // Assert
            this.menuRepository.Received(1).GetAll();
            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void GetAllMenusBySelectListItem_WithId_ShouldReturnAllMenu()
        {
            // Arrange
            IQueryable<Menu> menus = new List<Menu>()
            {
                new Menu()
                {
                    Id = 1,

                    Name = "PHP",
                    MenuItems = new List<MenuItem>(){ new MenuItem { Id = 1 } }
                },
                new Menu()
                {
                    Id = 2,
                    Name = "JAVA",
                    MenuItems = new List<MenuItem>()
                }
            }.AsQueryable();

            this.menuRepository.GetAll().Returns(menus);

            // Act
            var result = this.menuService.GetAllMenusBySelectListItem(1);

            // Assert
            this.menuRepository.Received(1).GetAll();
            Assert.AreEqual(result.FirstOrDefault().Selected, true);
        }

        [TestMethod]
        public void GetMenuById_WithValidId_ShouldReturnMenu()
        {
            // Arrange
            int menuId = 1;
            Menu menu = new Menu()
            {
                Id = 1,
                Name = "C#"
            };

            this.menuRepository.Find(Arg.Any<int>()).Returns(menu);

            // Act
            var result = this.menuService.GetMenuById(menuId);

            // Assert
            this.menuRepository.Received(1).Find(Arg.Any<int>());
            Assert.AreEqual(result.Id, menu.Id);
        }

        [TestMethod]
        public void GetMenuById_WithInValidId_ShouldReturnNull()
        {
            // Arrange
            int menuId = -1;

            this.menuRepository.Find(Arg.Any<int>()).ReturnsNull();

            // Act
            var result = this.menuService.GetMenuById(menuId);

            // Assert
            this.menuRepository.Received(1).Find(Arg.Any<int>());
            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void AddMenu_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            MenuViewModel menu = new MenuViewModel()
            {
                Name = "C#"
            };

            this.menuRepository.Add(Arg.Any<Menu>());
            this.uow.Commit().Returns(1);

            // Act
            var result = this.menuService.AddMenu(menu);

            // Assert
            this.uow.Received(1).Commit();
            this.menuRepository.Received(1).Add(Arg.Any<Menu>());

            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public void AddMenu_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            MenuViewModel menu = new MenuViewModel();

            this.menuRepository.Add(Arg.Any<Menu>());
            this.uow.Commit().Returns(0);

            // Act
            var result = this.menuService.AddMenu(menu);

            // Assert
            this.uow.Received(1).Commit();
            this.menuRepository.Received(1).Add(Arg.Any<Menu>());

            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public void UpdateMenu_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            MenuViewModel menu = new MenuViewModel()
            {
                Name = "C#",
                Id = 1,
                Description = "Test"
            };

            this.menuRepository.Update(Arg.Any<Menu>());
            this.uow.Commit().Returns(1);

            // Act
            var result = this.menuService.UpdateMenu(menu);

            // Assert
            this.menuRepository.Received(1).Update(Arg.Any<Menu>());
            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public void UpdateMenu_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            MenuViewModel menu = new MenuViewModel()
            {
                Name = "C#",
                Id = 1,
                Description = "Test"
            };

            this.menuRepository.Update(Arg.Any<Menu>());
            this.uow.Commit().Returns(0);

            // Act
            var result = this.menuService.UpdateMenu(menu);

            // Assert
            this.menuRepository.Received(1).Update(Arg.Any<Menu>());
            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public void RemoveMenu_WithValidId_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            int menuId = 1;
            this.menuRepository.Delete(Arg.Any<int>());
            this.uow.Commit().Returns(1);

            // Act
            var result = this.menuService.RemoveMenu(menuId);

            // Assert
            this.menuRepository.Received(1).Delete(Arg.Any<int>());
            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public void RemoveMenu_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            int menuId = 1;
            IQueryable<MenuItem> menuItems = new List<MenuItem>()
            {
                new MenuItem()
                {
                    Id = 1
                },
                new MenuItem()
                {
                    Id = 1
                }
            }.AsQueryable();

            this.menuItemRepository.GetAll(Arg.Any<Expression<Func<MenuItem, bool>>>(),  Arg.Any<Expression<Func<MenuItem, object>>[]>()).Returns(menuItems);
            this.menuRepository.Delete(Arg.Any<int>());
            this.menuItemRepository.DeleteRange(Arg.Any<ICollection<MenuItem>>());

            this.uow.Commit().Returns(1);

            // Act
            var result = this.menuService.RemoveMenu(menuId, true);

            // Assert
            this.menuRepository.Received(1).Delete(Arg.Any<int>());
            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public void RemoveMenu_WithNoMenuItem_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            int menuId = 1;
            IQueryable<MenuItem> menuItems = new List<MenuItem>().AsQueryable();

            this.menuItemRepository.GetAll(Arg.Any<Expression<Func<MenuItem, bool>>>(), Arg.Any<Expression<Func<MenuItem, object>>[]>()).Returns(menuItems);
            this.menuRepository.Delete(Arg.Any<int>());
            this.menuItemRepository.DeleteRange(Arg.Any<ICollection<MenuItem>>());

            this.uow.Commit().Returns(1);

            // Act
            var result = this.menuService.RemoveMenu(menuId, true);

            // Assert
            this.menuRepository.Received(1).Delete(Arg.Any<int>());
            Assert.AreEqual(true, result.IsSuccessful);
        }
    }
}
