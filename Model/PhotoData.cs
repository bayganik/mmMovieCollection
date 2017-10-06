using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Collections.Specialized;
using System.Windows.Controls;

namespace mmMovieCollection.Model
{
    public class ImageFile
    {
        private String _path;
        public String Path { get { return _path; } }
        private Uri _uri;
        private string _fileName;
        private string _id;
        private string _movieName;

        public string FileName { get { return _fileName; } }
        public string Id { get { return _id; } }
        public string MovieName { get { return _movieName; } }
        public Uri Uri { get { return _uri; } }
        private BitmapFrame _image;
        
        public BitmapFrame Image { get { return _image; } }

        public ImageFile(string path)
        {
            try
            {
                _path = path;
                //
                // get file name on left size of the extension.
                // This should have movie_name + "." + id of the movie
                //
                _fileName = System.IO.Path.GetFileNameWithoutExtension(path);
                _id = System.IO.Path.GetExtension(_fileName);
                if (_id.Contains("."))
                    _id = _id.Replace(".", "");

                _movieName = System.IO.Path.GetFileNameWithoutExtension(_fileName);
                _uri = new Uri(_path);
                _image = BitmapFrame.Create(_uri);
            }
            catch
            {
                MessageBox.Show("Can not display image.");
            }
        }

        public override string ToString()
        {
            return Path;
        }


    }

    public class PhotoList : ObservableCollection<ImageFile>
    {
        DirectoryInfo _directory;

        public PhotoList() 
        { 

        }

        public PhotoList(string path) : this(new DirectoryInfo(path)) 
        { 

        }

        public PhotoList(DirectoryInfo directory)
        {
            _directory = directory;
            Update();
        }

        public string Path
        {
            set
            {
                _directory = new DirectoryInfo(value);
                Update();
            }
            get { return _directory.FullName; }
        }

        public DirectoryInfo Directory
        {
            set
            {
                _directory = value;
                Update();
            }
            get { return _directory; }
        }
        public void AddFile(string _filePath)
        {
            Add(new ImageFile(_filePath));
            
            
        }
        public void Update()
        {
            Clear();
            foreach (FileInfo f in _directory.GetFiles("*.jpg"))
            {
                Add(new ImageFile(f.FullName));
            }
        }


    }
}
