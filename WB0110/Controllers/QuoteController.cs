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
        // GET api/<QuoteController>
        // get random quote
        [HttpGet]
        public ActionResult<Quote> Get()
        {
            _rand = new Random();

            int MaxQuotes = _db.Quotes.Count();
            int rnd = _rand.Next(1, MaxQuotes);
            return _db.Quotes.Find(rnd);
        }

        // POST api/<QuoteController>
        // insert new quote (without tags)
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
                _db.SaveChanges();
                return _db.Quotes.Find(value.Id);
            }
        }
        // GET api/<QuoteController/5>
        // get quote with id 5
        [HttpGet("{id}")]
        public ActionResult<Quote> Get(int id)
        {
            if (_db.Quotes.Contains(new Quote { Id = id }))
            {
                return _db.Quotes.Find(id);
            }
            else
            {
                return NotFound();
            }
        }

        //// DELETE api/<QuoteController>/5
        //// delete quote with id 5
        [HttpDelete("{id?}")]
        public ActionResult<Quote> Delete(int id)
        {
            if (_db.Quotes.Contains(new Quote { Id = id }))
            {
                _db.Quotes.Remove(_db.Quotes.Find(id));
                _db.SaveChanges();
                return Accepted();
            }
            else
            {
                return NotFound();
            }
        }
        //*Problém s vyhledáním správných věcí. Zeptat se, jak to ozkoušet na postmanovi*


        // POST api/<QuoteController/5/tags>
        // link new tags with quote 5
        [HttpPost("{id}/tags")]
        public ActionResult<IEnumerable<Tag>> InsertTags(int id, [FromBody] IEnumerable<int> tagIds)
        {
            if (_db.Quotes.Contains(new Quote { Id = id }))
            {
                var quote = _db.Quotes.Find(id);
                foreach (var item in tagIds)
                {
                    var tag = _db.Tags.Find(item);
                    _db.TagQuotes.Add(new TagQuote { Tag = tag, Quote = quote });
                }
                _db.SaveChanges();
                return Accepted();
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/<QuoteController/5/tags>
        // unlink tags connected with quote 5
        [HttpDelete("{id}/tags")]
        public ActionResult<IEnumerable<Tag>> DeleteTags(int id, [FromBody] IEnumerable<int> tagIds)
        {
            if (_db.Quotes.Contains(new Quote { Id = id }))
            {

                foreach (var item in tagIds)
                {
                    var tag = _db.Tags.Find(item);
                    _db.TagQuotes.Remove(_db.TagQuotes.Where(x => x.QuoteId == id).SingleOrDefault(x => x.Tag == tag));
                }
                _db.SaveChanges();
                return Accepted();
            }
            else
            {
                return NotFound();
            }
        }


        // GET api/<QuoteController/5/tags>
        // get linked tags with quote 5
        [HttpGet("{id}/tags")]
        public ActionResult<IEnumerable<Tag>> GetTags(int id)
        {
            if (_db.Quotes.Contains(new Quote { Id = id }))
            {
                var quote = _db.Quotes.Include(x => x.TagQuotes).SingleOrDefault(x => x.Id == id);
                List<Tag> tags = new List<Tag>();
                foreach (var item in quote.TagQuotes)
                {
                    tags.Add(item.Tag);
                }
                return tags;
            }
            else
            {
                return NotFound();
            }
        }
    }
}
