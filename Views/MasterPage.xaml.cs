using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace mmMovieCollection.Views
{
    /// <summary>
    /// Interaction logic for MasterPage.xaml
    /// </summary>
    public partial class MasterPage : Page
    {
        public MasterPage()
        {
            InitializeComponent();
            Messenger.Default.Register<GoToPage>(this, (action) => ReceiveMessage(action));
        }
        //
        // The page navigation login has to be here, because the "frmMain" is the Frame
        // control that contains all other views.
        //
        private object ReceiveMessage(GoToPage action)
        {
            StringBuilder sb = new StringBuilder("/Views/");
            sb.Append(action.PageName);
            sb.Append(".xaml");
            try
            {
                this.frmMain.NavigationService.Navigate(new System.Uri(sb.ToString(), System.UriKind.Relative));
            }
            catch (Exception)
            {
                MessageBox.Show("Can not find " + action.PageName + " page.");
            }

            return null;
        }
    }
}
