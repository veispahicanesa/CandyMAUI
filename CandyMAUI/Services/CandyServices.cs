using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CandyMAUI.Models;

namespace CandyMAUI.Services
{
  public class CandyServices
    {
        private readonly static IEnumerable<Candy> _candies = new
        List<Candy>
        {
  new Candy {
            Name = "Crème Brûlée (Karamelizovani krem)",
            Image = "candy1.png",
             Price = 7
            },
            new Candy {
                 Name = "Macaron (Macaron kolačići)",
                 Image = "macarons.png",
                 Price = 4
            },
            new Candy{
              Name = "Madeleine (Madeleine tortice)",
              Image = "candy6.png",
              Price = 3.5
            },
new Candy {
    Name = "Éclair (Čokoladni ekler)",
    Image = "candi2.png",
    Price = 4.5
},
new Candy {
    Name = "Kouign-Amann (Kolač sa karamelizovanim maslacem)",
    Image = "candy13.png",
    Price = 5.5
},
new Candy {
    Name = "Paris-Brest (Pariz-Brest kolač)",
    Image = "candi1.png",
    Price = 6.5
},
new Candy{
    Name = "Clafoutis (Kolač s višnjama)",
    Image = "candy2.png",
    Price = 5
},
new Candy {
    Name = "Chouquette (Slatki chou kolačići)",
    Image = "candy7.png",
    Price = 3.5
},
new Candy {
    Name = "Croissant",
    Image = "candy9.png",
    Price = 6
},
new Candy {
    Name = "Tarte au Citron (Limunska pita)",
    Image = "candy8.png",
    Price = 5
         }
        };

        public IEnumerable<Candy> GetAllCandies() => _candies;
        public IEnumerable<Candy> GetPopularCandies(int count = 6) =>
            _candies.OrderBy(p=>Guid.NewGuid()).Take(count);

        public IEnumerable<Candy> SearchCandies(string searchTerm) =>
            string.IsNullOrWhiteSpace(searchTerm)
            ? _candies
            : _candies.Where(p => p.Name.Contains(searchTerm,
                StringComparison.OrdinalIgnoreCase));
    }
}
