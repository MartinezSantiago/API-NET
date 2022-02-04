using System.ComponentModel.DataAnnotations;

namespace ChallengeApi.Models
{
    public class Genre
    {
        public Genre(string name)
        {
            Name = name;
        
        }

        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "The name must have minimally 2 letters and maximally 25 letters"), MaxLength(25, ErrorMessage = "The name must have minimally 2 letters and maximally 25 letters")]
        public string Name { get; set; }
       
        public virtual List<Film> Films { get; set; }

    }
}
