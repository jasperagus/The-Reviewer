using TheReviewer.Data.Repositories;
using TheReviewer.Logic.Enums;
using TheReviewer.Logic.Services;
using Xunit;

namespace TheReviewer.UnitTesting;

public class ReviewerServiceTests
{
    [Fact]
    public void Create_WithValidDetails_CreatesReviewerWithoutReturningPassword()
    {
        var repository = new MockReviewerRepository();
        var service = new ReviewerService(repository);

        var result = service.Create("TestUser@example.com", "Password123");

        Assert.True(result.Success);
        Assert.NotNull(result.Reviewer);
        Assert.Equal("testuser@example.com", result.Reviewer.Email);
    }

    [Fact]
    public void Create_WithValidDetails_StoresHashedPassword()
    {
        var repository = new MockReviewerRepository();
        var service = new ReviewerService(repository);

        service.Create("test@example.com", "Password123");

        var savedReviewer = repository.GetByEmail("test@example.com");
        Assert.NotNull(savedReviewer);
        Assert.NotEqual("Password123", savedReviewer.PasswordHash);
        Assert.StartsWith("PBKDF2-SHA256.", savedReviewer.PasswordHash);
    }

    [Fact]
    public void Create_WithDuplicateEmail_Fails()
    {
        var repository = new MockReviewerRepository();
        var service = new ReviewerService(repository);

        service.Create("test@example.com", "Password123");
        var result = service.Create("TEST@example.com", "Password123");

        Assert.False(result.Success);
        Assert.Equal(CreateReviewerError.EmailAlreadyExists, result.Error);
    }

    [Fact]
    public void Create_WithInvalidEmail_Fails()
    {
        var service = new ReviewerService(new MockReviewerRepository());

        var result = service.Create("not-an-email", "Password123");

        Assert.False(result.Success);
        Assert.Equal(CreateReviewerError.InvalidEmail, result.Error);
    }

    [Fact]
    public void Create_WithWeakPassword_Fails()
    {
        var service = new ReviewerService(new MockReviewerRepository());

        var result = service.Create("test@example.com", "password");

        Assert.False(result.Success);
        Assert.Equal(CreateReviewerError.WeakPassword, result.Error);
    }

    [Fact]
    public void Login_WithValidCredentials_ReturnsReviewer()
    {
        var repository = new MockReviewerRepository();
        var service = new ReviewerService(repository);
        service.Create("test@example.com", "Password123");

        var reviewer = service.Login("TEST@example.com", "Password123");

        Assert.NotNull(reviewer);
        Assert.Equal("test@example.com", reviewer.Email);
    }

    [Fact]
    public void Login_WithInvalidPassword_ReturnsNull()
    {
        var repository = new MockReviewerRepository();
        var service = new ReviewerService(repository);
        service.Create("test@example.com", "Password123");

        var reviewer = service.Login("test@example.com", "WrongPassword123");

        Assert.Null(reviewer);
    }
}
