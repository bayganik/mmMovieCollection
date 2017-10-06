using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using mmMovieCollection;

namespace mmMovieCollection.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            txtDomain.Text = Environment.UserDomainName.ToLower();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //ActDirHelper ad = new ActDirHelper();

            //if (ad.AuthenticateUser(txtDomain.Text,
            //      txtUserName.Text, txtPassword.Password))
            //    DialogResult = true;
            //else
                MessageBox.Show("Unable to Authenticate Using the Supplied Credentials");
        }
    }
}
