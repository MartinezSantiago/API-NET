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
    public class GenresController : GenericsController<Genre, AppDbContext>, GenreInterface
    {
        private readonly AppDbContext _AppDbContext;
        public GenresController(AppDbContext context) : base(context)
        {
            _AppDbContext = context;
        }
       [HttpGet]
       public List<Genre> GetAll()
        {
            return _AppDbContext.Genres.ToList();
        }
    }
}
