using TheReviewer.Logic.Enums;

namespace TheReviewer.Logic.Models;

public class CreateReviewerResult
{
    public bool Success => Reviewer is not null;
    public ReviewerModel? Reviewer { get; }
    public CreateReviewerError? Error { get; }

    private CreateReviewerResult(ReviewerModel? reviewer, CreateReviewerError? error)
    {
        Reviewer = reviewer;
        Error = error;
    }

    public static CreateReviewerResult Created(ReviewerModel reviewer)
    {
        return new CreateReviewerResult(reviewer, null);
    }

    public static CreateReviewerResult Failed(CreateReviewerError error)
    {
        return new CreateReviewerResult(null, error);
    }

    public string AddCreateReviewerError()
    {
        var message = Error switch
        {
            CreateReviewerError.InvalidEmail => "Enter a valid email address",
            CreateReviewerError.WeakPassword => "Password must be at least 8 characters and include uppercase, lowercase, and a number",
            CreateReviewerError.EmailAlreadyExists => "An account with this email already exists",
            _ => "Could not create reviewer"
        };

        return message;
    }
}
