using Cupidon.View;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Principal;
using System.Security.Cryptography;
using System.Data;
using Cupidon.Helper;
using Cupidon.Models;
using System.Drawing;

namespace Cupidon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        AcquaintancesContext db;
        public Authorization()
        {
            InitializeComponent();
            db = new AcquaintancesContext(); ;
        }

        // Проверка авторизации пользователя
        public string CheckAuthorization(string username, string password)
        {
            if (username == "admin001" && password == "$NR5v#QR")
            {
                return "admin";
            }
            else
            {
                return "client";
            }
        }
        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLodin.Text.Trim();
            string password = passwordBox.Password.Trim();
            string role = CheckAuthorization(login, password);

            // Проверка логина и пароля в зависимости от роли
            if (login.Length < 5)
            {
                MessageBox.Show("Логин введен некорректно!\nЛогин должен содержать минимум 5 символов.", "Ошибка");
                textBoxLodin.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(224, 169, 175));
                return; // Прерываем выполнение, если логин некорректен
            }

            if (role == "admin")
            {
                // Проверка логина администратора
                if (!Regex.IsMatch(login, @"^[a-zA-Z0-9]+$"))
                {
                    MessageBox.Show("Логин введен некорректно!\nЛогин должен содержать только латинские буквы и цифры.", "Ошибка");
                    textBoxLodin.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(224, 169, 175));
                    return;
                }
            }
            else
            {
                // Проверка почты пользователя
                if (!Regex.IsMatch(login, @"^[^@]+@[^@]+\.[^@]+$"))
                {
                    MessageBox.Show("Почта введена некорректно!\nПочта должна быть в формате example@domain.com.", "Ошибка");
                    textBoxLodin.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(224, 169, 175));
                    return;
                }
            }

            //Проверка пароля на корректный ввод значений
            if (password.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать минимум 6 символов", "Ошибка");
                passwordBox.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(224, 169, 175));
            }
            else if (!Regex.IsMatch(password, @"^[a-zA-Z0-9!@#$%^]+$"))
            {
                MessageBox.Show("Пароль должен содержать только латинские буквы, цифры и символы: ! @ # $ % ^", "Ошибка");
                passwordBox.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(224, 169, 175));
            }
            else if (!password.Any(char.IsUpper))
            {
                MessageBox.Show("Пароль должен содержать минимум 1 прописную букву", "Ошибка");
                passwordBox.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(224, 169, 175));
            }
            else if (!password.Any(char.IsDigit))
            {
                MessageBox.Show("Пароль должен содержать минимум 1 цифру", "Ошибка");
                passwordBox.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(224, 169, 175));
            }
            else if (!password.Any("!@#$%^".Contains))
            {
                MessageBox.Show("Пароль должен содержать минимум 1 символ из набора: ! @ # $ % ^", "Ошибка");
                passwordBox.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(224, 169, 175));
            }

            else
            {
                textBoxLodin.ToolTip = "";
                textBoxLodin.Background = System.Windows.Media.Brushes.Transparent;
                passwordBox.ToolTip = "";
                passwordBox.Background = System.Windows.Media.Brushes.Transparent;

                if (role == "admin")
                {
                    MessageBox.Show("Добро пожаловать, администратор!");
                    // Перенаправление на окно администратора
                    ApplicantsAdmin adminWindow = new ApplicantsAdmin();
                    adminWindow.Show();
                    this.Close();
                }
                else if (role == "client")
                {
                    try
                    {
                        // Перенаправление на окно клиента
                        using (AcquaintancesContext context = new AcquaintancesContext())
                        {
                            var authClient = context.Clients.Where(u => u.Login == login && u.Pwd == password).FirstOrDefault();

                            if (authClient != null)
                            {
                                    App.currentClient = authClient;

                                MessageBox.Show("Добро пожаловать, " + authClient.Surname + " " + authClient.Name + " " + authClient.Patronymic + "!");
                                ApplicantsClient clientWindow = new ApplicantsClient();
                                    clientWindow.Show();
                                    this.Close();
                                }
                                //else
                                //{
                                //    MessageBox.Show("Клиент не найден.");
                                //}
                            //}
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Произошла ошибка: {ex.Message}");
                    }
                }
            }
        }
            //Переход на окно с регистрацией
            private void LoginButton_Click(object sender, RoutedEventArgs e)
            {
                RegistrationForm registration = new RegistrationForm();
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
                Application.Current.Shutdown();
            }
            private void Window_KeyDown(object sender, KeyEventArgs e)
            {
                KeyboardHelper.HandleKeyDown(sender, e);
            }
        }
    }