using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics._14.Module
{
    public class Game<T> where T : Karta
    {
        private List<Player> players;
        private List<T> koloda;
        private int initialCardCount; 

        public Game(List<string> masts, List<string> tips, List<Player> players)
        {
            this.players = players;
            koloda = new List<T>();
            initialCardCount = masts.Count * tips.Count; 


           
            foreach (var mast in masts)
            {
                foreach (var tip in tips)
                {
                    T karta = (T)Activator.CreateInstance(typeof(T), mast, tip);
                    koloda.Add(karta);
                }
            }

            
            ShuffleDeck();

    
            if (players.Count > 0)
                DealCards();
        }
        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        private void ShuffleDeck()
        {
            Random random = new Random();
            koloda = koloda.OrderBy(x => random.Next()).ToList();
        }

        private void DealCards()
        {
            int playersCount = players.Count;
            int cardsPerPlayer = koloda.Count / playersCount;
            for (int i = 0; i < playersCount; i++)
            {
                List<T> playerCards = koloda.Skip(i * cardsPerPlayer).Take(cardsPerPlayer).ToList();

                foreach (var card in playerCards)
                {
                    players[i].Koloda.Add(card.ToKarta());
                }
            }
        }

        public void Play()
        {
            int count = 36 / players.Count;
            while (true)
            {
                if (count != 0)
                {
                    PlayRound();
                }
                count--;
                // Проверка на конец игры
                if (players.All(player => player.GetCardCount() == initialCardCount) || count == 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Игра завершена!");

                    // Определение победителя
                    Player winner = DetermineWinnerByCardCount();

                    Console.WriteLine($"Игрок {players.IndexOf(winner) + 1} выиграл!");
                    Console.WriteLine("");

                    // Вывод информации о картах каждого игрока   
                    foreach (var player in players)
                    {
                        Console.Write($"Игрок {players.IndexOf(player) + 1}");
                        player.PrintPlayerInfo();
                        Console.WriteLine("");
                    }

                    break;
                }
            }
        }

        private void PlayRound()
        {
            List<T> cardsInPlay = new List<T>();
            foreach (var player in players)
            {
                if (player.Koloda.Count > 0)
                {
                    T playedCard = (T)player.Koloda.First();
                    player.Koloda.RemoveAt(0);
                    cardsInPlay.Add(playedCard);

                    Console.WriteLine($"Игрок {players.IndexOf(player) + 1} кладет карту: {playedCard.Mast} {playedCard.Tip}");
                }
            }

            if (cardsInPlay.Count > 0)
            {
                T winningCard = DetermineWinningCard(cardsInPlay);
                Player winningPlayer = players[cardsInPlay.IndexOf(winningCard)];

                Console.WriteLine($"Выигрывает игрок {players.IndexOf(winningPlayer) + 1}");
                Console.WriteLine("");
                // Перемещение карт в колоду выигравшего игрока
                winningPlayer.Koloda.AddRange(cardsInPlay);
            }
        }

        private T DetermineWinningCard(List<T> cardsInPlay)
        {
            // Логика определения победной карты (может потребоваться кастомизация)
            return cardsInPlay.OrderByDescending(x => GetValue(x.Tip)).First();
        }

        private Player DetermineWinnerByCardCount()
        {
            // Определение победителя по количеству карт
            Player winner = players.OrderByDescending(player => player.GetCardCount()).First();

            // Если есть несколько игроков с одинаковым количеством карт, определение победителя по сумме
            if (players.Count(player => player.GetCardCount() == winner.GetCardCount()) > 1)
            {
                winner = players.OrderByDescending(player => player.GetCardSum()).First();
            }

            return winner;
        }
        private int GetValue(string tip)
        {
            // Упрощенная логика для определения значения карты
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
    }

}
