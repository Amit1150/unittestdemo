using MediaStorage.Common;
using MediaStorage.Common.ViewModels.Department;
using MediaStorage.Data.Read;
using MediaStorage.Data.Repository;
using MediaStorage.Data.Write;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaStorage.Service.Test
{
    [TestClass]
    public class DepartmentServiceTest
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IDepartmentReadRepository departmentReadRepository;
        private readonly IDepartmentWriteRepository departmentWriteRepository;
        private readonly ILogger logger;
        private DepartmentService departmentService;

        public DepartmentServiceTest()
        {
            this.departmentRepository = NSubstitute.Substitute.For<IDepartmentRepository>();
            this.departmentReadRepository = NSubstitute.Substitute.For<IDepartmentReadRepository>();
            this.departmentWriteRepository = NSubstitute.Substitute.For<IDepartmentWriteRepository>();
            this.logger = NSubstitute.Substitute.For<ILogger>();

            this.departmentRepository.DepartmentReadRepository = this.departmentReadRepository;
            this.departmentRepository.DepartmentWriteRepository = this.departmentWriteRepository;
        }

        [TestInitialize]
        public void TestSetup()
        {
            departmentService = new DepartmentService(this.departmentRepository, this.logger);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            departmentRepository.ClearReceivedCalls();
            logger.ClearReceivedCalls();
        }

        [TestMethod]
        public async Task GetAllDepartments_WithDepartmentList_ShouldReturnsDepartmentList()
        {
            // Arrange
            List<DepartmentListViewModel> departments = new List<DepartmentListViewModel>()
            {
                new DepartmentListViewModel()
                {
                    Id = 1,
                    Name = "Management",
                    LibraryName = "Sales"
                }
            };

            this.departmentReadRepository.GetAllDepartments().Returns(departments);

            // Act
            var result = await this.departmentService.GetAllDepartments();

            // Assert
            await this.departmentReadRepository.Received(1).GetAllDepartments();
            Assert.AreEqual(departments, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public async Task GetAllDepartments_WithEmptyDepartmentList_ShouldThrowResourceNotFoundException()
        {
            // Arrange
            this.departmentReadRepository.GetAllDepartments().Returns(new List<DepartmentListViewModel>());

            // Act
            var result = await this.departmentService.GetAllDepartments();

            // Assert
            await this.departmentReadRepository.Received(1).GetAllDepartments();
        }

        [TestMethod]
        public async Task GetDepartmentById_WithDepartment_ShouldReturnsDepartment()
        {
            // Arrange
            DepartmentViewModel department = new DepartmentViewModel()
            {
                Id = 1,
                Name = "Management",
                LibraryId = 1
            };

            this.departmentReadRepository.GetDepartmentById(Arg.Any<int>()).Returns(department);

            // Act
            var result = await this.departmentService.GetDepartmentById(1);

            // Assert
            await this.departmentReadRepository.Received(1).GetDepartmentById(Arg.Any<int>());
            Assert.AreEqual(department.Id, result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public async Task GetDepartmentById_WithNULL_ShouldThrowResourceNotFoundException()
        {
            // Arrange
            this.departmentReadRepository.GetDepartmentById(Arg.Any<int>()).ReturnsNull();

            // Act
            var result = await this.departmentService.GetDepartmentById(1);

            // Assert
            await this.departmentReadRepository.Received(1).GetDepartmentById(Arg.Any<int>());
            this.logger.Received(1).Error(Arg.Any<string>());
        }

        [TestMethod]
        public async Task AddDepartment_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            DepartmentViewModel department = new DepartmentViewModel()
            {
                Name = "Management",
                LibraryId = 1
            };

            this.departmentRepository.DepartmentWriteRepository.AddDepartment(Arg.Any<DepartmentViewModel>()).Returns(1);

            // Act
            var result = await this.departmentService.AddDepartment(department);

            // Assert
            await this.departmentRepository.DepartmentWriteRepository.Received(1).AddDepartment(Arg.Any<DepartmentViewModel>());
            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public async Task AddDepartment_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            DepartmentViewModel department = new DepartmentViewModel()
            {
                Name = "Management",
                LibraryId = 1
            };

            this.departmentRepository.DepartmentWriteRepository.AddDepartment(Arg.Any<DepartmentViewModel>()).Returns(-1);

            // Act
            var result = await this.departmentService.AddDepartment(department);

            // Assert
            await this.departmentRepository.DepartmentWriteRepository.Received(1).AddDepartment(Arg.Any<DepartmentViewModel>());
            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public async Task UpdateDepartment_WithValidId_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            DepartmentViewModel department = new DepartmentViewModel()
            {
                Id = 1,
                Name = "Management",
                LibraryId = 1
            };

            this.departmentRepository.DepartmentWriteRepository.UpdateDepartment(Arg.Any<DepartmentViewModel>()).Returns(true);

            // Act
            var result = await this.departmentService.UpdateDepartment(department);

            // Assert
            await this.departmentRepository.DepartmentWriteRepository.Received(1).UpdateDepartment(Arg.Any<DepartmentViewModel>());
            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public async Task UpdateDepartment_WithInvalidId_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            DepartmentViewModel department = new DepartmentViewModel()
            {
                Id = -1,
                Name = "Management",
                LibraryId = 1
            };

            this.departmentRepository.DepartmentWriteRepository.UpdateDepartment(Arg.Any<DepartmentViewModel>()).Returns(false);

            // Act
            var result = await this.departmentService.UpdateDepartment(department);

            // Assert
            await this.departmentRepository.DepartmentWriteRepository.Received(1).UpdateDepartment(Arg.Any<DepartmentViewModel>());
            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public async Task RemoveDepartment_WithValidId_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            int departmentId = 1;
            this.departmentRepository.DepartmentWriteRepository.DeleteDepartment(Arg.Any<int>()).Returns(true);

            // Act
            var result = await this.departmentService.RemoveDepartment(departmentId);

            // Assert
            await this.departmentRepository.DepartmentWriteRepository.Received(1).DeleteDepartment(Arg.Any<int>());
            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public async Task RemoveDepartment_WithValidId_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            int departmentId = -1;
            this.departmentRepository.DepartmentWriteRepository.DeleteDepartment(Arg.Any<int>()).Returns(false);

            // Act
            var result = await this.departmentService.RemoveDepartment(departmentId);

            // Assert
            await this.departmentRepository.DepartmentWriteRepository.Received(1).DeleteDepartment(Arg.Any<int>());
            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public async Task GetDepartmentsByLibraryId_WithValidLibraryId_ShouldReturnsDepartment()
        {
            // Arrange
            int libraryId = 1;
            List<DepartmentListViewModel> departments = new List<DepartmentListViewModel>()
            {
                new DepartmentListViewModel()
                {
                    Id = 1,
                    Name = "Management",
                    LibraryName = "Sales"
                },
                new DepartmentListViewModel()
                {
                    Id = 2,
                    Name = "HR",
                    LibraryName = "Sales"
                }
            };

            this.departmentRepository.DepartmentReadRepository.GetDepartmentsByLibraryId(Arg.Any<int>()).Returns(departments);

            // Act
            var result = await this.departmentService.GetDepartmentsByLibraryId(libraryId);

            // Assert
            await this.departmentRepository.DepartmentReadRepository.Received(1).GetDepartmentsByLibraryId(Arg.Any<int>());
            Assert.AreEqual(departments.Count, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public async Task GetDepartmentsByLibraryId_WithInValidLibraryId_ShouldThrowResourceNotFoundException()
        {
            // Arrange
            int libraryId = -1;

            this.departmentRepository.DepartmentReadRepository.GetDepartmentsByLibraryId(Arg.Any<int>()).ReturnsNull();

            // Act
            var result = await this.departmentService.GetDepartmentsByLibraryId(libraryId);

            // Assert
            await this.departmentRepository.DepartmentReadRepository.Received(1).GetDepartmentsByLibraryId(Arg.Any<int>());
        }

        [TestMethod]
        public async Task HasDepartmentsByLibraryId_WithValidLibraryId_ShouldReturnsTrue()
        {
            // Arrange
            int libraryId = 1;
            DepartmentViewModel department = new DepartmentViewModel()
            {
                Id = 1,
                Name = "Management",
                LibraryId = 1
            };

            this.departmentReadRepository.GetDepartmentById(Arg.Any<int>()).Returns(department);

            // Act
            var result = await this.departmentService.HasDepartmentsByLibraryId(libraryId);

            // Assert
            await this.departmentReadRepository.Received(1).GetDepartmentById(Arg.Any<int>());
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public async Task HasDepartmentsByLibraryId_WithInValidLibraryId_ShouldThrowResourceNotFoundException()
        {
            // Arrange
            int libraryId = -1;
            this.departmentReadRepository.GetDepartmentById(Arg.Any<int>()).ReturnsNull();

            // Act
            var result = await this.departmentService.HasDepartmentsByLibraryId(libraryId);

            // Assert
            await this.departmentReadRepository.Received(1).GetDepartmentById(Arg.Any<int>());
        }
    }

}
