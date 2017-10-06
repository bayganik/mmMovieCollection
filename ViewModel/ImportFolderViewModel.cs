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

namespace mmMovieCollection.ViewModel
{
    /// <summary>
    /// Window shows all the movie files in a folder and sends the selected one to the search window
    /// </summary>
    public class ImportFolderViewModel : ViewModelBase
    {
        //
        // Command buttons on top
        //
        public RelayCommand CmdFolder { get; set; }
        public RelayCommand<Window> CmdExit { get; set; }

        private ObservableCollection<FolderFiles> _movieList;
        public ImportFolderViewModel()
        {
            CmdExit = new RelayCommand<Window>((x) => ExitWindow(x));
            CmdFolder = new RelayCommand(() => MovieFolder());

            //string path = System.Reflection.Assembly.GetExecutingAssembly().Location;

            string path = @"C:\Collection_Movie";
            if (Directory.Exists(path))
                LoadFolderFiles(path);
        }
        private void MovieFolder()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;

            LoadFolderFiles(dialog.SelectedPath);

            //FileInfo fclass;
            //DirectoryInfo dirInfo = new DirectoryInfo(dialog.SelectedPath);
            //FileInfo[] fInfo = dirInfo.GetFiles("*.*", System.IO.SearchOption.TopDirectoryOnly);
            ////
            //// We have a collection of file names
            ////
            //_movieList = null;
            //List<FolderFiles> ls = new List<FolderFiles>();

            //foreach (FileInfo fle in fInfo)
            //{
            //    string i = fle.Name;
            //    string j = fle.FullName;
            //    string k = fle.Extension;
            //    long m = fle.Length;
            //    FolderFiles ff = new FolderFiles();
            //    ff.FullPath = fle.FullName;
            //    ff.Title = System.IO.Path.GetFileNameWithoutExtension(fle.FullName);
            //    ff.Title = ff.Title.Replace('-', ' ');
            //    ff.Title = ff.Title.Replace('_', ' ');
            //    ff.Title = ff.Title.Replace('.', ' ');
            //    ff.Name = fle.Name;
            //    ls.Add(ff);
            //}

            //MovieList = new ObservableCollection<FolderFiles>((ls.OrderBy(x => x.Title).ToList()));
        }
        private void LoadFolderFiles(string path)
        {
            FileInfo fclass;
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] fInfo = dirInfo.GetFiles("*.*", System.IO.SearchOption.TopDirectoryOnly);
            //
            // We have a collection of file names
            //
            _movieList = null;
            List<FolderFiles> ls = new List<FolderFiles>();

            foreach (FileInfo fle in fInfo)
            {
                string i = fle.Name;
                string j = fle.FullName;
                string k = fle.Extension;
                long m = fle.Length;
                FolderFiles ff = new FolderFiles();
                ff.FullPath = fle.FullName;
                ff.Title = System.IO.Path.GetFileNameWithoutExtension(fle.FullName);
                ff.Title = ff.Title.Replace('-', ' ');
                ff.Title = ff.Title.Replace('_', ' ');
                ff.Title = ff.Title.Replace('.', ' ');
                ff.Title = ff.Title.Replace('*', ' ');
                ff.Title = ff.Title.Replace(':', ' ');
                ff.Title.Trim();
                ff.Name = fle.Name;
                ls.Add(ff);
            }

            MovieList = new ObservableCollection<FolderFiles>((ls.OrderBy(x => x.Title).ToList()));
        }
        private void ExitWindow(Window win)
        {
            // Close the window that is showing here
            //
            //win.DialogResult = true;
            win.Close();
        }
        public ObservableCollection<FolderFiles> MovieList
        {
            get { return _movieList; }
            set
            {
                _movieList = value;
                RaisePropertyChanged("MovieList");
            }
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

        FolderFiles _selectedMovie;
        public FolderFiles SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                _selectedMovie = value;

                if (_selectedMovie == null)
                    return;
                RaisePropertyChanged("SelectedMovie");
                //
                // NewMovieViewModel is listening for this message
                //
                var msg = new SendMovieName() { PageName = "", SelectedMovie = _selectedMovie };
                Messenger.Default.Send<SendMovieName>(msg);
            }
        }
    }
}