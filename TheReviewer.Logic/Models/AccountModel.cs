namespace TheReviewer.Logic.Models;

public class AccountModel
{
    public int Id { get; private set; }
    public string Email { get; private set; }
    public string Role { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public AccountModel(int id, string email, string role, DateTime createdAt)
    {
        Id = id;
        Email = email;
        Role = role;
        CreatedAt = createdAt;
    }
}
