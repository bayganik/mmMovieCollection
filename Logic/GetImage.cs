using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace mmMovieCollection.Logic
{
    public static class Helper
    {
        //
        // Get a resource image from the project.
        // example: if you like to store your images under “ProjectFolder/Images” folder then the relative path to your resource will be  “Images/ImageName.png”.
        //
        //public static BitmapImage GetImage(string resourcePath)
        //{
        //    var image = new BitmapImage();

        //    string moduleName = GetType().Assembly.GetName().Name;
        //    string resourceLocation =
        //        string.Format("pack://application:,,,/{0};component/{1}", moduleName,
        //                      resourcePath);

        //    try
        //    {
        //        image.BeginInit();
        //        image.CacheOption = BitmapCacheOption.OnLoad;
        //        image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
        //        image.UriSource = new Uri(resourceLocation);
        //        image.EndInit();
        //    }
        //    catch (Exception e)
        //    {
        //        System.Diagnostics.Trace.WriteLine(e.ToString());
        //    }

        //    return image;
        //}
    }
}
    

