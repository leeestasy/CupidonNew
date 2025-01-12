﻿using Cupidon.Helper;
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
    /// Логика взаимодействия для BlackListAdmin.xaml
    /// </summary>
    public partial class BlackListAdmin : Page
    {
        AcquaintancesContext db = new AcquaintancesContext();
        public BlackListAdmin()
        {
            InitializeComponent();

            //BlackListView.MouseDoubleClick += DataGrid_DoubleClick;
            // Фильтрация данных для отображения в BlackListView
            var profileClientData = db.Clients.Where(item => item.Flag == true).ToList();
            BlackListView.ItemsSource = profileClientData;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardHelper.HandleKeyDown(sender, e);
        }
        private void BlackListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
