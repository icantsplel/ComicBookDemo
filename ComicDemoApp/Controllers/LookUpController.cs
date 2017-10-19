using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComicDemoModels;
using Microsoft.Extensions.Options;
using ComicDemoApp.Utility;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ComicDemoApp.Controllers
{
    public class LookUpController : Controller
    {
        private readonly ComicBookContext _context;
        protected string Baseurl = "http://localhost:49277/";

        public LookUpController(ComicBookContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            List<ComicBook> ComicBooks = new List<ComicBook>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/values/getcomics");

                if (Res.IsSuccessStatusCode)
                {
                    var ComicResponse = Res.Content.ReadAsStringAsync().Result;
                    ComicBooks = JsonConvert.DeserializeObject<List<ComicBook>>(ComicResponse);

                }
                return View(ComicBooks);
            }
        }

        //public async Task<ActionResult> AddToMyCollection()
        //{
        //    List<ComicBook> ComicBooks = new List<ComicBook>();

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(Baseurl);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage Res = await client.GetAsync("api/values/getcomics");

        //        if (Res.IsSuccessStatusCode)
        //        {
        //            var ComicResponse = Res.Content.ReadAsStringAsync().Result;
        //            ComicBooks = JsonConvert.DeserializeObject<List<ComicBook>>(ComicResponse);

        //        }
        //        return View(ComicBooks);
        //    }
        //}


        // POST: LookUp/AddToMyCollection/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("LookUp/AddToMyCollection/{id}")]
        public async Task<IActionResult> AddToMyCollection(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<ComicBook> comicBooks = new List<ComicBook>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string endpointName = "api/values/getcomics/" + id.ToString(); ;
                HttpResponseMessage Res = await client.GetAsync(endpointName);

                if (Res.IsSuccessStatusCode)
                {
                    var ComicResponse = Res.Content.ReadAsStringAsync().Result;
                    comicBooks = JsonConvert.DeserializeObject<List<ComicBook>>(ComicResponse);
                }
                 
            }

            if (ModelState.IsValid)
            {
                _context.Add(comicBooks[0]);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comicBooks[0]);
        }


        //// GET: LookUp/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    //if (id == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //var comicBook = await _context.ComicBooks
        //    //    .SingleOrDefaultAsync(m => m.Id == id);
        //    //if (comicBook == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //return View(comicBook);
        //}

        // GET: LookUp/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: LookUp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,CoverDate,DateAdded,DateLastUpdated,Deck,Description,Image,IssueNumber,Name,IssueDetailURL,Volume,VolumeDetailURL,UserNote")] ComicBook comicBook)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(comicBook);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(comicBook);
        //}

        // GET: LookUp/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comicBook = await _context.ComicBooks.SingleOrDefaultAsync(m => m.Id == id);
        //    if (comicBook == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(comicBook);
        //}

        //// POST: LookUp/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,CoverDate,DateAdded,DateLastUpdated,Deck,Description,Image,IssueNumber,Name,IssueDetailURL,Volume,VolumeDetailURL,UserNote")] ComicBook comicBook)
        //{
        //    if (id != comicBook.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(comicBook);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ComicBookExists(comicBook.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(comicBook);
        //}

        //// GET: LookUp/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comicBook = await _context.ComicBooks
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (comicBook == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(comicBook);
        //}


        private bool ComicBookExists(int id)
        {
            return _context.ComicBooks.Any(e => e.Id == id);
        }
    }
}
