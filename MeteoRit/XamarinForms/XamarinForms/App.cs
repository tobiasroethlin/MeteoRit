using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinForms
{
    using XamarinForms.Views;

    public class App
    {
        public static Page GetMainPage()
        {
            return new MeteoRitView();

//            return new ContentPage
//            {
//                Content = new Label
//                {
//                    Text = "Hello, Forms !",
//                    VerticalOptions = LayoutOptions.CenterAndExpand,
//                    HorizontalOptions = LayoutOptions.CenterAndExpand,
//                },
//            };
        }
    }
}
