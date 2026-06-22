using TheReviewer.Data.DTOs;
using TheReviewer.Data.Interfaces;

namespace TheReviewer.Data.Repositories;

public class MockReviewerRepository : IReviewerRepository
{
    private readonly List<GetReviewerDTO> _reviewers = [];
    private int _currentId = 1;

    public List<GetReviewerDTO> GetAll()
    {
        return _reviewers;
    }

    public GetReviewerDTO? GetByEmail(string email)
    {
        return _reviewers.FirstOrDefault(reviewer =>
            string.Equals(reviewer.Email, email, StringComparison.OrdinalIgnoreCase));
    }

    public GetReviewerDTO Create(CreateReviewerDTO reviewer)
    {
        var savedReviewer = new GetReviewerDTO(
            _currentId++,
            reviewer.Name,
            reviewer.Email,
            reviewer.PasswordHash,
            DateOnly.FromDateTime(reviewer.CreatedAt),
            reviewer.CreatedAt,
            reviewer.CreatedAt
        );

        _reviewers.Add(savedReviewer);

        return savedReviewer;
    }
}
