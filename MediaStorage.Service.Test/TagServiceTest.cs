using MediaStorage.Common;
using MediaStorage.Common.ViewModels.Tag;
using MediaStorage.Data;
using MediaStorage.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;

namespace MediaStorage.Service.Test
{
    [TestClass]
    public class TagServiceTest
    {
        private IUnitOfWork uow;
        private IRepository<Tag> tagRepository;
        private ITagService tagService;

        public TagServiceTest()
        {
            this.uow = NSubstitute.Substitute.For<IUnitOfWork>();
            this.tagRepository = NSubstitute.Substitute.For<IRepository<Tag>>();
        }

        [TestInitialize]
        public void TestSetup()
        {
            tagService = new TagService(this.uow, this.tagRepository);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            tagRepository.ClearReceivedCalls();
            uow.ClearReceivedCalls();
        }

        [TestMethod]
        public void GetAllTags_ShouldReturnAllTags()
        {
            // Arrange
            IQueryable<Tag> tags = new List<Tag>()
            {
                new Tag()
                {
                    Id = 1,

                    Name = "PHP"
                },
                new Tag()
                {
                    Id = 1,
                    Name = "JAVA"
                }
            }.AsQueryable();

            this.tagRepository.GetAll().Returns(tags);

            // Act
            var result = this.tagService.GetAllTags();

            // Assert
            this.tagRepository.Received(1).GetAll();
            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void GetTagById_WithValidId_ShouldReturnTag()
        {
            // Arrange
            int tagId = 1;
            Tag tag = new Tag()
            {
                Id = 1,
                Name = "C#"
            };

            this.tagRepository.Find(Arg.Any<int>()).Returns(tag);

            // Act
            var result = this.tagService.GetTagById(tagId);

            // Assert
            this.tagRepository.Received(1).Find(Arg.Any<int>());
            Assert.AreEqual(result.Id, tag.Id);
        }

        [TestMethod]
        public void AddTag_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            TagViewModel tag = new TagViewModel()
            {
                Name = "C#"
            };

            this.tagRepository.Add(Arg.Any<Tag>());
            this.uow.Commit().Returns(1);

            // Act
            var result = this.tagService.AddTag(tag);

            // Assert
            this.uow.Received(1).Commit();
            this.tagRepository.Received(1).Add(Arg.Any<Tag>());

            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public void AddTag_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            TagViewModel tag = new TagViewModel();

            this.tagRepository.Add(Arg.Any<Tag>());
            this.uow.Commit().Returns(0);

            // Act
            var result = this.tagService.AddTag(tag);

            // Assert
            this.uow.Received(1).Commit();
            this.tagRepository.Received(1).Add(Arg.Any<Tag>());

            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public void UpdateTag_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            TagViewModel tag = new TagViewModel()
            {
                Name = "C#",
                Id = 1
            };

            this.tagRepository.Update(Arg.Any<Tag>());
            this.uow.Commit().Returns(1);

            // Act
            var result = this.tagService.UpdateTag(tag);

            // Assert
            this.tagRepository.Received(1).Update(Arg.Any<Tag>());
            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public void UpdateTag_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            TagViewModel tag = new TagViewModel()
            {

                Name = "C#",
                Id = 1
            };

            this.tagRepository.Update(Arg.Any<Tag>());
            this.uow.Commit().Returns(0);

            // Act
            var result = this.tagService.UpdateTag(tag);

            // Assert
            this.tagRepository.Received(1).Update(Arg.Any<Tag>());
            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public void RemoveTag_WithValidId_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            int tagId = 1;

            this.tagRepository.Delete(Arg.Any<int>());
            this.uow.Commit().Returns(1);

            // Act
            var result = this.tagService.RemoveTag(tagId);

            // Assert
            this.tagRepository.Received(1).Delete(Arg.Any<int>());
            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public void RemoveTag_WithInValidId_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            int tagId = -1;

            this.tagRepository.Delete(Arg.Any<int>());
            this.uow.Commit().Returns(0);

            // Act
            var result = this.tagService.RemoveTag(tagId);

            // Assert
            this.tagRepository.Received(1).Delete(Arg.Any<int>());
            Assert.AreEqual(false, result.IsSuccessful);
        }
    }
}
