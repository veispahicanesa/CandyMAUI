using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CandyMAUI.ViewModels
{
    [QueryProperty(nameof(FromSearch),nameof(_fromSearch))]
    public partial class AllCandiesViewModel :ObservableObject
    {
        private readonly CandyServices _candyServices;
        public AllCandiesViewModel(CandyServices candyServices)
        {
            _candyServices = candyServices;
            Candies=new(_candyServices.GetAllCandies());
        }
        public ObservableCollection<Candy> Candies { get; set; }

        [ObservableProperty]
        private bool _fromSearch;


        private bool _searching;

        [RelayCommand]
        private async Task SearchCandies(string searchTerm)
        {
            Candies.Clear();
            _searching = true;

            await Task.Delay(1000);

            foreach (var candies in _candyServices.SearchCandies(searchTerm)) { 
            
            Candies.Add(candies);
            
            } 
            _searching = false;
        }

        [RelayCommand]
        private async Task GoToDetailsPage( Candy candy)
        {
            var parameters = new Dictionary<string, object>
            {
                [nameof(DetailsViewModel.Candy)] = candy
            };

            await Shell.Current.GoToAsync(nameof(DetailPage), animate: true, parameters);
        }
    }
}