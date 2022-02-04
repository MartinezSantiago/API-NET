using ChallengeApi.Context;
using ChallengeApi.Interfaces;
using ChallengeApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ChallengeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CharactersController : GenericsController<Character, AppDbContext>, CharacterInterface
    {
        private readonly AppDbContext _AppDbContext;
        public CharactersController(AppDbContext context) : base(context)
        {
            _AppDbContext = context;
        }

        [HttpGet]
        public List<ShortCharacter> GetAll()
        {
      
            return _AppDbContext.Characters.Select(p => new ShortCharacter
            {
                Id = p.Id,
                Name = p.Name,
                Image = p.Image
               
            }).ToList();
        }
        [HttpGet("api/[controller]/Search")]
        public List<Character>Search(int FilmId, string name, int age)
        {
           

            var query = (from character in _AppDbContext.Characters where character.Name.Contains(name) && character.Age==age && character.FilmId==FilmId select character).ToList();
            return query;
        }
      

    }
}
