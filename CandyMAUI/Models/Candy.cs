using CommunityToolkit.Mvvm.ComponentModel;

namespace CandyMAUI.Models
{
    public partial class Candy : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Amount))]
        private int cartQuantity; // Ispravljen pravopis

        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }

        public double Amount => CartQuantity * Price; // Koristi automatski generiranu svojinu `CartQuantity`

        public Candy Clone() => (Candy)MemberwiseClone();

        // Opcioni konstruktor za jednostavnije inicijalizacije
        public Candy(string name, string image, double price)
        {
            Name = name;
            Image = image;
            Price = price;
        }

        // Prazan konstruktor za fleksibilnost
        public Candy() { }
    }
}
