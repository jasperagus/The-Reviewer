using TheReviewer.Logic.Models;

namespace TheReviewer.Frontend.Models
{
    public class MediaViewModel
    {
        public List<MediaModel> Media { get; set; }
        public List<ReviewerModel> Reviewers { get; set; }
        public List<ReviewModel> Reviews { get; set; }
        public int MediaTypeId { get; set; } 

        public MediaViewModel(List<MediaModel> media, List<ReviewerModel> reviewers, List<ReviewModel> reviews, int mediaTypeId)
        {
            Media = media;
            Reviewers = reviewers;
            Reviews = reviews;
            MediaTypeId = mediaTypeId;
        }
    }
}