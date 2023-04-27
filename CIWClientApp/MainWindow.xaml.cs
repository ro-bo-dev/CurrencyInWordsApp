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

namespace CIWClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ConvertIntoWords_Click(object sender, RoutedEventArgs e)
        {
            string numericalInput = NumericalInput.Text.Trim().Replace(" ", "");

            if (string.IsNullOrEmpty(numericalInput)) 
            {
                VerbalOutput.Text = "input format must be d[,d[d]]";
                return;
            }
            VerbalOutput.Text = await RequestViaApi.RequestAsync(numericalInput);
        }
    }
}
