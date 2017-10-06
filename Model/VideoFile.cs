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
    public class VideoFile
    {
        string _fileName;
        string _path;



        public VideoFile(string path)
        {
            _path = path;
            //
            // get file name on left size of the extension.
            // This should have movie_name + "." + type of video
            //
            _fileName = System.IO.Path.GetFileName(path);
        }
        public string FileName
        {
            get { return _fileName; }
        }
        public string Path 
        { 
            get { return _path; }
        }
    }

    public class VideoList : ObservableCollection<VideoFile>
    {
        public VideoList()
        {

        }
        public VideoList(string videoAll)
        {
            if (string.IsNullOrEmpty(videoAll))
                return;
            //
            // ONE or more video file paths are in the string 
            // separated by "|" 
            //
            string[] vids = videoAll.Split('|');
            for (int i = 0; i < vids.Length; i++)
            {
                Add(new VideoFile(vids[i]));
            }
        }
        public void AddVideoFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            Add(new VideoFile(fileName));
          
        }
        public int Find(string _vid)
        {
            if (string.IsNullOrEmpty(_vid))
                return -1;

            
            VideoFile tmp = new VideoFile(_vid);
            if (Contains(tmp))
                return IndexOf(tmp);

            int i = IndexOf(tmp);
            return 1;

        }

    }
}
