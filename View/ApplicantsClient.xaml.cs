using Cupidon.Helper;
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
using System.Windows.Shapes;

namespace Cupidon.View
{
    /// <summary>
    /// Логика взаимодействия для ApplicantsClient.xaml
    /// </summary>
    public partial class ApplicantsClient : Window
    {
        public ApplicantsClient()
        {
            InitializeComponent();
            mainFrame.Navigate(new Uri("View/ProfileClient.xaml", UriKind.Relative));

        }

        //private void FriendsButton_Click(object sender, RoutedEventArgs e)
        //{
        //   mainFrame.Navigate(new Uri("View/FriendsClient.xaml", UriKind.Relative));
        //}

        private void ApplicantsButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Uri("View/ProfileClient.xaml", UriKind.Relative));
        }

        private void ExitButtonClient_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }
        //private void Window_KeyDown(object sender, KeyEventArgs e)
        //{
        //    KeyboardHelper.HandleKeyDown(sender, e);
        //}

        //private void InvitationsButton_Click(object sender, RoutedEventArgs e)
        //{
        //    mainFrame.Navigate(new Uri("View/InvitationsClient.xaml", UriKind.Relative));
        //}
    }
}
