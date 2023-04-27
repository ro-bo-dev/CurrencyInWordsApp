using CIWClientApp.ViewModel;
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
using System.Timers;
using System.ComponentModel;
using System.Diagnostics;

namespace CIWClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties and Constructor
        private readonly MainWindowViewModel MainViewModel = new MainWindowViewModel();

        private const int Delay = 500;
        private Timer DelayTimer = new Timer(Delay) { Enabled = false, AutoReset = false };

        public MainWindow()
        {
            InitializeComponent();

            DataContext = MainViewModel;

            MainViewModel.PropertyChanged += NumericalInputChanged;
        }
        #endregion

        #region Event Handling
        private void ConvertIntoWords_Click(object sender, RoutedEventArgs e)
        {
            CallConversionService();
        }

        private void CopyResult_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(MainViewModel.VerbalOutputString);
        }

        private void NumericalInputChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "NumericalInputString" && MainViewModel.ConvertOnTyping)
            {
                Debug.WriteLine("Entered ConvertOnTyping..");
                CallMethodDelayedWithReset(() => CallConversionService());
            }
        }
        #endregion

        #region Service Calls
        // Based on following source: https://stackoverflow.com/questions/35873235/c-sharp-wait-timeout-before-calling-method-and-reset-timer-on-consecutive-calls
        public void CallMethodDelayedWithReset(Action action)
        {
            if (!DelayTimer.Enabled)
            {
                DelayTimer = new Timer() { Interval = Delay, AutoReset = false };
                DelayTimer.Elapsed += (object sender, ElapsedEventArgs e) => action();
                DelayTimer.Start();
            }
            else
            {
                DelayTimer.Stop();
                DelayTimer.Start();
            }
        }

        private async void CallConversionService()
        {
            Debug.WriteLine("Calling service..");

            string numericalInput = MainViewModel.NumericalInputString.Trim().Replace(" ", "");

            if (string.IsNullOrEmpty(numericalInput))
            {
                MainViewModel.VerbalOutputString = "input format must be d[,d[d]]";
                return;
            }
            MainViewModel.VerbalOutputString = await RequestViaApi.RequestAsync(numericalInput);
        }
        #endregion
    }
}
