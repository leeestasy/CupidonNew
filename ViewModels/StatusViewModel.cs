using Cupidon.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupidon.ViewModels
{
    public class StatusViewModel : INotifyPropertyChanged
    {
        private int selectedStatusID;

        public int SelectedStatusID
        {
            get { return selectedStatusID; }
            set
            {
                if (selectedStatusID != value)
                {
                    selectedStatusID = value;
                    OnPropertyChanged("SelectedStatusID");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    }
