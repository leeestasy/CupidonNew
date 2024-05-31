using Cupidon.Helper;
using Cupidon.Models;
using Microsoft.VisualBasic.ApplicationServices;
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

namespace Cupidon.View
{
    /// <summary>
    /// Логика взаимодействия для ProfileAdmin.xaml
    /// </summary>
    public partial class ProfileAdmin : Page
    {
        AcquaintancesContext db = new AcquaintancesContext();

        public ProfileAdmin()
        {
            InitializeComponent();

            ProfileAdminListView.MouseDoubleClick += DataGrid_DoubleClick;
            // Фильтрация данных для отображения в ProfileAdminDataGrid
            var profileAdminData = db.Clients.Where(item => item.StatusId != 2 && item.Flag == false).ToList();
            ProfileAdminListView.ItemsSource = profileAdminData;
        }

        private void DataGrid_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Client selectedClient = (Client)ProfileAdminListView.SelectedItem;
            ProfileDataAdmin clientDetailsPage = new ProfileDataAdmin(selectedClient);
            clientDetailsPage.DataContext = selectedClient;
            this.NavigationService.Navigate(clientDetailsPage);
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardHelper.HandleKeyDown(sender, e);
        }

        private void ProfileAdminListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

                if (ProfileAdminListView.SelectedItem != null)
                {
                    AdminDataStore.SelectedClient = (Client)ProfileAdminListView.SelectedItem;
                }
 
        }

        private void SearchButtonAdmin_Click(object sender, RoutedEventArgs e)
        {
           var newClients = db.Clients
           .Where(client => client.Status.Title != "В браке") // Filter by status
           .Where(client => (bool)!client.Flag); // Filter by Flag

            // Gender filtering
            if (GenderMen.IsChecked == true)
                newClients = newClients.Where(x => x.Gender == "муж");
            else if (GenderWomen.IsChecked == true) // Assuming you have a GenderWomen checkbox
                newClients = newClients.Where(x => x.Gender == "жен");

            // Search based on text inputs
            if (!string.IsNullOrEmpty(SurnameSearch.Text))
                newClients = newClients.Where(x => x.Surname.Contains(SurnameSearch.Text));

            if (!string.IsNullOrEmpty(NameSearch.Text))
                newClients = newClients.Where(x => x.Name.Contains(NameSearch.Text));

            if (!string.IsNullOrEmpty(PatronymicSearch.Text))
                newClients = newClients.Where(x => x.Patronymic.Contains(PatronymicSearch.Text));

            if (!string.IsNullOrEmpty(WordSearch.Text))
                newClients = newClients.Where(x => x.Description.Contains(WordSearch.Text));

            ProfileAdminListView.ItemsSource = newClients.ToList();
        }
    }
}
