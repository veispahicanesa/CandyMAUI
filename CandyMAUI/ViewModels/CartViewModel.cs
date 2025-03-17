using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace CandyMAUI.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        public event EventHandler<Candy> CartItemRemoved;
        public event EventHandler<Candy> CartItemUpdated;
        public event EventHandler CartCleared;
        public ObservableCollection<Candy> Items { get; set; } = new();

        [ObservableProperty]
        private double _totalAmount;

        private void RecalculateTotalAmount() => TotalAmount = Items.Sum(i => i.Amount);

        [RelayCommand]
        private void UpdateCartItem(Candy candy)
        {
            var item = Items.FirstOrDefault(i => i.Name == candy.Name);
            if (item is not null)
            {
                item.CartQuantity = candy.CartQuantity;
            }
            else
            {
                Items.Add(candy.Clone());
            }
            RecalculateTotalAmount();
        }

        [RelayCommand]
        private async Task RemoveCartItem(string name)  // Marked as async
        {
            var item = Items.FirstOrDefault(i => i.Name == name);
            if (item is not null)
            {
                Items.Remove(item);
                RecalculateTotalAmount();

                CartItemRemoved?.Invoke(this, item);

                var snackbarOptions = new SnackbarOptions
                {
                    CornerRadius = 10,
                    BackgroundColor = Colors.DarkKhaki
                };
                var snackbar = Snackbar.Make(
                    $"'{item.Name}' uklonjeno iz korpe",
                    () =>
                    {
                        // Povratak uklonjenog artikla
                        Items.Add(item);
                        RecalculateTotalAmount();
                        CartItemUpdated?.Invoke(this, item);
                    },
                    "Povrat",
                    TimeSpan.FromSeconds(5),
                    snackbarOptions
                );

                await snackbar.Show();  // Now it can be awaited
            }
        }

        [RelayCommand]
        private async Task ClearCart()
        {
            if (await Shell.Current.DisplayAlert("Da li ste sigurni?", "Da li želite da ispraznite korpu?", "DA", "NE"))
            {
                Items.Clear();
                RecalculateTotalAmount();
                CartCleared?.Invoke(this, EventArgs.Empty);
                await Toast.Make("Korpa je prazna", ToastDuration.Short).Show();
            }
        }

        [RelayCommand]
        private async Task PlaceOrder()  // Fixed typo in method name from 'PlaaceOrder' to 'PlaceOrder'
        {
            Items.Clear();
            CartCleared?.Invoke(this, EventArgs.Empty);
            RecalculateTotalAmount();
            await Shell.Current.GoToAsync(nameof(CheckoutPage),animate:true);
        }

        [RelayCommand]
        private async Task ViewCart()
        {
            // Navigacija ka stranici sa korpom
            await Shell.Current.GoToAsync(nameof(CheckoutPage), animate: true);  // Pretpostavljam da je stranica sa korpom "CheckoutPage"
        }


        public void RemoveItemFromCart(Candy candy)
        {
            var item = Items.FirstOrDefault(i => i.Name == candy.Name);
            if (item != null)
            {
                Items.Remove(item);  // Uklanja stavku iz kolekcije
                RecalculateTotalAmount();
                CartItemRemoved?.Invoke(this, item);  // Obavještava ostale delove aplikacije da je stavka uklonjena
            }
        }


    }
}
