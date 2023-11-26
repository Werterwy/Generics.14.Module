using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics._14.Module
{
    public class Player
    {
        public List<Karta> Koloda { get; set; }

        public Player()
        {
            Koloda = new List<Karta>();
        }

        public void PrintPlayerInfo()
        {
            Console.WriteLine($" имеет {Koloda.Count} карт:");

            foreach (var karta in Koloda)
            {
                Console.WriteLine($"{karta.Mast} {karta.Tip}");
            }
        }
         private int GetValue(string tip)
        {
            switch (tip)
            {
                case "6":
                    return 6;
                case "7":
                    return 7;
                case "8":
                    return 8;
                case "9":
                    return 9;
                case "10":
                    return 10;
                case "Валет":
                    return 11;
                case "Дама":
                    return 12;
                case "Король":
                    return 13;
                case "Туз":
                    return 14;
                default:
                    return 0;
            }
        }
        public int GetCardSum()
        {
            return Koloda.Sum(card => GetValue(card.Tip));
        }

        public int GetCardCount()
        {
            return Koloda.Count;
        }

    }

}
