using System.Collections.Generic;

namespace MeteoRitRichClient
{
    using System.Windows;
    using Common;

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
            /*
            var a =sc.CreateCityRequest();
            sc.measureEvent += ScOnCityEvent;
            sc.GetMeasurement<List<Sensor>>(a);
            */
            
            var request = sc.CreateRequest(IdTextBox.Text, TimestampTextBox.Text);
            sc.measureEvent += ScOnMeasureEvent;
            sc.GetMeasurement<Measurement>(request);
        }

        private void ScOnMeasureEvent(object sender)
        {
            this.measurement = (Measurement)sender;
            this.Dispatcher.Invoke(Method);
        }

        private void ScOnCityEvent(object sender)
        {
            var sensor = (List<Sensor>)sender;

        }

        private void  Method()
        {
            TempTextBox.Text = measurement.Temperature.ToString();
            HumidityTextBox.Text = measurement.Humidity.ToString();
            PressureTextBox.Text = measurement.Pressure.ToString();
            Timestamp2TextBox.Text = measurement.Timestamp.ToString();
        }
        
    }
}