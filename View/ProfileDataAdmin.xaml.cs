using Cupidon.Helper;
using Cupidon.Models;
using Microsoft.EntityFrameworkCore;
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
using static MaterialDesignThemes.Wpf.Theme;

namespace Cupidon.View
{
    /// <summary>
    /// Логика взаимодействия для ProfileDataAdmin.xaml
    /// </summary>
    public partial class ProfileDataAdmin : Page
    {
        AcquaintancesContext db = new AcquaintancesContext();
        private Client currentClient; 

        public ProfileDataAdmin(Client client)
        {
            InitializeComponent();

            this.currentClient = client;

            LoadDataFromDatabase();
        }
        private void LoadDataFromDatabase()
        {
            using (var db = new AcquaintancesContext())
            {
                var status = db.Clients.Include(b => b.Status)
                                       .FirstOrDefault(b => b.StatusId == currentClient.StatusId)?.Status;

                var education = db.Clients.Include(b => b.Education)
                                          .FirstOrDefault(b => b.EducationId == currentClient.EducationId)?.Education;

                var zodiacSign = db.Clients.Include(b => b.ZodiacSign)
                                          .FirstOrDefault(b => b.ZodiacSignId == currentClient.ZodiacSignId)?.ZodiacSign;
                ChildrenTextBlock.Text = (bool)currentClient.Children ? "Есть" : "Нет";

                StatusTextBlock.Text = status?.Title ?? "Статус не найден.";
                EducationTextBlock.Text = education?.Title ?? "Образование не найдено.";
                ZodiacSignTextBlock.Text = zodiacSign?.Title ?? "Знак зодиака не найден.";
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                $"Редактировать клиента: \n" +
                $"{currentClient.Surname} {currentClient.Name} {currentClient.Patronymic}",
                "Предупреждение",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.OK)
            {
                Client selectedClient = AdminDataStore.SelectedClient; // Получение данных пользователя, на чью анкету зашли

                if (selectedClient != null)
                {
                    EditRegistr editWindow = new EditRegistr(selectedClient);

                    editWindow.Owner = Window.GetWindow(this);

                    editWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите клиента для редактирования.");
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные клиента: \n" + currentClient.Surname + " " + currentClient.Name + " " + currentClient.Patronymic, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                // Удаление клиента из базы данных
                db.Clients.Remove(currentClient);
                db.SaveChanges();

                //Обновление DataGrid
                ProfileAdmin profileAdmin = new ProfileAdmin();
                profileAdmin.ProfileAdminListView.Items.Refresh();

                // Возврат на предыдущую страницу
                NavigationService.GoBack();
            }
        }

        private async void BlockButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentClient != null)
            {
                // Display a confirmation message box
                MessageBoxResult result = MessageBox.Show(
                    $"Добавить клиента: \n" +
                    $"{currentClient.Surname} {currentClient.Name} {currentClient.Patronymic}" +
                    $" в черный список?",
                    "Предупреждение",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.OK)
                {
                    currentClient.Flag = true;

                    using (var db = new AcquaintancesContext())
                    {
                        db.Clients.Attach(currentClient);
                        db.Entry(currentClient).State = EntityState.Modified;

                        try
                        {
                            await db.SaveChangesAsync();
                            MessageBox.Show("Клиент добавлен в черный список");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите клиента");
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardHelper.HandleKeyDown(sender, e);
        }
    }
}
