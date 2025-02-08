using FluentAssertions;
using YouDo.Core.Entities;
using YouDo.Core.Validations.ToDo;

namespace YouDo.Core.Tests
{
    public class ToDoTests
    {
        [Fact]
        public void Constructor_ShouldCreateToDo_WhenValidDataProvided()
        {
            var title = "Valid Title";
            var details = "This is a valid detail.";
            var userId = Guid.NewGuid();

            var toDo = new ToDo(title, details, userId);

            toDo.Should().NotBeNull();
            toDo.Title.Should().Be(title);
            toDo.Details.Should().Be(details);
            toDo.UserId.Should().Be(userId);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenTitleIsNull()
        {
            string title = null;
            var details = "Valid details";
            var userId = Guid.NewGuid();

            var act = () => new ToDo(title, details, userId);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(ToDoErrors.InvalidTitle.Description);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenTitleIsTooShort()
        {
            var title = "abc";
            var details = "Valid details";
            var userId = Guid.NewGuid();

            var act = () => new ToDo(title, details, userId);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(ToDoErrors.InvalidTitleLength.Description);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenTitleIsTooLong()
        {
            var title = new string('a', 101);
            var details = "Valid details";
            var userId = Guid.NewGuid();

            var act = () => new ToDo(title, details, userId);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(ToDoErrors.InvalidTitleMaxLength.Description);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenDetailsExceedsMaxLength()
        {
            var title = "Valid Title";
            var details = new string('b', 501);
            var userId = Guid.NewGuid();

            var act = () => new ToDo(title, details, userId);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(ToDoErrors.InvalidDetailsMaxLength.Description);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenUserIdIsEmpty()
        {
            var title = "Valid Title";
            var details = "Valid details";
            var userId = Guid.Empty;

            var action = () => new ToDo(title, details, userId);

            action.Should()
                .Throw<ArgumentException>()
                .WithMessage(ToDoErrors.InvalidUserId.Description);
        }
    }
}
