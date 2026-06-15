using TheReviewer.Data.Repositories;
using TheReviewer.Logic.Models;
using TheReviewer.Logic.Services;
using Xunit;

namespace TheReviewer.UnitTesting;

public class AccountServiceTests
{
    [Fact]
    public void Create_WithValidDetails_CreatesAccountWithoutReturningPassword()
    {
        var repository = new MockAccountRepository();
        var service = new AccountService(repository);

        var result = service.Create("TestUser@example.com", "Password123");

        Assert.True(result.Success);
        Assert.NotNull(result.Account);
        Assert.Equal("testuser@example.com", result.Account.Email);
        Assert.Equal("User", result.Account.Role);
    }

    [Fact]
    public void Create_WithValidDetails_StoresHashedPassword()
    {
        var repository = new MockAccountRepository();
        var service = new AccountService(repository);

        service.Create("test@example.com", "Password123");

        var savedAccount = repository.GetByEmail("test@example.com");
        Assert.NotNull(savedAccount);
        Assert.NotEqual("Password123", savedAccount.PasswordHash);
        Assert.StartsWith("PBKDF2-SHA256.", savedAccount.PasswordHash);
    }

    [Fact]
    public void Create_WithDuplicateEmail_Fails()
    {
        var repository = new MockAccountRepository();
        var service = new AccountService(repository);

        service.Create("test@example.com", "Password123");
        var result = service.Create("TEST@example.com", "Password123");

        Assert.False(result.Success);
        Assert.Equal(CreateAccountError.EmailAlreadyExists, result.Error);
    }

    [Fact]
    public void Create_WithInvalidEmail_Fails()
    {
        var service = new AccountService(new MockAccountRepository());

        var result = service.Create("not-an-email", "Password123");

        Assert.False(result.Success);
        Assert.Equal(CreateAccountError.InvalidEmail, result.Error);
    }

    [Fact]
    public void Create_WithWeakPassword_Fails()
    {
        var service = new AccountService(new MockAccountRepository());

        var result = service.Create("test@example.com", "password");

        Assert.False(result.Success);
        Assert.Equal(CreateAccountError.WeakPassword, result.Error);
    }

    [Fact]
    public void Login_WithValidCredentials_ReturnsAccount()
    {
        var repository = new MockAccountRepository();
        var service = new AccountService(repository);
        service.Create("test@example.com", "Password123");

        var account = service.Login("TEST@example.com", "Password123");

        Assert.NotNull(account);
        Assert.Equal("test@example.com", account.Email);
    }

    [Fact]
    public void Login_WithInvalidPassword_ReturnsNull()
    {
        var repository = new MockAccountRepository();
        var service = new AccountService(repository);
        service.Create("test@example.com", "Password123");

        var account = service.Login("test@example.com", "WrongPassword123");

        Assert.Null(account);
    }
}
