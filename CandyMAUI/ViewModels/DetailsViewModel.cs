using CommunityToolkit.Maui.Alerts;
using System;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CandyMAUI.ViewModels
{
    [QueryProperty(nameof(Candy), nameof(Candy))]
    public partial class DetailsViewModel : ObservableObject, IDisposable
    {
        private readonly CartViewModel _cartViewModel;

        // Inicijalizacija ViewModel-a sa zavisnošću od CartViewModel-a
        public DetailsViewModel(CartViewModel cartViewModel)
        {
            _cartViewModel = cartViewModel;
            _cartViewModel.CartItemRemoved += OnCartItemRemoved;  // Pravilno povezivanje
            _cartViewModel.CartCleared += OnCartCleared;
            _cartViewModel.CartItemUpdated += OnCartItemUpdated;
        }

        // Event handler kada se korpa očisti
        private void OnCartCleared(object? _, EventArgs e) => Candy.CartQuantity = 0;

        // Event handler za uklanjanje stavke iz korpe
        private void OnCartItemRemoved(object? _, Candy c) => OnCartItemChanged(c, 0);

        // Event handler za ažuriranje stavke u korpi
        private void OnCartItemUpdated(object? _, Candy c) => OnCartItemChanged(c, c.CartQuantity);

        // ObservableProperty za Candy objekat (automatski generisano)
        [ObservableProperty]
        private Candy _candy;

        // Komanda za dodavanje stavke u korpu
        [RelayCommand]
        private void AddToCart()
        {
            Candy.CartQuantity++;
            _cartViewModel.UpdateCartItemCommand.Execute(Candy);
        }

        // Komanda za uklanjanje stavke iz korpe
        [RelayCommand]
        private void RemoveFromCart()
        {
            if (Candy.CartQuantity > 0)
            {
                Candy.CartQuantity--;
                _cartViewModel.UpdateCartItemCommand.Execute(Candy);
            }
        }

        // Komanda za pregledanje korpe
        [RelayCommand]
        private async Task ViewCart()
        {
            Console.WriteLine("ViewCart is called.");
            if (Candy.CartQuantity > 0)
            {
                await Shell.Current.GoToAsync(nameof(CartPage), animate: true);
            }
            else
            {
                await Toast.Make("Please select the quantity using the plus (+) button.",
                    ToastDuration.Short).Show();
            }
        }

        // IDisposable metoda za otkazivanje događaja
        public void Dispose()
        {
            _cartViewModel.CartCleared -= OnCartCleared;
            _cartViewModel.CartItemUpdated -= OnCartItemUpdated;
        }

        // Metoda koja se koristi za promene u stavkama u korpi
        private void OnCartItemChanged(Candy candy, int quantity)
        {
            // Ako je količina 0, uklonite stavku iz korpe
            if (quantity == 0)
            {
                _cartViewModel.RemoveItemFromCart(candy);  // Ovdje pozivamo metodu RemoveItemFromCart
            }
            else
            {
                // Inače, samo ažurirajte količinu stavke
                candy.CartQuantity = quantity;
            }
        }
    }
}
