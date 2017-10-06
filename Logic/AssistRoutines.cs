using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Windows.Input;

using System.IO;
using mmMovieCollection.Model;

using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Net;

namespace mmMovieCollection
{
    public class AssistRoutines
    {
        public class WaitCursor : IDisposable
        {
            private Cursor _presviousCursor;

            public WaitCursor()
            {
                _presviousCursor = Mouse.OverrideCursor;

                Mouse.OverrideCursor = Cursors.Wait;
            }

            public void Dispose()
            {
                Mouse.OverrideCursor = _presviousCursor;
            }
        }
        /// <summary>
        /// Top level data folder which will hold all import schedule folders.
        /// </summary>
        public static string DataFolder;                                //top level folder
        public static string UserIdValue;
        public static string DomainValue;
        public static string PasswordValue;

        public static Movie NewMovie;
        public static BitmapImage NewMovieImage;
        public static MovieTable MovieTbl;
        public static bool AddANewMovie;
        /// <summary>
        /// Write an image from a URI on the web to disk
        /// </summary>
        public static void DownloadImage(Uri uri, string savePath)
        {
            var request = WebRequest.Create(uri);
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                Byte[] buffer = new Byte[response.ContentLength];
                int offset = 0, actuallyRead = 0;
                do
                {
                    actuallyRead = stream.Read(buffer, offset, buffer.Length - offset);
                    offset += actuallyRead;
                }
                while (actuallyRead > 0);
                File.WriteAllBytes(savePath, buffer);
                //return new MemoryStream(buffer);
            }
        }
        /// <summary>
        /// Write a Bitmap image from memory to disk
        /// </summary>
        public static void WriteImage(BitmapImage image, string savePath)
        {
            if (File.Exists(savePath))
                return;

            //savePath = savePath.Replace(":", "-").Replace(">","-").Replace("<","-").Replace("*","").Trim();

            using (FileStream fileStream = new FileStream(savePath, FileMode.Append))
            {
	            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
	            encoder.Frames.Add(BitmapFrame.Create((BitmapImage)image));
	            encoder.QualityLevel = 100;
	            encoder.Save(fileStream);
            }
        }
        //public static Stream DownloadImage(Uri uri, string savePath)
        //{
        //    var request = WebRequest.Create(uri);
        //    var response = request.GetResponse();
        //    using (var stream = response.GetResponseStream())
        //    {
        //        Byte[] buffer = new Byte[response.ContentLength];
        //        int offset = 0, actuallyRead = 0;
        //        do
        //        {
        //            actuallyRead = stream.Read(buffer, offset, buffer.Length - offset);
        //            offset += actuallyRead;
        //        }
        //        while (actuallyRead > 0);
        //        File.WriteAllBytes(savePath, buffer);
        //        return new MemoryStream(buffer);
        //    }
        //} 
        public static bool CheckDataFolder(string subFolder)
        {
            //
            // If datafolder is not established, then something is wrong
            //
            if (DataFolder.Length <= 0)
                return false;

            string dbTransfer = System.IO.Path.Combine(DataFolder, subFolder);

            try
            {
                if (!System.IO.Directory.Exists(dbTransfer))
                    System.IO.Directory.CreateDirectory(dbTransfer);

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
