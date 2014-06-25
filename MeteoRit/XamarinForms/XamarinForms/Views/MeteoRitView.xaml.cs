using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinForms.Views
{
    using Xamarin.Forms;

    using XamarinCore.ViewModels;

    public partial class MeteoRitView : ContentPage
    {
        private MeteoRitViewModel meteoRitViewModel;

        public MeteoRitView()
        {
            InitializeComponent();
            meteoRitViewModel = new MeteoRitViewModel();
            this.BindingContext = meteoRitViewModel;
        }

        public void UpdateMeteoRitData(object sender, EventArgs e)
        {
            meteoRitViewModel.UpdateMeteoRitData();
        }
    }
}
