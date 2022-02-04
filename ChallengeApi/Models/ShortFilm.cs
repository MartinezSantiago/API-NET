namespace ChallengeApi.Models
{
    public class ShortFilm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreatedDate { get; set;}
    }
}
