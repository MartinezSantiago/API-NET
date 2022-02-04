using Microsoft.AspNetCore.Identity;

namespace ChallengeApi.Models
{
    public class User: IdentityUser
    {
        public bool IsActive { get; set; }
    }
}
