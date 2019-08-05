using MediaStorage.Common;
using MediaStorage.Common.ViewModels.Department;
using MediaStorage.Common.ViewModels.Library;
using MediaStorage.Data.Read;
using MediaStorage.Data.Repository;
using MediaStorage.Data.Write;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStorage.Service.Test
{
    [TestClass]
    public class LibraryServiceTest
    {
        private ILibraryRepository libraryRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IDepartmentReadRepository departmentReadRepository;
        private readonly IDepartmentWriteRepository departmentWriteRepository;

        private readonly ILibraryReadRepository libraryReadRepository;
        private readonly ILibraryWriteRepository libraryWriteRepository;

        private readonly ILogger logger;
        private ILibraryService libraryService;

        public LibraryServiceTest()
        {
            this.departmentRepository = NSubstitute.Substitute.For<IDepartmentRepository>();
            this.libraryRepository = NSubstitute.Substitute.For<ILibraryRepository>();
            this.departmentReadRepository = NSubstitute.Substitute.For<IDepartmentReadRepository>();
            this.departmentWriteRepository = NSubstitute.Substitute.For<IDepartmentWriteRepository>();
            this.libraryReadRepository = NSubstitute.Substitute.For<ILibraryReadRepository>();
            this.libraryWriteRepository = NSubstitute.Substitute.For<ILibraryWriteRepository>();

            this.logger = NSubstitute.Substitute.For<ILogger>();

            this.departmentRepository.DepartmentReadRepository = this.departmentReadRepository;
            this.departmentRepository.DepartmentWriteRepository = this.departmentWriteRepository;
            this.libraryRepository.LibraryReadRepository = this.libraryReadRepository;
            this.libraryRepository.LibraryWriteRepository = this.libraryWriteRepository;
        }

        [TestInitialize]
        public void TestSetup()
        {
            libraryService = new LibraryService(this.libraryRepository,this.departmentRepository, this.logger);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            libraryRepository.ClearReceivedCalls();
            departmentRepository.ClearReceivedCalls();
            logger.ClearReceivedCalls();
        }

        [TestMethod]
        public async Task GetAllLibraries_ShouldReturnAllLibraries()
        {
            // Arrange
            List<LibraryViewModel> libraries = new List<LibraryViewModel>()
            {
                new LibraryViewModel()
                {
                    Id = 1,
                    Name = "Sales"
                }
            };

            this.libraryRepository.LibraryReadRepository.GetAllLibraries().Returns(libraries);

            // Act
            var result = await this.libraryService.GetAllLibraries();

            // Assert
            await this.libraryRepository.LibraryReadRepository.Received(1).GetAllLibraries();
            Assert.AreEqual(result.FirstOrDefault().Id, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public async Task GetAllLibraries_WithNull_ShouldThrowResourceNotFoundException()
        {
            // Arrange
            this.libraryRepository.LibraryReadRepository.GetAllLibraries().ReturnsNull();

            // Act
            var result = await this.libraryService.GetAllLibraries();

            // Assert
            await this.libraryRepository.LibraryReadRepository.Received(1).GetAllLibraries();
        }

        [TestMethod]
        public async Task GetLibrariesAsSelectListItem_ShouldReturnAllLibrariesAsSelectListItem()
        {
            // Arrange
            int departmentId = 1;
            List<CustomSelectListItem> libraries = new List<CustomSelectListItem>()
            {
                new CustomSelectListItem()
                {
                    Value = "1",
                    Text = "Sales",
                    Selected = false
                }, new CustomSelectListItem()
                {
                    Value = "2",
                    Text = "HR",
                    Selected = true
                }
            };

            this.libraryRepository.LibraryReadRepository.GetLibrariesAsSelectListItem(Arg.Any<int>()).Returns(libraries);

            // Act
            var result = await this.libraryService.GetLibrariesAsSelectListItem(departmentId);

            // Assert
            await this.libraryRepository.LibraryReadRepository.Received(1).GetLibrariesAsSelectListItem(Arg.Any<int>());
            Assert.AreEqual(result, libraries);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public async Task GetLibrariesAsSelectListItem_WithNull_ShouldThrowResourceNotFoundException()
        {
            // Arrange
            int departmentId = 1;

            this.libraryRepository.LibraryReadRepository.GetLibrariesAsSelectListItem(Arg.Any<int>()).ReturnsNull();

            // Act
            var result = await this.libraryService.GetLibrariesAsSelectListItem(departmentId);

            // Assert
            await this.libraryRepository.LibraryReadRepository.Received(1).GetLibrariesAsSelectListItem(Arg.Any<int>());
        }

        [TestMethod]
        public async Task GetLibraryById_WithValidId_ShouldReturnLibrary()
        {
            // Arrange
            int libraryId = 1;
            LibraryViewModel library = new LibraryViewModel()
            {
                Id = 1,
                Name = "Sales"
            };

            this.libraryRepository.LibraryReadRepository.GetLibraryById(Arg.Any<int>()).Returns(library);

            // Act
            var result = await this.libraryService.GetLibraryById(libraryId);

            // Assert
            await this.libraryRepository.LibraryReadRepository.Received(1).GetLibraryById(Arg.Any<int>());
            Assert.AreEqual(result, library);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public async Task GetLibraryById_WithInValidId_ShouldThrowResourceNotFoundException()
        {
            // Arrange
            int libraryId = -1;

            this.libraryRepository.LibraryReadRepository.GetLibraryById(Arg.Any<int>()).ReturnsNull();

            // Act
            var result = await this.libraryService.GetLibraryById(libraryId);

            // Assert
            await this.libraryRepository.LibraryReadRepository.Received(1).GetLibraryById(Arg.Any<int>());
        }


        [TestMethod]
        public async Task AddLibrary_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            LibraryViewModel library = new LibraryViewModel()
            {
                Name = "HR"
            };

            this.libraryRepository.LibraryWriteRepository.AddLibrary(Arg.Any<LibraryViewModel>()).Returns(1);

            // Act
            var result = await this.libraryService.AddLibrary(library);

            // Assert
            await this.libraryRepository.LibraryWriteRepository.Received(1).AddLibrary(Arg.Any<LibraryViewModel>());
            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public async Task AddLibrary_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            LibraryViewModel library = new LibraryViewModel()
            {
                Name = "HR"
            };
            this.libraryRepository.LibraryWriteRepository.AddLibrary(Arg.Any<LibraryViewModel>()).Returns(-1);

            // Act
            var result = await this.libraryService.AddLibrary(library);

            // Assert
            await this.libraryRepository.LibraryWriteRepository.Received(1).AddLibrary(Arg.Any<LibraryViewModel>());
            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public async Task UpdateLibrary_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            LibraryViewModel library = new LibraryViewModel()
            {
                Name = "HR",
                Id = 1
            };

            this.libraryRepository.LibraryWriteRepository.UpdateLibrary(Arg.Any<LibraryViewModel>()).Returns(true);

            // Act
            var result = await this.libraryService.UpdateLibrary(library);

            // Assert
            await this.libraryRepository.LibraryWriteRepository.Received(1).UpdateLibrary(Arg.Any<LibraryViewModel>());
            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public async Task UpdateLibrary_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            LibraryViewModel library = new LibraryViewModel()
            {
                Name = "HR",
                Id = -1
            };

            this.libraryRepository.LibraryWriteRepository.UpdateLibrary(Arg.Any<LibraryViewModel>()).Returns(false);

            // Act
            var result = await this.libraryService.UpdateLibrary(library);

            // Assert
            await this.libraryRepository.LibraryWriteRepository.Received(1).UpdateLibrary(Arg.Any<LibraryViewModel>());
            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public async Task RemoveLibrary_WithValidId_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            int libraryId = 1;
            List<DepartmentListViewModel> departments = new List<DepartmentListViewModel>
            {
                new DepartmentListViewModel{ Id = 1, LibraryName = "HR", Name = "Management" }
            };

            this.libraryRepository.LibraryWriteRepository.DeleteLibrary(Arg.Any<int>()).Returns(true);
            this.departmentRepository.DepartmentReadRepository.GetDepartmentsByLibraryId(Arg.Any<int>()).Returns(departments);
            this.departmentRepository.DepartmentWriteRepository.DeleteDepartment(Arg.Any<int>()).Returns(true);


            // Act
            var result = await this.libraryService.RemoveLibrary(libraryId);

            // Assert
            await this.libraryRepository.LibraryWriteRepository.Received(1).DeleteLibrary(Arg.Any<int>());
            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public async Task RemoveLibrary_WithNoDepartments_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            int libraryId = 1;
            List<DepartmentListViewModel> departments = new List<DepartmentListViewModel>();

            this.libraryRepository.LibraryWriteRepository.DeleteLibrary(Arg.Any<int>()).Returns(true);
            this.departmentRepository.DepartmentReadRepository.GetDepartmentsByLibraryId(Arg.Any<int>()).Returns(departments);
            this.departmentRepository.DepartmentWriteRepository.DeleteDepartment(Arg.Any<int>()).Returns(true);


            // Act
            var result = await this.libraryService.RemoveLibrary(libraryId);

            // Assert
            await this.libraryRepository.LibraryWriteRepository.Received(1).DeleteLibrary(Arg.Any<int>());
            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task RemoveLibrary_WithValidId_ShouldThrowException()
        {
            // Arrange
            int libraryId = 1;
            List<DepartmentListViewModel> departments = new List<DepartmentListViewModel>
            {
                new DepartmentListViewModel{ Id = 1, LibraryName = "HR", Name = "Management" }
            };

            this.libraryRepository.LibraryWriteRepository.DeleteLibrary(Arg.Any<int>()).Returns(true);
            this.departmentRepository.DepartmentReadRepository.GetDepartmentsByLibraryId(Arg.Any<int>()).Returns(departments);
            this.departmentRepository.DepartmentWriteRepository.DeleteDepartment(Arg.Any<int>()).Returns(false);


            // Act
            var result = await this.libraryService.RemoveLibrary(libraryId);

            // Assert
            this.logger.Received(1).Error(Arg.Any<string>());
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task RemoveLibrary_WithInValidId_ShouldThrowException()
        {
            // Arrange
            int libraryId = -1;
            List<DepartmentListViewModel> departments = new List<DepartmentListViewModel>
            {
                new DepartmentListViewModel{ Id = 1, LibraryName = "HR", Name = "Management" }
            };

            this.libraryRepository.LibraryWriteRepository.DeleteLibrary(Arg.Any<int>()).Returns(true);
            this.departmentRepository.DepartmentReadRepository.GetDepartmentsByLibraryId(Arg.Any<int>()).Returns(departments);
            this.departmentRepository.DepartmentWriteRepository.DeleteDepartment(Arg.Any<int>()).Returns(false);


            // Act
            var result = await this.libraryService.RemoveLibrary(libraryId);

            // Assert
            await this.libraryRepository.LibraryWriteRepository.Received(1).DeleteLibrary(Arg.Any<int>());
        }

        [TestMethod]
        public async Task RemoveLibrary_WithValidId_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            int libraryId = 1;
            List<DepartmentListViewModel> departments = new List<DepartmentListViewModel>
            {
                new DepartmentListViewModel{ Id = 1, LibraryName = "HR", Name = "Management" }
            };

            this.libraryRepository.LibraryWriteRepository.DeleteLibrary(Arg.Any<int>()).Returns(false);
            this.departmentRepository.DepartmentReadRepository.GetDepartmentsByLibraryId(Arg.Any<int>()).Returns(departments);
            this.departmentRepository.DepartmentWriteRepository.DeleteDepartment(Arg.Any<int>()).Returns(true);


            // Act
            var result = await this.libraryService.RemoveLibrary(libraryId);

            // Assert
            await this.libraryRepository.LibraryWriteRepository.Received(1).DeleteLibrary(Arg.Any<int>());
            Assert.AreEqual(false, result.IsSuccessful);
        }
    }
}
