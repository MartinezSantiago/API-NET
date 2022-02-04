using System.ComponentModel.DataAnnotations;

namespace ChallengeApi.Model
{
    public class RequestToken
    {
       
        [Required]
        public string ?TokenCode { get; set; }

        [Required]
        public DateTime ValidTo { get; set; }

       
    }
}
