using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Windows.Media.Imaging;
using System.Net;
using System.IO;

namespace mmMovieCollection.IMDB.Movie
{
    internal class ISearch
    {
        public HtmlDocument doc = new HtmlDocument();
        public HtmlWeb web = new HtmlWeb();
        public string IMDB = "http://imdb.com";
        //
        // Exact Match search URL
        //
        public string FindExact = "http://imdb.com/find?s=tt&ttype=ft&q=";

        public string TrimN(string input)
        {
            return input.Replace("\n", " ").Trim();
        }
        public string TrimB(string input)
        {
            return input.Replace("(", "").Replace(")","").Trim();
        }
        //public  Image URLtoImage(string url)
        //{
        //    using (WebClient wc = new WebClient())
        //    {
        //        byte[] bytes = wc.DownloadData(url);
        //        MemoryStream ms = new MemoryStream(bytes);
        //        return Image.FromStream(ms);
        //    }
        //}
        public BitmapImage URLtoImage(string url)
        {
            BitmapImage image = null;
            Uri imgUri = new Uri(url);
            WebRequest webRequest = WebRequest.CreateDefault(imgUri);
            webRequest.ContentType = "image/jpeg";
            WebResponse webResponse = webRequest.GetResponse();

            image = new BitmapImage();
            image.CreateOptions = BitmapCreateOptions.None;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.BeginInit();
            image.StreamSource = webResponse.GetResponseStream();
            image.EndInit();
            
            return image;

        }
    }

    /// <summary>
    /// Class for each result in the List of search results
    /// </summary>
    public class IMDBResult
    {
        private ISearch srch = new ISearch();
        internal string ImgUrl { get; set; }
        /// <summary>
        /// Get the poster icon of the movie.
        /// </summary>
        public BitmapImage PosterIcon { get { return srch.URLtoImage(ImgUrl); } }
        /// <summary>
        /// Get the title of the movie.
        /// </summary>
        public string Title { get; internal set; }
        /// <summary>
        /// Get the IMDb url of the movie.
        /// </summary>
        public string Url { get; internal set; }
    }
    
    /// <summary>
    /// Class gets a list of results from a search.
    /// </summary>
    public class IMDBSearch
    {
        private List<IMDBResult> _srchResut;
        private ISearch srch = new ISearch();
        /// <summary>
        /// Movie name to search IMDB
        /// </summary>
        public string Keyword {get; set;}

        /// <summary>
        /// Initialize a new instance of the AMDb.IMDb.SearchResult class to a specified keyword.
        /// </summary>
        /// <param name="Keyword">Keyword to be searched.</param>
        public IMDBSearch(string Keyword)
        {
            this.Keyword = Keyword;
        }
       
        /// <summary>
        /// 
        /// </summary>
        public IMDBSearch() 
        { 
        }

        /// <summary>
        /// Get search results.
        /// </summary>
        /// <returns>List of IMDBResult class</returns>
        public List<IMDBResult> getSearchResult()
        {
            //
            // Try-catch will allow for non-movie files to be processed
            //
            try
            {
                IMDBResult result;
                _srchResut = new List<IMDBResult>();
                srch.doc = srch.web.Load(srch.FindExact + Keyword.Replace(" ", "+"));
                //
                // srch.doc is the html page holding the result of the search page of IMDB
                // return it as a list so it can displayed for user to pick which movie
                //
                foreach (HtmlNode tr in srch.doc.DocumentNode.SelectNodes("//table[@class='findList']/tr"))
                {
                    HtmlNodeCollection td = tr.SelectNodes(".//td");
                    result = new IMDBResult();
                    result.ImgUrl = td[0].SelectSingleNode("./a/img").Attributes["src"].Value;
                    result.Title = srch.TrimN(td[1].InnerText);
                    result.Url = srch.IMDB + System.Text.RegularExpressions.Regex.Match(
                        td[1].SelectSingleNode("./a").Attributes["href"].Value, @".*(?=\?)").Value;
                    _srchResut.Add(result);
                }
            }
            catch (Exception ex)
            {
                _srchResut = new List<IMDBResult>();
            }
            return _srchResut;
        }

        /// <summary>
        /// Get search result of a movie by title.
        /// </summary>
        /// <param name="Title">Title of the movie.</param>
        /// <param name="ResultList">List of search results.</param>
        /// <returns>IMDBResult class</returns>
        public IMDBResult getMovieByTitle(string Title, List<IMDBResult> ResultList)
        {
            foreach (IMDBResult r in ResultList)
            {
                if (srch.TrimB(r.Title) == srch.TrimB(Title))
                    return r;
            }
            return null;
        }
        /// <summary>
        /// Get search IMDBResult of a movie by IMDb id.
        /// </summary>
        /// <param name="ID">IMDb id of the movie (e.g. tt1300854).</param>
        /// <param name="ResultList">List of search results.</param>
        /// <returns>IMDBResult class</returns>
        public IMDBResult getMovieByID(string ID, List<IMDBResult> ResultList)
        {
            foreach (IMDBResult r in ResultList)
            {
                if (r.Url.Contains(ID))
                    return r;
            }
            return null;
        }
    }
}
