using Cupidon.Helper;
using Cupidon.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using static MaterialDesignThemes.Wpf.Theme;

namespace Cupidon.View
{
    /// <summary>
    /// Логика взаимодействия для ProfileClient.xaml
    /// </summary>
    public partial class ProfileClient : Page
    {
        AcquaintancesContext db = new AcquaintancesContext();

        public ProfileClient()
        {
            InitializeComponent();

            ProfileClientListView.MouseDoubleClick += DataGrid_DoubleClick;

            var profileClientData = db.Clients.Where(item => item.StatusId != 2 &&  item.Flag == false).ToList();
            //from client in db.Clients
            //join statuses in db.Statuses on client.StatusId equals statuses.StatusId
            //where client.Flag == false && statuses.Title != "В браке"
            //select new
            //{
            //    surname = client.Surname,
            //    name = client.Name,
            //    patronymic = client.Patronymic,
            //    birthday = client.Birthday,
            //    gender = client.Gender,
            //    city = client.City,
            //};

            ProfileClientListView.ItemsSource = profileClientData.ToList();
        }
        private void DataGrid_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Client selectedClient = (Client)ProfileClientListView.SelectedItem;
            ProfileDataClient clientDetailsPage = new ProfileDataClient(selectedClient);
            clientDetailsPage.DataContext = selectedClient;
            this.NavigationService.Navigate(clientDetailsPage);
            //// Получение данных о клиенте из выбранной записи в DataGrid
            //Client selectedClient = (Client)ProfileClientListView.SelectedItem;

            //// Переход на новую страницу с передачей данных о клиенте
            //ProfileDataClient clientDetailsPage = new ProfileDataClient();
            //clientDetailsPage.FullNameTextBlock.Text = $"{selectedClient.Surname} {selectedClient.Name} {selectedClient.Patronymic}";
            //clientDetailsPage.DateOfBirthTextBlock.Text = $"Дата рождения: {selectedClient.Birthday}";
            //clientDetailsPage.GenderTextBlock.Text = $"Пол: {selectedClient.Gender}";
            //clientDetailsPage.HeightTextBlock.Text = $"Рост (см): {selectedClient.Height}";
            //clientDetailsPage.WeightTextBlock.Text = $"Вес (кг): {selectedClient.Height}";
            //clientDetailsPage.CountryTextBlock.Text = $"Старна: {selectedClient.Country}";
            //clientDetailsPage.CityTextBlock.Text = $"Город: {selectedClient.City}";
            //clientDetailsPage.ChildrenTextBlock.Text = $"Дети: {selectedClient.Children}";
            //clientDetailsPage.ZodiacSignTextBlock.Text = $"Знак зодиака: {selectedClient.ZodiacSign.Title}";
            //clientDetailsPage.EducationTextBlock.Text = $"Образование: {selectedClient.Education.Title}";
            //clientDetailsPage.StatusTextBlock.Text = $"Статус: {selectedClient.Status.Title}";
            //clientDetailsPage.DescriptionTextBlock.Text = $"Описание: {selectedClient.Description}";

            // Установите другие данные о клиенте на новой странице

            this.NavigationService.Navigate(clientDetailsPage);
        }

        private void SearchButtonClient_Click(object sender, RoutedEventArgs e)
        {
            var newClients = db.Clients
            .Where(client => client.Status.Title != "В браке") 
            .Where(client => (bool)!client.Flag); 

            // Gender filtering
            if (GenderMen.IsChecked == true)
                newClients = newClients.Where(x => x.Gender == "муж");
            else if (GenderWomen.IsChecked == true) 
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

            ProfileClientListView.ItemsSource = newClients.ToList(); 
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardHelper.HandleKeyDown(sender, e);
        }
        private void ProfileClientListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProfileClientListView.SelectedItem != null)
            {
                ClientDataStore.SelectedClient = (Client)ProfileClientListView.SelectedItem;
            }
        }
        private void SurnameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
