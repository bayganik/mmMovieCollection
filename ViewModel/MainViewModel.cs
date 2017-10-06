using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Configuration;
using System.IO;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using mmMovieCollection.Model;
using mmMovieCollection.Views;
//
// Movie video collection database 
//
// Using an XML database system.  See Movie.cs in Model folder.
//
// Search option will allow for adding new movies 
//
namespace mmMovieCollection.ViewModel
{
    public class MainViewModel : ViewModelBase 
    {
        bool makeDB = false;                            //temp for recreating db
        bool _idEnabled;
        bool _listEnabled;
        bool _saveEnabled;
        string _searchText;
        int _selectedIndex;

        string dbLocation, dbTransfer, csvLocation;
        
        //
        // List of video files for each movie
        // TV Series may have more than one video
        //
        VideoList _videoFiles;
        //
        // Command buttons on top
        //
        public RelayCommand CmdNew { get;  set; }
        public RelayCommand CmdExit { get;  set; }
        public RelayCommand CmdSearch { get;  set; }
        //
        // Command for each movie
        //
        public RelayCommand CmdSave { get; set; }
        public RelayCommand CmdDelete { get; set; }
        public RelayCommand CmdCancel { get; set; }
        public RelayCommand CmdAddVid { get; set; }
        public RelayCommand CmdPlayVid { get; set; }
        //
        // Poster images on top
        //
        public DirectoryInfo DBFolder { get; set; }
        public PhotoList PhotoFiles { get; set; }
        //
        // Search Fields
        //
        public ObservableCollection<string> SearchFields { get; set; }
        //
        // Movie names on the left
        //
        ObservableCollection<Movie> _movieList;

        ImageFile _selectedPhoto;
        VideoFile _selectedVideo;
        Movie _selectedMovie;
        string _selectedSearch;
        List<string> DeletePosters = new List<string>();
        //MovieTable MovieTbl;

        public MainViewModel()
        {
            //
            // The MOVIE model knows the name of the xml file,
            // We need to establish the location of the xml file
            // and the location of the posters.
            //
            DBLayer.Connection = ConfigurationManager.AppSettings["MovieLocation"].ToString();
            DBLayer.DBPosters = ConfigurationManager.AppSettings["PosterLocation"].ToString();
            DBLayer.MovieFiles = ConfigurationManager.AppSettings["MovieFileLocation"].ToString();

            CmdExit = new RelayCommand(() => ExitApplication());
            CmdSave = new RelayCommand(() => SaveRecord());
            CmdDelete = new RelayCommand(() => DeleteRecord());

            CmdNew = new RelayCommand(() => NewRecord());
            CmdCancel = new RelayCommand(() => CancelNewRecord());
            CmdSearch = new RelayCommand(() => SearchRecord());
            CmdAddVid = new RelayCommand(() => AddVidRecord());
            CmdPlayVid = new RelayCommand(() => PlayVideoFile());


            DBFolder = new DirectoryInfo(DBLayer.DBPosters);

            _selectedMovie = new Movie();
            AssistRoutines.MovieTbl = new MovieTable();
            VideoIndex = -1;

            LoadMovieList();

            IdEnabled = false;
            ListEnabled = true;
            SaveEnabled = false;
            //
            // Load the search fields
            //
            SearchFields = new ObservableCollection<string>(new string[]
	        {
                "Cast",
	            "Gener", 
                "Rating",
	            "Summary",
                "Title",
                "Year"   

	        });
            _selectedSearch = "Title";                  //default search field
        }
        private void DeleteRecord()
        {
            MessageBox.Show(SelectedMovie.CurrentVideo + " deleted, but poster will remain. \n Exit program to reload.");
            //
            // Delete video file
            //
            File.Delete(SelectedMovie.CurrentVideo);
            //
            // Poster files can not be deleted while program is running.
            // Save a copy of the path and delete on the way out
            //
            SelectedMovie.Poster = SelectedMovie.Title + "." + SelectedMovie.ID + ".jpg";

            string tpath = System.IO.Path.Combine(DBLayer.DBPosters, SelectedMovie.Poster);
            DeletePosters.Add(tpath);
            
            //var bitmap = new BitmapImage();
            //bitmap.BeginInit();
            //bitmap.UriSource = new Uri(tpath);
            //bitmap.CacheOption = BitmapCacheOption.None;
            //bitmap.EndInit();
            //File.Delete(tpath);
            //
            // Delete database record
            //
            AssistRoutines.MovieTbl.Delete(SelectedMovie.ID);
        }
        public static ImageSource BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }
        private void PlayVideoFile()
        {
            System.Diagnostics.Process.Start(SelectedMovie.CurrentVideo);


        }
        private void LoadMovieList()
        {
            _movieList = null;
            PhotoFiles = new PhotoList(DBFolder);
            List<Movie> ls = AssistRoutines.MovieTbl.GetAllList();
            MovieList = new ObservableCollection<Movie>((ls.OrderBy(x => x.Title).ToList()));
        }
        private void ReLoadMovieList(List<Movie> ls)
        {
            _movieList = null;
            MovieList = new ObservableCollection<Movie>((ls.OrderBy(x => x.Title).ToList()));
        }
        private void CancelNewRecord()
        {
            SelectedMovie = new Movie();
            SelectedMovie = null;
            IdEnabled = false;
            ListEnabled = true;
            AssistRoutines.AddANewMovie = false;
        }
        private void NewRecord()
        {

            SelectedMovie = new Movie();
            IdEnabled = true;
            //ListEnabled = false;
            SaveEnabled = true;

            Window nw = new NewMovie();
            nw.ShowDialog();
            //
            // Reset
            //
            LoadMovieList();

            IdEnabled = false;
            ListEnabled = true;
            SaveEnabled = false;
            //if (AssistRoutines.AddANewMovie)
            //{
            //    SelectedMovie = AssistRoutines.NewMovie;
            //    PosterImage = AssistRoutines.NewMovieImage;
            //}
        }
        private void AddVidRecord()
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.InitialDirectory = DBLayer.MovieFiles;

