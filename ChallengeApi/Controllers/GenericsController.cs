using ChallengeApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChallengeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericsController<TEntity, TContext> : ControllerBase, GenericsInterface<TEntity> where TEntity : class  where TContext : DbContext
    {
        private readonly TContext _context;

        public GenericsController(TContext context)
        {
            _context = context;
        }

        /* GET: api/<GenericsController>
        [HttpGet]
         public List<TShortEntity> GetAll()
        {
            return _context.Set<TShortEntity>().ToList(); 
        } 
        */

        // GET api/<GenericsController>/5
        [HttpGet("{id}")]
        public TEntity ?Get(int ?id)
        {
            
            return _context.Set<TEntity>().Find(id);
        }

        // POST api/<GenericsController>
        [HttpPost]
        public void Post(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        // PUT api/<GenericsController>/5
        [HttpPut("{id}")]
        public void Put(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // DELETE api/<GenericsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            var aux = Get(id);
            if (aux != null)
            {
                _context.Set<TEntity>().Remove(aux);
                _context.SaveChanges();
            }
  
           

        }
     


      
    }
}
