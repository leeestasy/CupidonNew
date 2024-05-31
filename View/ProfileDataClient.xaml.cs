using Cupidon.Helper;
using Cupidon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cupidon.View
{
    /// <summary>
    /// Логика взаимодействия для ProfileDataClient.xaml
    /// </summary>
    public partial class ProfileDataClient : Page
    {
        //private Client currentClient;
        //private Status statuses;
        //private Education educations;
        //private ZodiacSign zodiacSigns;
        private Client currentClient; // Make 'currentClient' a property of the class

        public ProfileDataClient(Client client) 
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
        //private void CreateButton_Click(object sender, RoutedEventArgs e)
        //{

        //}
        private Client GetCurrentClient()
        {
            return App.currentClient;
        }

        private void InventButton_Click(object sender, RoutedEventArgs e)
        {
            Client currentClient = GetCurrentClient(); // Получение данных текущего пользователя из приложения
            Client selectedClient = ClientDataStore.SelectedClient; // Получение данных пользователя, на чью анкету зашли

            if (currentClient != null && selectedClient != null)
            {
                InvitationGenerator.GenerateInvitation(currentClient, selectedClient);
                System.Windows.MessageBox.Show("Приглашение было сохранено на рабочем столе.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                System.Windows.MessageBox.Show("Не удалось получить данные пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class InvitationGenerator
    {
        public static void GenerateInvitation(Client sender, Client receiver)
        {
            string senderFullName = $"{sender.Name} {sender.Surname}";
            string receiverFullName = $"{receiver.Name} {receiver.Surname}";
            string currentDate = DateTime.Now.ToString("dd.MM.yyyy");

            string invitationText = $"Приглашение на встречу\n\n" +
                                    $"От: {senderFullName}\n" +
                                    $"Кому: {receiverFullName}\n" +
                                    $"Дата: {currentDate}\n\n";

            // Сохранение текстового документа
            string fileName = "Приглашение.txt";
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            File.WriteAllText(filePath, invitationText);

            // Отображение текстового документа в консоли
            Console.WriteLine(invitationText);
        }
        //private void DeleteButtonClient_Click(object sender, RoutedEventArgs e)
        //{

        //}
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyboardHelper.HandleKeyDown(sender, e);
        }
    }
}
