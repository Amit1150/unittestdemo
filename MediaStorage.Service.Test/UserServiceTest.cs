using MediaStorage.Common.ViewModels.User;
using MediaStorage.Data.Entities;
using MediaStorage.Data.Read;
using MediaStorage.Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaStorage.Service.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserRepository userRepository;
        private readonly IUserReadRepository userReadRepository;
        private readonly IUserWriteRepository userWriteRepository;
        private IUserService userService;

        public UserServiceTest()
        {
            this.userRepository = NSubstitute.Substitute.For<IUserRepository>();
            this.userReadRepository = NSubstitute.Substitute.For<IUserReadRepository>();
            this.userWriteRepository = NSubstitute.Substitute.For<IUserWriteRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            userRepository.ClearReceivedCalls();
        }

        [TestInitialize]
        public void TestSetup()
        {
            userService = new UserService(this.userRepository);
            this.userRepository.userReadRepository = this.userReadRepository;
            this.userRepository.userWriteRepository = this.userWriteRepository;
        }

        [TestMethod]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            List<UserViewModel> users = new List<UserViewModel>()
            {
                new UserViewModel()
                {
                    Id = "1",
                    Username = "Amit",
                    Mail = "Amit@test.com",
                    IsActive = true
                }
            };

            this.userRepository.userReadRepository.GetAllUsers().Returns(users);

            // Act
            var result = this.userService.GetAllUsers();

            // Assert
            this.userRepository.userReadRepository.Received(1).GetAllUsers();
            Assert.AreEqual(result.FirstOrDefault().Id, "1");
        }

        [TestMethod]
        public void GetUserById_WithValidId_ShouldReturnUser()
        {
            // Arrange
            var userId = new Guid();
            User user = new User()
            {
                Id = userId,
                Username = "Amit",
                Mail = "Amit@test.com",
                IsActive = true
            };

            this.userRepository.userReadRepository.GetUserById(Arg.Any<Guid>()).Returns(user);

            // Act
            var result = this.userService.GetUserById(userId);

            // Assert
            this.userRepository.userReadRepository.Received(1).GetUserById(Arg.Any<Guid>());
            Assert.AreEqual(result.Username, user.Username);
        }

        [TestMethod]
        public void GetUserById_WithInValidId_ShouldReturnNull()
        {
            // Arrange
            var userId = Guid.Empty;

            this.userRepository.userReadRepository.GetUserById(Arg.Any<Guid>()).ReturnsNull();

            // Act
            var result = this.userService.GetUserById(userId);

            // Assert
            this.userRepository.userReadRepository.Received(1).GetUserById(Arg.Any<Guid>());
            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void Login_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            var userId = new Guid();
            LoginViewModel loginViewModel = new LoginViewModel() { Username = "Amit", Password = "Test123" };

            User user = new User()
            {
                Id = userId,
                Username = "Amit",
                Mail = "Amit@test.com",
                IsActive = true
            };

            this.userRepository.userReadRepository.GetByUserIdPassword(Arg.Any<string>(), Arg.Any<string>()).Returns(user);

            // Act
            var result = this.userService.Login(loginViewModel);

            // Assert
            this.userRepository.userReadRepository.Received(1).GetByUserIdPassword(Arg.Any<string>(), Arg.Any<string>());
            Assert.AreEqual(result.IsSuccessful, true);
        }

        [TestMethod]
        public void Login_WithEmptyPassword_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            LoginViewModel loginViewModel = new LoginViewModel() { Username = "Amit", Password = "" };
            this.userRepository.userReadRepository.GetByUserIdPassword(Arg.Any<string>(), Arg.Any<string>()).ReturnsNull();

            // Act
            var result = this.userService.Login(loginViewModel);

            // Assert
            this.userRepository.userReadRepository.Received(1).GetByUserIdPassword(Arg.Any<string>(), Arg.Any<string>());
            Assert.AreEqual(result.IsSuccessful, false);
        }

        [TestMethod]
        public void AddUser_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            UserPostViewModel user = new UserPostViewModel()
            {
                Username = "Amit",
                Mail = "Amit@test.com",
                IsActive = true
            };

            this.userRepository.userReadRepository.GetByUserName(Arg.Any<string>()).ReturnsNull();
            this.userRepository.userReadRepository.GetByUserByEmail(Arg.Any<string>()).ReturnsNull();
            this.userRepository.userWriteRepository.AddUser(Arg.Any<User>()).Returns(true);

            // Act
            var result = this.userService.AddUser(user);

            // Assert
            this.userRepository.userWriteRepository.Received(1).AddUser(Arg.Any<User>());
            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public void AddUser_WithDuplicateUserName_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            UserPostViewModel userModel = new UserPostViewModel()
            {
                Username = "Amit",
                Mail = "Amit@test.com",
                IsActive = true
            };

            User user = new User()
            {
                Id = new Guid(),
                Username = "Amit",
                Mail = "Amit@test.com",
                IsActive = true
            };

            this.userRepository.userReadRepository.GetByUserName(Arg.Any<string>()).Returns(user);
            this.userRepository.userReadRepository.GetByUserByEmail(Arg.Any<string>()).ReturnsNull();
            this.userRepository.userWriteRepository.AddUser(Arg.Any<User>()).Returns(true);

            // Act
            var result = this.userService.AddUser(userModel);

            // Assert
            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public void AddUser_WithDuplicateEmail_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            UserPostViewModel userModel = new UserPostViewModel()
            {
                Username = "Amit",
                Mail = "Amit@test.com",
                IsActive = true
            };

            User user = new User()
            {
                Id = new Guid(),
                Username = "Amit",
                Mail = "Amit@test.com",
                IsActive = true
            };

            this.userRepository.userReadRepository.GetByUserName(Arg.Any<string>()).ReturnsNull();
            this.userRepository.userReadRepository.GetByUserByEmail(Arg.Any<string>()).Returns(user);
            this.userRepository.userWriteRepository.AddUser(Arg.Any<User>()).Returns(true);

            // Act
            var result = this.userService.AddUser(userModel);

            // Assert
            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public void AddUser_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            UserPostViewModel userModel = new UserPostViewModel()
            {
                Username = "Amit",
                Mail = "Amit@test.com",
                IsActive = true
            };
            this.userRepository.userReadRepository.GetByUserName(Arg.Any<string>()).ReturnsNull();
            this.userRepository.userReadRepository.GetByUserByEmail(Arg.Any<string>()).ReturnsNull();
            this.userRepository.userWriteRepository.AddUser(Arg.Any<User>()).Returns(false);

            // Act
            var result = this.userService.AddUser(userModel);

            // Assert
            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public void RemoveUser_WithValidId_ShouldReturnsServiceResultIsSuccessfulTrue()
        {
            // Arrange
            var userid = new Guid();
            User user = new User()
            {
                Id = new Guid(),
                Username = "Amit",
                Mail = "Amit@test.com",
                IsActive = true
            };
            this.userRepository.userReadRepository.GetUserById(Arg.Any<Guid>()).Returns(user);
            this.userRepository.userWriteRepository.DeleteUser(Arg.Any<User>()).Returns(true);

            // Act
            var result = this.userService.RemoveUser(userid);

            // Assert
            this.userRepository.userReadRepository.Received(1).GetUserById(Arg.Any<Guid>());
            this.userRepository.userWriteRepository.Received(1).DeleteUser(Arg.Any<User>());
            Assert.AreEqual(true, result.IsSuccessful);
        }

        [TestMethod]
        public void RemoveUser_WithInValidId_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            var userid = new Guid();
            this.userRepository.userReadRepository.GetUserById(Arg.Any<Guid>()).ReturnsNull();
            this.userRepository.userWriteRepository.DeleteUser(Arg.Any<User>()).Returns(true);

            // Act
            var result = this.userService.RemoveUser(userid);

            // Assert
            this.userRepository.userReadRepository.Received(1).GetUserById(Arg.Any<Guid>());
            this.userRepository.userWriteRepository.Received(0).DeleteUser(Arg.Any<User>());
            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public void RemoveUser_ShouldReturnsServiceResultIsSuccessfulFalse()
        {
            // Arrange
            var userid = new Guid();
            User user = new User()
            {
                Id = new Guid(),
                Username = "Amit",
                Mail = "Amit@test.com",
                IsActive = true
            };
            this.userRepository.userReadRepository.GetUserById(Arg.Any<Guid>()).Returns(user);
            this.userRepository.userWriteRepository.DeleteUser(Arg.Any<User>()).Returns(false);

            // Act
            var result = this.userService.RemoveUser(userid);

            // Assert
            this.userRepository.userReadRepository.Received(1).GetUserById(Arg.Any<Guid>());
            this.userRepository.userWriteRepository.Received(1).DeleteUser(Arg.Any<User>());
            Assert.AreEqual(false, result.IsSuccessful);
        }

    }
}
