namespace TheReviewer.Data.DTOs;

public class CreateAccountDTO
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }

    public CreateAccountDTO(string email, string passwordHash, DateTime createdAt)
    {
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
    }
}

