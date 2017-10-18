using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComicDemoModels;
using ComicDemoApi;

namespace ComicDemoApp.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<ComicBook> Comics { get; set; }


    }
}
