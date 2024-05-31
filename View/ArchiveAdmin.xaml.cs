using Cupidon.Helper;
using Cupidon.Models;
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
    /// Логика взаимодействия для ArchiveAdmin.xaml
    /// </summary>
    public partial class ArchiveAdmin : Page
    {
        AcquaintancesContext db = new AcquaintancesContext();

        public ArchiveAdmin()
        {
            InitializeComponent();

            ArchiveAdminListView.MouseDoubleClick += DataGrid_DoubleClick;
            // Фильтрация данных для отображения в ArchiveAdminListView
            var archiveAdminListView = db.Clients.Where(item => item.StatusId == 2).ToList();
            ArchiveAdminListView.ItemsSource = archiveAdminListView;
        }
        private void DataGrid_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Client selectedClient = (Client)ArchiveAdminListView.SelectedItem;
            ProfileDataAdmin clientDetailsPage = new ProfileDataAdmin(selectedClient);
            clientDetailsPage.DataContext = selectedClient;
            this.NavigationService.Navigate(clientDetailsPage);
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardHelper.HandleKeyDown(sender, e);
        }
        private void ArchiveAdminListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
