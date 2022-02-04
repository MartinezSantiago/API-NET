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
    public class FilmsController : GenericsController<Film, AppDbContext>, FilmInterface
    {
        private readonly AppDbContext _AppDbContext;
        public FilmsController(AppDbContext context) : base(context)
        {
            _AppDbContext = context;
        }
        [HttpGet]
        public List<ShortFilm> GetAll()
        {
            
            return _AppDbContext.Films.Select(p => new ShortFilm
            {
                Id = p.Id,
                Title = p.Title,
                Image = p.Image,
                CreatedDate = p.CreatedDate

            }).ToList();
        } 
        [HttpGet("api/[controller]/Search")]
        
        public List<Film> Search(string Title,int GenreId,string order)
        {
           
            var query = new List<Film>();
            if (String.Equals(order, "Asc") || String.Equals(order, "ASC") || String.Equals(order, "asc"))
            {
                query = (from Film in _AppDbContext.Films where Film.Title.Contains(Title) && GenreId == Film.Id orderby Film.CreatedDate ascending select Film).ToList();
                return query;
            }
            else if(String.Equals(order, "Desc") || String.Equals(order, "DESC") || String.Equals(order, "desc"))
            {
                query = (from Film in _AppDbContext.Films where Film.Title.Contains(Title) && GenreId == Film.Id orderby Film.CreatedDate descending select Film).ToList();
                return query;
            }
            else
            {
                return query;
            }
        }

    }
}
