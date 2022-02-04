using System.ComponentModel.DataAnnotations;

namespace ChallengeApi.Models
{
    public class Film
    {
        public Film(byte[] image, string title, float calification)
        {
            Image = image;
            Title = title;
            Calification = calification;
          
        }

        [Required]
        public int Id { get; set; }
        [Required]
        public byte[] Image { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "The Title must have minimally 2 letters and maximally 25 letters"), MaxLength(25, ErrorMessage = "The Title must have minimally 2 letters and maximally 25 letters")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "The calification must be between 1 and 5.")]
        public float Calification { get; set; }
        [Required]
        public virtual int GenreId { get; set; }
        public virtual List<Character> Characters { get; set; }
        
    }
}
