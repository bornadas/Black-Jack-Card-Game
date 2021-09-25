using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCardGame
{
    class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public bool IsNaturalBlackJack { get; set; }
        public bool IsBusted { get; set; } = false;
        public int TotalWins { get; set; } = 0;
        public static int TotalWinsCounter { get; private set; } = 0;
        public bool Turn { get; set; } = true;
        public List<Player> players = new List<Player>();
        public Player(string Name = "Dealer")
        {
            this.Name = Name;
            IsBusted = false;
            Hand = new List<Card>(6);
        }
        public int GetHandValue()
        {
            int value = 0;
            foreach (Card card in Hand)
            {
                value += card.Value;
            }
            return value;
        }
        public void ShowHandValue()
        {
            Console.WriteLine($"{this.Name}'s hand value is: {this.GetHandValue()} ({this.Hand.Count} cards)");
        }
        public void ShowUpCards(bool isDealer = false)
        {
            Console.WriteLine($"\n{this.Name}'s hand has:");
            if (isDealer)
            {
                Console.WriteLine($"{this.Hand[0].Sign}{this.Hand[0].RankName}");
                Console.WriteLine("<Hold Card>");
                Console.WriteLine($"{this.Name}'s Hand value is: {this.Hand[0].Value}");
            }
            else
            {
                foreach (var card in this.Hand)
                {
                    card.PrintCard();
                }
                ShowHandValue();
            }
        }
        public void AddWin()
        {
            this.TotalWins = this.TotalWins + 1;
        }
        public void Hit(CardDeck cardDeck)
        {
            Console.Write($"{this.Name} hits");
            Card card = cardDeck.DrawCard(this);
            card.PrintCard();
        }
        public void Stand()
        {
            Console.WriteLine($"{this.Name} stands");
            this.ShowUpCards();
            this.Turn = false;
        }
        public bool CanPlayerStand(bool isPlayerBusted)
        {
            if (!this.Name.Equals("Dealer"))
                return true;
            else if (isPlayerBusted)
                return true;

            return false;
        }
        public void ResetPlayerHand()
        {
            this.Hand = new List<Card>(6);
            this.IsNaturalBlackJack = false;
            this.IsBusted = false;
        }
    }
}
