namespace TheReviewer.Data.DTOs;

public class GetMediaDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Publisher { get; set; }
    public int Score { get; set; }
    public int TypeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}