using Cupidon.Helper;
using Cupidon.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        AcquaintancesContext db;
        private Client _client;

        public RegistrationForm()
        {
            InitializeComponent();

            db = new AcquaintancesContext();
            _client = new Client();

            LoadEducation();
            LoadStatus();
            LoadZodiacSign();
        }

        private void LoadEducation()
        {
            Education.ItemsSource = db.Educations.ToList();
            Education.DisplayMemberPath = "Title";
            Education.SelectedValuePath = "EducationId";
        }

        private void LoadStatus()
        {
            Status.ItemsSource = db.Statuses.ToList();
            Status.DisplayMemberPath = "Title";
            Status.SelectedValuePath = "StatusId";
        }

        private void LoadZodiacSign()
        {
            ZodiacSign.ItemsSource = db.ZodiacSigns.ToList();
            ZodiacSign.DisplayMemberPath = "Title";
            ZodiacSign.SelectedValuePath = "ZodiacSignId";
        }

        private void RegistrButton_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = passwordBox.Password.Trim();
            string surname = Surname.Text.Trim();
            string name = Name.Text.Trim();
            string patronymic = Patronymic.Text.Trim();
            string birthday = Birthday.Text.Trim();
            string gender = Gender.Text.Trim();

            int height;
            if (!int.TryParse(Height.Text.Trim(), out height))
            {
                MessageBox.Show("Некорректный ввод для роста.", "Ошибка");
                Height.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
                return;
            }

            int weight;
            if (!int.TryParse(Weight.Text.Trim(), out weight))
            {
                MessageBox.Show("Некорректный ввод для веса.", "Ошибка");
                Weight.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
                return;
            }

            // Load Education, Status, and ZodiacSign if you need to populate them 
            // from the database (likely already done in your constructor)

            bool children = Children.IsChecked ?? false;
            bool flag = false;

            if (IsValidRegistrationData())
            {
                Client client = new Client
                {
                    Login = login,
                    Pwd = password,
                    Surname = surname,
                    Name = name,
                    Patronymic = patronymic,
                    Birthday = DateOnly.Parse(birthday),
                    Gender = gender,
                    Height = height,
                    Weight = weight,
                    StatusId = _client.StatusId,
                    EducationId = _client.EducationId,
                    ZodiacSignId = _client.ZodiacSignId,
                    Children = children,
                    Flag = flag
                };

                db.Clients.Add(client);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Регистрация прошла успешно!");

                    Authorization authorization = new Authorization();
                    authorization.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
                }
            }
        }


        private bool IsValidRegistrationData()
        {
            if (!IsValidLogin(textBoxLogin.Text))
            {
                MessageBox.Show("Почта введена некорректно!\nПочта должна быть в формате example@domain.com.", "Ошибка");
                textBoxLogin.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
                return false;
            }

            if (IsEmailInUseInDatabase(textBoxLogin.Text)) 
            {
                MessageBox.Show("Этот адрес электронной почты уже используется.", "Ошибка");
                textBoxLogin.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
                return false;
            }

            if (!IsValidPassword(passwordBox.Password))
            {
                MessageBox.Show("Пароль не соответствует требованиям.", "Ошибка");
                passwordBox.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
                return false;
            }

            if (string.IsNullOrEmpty(Surname.Text))
            {
                MessageBox.Show("Пожалуйста, введите фамилию.", "Ошибка");
                Surname.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
                return false;
            }

            if (string.IsNullOrEmpty(Name.Text))
            {
                MessageBox.Show("Пожалуйста, введите имя.", "Ошибка");
                Name.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
                return false;
            }
            return true;
        }

        private bool IsValidLogin(string login)
        {
            return Regex.IsMatch(login, @"^[^@]+@[^@]+\.[^@]+$");
        }

        private bool IsValidPassword(string password)
        {
            return password.Length >= 6 &&
                       Regex.IsMatch(password, @"^[a-zA-Z0-9!@#$%^]+$") &&
                       password.Any(char.IsUpper) &&
                       password.Any(char.IsDigit) &&
                       password.Any("!@#$%^".Contains);
        }

        private bool IsEmailInUseInDatabase(string login)
        {
            return db.Clients.Any(c => c.Login == login);
        }

        //Переход на окно с регистрацией
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }

        //Переход на окно с авторизацией
        private void SignButton_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }

        private void ExitButtonAdmin_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardHelper.HandleKeyDown(sender, e);
        }

        private void ChoosePhotoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Children_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void Birthday_TextChanged(object sender, TextChangedEventArgs e)
        {
            string birthday = Birthday.Text.Trim();

            // Убираем нечисловые символы
            birthday = Regex.Replace(birthday, @"[^0-9]", "");

            // Проверяем, что длина текста не превышает 8 символов (дд.мм.гггг)
            if (birthday.Length > 0 && birthday.Length < 8)
            {
                // Добавляем точки-разделители
                if (birthday.Length >= 2 && !birthday.Contains("."))
                {
                    birthday = birthday.Insert(2, ".");
                }
                if (birthday.Length >= 5 && !birthday.Contains("."))
                {
                    birthday = birthday.Insert(5, ".");
                }
            }

            // Проверяем, является ли введенная дата корректной
            if (birthday.Length == 10) // Проверка на полную длину даты
            {
                DateTime date;
                if (DateTime.TryParseExact(birthday, "dd.MM.yyyy", null, DateTimeStyles.None, out date))
                {
                    // Дата корректна
                    Birthday.Text = birthday; // Обновляем текст в TextBox
                    Birthday.SelectionStart = Birthday.Text.Length; // Устанавливаем курсор в конец
                    Birthday.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255)); // Возвращаем обычный фон
                }
                else
                {
                    // Дата некорректна
                    MessageBox.Show("Неверный формат даты. Введите дату в формате дд.мм.гггг.", "Ошибка");
                    Birthday.Text = ""; // Сбрасываем текст в TextBox
                    Birthday.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
                }
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }
    }
}
