using CRUDOperationDotNet.Models;
using System.ComponentModel.DataAnnotations;

namespace CRUDOperationDotNet.ViewModels
{
    public class MovieFormVM
    {
        public int Id { get; set; }

        [Required, StringLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }

        [Range(1,10)]
        public float Rate { get; set; }

        [Required, StringLength(2500)]
        public string Storyline { get; set; }

        public string PosterName { get; set; }

        [Display(Name ="Choose Poster ...." ) , Required(ErrorMessage ="Poster Is Required")]
        public IFormFile Poster { get; set; }


        [Display(Name ="Genre")]
        public byte GenreId { get; set; }

        public IEnumerable<Genre> Genres { get; set; }


    }
}
