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

using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

using System.Windows.Media.Imaging;
using System.Net;

using mmMovieCollection.Model;
using mmMovieCollection.IMDB.Movie;
using mmMovieCollection.Views;

namespace mmMovieCollection.ViewModel
{
    public class NewMovieViewModel : ViewModelBase
    {
        bool _idEnabled;
        bool _listEnabled;
        bool _saveEnabled;
        string _searchText;

        string dbLocation, dbTransfer, csvLocation;
        //
        // Command buttons on top
        //
        public RelayCommand CmdImport { get; set; }
        public RelayCommand<Window> CmdExit { get; set; }
        public RelayCommand CmdSearch { get; set; }
        public RelayCommand<TextBox> CmdSearchText { get; set; }
        //
        // Command for each movie
        //
        public RelayCommand CmdSave { get; set; }
        public RelayCommand CmdCancel { get; set; }
        public RelayCommand CmdAddVid { get; set; }
        //
        // Poster images on top
        //
        public DirectoryInfo DBFolder { get; set; }
        public PhotoList PhotoFiles { get; set; }
        //
        // Movie names on the left
        //
        ObservableCollection<IMDBResult> _movieList;

        BitmapImage _selectedPhoto;
        VideoFile _selectedVideo;
        Movie _selectedMovie;
        IMDBResult _selectedIMDB;

        string _videoPath;
        bool _importVideo;
        bool _copyFile;

        public NewMovieViewModel()
        {
            CmdExit = new RelayCommand<Window>((x) => ExitApplication(x));
            CmdCancel = new RelayCommand(() => CancelNewRecord());
            CmdSearch = new RelayCommand(() => SearchRecord());
            CmdSearchText = new RelayCommand<TextBox>((x) => SearchTextEnter(x));
            CmdImport = new RelayCommand(() => ImportFolder());
            CmdSave = new RelayCommand(() => SaveRecord());

            _selectedMovie = new Movie();

            IdEnabled = false;
            ListEnabled = true;
            SaveEnabled = false;
            IsExitEnable = true;

            AssistRoutines.AddANewMovie = true;

            Messenger.Default.Register<SendMovieName>(this, (action) => ReceiveNameMessage(action));
            _importVideo = false;
            _videoPath = string.Empty;
            _copyFile = false;
        }
        private void ReceiveNameMessage(SendMovieName action)
        {
            SearchText = action.SelectedMovie.Title;
            //
            // This is a video file being imported
            //
            _videoPath = action.SelectedMovie.FullPath;
            _importVideo = true;

            string folderName = System.IO.Path.GetDirectoryName(_videoPath);
            if (folderName == DBLayer.MovieFiles)
                _copyFile = false;
            else
                _copyFile = true;

            SearchRecord();
        }
        private void ReLoadMovieList(List<IMDBResult> ls)
        {
            List<IMDBResult> lstv = new List<IMDBResult>();
            //MovieList = new ObservableCollection<IMDBResult>(ls);

            foreach (IMDBResult ir in ls)
            {
                ir.Title.Replace(':', ' ');
                ir.Title.Replace('*', ' ');
                ir.Title.Replace('<', ' ');
                ir.Title.Replace('>', ' ');
                lstv.Add(ir);
            }
            _movieList = null;
            MovieList = new ObservableCollection<IMDBResult>(lstv);
        }
        private void CancelNewRecord()
        {

            SelectedMovie = null;
            SelectedMovie = new Movie();
            IdEnabled = false;
            ListEnabled = true;
            _importVideo = false;
        }
        //private void NewRecord()
        //{

