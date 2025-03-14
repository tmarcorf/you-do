﻿using FluentAssertions;
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

            var toDo = new ToDo(title, details);
            toDo.UserId = userId;

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

            var act = () => new ToDo(title, details);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(ToDoErrors.InvalidTitle.Message);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenTitleIsTooShort()
        {
            var title = "abc";
            var details = "Valid details";
            var userId = Guid.NewGuid();

            var act = () => new ToDo(title, details);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(ToDoErrors.InvalidTitleLength.Message);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenTitleIsTooLong()
        {
            var title = new string('a', 101);
            var details = "Valid details";
            var userId = Guid.NewGuid();

            var act = () => new ToDo(title, details);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(ToDoErrors.InvalidTitleMaxLength.Message);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenDetailsExceedsMaxLength()
        {
            var title = "Valid Title";
            var details = new string('b', 501);

            var act = () => new ToDo(title, details);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(ToDoErrors.InvalidDetailsMaxLength.Message);
        }

        [Fact]
        public void Update_ShouldThrowException_InvalidTitle()
        {
            var title = "Valid Title";
            var details = new string('b', 20);
            var toDo = new ToDo(title, details);

            var act = () => toDo.Update("", "", false);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(ToDoErrors.InvalidTitle.Message);
        }

        [Fact]
        public void Update_ShouldThrowException_InvalidTitleLength()
        {
            var title = "Valid Title";
            var details = new string('b', 20);
            var userId = Guid.NewGuid();
            var toDo = new ToDo(title, details);

            var act = () => toDo.Update("Inv", "", false);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(ToDoErrors.InvalidTitleLength.Message);
        }

        [Fact]
        public void Update_ShouldThrowException_InvalidTitleMaxLength()
        {
            var title = "Valid Title";
            var details = new string('b', 20);
            var userId = Guid.NewGuid();
            var toDo = new ToDo(title, details);

            var act = () => toDo.Update(new string('a', 101), "", false);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(ToDoErrors.InvalidTitleMaxLength.Message);
        }

        [Fact]
        public void Update_ShouldThrowException_InvalidDetailsMaxLength()
        {
            var title = "Valid Title";
            var details = new string('b', 20);
            var userId = Guid.NewGuid();
            var toDo = new ToDo(title, details);

            var act = () => toDo.Update("Valid title", new string('a', 501), false);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage(ToDoErrors.InvalidDetailsMaxLength.Message);
        }

        [Fact]
        public void Update_ShouldNotThrowException_ValidData()
        {
            var title = "Valid Title";
            var details = new string('b', 20);
            var userId = Guid.NewGuid();
            var toDo = new ToDo(title, details);

            var act = () => toDo.Update("Valid title", "Valid details", false);

            act.Should()
                .NotThrow<ArgumentException>();
        }
    }
}
