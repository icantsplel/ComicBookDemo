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

namespace ComicDemoApp.Controllers
{
    public class HomeController : Controller
    {
        
        //public IActionResult Index()
        //{
        //    //IEnumerable<ComicBook> comics = 
        //    return View();
        //}

        string Baseurl = "http://localhost:3685/";
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

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