            ofd.Multiselect = true;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach(string fil in ofd.FileNames)
                {
                    VideoFiles.AddVideoFile(fil);
                }
            }
        }
        private void SaveRecord()
        {
            //
            // Record must have a unique id
            //
            if (SelectedMovie == null)
                return;

            if (string.IsNullOrEmpty(SelectedMovie.ID))
            {
                MessageBox.Show("You must have an ID for this movie.");
                return;
            }

            if (IdEnabled)
            {
                Movie temp = AssistRoutines.MovieTbl.Find(SelectedMovie.ID);
                if (temp == null)
                    AssistRoutines.MovieTbl.Insert(SelectedMovie);
                else
                {
                    MessageBox.Show("This movie ID belongs to: " + temp.Title);
                    return;
                }
            }
            else
            {
                int i = 0;
                SelectedMovie.AllVideo = "";
                foreach (var item in VideoFiles)
                {
                    i++;
                    SelectedMovie.AllVideo += item.Path;
                    if (i == VideoFiles.Count)
                        break;
                    SelectedMovie.AllVideo += "|";
                }
                //
                // Add the Poster if new movie
                //
                if (AssistRoutines.AddANewMovie)
                {
                    SelectedMovie.Poster = SelectedMovie.Title + "." + SelectedMovie.ID + ".jpg";

                    string tpath = System.IO.Path.Combine(DBLayer.DBPosters, SelectedMovie.Poster);
                    AssistRoutines.WriteImage(PosterImage, tpath);
                    PhotoFiles.AddFile(tpath);
                    //PhotoFiles.Update();
                }
                AssistRoutines.MovieTbl.Update(SelectedMovie);
                
            }

            //
            // Reset
            //
            LoadMovieList();
            IdEnabled = false;
            ListEnabled = true;
            SaveEnabled = false;
        }
        //private void SearchRecordTitle()
        //{
        //    if (string.IsNullOrEmpty(SearchText))
        //    {
        //        LoadMovieList();
        //        return;
        //    }

        //    List<Movie> tmpList = AssistRoutines.MovieTbl.Search("Title Like '%" + SearchText + "%'");
        //    if (tmpList.Count <= 0)
        //        MessageBox.Show("No movies found matching that search.");
        //    else
        //        ReLoadMovieList(tmpList);
        //    //
        //    // Clear the search phrase Gener
        //    //
        //    SearchText = String.Empty;
        //}

        private void SearchRecord()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                LoadMovieList();
                return;
            }
            //
            // Drop down "Search Fields" will tell us what field in db to search for phrase
            // example: "Title Like %star war%"  OR  "Gener Like %Comedy%"
            //
            List<Movie> tmpList = AssistRoutines.MovieTbl.Search(_selectedSearch + " Like '%" + SearchText + "%'");
            if (tmpList.Count <= 0)
                MessageBox.Show("No movies found matching the search for \n___________________________________________\n" + _selectedSearch + " Like " + SearchText);
            else
                ReLoadMovieList(tmpList);
            //
            // Clear the search phrase Gener
            //
            SearchText = String.Empty;
        }
        private void ExitApplication()
        {
            AssistRoutines.MovieTbl.Save();
            PosterImage = null;


            Application.Current.Shutdown();
            //
            // Delete poster files now
            //
            //foreach (string f in DeletePosters)
            //    File.Delete(f);
        }
        int _selectedMovieIndex;
        public int SelectedMovieIndex
        {
            get { return _selectedMovieIndex; }
            set
            {
                _selectedMovieIndex = value;
                RaisePropertyChanged("SelectedMovieIndex");
            }
        }
        public Movie SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                _selectedMovie = value;
                IdEnabled = false;
                SaveEnabled = true;


                if (_selectedMovie == null)
                    return;
                //
                // Get all video files for this movie
                //
                VideoFiles = new VideoList(SelectedMovie.AllVideo);
                //VideoIndex = VideoFiles.Find(SelectedMovie.CurrentVideo);
                int ind = -1;
                _selectedIndex = -1;
                //
                // Find out which video should currently be playing
                //
                foreach (VideoFile vf in VideoFiles)
                {
                    ind++;
                    if (vf.Path == SelectedMovie.CurrentVideo)
                    {
                        VideoIndex = ind;
                        break;
                    }
                }
                //
                // Convert the poster name into a BitmapImage
                //
                if (SelectedMovie.Poster.Trim().Length > 0)
                {
                    string pimg = System.IO.Path.Combine(DBFolder.FullName, SelectedMovie.Poster);
                    BitmapImage source = new BitmapImage();
                    try
                    {
                    source.BeginInit();
                    source.UriSource = new Uri(pimg, UriKind.Absolute);
                    source.EndInit();
                    PosterImage = source;
                    }
                    catch
                    {
                        PosterImage = null;
                    }

                }
                RaisePropertyChanged("SelectedMovie");
            }
        }
        public string SelectedSearch
        {
            get { return _selectedSearch; }
            set
            {
                _selectedSearch = value;
                RaisePropertyChanged("SelectedSearch");
            }
        }
        //
        // Selected Photo needs to become a Selected Movie
        //
        public ImageFile SelectedPhoto
        {
            get { return _selectedPhoto; }
            set
            {
                _selectedPhoto = value;
                string NameExt = _selectedPhoto.FileName;
                string Pkey = _selectedPhoto.Id;
                string NameOnly = _selectedPhoto.MovieName;

                RaisePropertyChanged("SelectedPhoto");

                //
                // Find the movie record for the poster
                //
                Movie tempMov = AssistRoutines.MovieTbl.Find(Pkey);
                if (tempMov == null)
                    return;

                for (int i = 0; i < _movieList.Count ; i++)
                {
                    if (tempMov.ID == _movieList[i].ID)
                    {
                        SelectedMovieIndex = i;
                        SelectedMovie = tempMov;
                        return;
                    }
                }
            }
        }
        BitmapImage _posterImage;
        public BitmapImage PosterImage
        {
            get { return _posterImage; }
            set
            {
                _posterImage = value;
                RaisePropertyChanged("PosterImage");
            }
        }
        //
        // Index for Combobox to show the current video file
        //
        public int VideoIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (value == null)
                    return;

                _selectedIndex = value;
                RaisePropertyChanged("VideoIndex");
            }
        }
        //
        // Current video file chosen to be played
        //
        public VideoFile SelectedVideo
        {
            get { return _selectedVideo; }
            set
            {
                if (value == null)
                    return;

                _selectedVideo = value;
                RaisePropertyChanged("SelectedVideo");
                //
                // Reset the SelectedMovie so the CurrentVideo can be redisplayed
                //
                SelectedMovie.CurrentVideo = _selectedVideo.Path;
                RaisePropertyChanged("SelectedMovie");
            }
        }

        public ObservableCollection<Movie> MovieList
        {
            get { return _movieList; }
            set
            {
                _movieList = value;
                RaisePropertyChanged("MovieList");
            }
        }
        public VideoList VideoFiles
        {
            get { return _videoFiles; }
            set
            {
                _videoFiles = value;
                RaisePropertyChanged("VideoFiles");
            }
        }
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                RaisePropertyChanged("SearchText");
            }
        }

        public bool ListEnabled
        {
            get
            {
                return _listEnabled;
            }
            set
            {
                _listEnabled = value;
                RaisePropertyChanged("ListEnabled");
            }
        }
        public bool SaveEnabled
        {
            get
            {
                return _saveEnabled;
            }
            set
            {
                _saveEnabled = value;
                RaisePropertyChanged("SaveEnabled");
            }
        }
        public bool IdEnabled
        {
            get
            {
                return _idEnabled;
            }
            set
            {
                _idEnabled = value;
                RaisePropertyChanged("IdEnabled");
            }
        }
        //
        // Method to connection to other pages.
        //
        private object GoToPage(string pageNum)
        {
            var msg = new GoToPage() { PageName = pageNum };
            Messenger.Default.Send<GoToPage>(msg);
            return null;
        }
    }

}
