using TheReviewer.Models;
namespace TheReviewer.Frontend.Models
{
    public class CreateFilmViewModel
    {
         public List<FilmModel> Films { get; set; }
        public CreateFilmViewModel(List<FilmModel>films)
        {
            Films = films;
        }
    }
}
