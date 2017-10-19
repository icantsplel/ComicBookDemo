using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft;
using Newtonsoft.Json;
using System.Xml;
using System.Text;
using ComicDemoModels;

namespace ComicDemoApi.Controllers
{

    public class ValuesController : Controller
    {
        static string _address = "http://www.comicvine.com/api/issues/";
        static string _apikey = "7526d9a37e10834d099fd4d3f8feda72f1efc995"; //Comic Vine

        
        [Route("api/values/getcomics/{id?}")]
        [HttpGet]
        public IActionResult GetComics(int? id)
        {
            string filter = string.Empty;
            if (id !=null)
            {
                filter = "&filter=id:" + id.ToString();
            }

            var url = _address + "?api_key=" + _apikey + filter;
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.UserAgent = ".NET Framework Test Client";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            WebHeaderCollection header = response.Headers;

            string responseText = String.Empty;

            var encoding = ASCIIEncoding.ASCII;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                responseText = reader.ReadToEnd();
            }

            XmlDocument xmlResult = new XmlDocument();

            xmlResult.LoadXml(responseText);

            List<ComicBook> results = new List<ComicBook>();

            var nsm = new XmlNamespaceManager(xmlResult.NameTable);
            var elemList = xmlResult.GetElementsByTagName("issue");
            foreach (XmlNode node in elemList)
            {
                ComicBook comic = new ComicBook();

                comic.Id = int.Parse(node.SelectSingleNode("id", nsm).InnerText);
                comic.Name = node.SelectSingleNode("name", nsm).InnerText;
                comic.Image = node.SelectSingleNode("image", nsm).SelectSingleNode("icon_url", nsm).InnerText;
                comic.IssueDetailURL = node.SelectSingleNode("site_detail_url", nsm).InnerText;

                int issueNumber = 0;
                bool issueNumTest = Int32.TryParse(node.SelectSingleNode("issue_number", nsm).InnerText, out issueNumber);
                if (issueNumTest)                
                    comic.IssueNumber = issueNumber;
                else
                    comic.IssueNumber = null;

                DateTime coverDate = DateTime.MinValue;
                bool coverDateTest = DateTime.TryParse(node.SelectSingleNode("cover_date", nsm).InnerText, out coverDate);
                if (issueNumTest)
                    comic.CoverDate = (DateTime)coverDate;
                else
                    comic.CoverDate = null;

                DateTime dateAdded = DateTime.MinValue;
                bool dateAddedTest = DateTime.TryParse(node.SelectSingleNode("date_added", nsm).InnerText, out dateAdded);
                if (issueNumTest)
                    comic.DateAdded = (DateTime)dateAdded;
                else
                    comic.DateAdded = null;

                DateTime dateLastUpdated = DateTime.MinValue;
                bool dateLastUpdatedTest = DateTime.TryParse(node.SelectSingleNode("date_last_updated", nsm).InnerText, out dateLastUpdated);
                if (issueNumTest)
                    comic.DateLastUpdated = (DateTime)dateLastUpdated;
                else
                    comic.DateLastUpdated = null;

                comic.Deck = node.SelectSingleNode("deck", nsm).InnerText;
                comic.Description = node.SelectSingleNode("description", nsm).InnerText;
                comic.UserNote = string.Empty;
                comic.VolumeDetailURL = node.SelectSingleNode("volume", nsm).SelectSingleNode("site_detail_url", nsm).InnerText;

                int volume = 0;
                bool volumeTest = Int32.TryParse(node.SelectSingleNode("volume", nsm).SelectSingleNode("id", nsm).InnerText, out volume);
                if (issueNumTest)
                    comic.Volume = volume;
                else
                    comic.Volume = null;


                results.Add(comic);
            }

            return Ok(results);
        }
    }
}
