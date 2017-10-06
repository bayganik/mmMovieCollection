using System.Windows;
using mmMovieCollection.Model;
using System.Windows.Navigation;
using mmMovieCollection;

namespace mmMovieCollection.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        //
        // Main nvigation window that allows for login screen display
        //
        public PhotoList Photos;

        public MainWindow()
        {
            InitializeComponent();

        }
    }

}