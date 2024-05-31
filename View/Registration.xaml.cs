using Cupidon.Helper;
using Cupidon.Models;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        AcquaintancesContext db;
        public Registration()
        {
            InitializeComponent();

            db = new AcquaintancesContext();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLodin.Text.Trim();
            string password = passwordBox.Password.Trim();

            // Проверка логина на корректный ввод значений
            if (!Regex.IsMatch(login, @"^[^@]+@[^@]+\.[^@]+$"))
            {
                MessageBox.Show("Почта введена некорректно!\nПочта должна быть в формате example@domain.com.", "Ошибка");
                textBoxLodin.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
                return; // Прерываем выполнение, если логин некорректен
            }

            // Проверка, используется ли адрес электронной почты
            if (IsEmailInUse(login))
            {
                MessageBox.Show("Этот адрес электронной почты уже используется.", "Ошибка");
                textBoxLodin.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
                return;
            }

            //Проверка пароля на корректный ввод значений
            if (password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать минимум 6 символов", "Ошибка");
                passwordBox.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
            }
            else if (!Regex.IsMatch(password, @"^[a-zA-Z0-9!@#$%^]+$"))
            {
                MessageBox.Show("Пароль должен содержать только латинские буквы, цифры и символы: ! @ # $ % ^", "Ошибка");
                passwordBox.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
            }
            else if (!password.Any(char.IsUpper))
            {
                MessageBox.Show("Пароль должен содержать минимум 1 прописную букву", "Ошибка");
                passwordBox.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
            }
            else if (!password.Any(char.IsDigit))
            {
                MessageBox.Show("Пароль должен содержать минимум 1 цифру", "Ошибка");
                passwordBox.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
            }
            else if (!password.Any("!@#$%^".Contains))
            {
                MessageBox.Show("Пароль должен содержать минимум 1 символ из набора: ! @ # $ % ^", "Ошибка");
                passwordBox.Background = new SolidColorBrush(Color.FromRgb(224, 169, 175));
            }

            else
            {
                textBoxLodin.ToolTip = "";
                textBoxLodin.Background = Brushes.Transparent;
                passwordBox.ToolTip = "";
                passwordBox.Background = Brushes.Transparent;

                MessageBox.Show("Всё хорошо!");
                Client client = new Client();

                db.Clients.Add(client);
                db.SaveChanges();

                // Переход к окну регистрации
                RegistrationForm registrationForm = new RegistrationForm();
                registrationForm.Show();
                this.Close();
            }
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

        // Функция для проверки, используется ли адрес электронной почты
        private bool IsEmailInUse(string email)
        {
            // Здесь реализовать логику проверки в базе данных
            // Например, можно сделать запрос к базе данных и проверить, существует ли уже пользователь с таким email
            // Возвращаем true, если email уже используется, иначе false
            return false;
        } 
        private void ExitButtonAdmin_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardHelper.HandleKeyDown(sender, e);
        }
    }
}