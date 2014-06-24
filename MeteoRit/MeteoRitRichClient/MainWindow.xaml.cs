using System;
using System.Windows;
using System.Windows.Threading;
using Common;
using XamarinCore;

namespace MeteoRitRichClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Measurement measurement;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            var sc = new ServiceCaller();
            var request = sc.CreateRequest(IdTextBox.Text, TimestampTextBox.Text);
            sc.measureEvent += ScOnMeasureEvent;
            sc.GetMeasurement(request);
        }

        private void ScOnMeasureEvent(object sender)
        {
            
            this.measurement = (Measurement)sender;
            this.Dispatcher.Invoke(Method);
        }

        private void  Method()
        {
            TempTextBox.Text = measurement.Temperature.ToString();
            HumidityTextBox.Text = measurement.Humidity.ToString();
            PressureTextBox.Text = measurement.Pressure.ToString();
        }
        
    }
}