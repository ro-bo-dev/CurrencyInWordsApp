using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CIWClientApp.ViewModel
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Event Handling
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region Properties
        private string numericalInputString = "";
        public string NumericalInputString
        {
            get { return numericalInputString; }
            set
            {
                numericalInputString = value;
                OnPropertyChanged("NumericalInputString");
            }
        }

        private string verbalOutputString = "";
        public string VerbalOutputString
        {
            get { return verbalOutputString; }
            set
            {
                verbalOutputString = value;
                OnPropertyChanged("VerbalOutputString");
            }
        }

        public bool ConvertOnTyping { get; set; }
        #endregion
    }
}
