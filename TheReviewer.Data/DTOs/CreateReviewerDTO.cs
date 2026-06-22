namespace TheReviewer.Data.DTOs;

public class CreateReviewerDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }

    public CreateReviewerDTO(string name, string email, string passwordHash, DateTime createdAt)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
    }
}
