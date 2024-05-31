using Cupidon.Helper;
using Cupidon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
    /// Логика взаимодействия для EditRegistr.xaml
    /// </summary>
    public partial class EditRegistr : Window
    {
        AcquaintancesContext db;
        private Client _client;

        public EditRegistr(Client client) 
        {
            InitializeComponent();
            db = new AcquaintancesContext();
            _client = client;

            //LoadEducation();
            LoadStatus();
            //LoadZodiacSign();
            LoadData();

            Status.ItemsSource = db.Statuses.ToList();
            Status.SelectedItem = _client.Status;

            //Education.ItemsSource = db.Educations.ToList();
            //Education.SelectedItem = _client.Education;

            //ZodiacSign.ItemsSource = db.ZodiacSigns.ToList();
            //ZodiacSign.SelectedItem = _client.ZodiacSign;
        }

        private void RegistrButton_Click(object sender, RoutedEventArgs e)
        {
            Status newStatus = Status.SelectedItem as Status;

            if (newStatus != null)
            {
                _client.Status = newStatus;

                LoadData();

                db.Entry(_client).State = EntityState.Modified;
                //db.Clients.Update(_client);
                db.SaveChanges();

                this.Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите новый статус.");
            }
        }
        //private void LoadEducation()
        //{
        //    Education.ItemsSource = db.Educations.ToList();
        //    Education.DisplayMemberPath = "Title";
        //    Education.SelectedValuePath = "EducationId";
        //}
        private void LoadStatus()
        {
            Status.ItemsSource = db.Statuses.ToList();
            Status.DisplayMemberPath = "Title";
            Status.SelectedValuePath = "StatusId";
        }
        //private void LoadZodiacSign()
        //{
        //    ZodiacSign.ItemsSource = db.ZodiacSigns.ToList();
        //    ZodiacSign.DisplayMemberPath = "Title";
        //    ZodiacSign.SelectedValuePath = "ZodiacSignId";
        //}
        private void LoadData()
        {
            Surname.Text = _client.Surname;
            Name.Text = _client.Name;
            Patronymic.Text = _client.Patronymic;
            Birthday.Text = _client.Birthday.ToString();
            Gender.Text = _client.Gender;
            Height.Text = _client.Height.ToString();
            Weight.Text = _client.Weight.ToString();
            Country.Text = _client.Country;
            City.Text = _client.City;
            Description.Text = _client.Description;
            Children.IsChecked = _client.Children;

            var statuses = db.Statuses.FirstOrDefault(s => s.StatusId == _client.StatusId);
            if (statuses != null)
            {
                Status.Text = statuses.Title;
            }
            var education = db.Educations.FirstOrDefault(s => s.EducationId == _client.EducationId);
            if (education != null)
            {
                Education.Text = education.Title;
            }
            var zodiac = db.ZodiacSigns.FirstOrDefault(s => s.ZodiacSignId == _client.ZodiacSignId);
            if (zodiac != null)
            {
                ZodiacSign.Text = zodiac.Title;
            }

        }
        private void ChoosePhotoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Children_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardHelper.HandleKeyDown(sender, e);
        }
    }
}
