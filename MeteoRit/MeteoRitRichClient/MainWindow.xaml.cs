using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeteoRitRichClient
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var sc = new ServiceCaller();
            var request = sc.CreateRequest("http://192.168.1.71:8080/MeteoRit/REST/measurement", IdTextBox.Text, TimestampTextBox.Text);
            var measurement = sc.GetMeasurement(request);
            TempTextBox.Text = measurement.Temperature.ToString();
            HumidityTextBox.Text = measurement.Humidity.ToString();
            PressureTextBox.Text = measurement.Pressure.ToString();
        }
    }
}
