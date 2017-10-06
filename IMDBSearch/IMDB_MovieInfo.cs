using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Windows.Media.Imaging;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace mmMovieCollection.IMDB.Movie
{
    /// <summary>
    /// A class that represent the infromation
    /// </summary>
    public class MovieInfo
    {
        public string ImdbURL { get; internal set; }
        public string Id { get; internal set; }
        /// <summary>
        ///title.
        /// </summary>
        public string Title { get; internal set; }
        /// <summary>
        ///release year.
        /// </summary>
        public string Year { get; internal set; }
        /// <summary>
        ///poster.
        /// </summary>
        public BitmapImage Poster { get; internal set; }
        /// <summary>
        ///poster location on web
        /// </summary>
        public string PosterUri { get; internal set; }
        /// <summary>
        ///meter rank.
        /// </summary>
        public string MeterRank { get; internal set; }
        /// <summary>
        ///popularity (up or down).
        /// </summary>
        public string Popularity { get; internal set; }
        /// <summary>
        ///difference of current meter rank with previous week.
        /// </summary>
        public string MeterChange { get; internal set; }
        /// <summary>
        ///content rating.
        /// </summary>
        public string ContentRating { get; internal set; }
        /// <summary>
        ///duration time (in minutes).
        /// </summary>
        public string Duration { get; internal set; }
        /// <summary>
        ///genre(s).
        /// </summary>
        public string Genre { get; internal set; }
        /// <summary>
        ///release date.
        /// </summary>
        public string ReleaseDate { get; internal set; }
        /// <summary>
        ///votes rating  xxxx out 10
        /// </summary>
        public string Rating { get; internal set; }
        /// <summary>
        ///number of votes.
        /// </summary>
        public string Votes { get; internal set; }
        /// <summary>
        ///number of reviews from users.
        /// </summary>
        public string UserReviews { get; internal set; }
        /// <summary>
        ///number of reviews from critics.
        /// </summary>
        public string CriticReviews { get; internal set; }
        /// <summary>
        ///score from Metacritic.co_movieInfo.
        /// </summary>
        public string Metascore { get; internal set; }
        /// <summary>
        ///number of reviews that metascore was based on.
        /// </summary>
        public string Metacritic { get; internal set; }
        /// <summary>
        ///description.
        /// </summary>
        public string Description { get; internal set; }
        /// <summary>
        ///director(s).
        /// </summary>
        public string Director { get; internal set; }
        /// <summary>
        ///writer(s).
        /// </summary>
        public string Writers { get; internal set; }
        /// <summary>
        ///star(s).
        /// </summary>
        public string Cast { get; internal set; }
        /// <summary>
        ///awards.
        /// </summary>
        public string Awards { get; internal set; }
    }

    /// <summary>
    /// A class that gets the information of a movie.
    /// </summary>
    public class IMDBMovie
    {
        private HtmlDocument doc = new HtmlDocument();
        private HtmlWeb web = new HtmlWeb();
        //Overview table
        private HtmlNode overview;

        public string Url { get; set; }

        private MovieInfo _movieInfo;
        /// <summary>
        /// Initialize a new instance of the AMDb.IMDb.MovieInfo class to a specified Url.
        /// </summary>
        /// <param name="Url">The IMDb url</param>
        public IMDBMovie(string Url)
        {
            this.Url = Url;
        }
        /// <summary>
        /// 
        /// </summary>
        public IMDBMovie() 
        { 
        }
        
        /// <summary>
        /// Get the information
        /// </summary>
        /// <returns>Movie class</returns>
        public MovieInfo getMovieInfo()
        {
            //Load the html of the page
            doc = web.Load(Url);
            //Load a part of html
            overview = doc.DocumentNode.SelectSingleNode("//div[@id='title-overview-widget']");
            _movieInfo = new MovieInfo();
            //
            // Get Movie Id (ttxxxxxx)
            //
            const string IMDB_ID_REGEX = @"(?<=\w\w)\d{7}";
            var nc = SN("//link[@rel='canonical']");
            Match tt = Regex.Match(nc[0].Attributes["href"].Value, IMDB_ID_REGEX, RegexOptions.IgnoreCase);
            try
            {
                _movieInfo.Id = "tt" + tt.Value;
                //
                // Get the URL (allows the user to visit the site)
                //
                _movieInfo.ImdbURL = Url;

                //Get the title
                _movieInfo.Title = SSN(".//h1/span");
                //Get the year
                _movieInfo.Year = SSN(".//h1/span[2]", "B");
                //Get the poster
                _movieInfo.PosterUri = SSNA(".//div[@class='image']/a/img", "src");
                _movieInfo.Poster = URLtoImage(_movieInfo.PosterUri );
                //Get the meter rank
                _movieInfo.MeterRank = SSN(".//a[@id='meterRank']");
                //Get the popularity (up or down)
                _movieInfo.Popularity = SSN(".//div[@id='meterChangeRow']/span");
                //Get the difference of current meter rank with previous week
                _movieInfo.MeterChange = SSN(".//div[@id='meterChangeRow']/span[2]");
                //Get the content rating
                _movieInfo.ContentRating = SSNA(".//span[@class='contentRating']", "title");
                //Get the duration
                _movieInfo.Duration = SSN(".//time[@itemprop='duration']");
                //Get the release date
                _movieInfo.ReleaseDate = SSN(".//a[@title='See all release dates']", "N");
                //Get the description
                _movieInfo.Description = SSN(".//p[@itemprop='description']");
                //Get the genre(s)
                var NodeCollection = SN(".//span[@itemprop='genre']");
                if (NodeCollection != null)
                {
                    foreach (var genre in NodeCollection)
                        _movieInfo.Genre += genre.InnerText + ", ";
                    _movieInfo.Genre = CLC(_movieInfo.Genre);
                }
                else
                    _movieInfo.Genre = null;
                //Get the director(s)
                NodeCollection = SN(".//div[@itemprop='director']//span");
                if (NodeCollection != null)
                {
                    foreach (var director in NodeCollection)
                        _movieInfo.Director += director.InnerText + ", ";
                    _movieInfo.Director = CLC(_movieInfo.Director);
                }
                else
                    _movieInfo.Director = null;
                //Get the writer(s)
                NodeCollection = SN(".//div[@itemprop='creator']//span");
                if (NodeCollection != null)
                {
                    foreach (var writer in NodeCollection)
                        _movieInfo.Writers += writer.InnerText + ", ";
                    _movieInfo.Writers = CLC(_movieInfo.Writers);
                }
                else
                    _movieInfo.Writers = null;
                //Get the star(s)
                NodeCollection = SN(".//div[@itemprop='actors']//span[@itemprop='name']");
                if (NodeCollection != null)
                {
                    foreach (var star in NodeCollection)
                        _movieInfo.Cast += star.InnerText + ", ";
                    _movieInfo.Cast = CLC(_movieInfo.Cast);
                }
                else
                    _movieInfo.Cast = null;
                //Get the votes rating
                //_movieInfo.Rating = SSN(".//div[@class='titlePageSprite star-box-giga-star']")+"/10";
                _movieInfo.Rating = SSN(".//div[@class='titlePageSprite star-box-giga-star']");
                //Get the number of votes
                _movieInfo.Votes = SSN(".//span[@itemprop='ratingCount']");
                //Get the number of reviews from users
                _movieInfo.UserReviews = SSN(".//a[@href='reviews?ref_=tt_ov_rt']");
                //Get the number of reviews from critics
                _movieInfo.CriticReviews = SSN(".//a[@href='externalreviews?ref_=tt_ov_rt']");
                //Get the score from Metacritic.com
                _movieInfo.Metascore = SSN(".//a[@href='criticreviews?ref_=tt_ov_rt']");
                //Get the number of reviews that metascore was based on.
                _movieInfo.Metacritic = SSN(".//a[@href='criticreviews?ref_=tt_ov_rt'][2]");
                //Get the awards
                NodeCollection = doc.DocumentNode.SelectNodes
                    (".//div[@id='titleAwardsRanks']/a | .//div[@id='titleAwardsRanks']//span[@itemprop='awards']");
                if (NodeCollection != null)
                {
                    foreach (var award in NodeCollection)
                    {
                        if (award.InnerText != "")
                            _movieInfo.Awards += award.InnerText + ", ";
                    }
                    _movieInfo.Awards = CLC(_movieInfo.Awards.Remove(_movieInfo.Awards.Length - 1));
                }
                else
                    _movieInfo.Awards = null;
            }
            catch
            {
                _movieInfo = null;
            }
            return _movieInfo;
        }

        //Select single node
        private string SSN(string XPath)
        {
            HtmlNode temp = overview.SelectSingleNode(XPath);
            if (temp != null)
                return temp.InnerText.Trim();
            else
                return null;
        }
        //Select Nodes
        private HtmlNodeCollection SN(string XPath)
        {
            HtmlNodeCollection temp = overview.SelectNodes(XPath);
            if (temp != null)
                return temp;
            else
                return null;
        }
        //Select single node and apply a trim (TrimN or TrimB)
        private string SSN(string XPath, string Trim)
        {
            HtmlNode temp = overview.SelectSingleNode(XPath);
            if (temp == null)
                return null;
            else if (Trim == "N")
                return TrimN(temp.InnerText);
            else if (Trim == "B")
                return TrimB(temp.InnerText);
            else
                return temp.InnerText;
        }
        //Select attribute of a single node
        private string SSNA(string XPath, string Attribute)
        {
            HtmlNode temp = overview.SelectSingleNode(XPath);
            if (temp != null)
                return temp.Attributes[Attribute].Value;
            else
                return null;
        }
        //Clear last comma
        private string CLC(string input)
        {
            return input.Remove(input.Length - 2);
        }
        //Trim new lines
        private string TrimN(string input)
        {
            return input.Replace("\n", " ").Trim();
        }
        //Trim brackets
        private string TrimB(string input)
        {
            return input.Replace("(", "").Replace(")", "").Trim();
        }
        //Convert url to image
        //private Image URLtoImage(string url)
        //{
        //    using (WebClient wc = new WebClient())
        //    {
        //        if (url == null)
        //            return null;
        //        byte[] bytes = wc.DownloadData(url);
        //        MemoryStream ms = new MemoryStream(bytes);
        //        return BitmapImage.FromStream(ms);
        //    }
        //}
        private BitmapImage URLtoImage(string url)
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
}
