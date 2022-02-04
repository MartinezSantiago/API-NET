using System.ComponentModel.DataAnnotations;

namespace ChallengeApi.Models
{
    public class Character
    {
        public Character(string name, byte[] image, string story)
        {
            Name = name;
            Image = image;
            Story = story;
        }
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(2,ErrorMessage ="The name must have minimally 2 letters and maximally 25 letters"),MaxLength(25,ErrorMessage = "The name must have minimally 2 letters and maximally 25 letters")]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public byte[] Image { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        [MinLength(20,ErrorMessage ="The story must have minimally 20 letters")]
        public string Story { get; set; }
        [Required]
        public virtual int FilmId{get;set;}
        
    }
}
