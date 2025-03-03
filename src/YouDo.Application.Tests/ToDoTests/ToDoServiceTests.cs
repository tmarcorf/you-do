using Microsoft.AspNetCore.Identity;
using Moq;
using YouDo.Application.DTOs.ToDo;
using YouDo.Application.Services;
using YouDo.Core.Entities;
using YouDo.Core.Enums;
using YouDo.Core.Interfaces;
using YouDo.Core.Validations.ToDo;

namespace YouDo.Application.Tests.ToDoTests
{
    public class ToDoServiceTests
    {
        private readonly Mock<IToDoRepository> _mockRepository;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly ToDoService _toDoService;

        public ToDoServiceTests()
        {
            _mockRepository = new Mock<IToDoRepository>();
            _mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            _toDoService = new ToDoService(_mockRepository.Object, _mockUserManager.Object);
        }

        [Fact]
        public async Task GetAllFromUserAsync_InvalidUserId_ReturnsFailure()
        {
            var userId = Guid.Empty;
            var skip = 0;
            var take = 10;

            var result = await _toDoService.GetAllFromUserAsync(userId, skip, take);

            Assert.False(result.IsSuccess);
            Assert.Equal(ToDoErrors.InvalidUserId, result.Error);
        }

        [Fact]
        public async Task GetAllFromUserAsync_ValidUserId_ReturnsToDoList()
        {
            var userId = Guid.NewGuid();
            var skip = 0;
            var take = 10;
            var toDoEntities = new List<ToDo> { new ToDo("Title", "Details", userId) };

            _mockRepository.Setup(repo => repo.GetAllFromUserAsync(userId, skip, take)).ReturnsAsync(toDoEntities);

            var result = await _toDoService.GetAllFromUserAsync(userId, skip, take);

            Assert.True(result.IsSuccess);
            Assert.Equal(toDoEntities.Count, result.Data.Count());
        }

        [Fact]
        public async Task GetAllFromUserWithSpecifiedCreationDateAsync_InvalidUserId_ReturnsFailure()
        {
            var userId = Guid.Empty;
            var creationDate = DateTime.UtcNow;
            var skip = 0;
            var take = 10;

            var result = await _toDoService.GetAllFromUserWithSpecifiedCreationDateAsync(userId, creationDate, skip, take);

            Assert.False(result.IsSuccess);
            Assert.Equal(ToDoErrors.InvalidUserId, result.Error);
        }

        [Fact]
        public async Task GetAllFromUserWithSpecifiedCreationDateAsync_InvalidCreationDate_ReturnsFailure()
        {
            var userId = Guid.NewGuid();
            var creationDate = DateTime.MinValue;
            var skip = 0;
            var take = 10;

            var result = await _toDoService.GetAllFromUserWithSpecifiedCreationDateAsync(userId, creationDate, skip, take);

            Assert.False(result.IsSuccess);
            Assert.Equal(ToDoErrors.InvalidCreationDate, result.Error);
        }

