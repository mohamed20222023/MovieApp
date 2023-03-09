using System.ComponentModel.DataAnnotations;

namespace CRUDOperationDotNet.Models
{
    public class Movies
    {

        public int Id { get; set; }

        [Required,MaxLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }

        public float Rate { get; set; }

        [Required,MaxLength(2500)]
        public string Storyline { get; set; }

        [Required]
        public string Poster { get; set; }

        public byte GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
