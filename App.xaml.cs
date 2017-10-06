using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

using GalaSoft.MvvmLight.Threading;
using mmMovieCollection.Model;
using mmMovieCollection.Views;

namespace mmMovieCollection
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //static App()
        //{
        //    DispatcherHelper.Initialize();
        //}
        void AppStartup(object sender, StartupEventArgs args)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            mainWindow.Photos = (PhotoList)(this.Resources["Photos"] as ObjectDataProvider).Data;
            mainWindow.Photos.Path = @"C:\()XMLdb\Posters";

        }
    }
}