        //    SelectedMovie = new Movie();
        //    IdEnabled = true;
        //    ListEnabled = false;
        //    SaveEnabled = true;
        //}
        private void ImportFolder()
        {
            IsExitEnable = false;
            Window imv = new ImportView();
            imv.Show();
            //
            // Now you can use the Exit button
            //
            IsExitEnable = true;
        }
        private void SaveRecord()
        {
            if (SelectedMovie == null)
                return;


            if (string.IsNullOrEmpty(SelectedMovie.ID))
            {
                MessageBox.Show("You must have an ID for this movie.");
                return;
            }

            AssistRoutines.AddANewMovie = true;
            //
            // test the new Id
            //
            Movie temp = AssistRoutines.MovieTbl.Find(SelectedMovie.ID);
            if (temp == null)
                AssistRoutines.MovieTbl.Insert(SelectedMovie);
            else
            {
                MessageBox.Show("This movie ID belongs to: " + temp.Title);
                return;
            }

            AssistRoutines.NewMovie = SelectedMovie;
            AssistRoutines.NewMovieImage = SelectedPhoto;
            //
            // If this video is thru the import from folder process
            //
            if (_importVideo)
            {
                SelectedMovie.AllVideo = _videoPath;
                SelectedMovie.CurrentVideo = _videoPath;

                if (_copyFile)
                {
                    // Use Path class to manipulate file and directory paths. 
                    string filename = System.IO.Path.GetFileName(_videoPath);
                    string destFile = System.IO.Path.Combine(DBLayer.MovieFiles, filename);
                    //
                    // To copy a folder's contents to a new location: 
                    // Create a new target folder, if necessary.
                    //
                    if (!System.IO.Directory.Exists(DBLayer.MovieFiles))
                    {
                        System.IO.Directory.CreateDirectory(DBLayer.MovieFiles);
                    }
                    //
                    // To copy a file to another location and  
                    // overwrite the destination file if it already exists.
                    //
                    //System.IO.File.Copy(_videoPath, destFile, true);
                    System.IO.File.Move(_videoPath, destFile);

                    SelectedMovie.AllVideo = destFile;
                    SelectedMovie.CurrentVideo = destFile;
                }
            }
            //
            // Poster is saved as a jpg
            //
            SelectedMovie.Poster = SelectedMovie.Title + "." + SelectedMovie.ID + ".jpg";
            string tpath = System.IO.Path.Combine(DBLayer.DBPosters, SelectedMovie.Poster);
            AssistRoutines.WriteImage(SelectedPhoto, tpath);
            //
            // Update the movie database xml file
            //
            AssistRoutines.MovieTbl.Update(SelectedMovie);
            AssistRoutines.MovieTbl.Save();
            //
            // Tell user
            //
            MessageBox.Show("Movie was added.");
            SelectedMovie = new Movie();
            //
            // Reset
            //
            ListEnabled = true;
            SaveEnabled = false;
            _importVideo = false;

        }
        //
        // EnterKey is captured in the Search Text box
        //
        private void SearchTextEnter(TextBox _textbox)
        {
            SearchText = _textbox.Text;
            SearchRecord();
        }
        //
        // Internet Search of IMDB screens (scraping)
        //
        private void SearchRecord()
        {
            if (string.IsNullOrEmpty(SearchText))
                return;

            using (new AssistRoutines.WaitCursor())
            {
                IMDBSearch srch = new IMDBSearch();
                srch.Keyword = SearchText;
                List<IMDBResult> rslt = srch.getSearchResult();
                //
                // Load the result into MovieList
                //
                if (rslt.Count <= 0)
                    MessageBox.Show("No movies found in IMDB matching that search.");
                else
                    ReLoadMovieList(rslt);
            }
            //
            // Clear the search phrase
            //
            SearchText = String.Empty;
        }
        private void ExitApplication(Window win)
        {
            //if (SelectedMovie != null)
            //{
            //    if (MessageBox.Show("Do you want to ADD this movie?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //        SaveRecord();
            //}
            //
            // Close the window that is showing here
            //
            win.DialogResult = true;
            win.Close();
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

        public IMDBResult SelectedIMDB
        {
            get { return _selectedIMDB; }
            set
            {
                _selectedIMDB = value;
                if (value == null)
                    return;

                RaisePropertyChanged("SelectedIMDB");
                //
                // Find the URL and get the info
                //
                IMDBMovie mov = new IMDBMovie(_selectedIMDB.Url);
                MovieInfo tempMov = mov.getMovieInfo();
                if (tempMov == null)
                {
                    MessageBox.Show("The movie you requested is not found.");
                    return;
                }
                _selectedMovie.ID = tempMov.Id;
                _selectedMovie.Title = tempMov.Title;
                _selectedMovie.Cast = tempMov.Cast;
                _selectedMovie.Gener = tempMov.Genre;
                _selectedMovie.ImdbURL = tempMov.ImdbURL;
                _selectedMovie.Rating = tempMov.Rating;
                _selectedMovie.RunTime = tempMov.Duration;
                _selectedMovie.Summary = tempMov.Description;
                _selectedMovie.Year = tempMov.Year;
                
                _selectedPhoto = tempMov.Poster;

                RaisePropertyChanged("SelectedMovie");
                RaisePropertyChanged("SelectedPhoto");
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
                RaisePropertyChanged("SelectedMovie");
            }
        }
        //int _selectedIndex;
        //public int VideoIndex
        //{
        //    get { return _selectedIndex; }
        //    set
        //    {
        //        if (value == null)
        //            return;

        //        _selectedIndex = value;
        //        RaisePropertyChanged("VideoIndex");
        //    }
        //}
        //public VideoFile SelectedVideo
        //{
        //    get { return _selectedVideo; }
        //    set
        //    {
        //        if (value == null)
        //            return;

        //        _selectedVideo = value;
        //        RaisePropertyChanged("SelectedVideo");
        //        //
        //        // Reset the SelectedMovie so the CurrentVideo can be redisplayed
        //        //
        //        SelectedMovie.CurrentVideo = _selectedVideo.Path;
        //        RaisePropertyChanged("SelectedMovie");
        //    }
        //}

        public ObservableCollection<IMDBResult> MovieList
        {
            get { return _movieList; }
            set
            {
                _movieList = value;
                RaisePropertyChanged("MovieList");
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
        public BitmapImage SelectedPhoto
        {
            get { return _selectedPhoto; }
            set
            {
                _selectedPhoto = value;
                RaisePropertyChanged("SelectedPhoto");
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

        bool _isExitEnable;
        public bool IsExitEnable
        {
            get
            {
                return _isExitEnable;
            }
            set
            {
                _isExitEnable = value;
                RaisePropertyChanged("IsExitEnable");
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