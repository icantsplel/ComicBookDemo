using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComicDemoApi;
using ComicDemoModels;
using ComicDemoApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ComicDemoApp.Utility;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace ComicDemoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ComicBookContext _context;
        protected string Baseurl = "http://localhost:49277/";

        public HomeController(ComicBookContext context)
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
 
                HttpResponseMessage Res = await client.GetAsync("api/ComicBooks/GetAllMyComics");

                if (Res.IsSuccessStatusCode)
                {
                    var ComicResponse = Res.Content.ReadAsStringAsync().Result;       
                    ComicBooks = JsonConvert.DeserializeObject<List<ComicBook>>(ComicResponse);

                }
                return View(ComicBooks);
            }
        }

        //public async Task<ActionResult> RemoveFromMyCollection()
        //{
        //    List<ComicBook> ComicBooks = new List<ComicBook>();

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(Baseurl);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage Res = await client.GetAsync("api/ComicBooks/GetAllMyComics");

        //        if (Res.IsSuccessStatusCode)
        //        {
        //            var ComicResponse = Res.Content.ReadAsStringAsync().Result;
        //            ComicBooks = JsonConvert.DeserializeObject<List<ComicBook>>(ComicResponse);

        //        }
        //        return View(ComicBooks);
        //    }
        //}

        // GET: ComicBooks/Delete/5
        public async Task<IActionResult> RemoveFromMyCollection(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comicBook = await _context.ComicBooks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (comicBook == null)
            {
                return NotFound();
            }

            return View(comicBook);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
