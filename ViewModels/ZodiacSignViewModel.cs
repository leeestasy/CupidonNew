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
    public class ZodiacSignViewModel : INotifyPropertyChanged
    {
        private int selectedZodiacSignID;

        public int SelectedZodiacSignID
        {
            get { return selectedZodiacSignID; }
            set
            {
                if (selectedZodiacSignID != value)
                {
                    selectedZodiacSignID = value;
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
