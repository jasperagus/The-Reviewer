using System;
using System.Collections.Generic;
using System.Text;

namespace TheReviewer.Data.DTO
{
    public class ReviewDTO
    {
        public int id {  get; set; }
        public string Content { get;  set; }
        public int Rating { get;  set; }
        public int ReviewerId { get;  set; }
        public int? FilmId { get;  set; }
        public int? GameId { get;  set; }
    }
}