        [Fact]
        public async Task GetAllFromUserWithSpecifiedCreationDateAsync_ValidInput_ReturnsToDoList()
        {
            var userId = Guid.NewGuid();
            var creationDate = DateTime.UtcNow;
            var skip = 0;
            var take = 10;
            var toDoEntities = new List<ToDo> { new ToDo("Title", "Details", userId) { CreatedAt = creationDate } };

            _mockRepository.Setup(repo => repo.GetAllFromUserWithSpecifiedCreationDateAsync(userId, creationDate, skip, take)).ReturnsAsync(toDoEntities);

            var result = await _toDoService.GetAllFromUserWithSpecifiedCreationDateAsync(userId, creationDate, skip, take);

            Assert.True(result.IsSuccess);
            Assert.Equal(toDoEntities.Count, result.Data.Count());
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ReturnsFailure()
        {
            var id = Guid.Empty;
            var result = await _toDoService.GetByIdAsync(id);

            Assert.False(result.IsSuccess);
            Assert.Equal(ToDoErrors.InvalidId, result.Error);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsToDo()
        {
            var id = Guid.NewGuid();
            var toDoEntity = new ToDo("Title", "Details", Guid.NewGuid()) { Id = id };

            _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(toDoEntity);

            var result = await _toDoService.GetByIdAsync(id);

            Assert.True(result.IsSuccess);
            Assert.Equal(toDoEntity.Id, result.Data.Id);
        }

        [Fact]
        public async Task CreateAsync_InvalidUserId_ReturnsFailure()
        {
            var createToDoDTO = new CreateToDoDTO { UserId = Guid.Empty, Title = "Test123" };

            _mockUserManager
                .Setup(um => um.FindByIdAsync(createToDoDTO.UserId.ToString()))
                .ReturnsAsync((User)null);

            var result = await _toDoService.CreateAsync(createToDoDTO);

            Assert.False(result.IsSuccess);
            Assert.Equal(ToDoErrors.InvalidUserId, result.Error);
        }

        [Fact]
        public async Task CreateAsync_ValidInput_ReturnsSuccess()
        {
            var createToDoDTO = new CreateToDoDTO { UserId = Guid.NewGuid(), Title = "Test123" };

            var toDo = new ToDo(createToDoDTO.Title, createToDoDTO.Details, createToDoDTO.UserId);

            var user = new User(
                email: "test@example.com",
                firstName: "John",
                lastName: "Doe",
                dateOfBirth: new DateTime(1990, 1, 1),
                gender: EnumGender.MALE
            );

            _mockUserManager
                .Setup(um => um.FindByIdAsync(createToDoDTO.UserId.ToString()))
                .ReturnsAsync(user);

            _mockRepository
                .Setup(repo => repo.CreateAsync(It.IsAny<ToDo>()))
                .ReturnsAsync(toDo);

            var result = await _toDoService.CreateAsync(createToDoDTO);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task UpdateAsync_InvalidId_ReturnsFailure()
        {
            var updateToDoDTO = new UpdateToDoDTO { Id = Guid.Empty, UserId = Guid.NewGuid(), Title = "Test123" };

            var user = new User(
                email: "test@example.com",
                firstName: "John",
                lastName: "Doe",
                dateOfBirth: new DateTime(1990, 1, 1),
                gender: EnumGender.MALE
            );

            _mockUserManager
                .Setup(um => um.FindByIdAsync(updateToDoDTO.UserId.ToString()))
                .ReturnsAsync(user);

            _mockRepository
                .Setup(repo => repo.GetByIdAsync(updateToDoDTO.Id))
                .ReturnsAsync((ToDo)null);

            var result = await _toDoService.UpdateAsync(updateToDoDTO);

            Assert.False(result.IsSuccess);
            Assert.Equal(ToDoErrors.InvalidId, result.Error);
        }

        [Fact]
        public async Task UpdateAsync_ValidInput_ReturnsSuccess()
        {
            var updateToDoDTO = new UpdateToDoDTO { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), Title = "Test123" };
            var toDoEntity = new ToDo("Old Title", "Details", Guid.NewGuid()) { Id = updateToDoDTO.Id };

            var user = new User(
                email: "test@example.com",
                firstName: "John",
                lastName: "Doe",
                dateOfBirth: new DateTime(1990, 1, 1),
                gender: EnumGender.MALE
            );

            _mockUserManager
                .Setup(um => um.FindByIdAsync(updateToDoDTO.UserId.ToString()))
                .ReturnsAsync(user);

            _mockRepository
                .Setup(repo => repo.GetByIdAsync(updateToDoDTO.Id))
                .ReturnsAsync(toDoEntity);

            _mockRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<ToDo>()))
                .ReturnsAsync(toDoEntity);

            var result = await _toDoService.UpdateAsync(updateToDoDTO);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ReturnsFailure()
        {
            var id = Guid.Empty;

            _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((ToDo)null);

            var result = await _toDoService.DeleteAsync(id);

            Assert.False(result.IsSuccess);
            Assert.Equal(ToDoErrors.InvalidId, result.Error);
        }

        [Fact]
        public async Task DeleteAsync_ValidId_ReturnsSuccess()
        {
            var id = Guid.NewGuid();
            var toDoEntity = new ToDo("Title", "Details", Guid.NewGuid()) { Id = id };

            _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(toDoEntity);
            _mockRepository.Setup(repo => repo.DeleteAsync(toDoEntity)).ReturnsAsync(true);

            var result = await _toDoService.DeleteAsync(id);

            Assert.True(result.IsSuccess);
            Assert.True(result.Data);
        }
    }
}