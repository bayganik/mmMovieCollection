﻿/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:mmMovieCollection.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using mmMovieCollection.Model;

namespace mmMovieCollection.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //if (ViewModelBase.IsInDesignModeStatic)
            //{
            //    SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            //}
            //else
            //{
            //    SimpleIoc.Default.Register<IDataService, DataService>();
            //}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<NewMovieViewModel>();
            SimpleIoc.Default.Register<ImportFolderViewModel>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public NewMovieViewModel NewMovie
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewMovieViewModel>();
            }
        }
        public ImportFolderViewModel ImportFolderMovie
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ImportFolderViewModel>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}