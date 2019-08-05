using MediaStorage.Common;
using MediaStorage.Common.ViewModels.MaterialType;
using MediaStorage.Data.Read;
using MediaStorage.Data.Write;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MediaStorage.Service.Test.Repository;

namespace MediaStorage.Service.Test
{
    [TestClass]
    public class MaterialTypeServiceTest
    {
        private MaterialTypeReadRepository materialTypeReadRepository;
        private MaterialTypeWriteRepository materialTypeWriteRepository;
        private readonly ILogger logger;
        private MaterialTypeRepository materialTypeRepository;

        public MaterialTypeServiceTest()
        {
            this.materialTypeReadRepository = NSubstitute.Substitute.For<MaterialTypeReadRepository>();
            this.materialTypeWriteRepository = NSubstitute.Substitute.For<MaterialTypeWriteRepository>();
            this.logger = NSubstitute.Substitute.For<ILogger>();
        }

        [TestMethod]
        public async Task GetAllMaterialTypes_ShouldReturnAllMaterialTypes()
        {
            // Arrange
            List<MaterialTypeViewModel> materials = new List<MaterialTypeViewModel>()
            {
                new MaterialTypeViewModel()
                {
                    Id = 1,
                    Name = "Paper"
                }
            };
            materialTypeRepository = new MaterialTypeRepository(this.logger, materials);

            // Act
            var result = await this.materialTypeRepository.GetAllMaterialTypes();

            // Assert
            Assert.AreEqual(result.FirstOrDefault().Id, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public async Task GetAllMaterialTypes_ShouldThrowResourceNotFoundException()
        {
            // Arrange
            List<MaterialTypeViewModel> materials = new List<MaterialTypeViewModel>();
            materialTypeRepository = new MaterialTypeRepository(this.logger, materials);

            // Act
            var result = await this.materialTypeRepository.GetAllMaterialTypes();

            // Assert
        }

        [TestMethod]
        public async Task GetMaterialTypesAsSelectListItem_ShouldReturnAllMaterialTypesAsSelectListItem()
        {
            // Arrange
            List<CustomSelectListItem> items = new List<CustomSelectListItem>()
            {
                new CustomSelectListItem()
                {
                    Text = "1",
                    Value = "Paper",
                    Selected = true
                }
            };
            materialTypeRepository = new MaterialTypeRepository(this.logger, items);

            // Act
            var result = await this.materialTypeRepository.GetMaterialTypesAsSelectListItem(1);

            // Assert
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public async Task GetMaterialTypeById_ShouldReturnMaterialTypes()
        {
            // Arrange
            MaterialTypeViewModel material = new MaterialTypeViewModel()
            {
                Id = 1,
                Name = "Paper"
            };
            materialTypeRepository = new MaterialTypeRepository(this.logger, material);

            // Act
            var result = await this.materialTypeRepository.GetMaterialTypeById(1);

            // Assert
            Assert.AreEqual(result.Id, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException))]
        public async Task GetMaterialTypeById_ShouldThrowResourceNotFoundException()
        {
            // Arrange
            MaterialTypeViewModel material = null;
            materialTypeRepository = new MaterialTypeRepository(this.logger, material);

            // Act
            var result = await this.materialTypeRepository.GetMaterialTypeById(1);

            // Assert
        }

        [TestMethod]
        public async Task AddMaterialType_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            MaterialTypeViewModel material = new MaterialTypeViewModel()
            {
                Name = "Paper"
            };
            materialTypeRepository = new MaterialTypeRepository(this.logger, true);

            // Act
            var result = await this.materialTypeRepository.AddMaterialType(material);

            // Assert
            Assert.AreEqual(result.IsSuccessful, true);
        }

        [TestMethod]
        public async Task AddMaterialType_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            MaterialTypeViewModel material = new MaterialTypeViewModel()
            {
                Name = ""
            };
            materialTypeRepository = new MaterialTypeRepository(this.logger, false);

            // Act
            var result = await this.materialTypeRepository.AddMaterialType(material);

            // Assert
            Assert.AreEqual(result.IsSuccessful, false);
        }

        [TestMethod]
        public async Task UpdateLibrary_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            MaterialTypeViewModel material = new MaterialTypeViewModel()
            {
                Id = 1,
                Name = "Paper"
            };
            materialTypeRepository = new MaterialTypeRepository(this.logger, true);

            // Act
            var result = await this.materialTypeRepository.UpdateLibrary(material);

            // Assert
            Assert.AreEqual(result.IsSuccessful, true);
        }

        public async Task UpdateLibrary_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            MaterialTypeViewModel material = new MaterialTypeViewModel()
            {
                Id = -1,
                Name = "Paper"
            };
            materialTypeRepository = new MaterialTypeRepository(this.logger, false);

            // Act
            var result = await this.materialTypeRepository.UpdateLibrary(material);

            // Assert
            Assert.AreEqual(result.IsSuccessful, false);
        }

        [TestMethod]
        public async Task RemoveLibrary_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            materialTypeRepository = new MaterialTypeRepository(this.logger, true);

            // Act
            var result = await this.materialTypeRepository.RemoveLibrary(1);

            // Assert
            Assert.AreEqual(result.IsSuccessful, true);
        }

        [TestMethod]
        public async Task RemoveLibrary_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            materialTypeRepository = new MaterialTypeRepository(this.logger, false);

            // Act
            var result = await this.materialTypeRepository.RemoveLibrary(-1);

            // Assert
            Assert.AreEqual(result.IsSuccessful, false);
        }
    }
}
