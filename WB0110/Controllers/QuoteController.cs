using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WB0110.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WB0110.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        public ApplicationDbContext _db { get; set; }
        private Random _rand;
        public QuoteController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<Quote> Get()
        {
            _rand = new Random();

            int max = _db.Quotes.Count();
            int rnd = _rand.Next(1, max);

            return _db.Quotes.Find(rnd);
        }
        [HttpPost]
        public ActionResult<Quote> Insert([FromBody] Quote value)
        {
            if (value == null)
            {
                return NotFound();
            }
            else
            {
                _db.Quotes.Add(value);
                _db.SaveChangesAsync();
                return _db.Quotes.Find(value.Id);
            }
        }
    }
}
