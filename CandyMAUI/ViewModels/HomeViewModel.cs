using CandyMAUI.Services;
using CandyMAUI.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CandyMAUI.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly CandyServices _candyServices;

        public HomeViewModel(CandyServices candyServices)
        {
            _candyServices = candyServices;
            Candies = new(_candyServices.GetPopularCandies());
        }

        public ObservableCollection<Candy> Candies { get; set; }

        [RelayCommand]
        private async Task GoToAllCandiesPage(bool fromSearch = false)
        {
            var parameters = new Dictionary<string, object>
            {
                ["FromSearch"] = fromSearch
            };

            await Shell.Current.GoToAsync(nameof(AllCandiesPage), animate: true, parameters);
        }
        [RelayCommand]
        private async Task GoToDetailsPage(Candy candy)
        {
            var parameters = new Dictionary<string, object>
            {
                [nameof(DetailsViewModel.Candy)] = candy
            };

            await Shell.Current.GoToAsync(nameof(DetailPage), animate: true, parameters);
        }
    }
}