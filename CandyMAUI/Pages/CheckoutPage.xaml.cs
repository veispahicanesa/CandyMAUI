using CandyMAUI.Pages;

namespace CandyMAUI.Pages
{
    public partial class CheckoutPage : ContentPage
    {
        public CheckoutPage()
        {
            InitializeComponent();
        }

        private async void homeBtn_Clicked(object sender, EventArgs e)
        {
            // Navigacija na po?etnu stranicu (pretpostavlja da je "MainPage" definisana u AppShell.xaml)
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
