namespace TheReviewer.Frontend.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShouldShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}