namespace XamarinCore.Test
{
    using NUnit.Framework;

    using XamarinCore.ViewModels;

    public class MeteoRitViewModelTest : BaseTest
    {
        [Test]
        public void GetMeteoRitData_VerifyServerReturnProperData()
        {
            var meteoRitViewModel = new MeteoRitViewModel();
            meteoRitViewModel.GetMeteoRitData();
        }
    }
}