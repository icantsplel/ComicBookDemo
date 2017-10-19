using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicDemoModels;

namespace ComicDemoApi.Controllers
{
    [Produces("application/json")]

    public class ComicBooksController : Controller
    {
        private readonly ComicBookContext _context;

        public ComicBooksController(ComicBookContext context)
        {
            _context = context;
        }

        [Route("api/ComicBooks/GetAllMyComics")]
        [HttpGet]
        public IEnumerable<ComicBook> GetComicBooks()
        {
            return _context.ComicBooks;
        }

        //// GET: api/ComicBooks/5
        //[HttpGet("{id}")]
        //[Route("api/ComicBooks/GetAllMyComics")]
        //public async Task<IActionResult> GetComicBook([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var comicBook = await _context.ComicBooks.SingleOrDefaultAsync(m => m.Id == id);

        //    if (comicBook == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(comicBook);
        //}

        // PUT: api/ComicBooks/5
        [HttpPut]
        [Route("api/ComicBooks/update/{id}")]
        public async Task<IActionResult> PutComicBook([FromRoute] int id, [FromBody] ComicBook comicBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comicBook.Id)
            {
                return BadRequest();
            }

            _context.Entry(comicBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComicBookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ComicBooks
        [HttpPost]
        [Route("api/ComicBooks/AddToCollection")]
        public async Task<IActionResult> PostComicBook([FromBody] ComicBook comicBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ComicBooks.Add(comicBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComicBook", new { id = comicBook.Id }, comicBook);
        }

        // DELETE: api/ComicBooks/5
        [HttpDelete]
        [Route("api/ComicBooks/delete/{id}")]
        public async Task<IActionResult> DeleteComicBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comicBook = await _context.ComicBooks.SingleOrDefaultAsync(m => m.Id == id);
            if (comicBook == null)
            {
                return NotFound();
            }

            _context.ComicBooks.Remove(comicBook);
            await _context.SaveChangesAsync();

            return Ok(comicBook);
        }

        private bool ComicBookExists(int id)
        {
            return _context.ComicBooks.Any(e => e.Id == id);
        }
    }
}