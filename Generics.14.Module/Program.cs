using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics._14.Module
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> masts = new List<string> { "Черви", "Бубны", "Крести", "Пики" };
            List<string> tips = new List<string> { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };

            List<Player> players = new List<Player>
            {
                new Player(),
                new Player()
            };

            Game<Karta> game = new Game<Karta>(masts, tips, players);

            // Вывод информации о картах каждого игрока 
            /*foreach (var player in players)
            {
                player.PrintPlayerInfo();
            }*/

            Console.WriteLine("Игра началась \n");
            // Начало игры
            game.Play();
        }
    }
}
