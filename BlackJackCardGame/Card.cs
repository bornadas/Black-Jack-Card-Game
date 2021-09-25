using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCardGame
{
    public enum Suit
    {
        Diamonds, Clubs, Hearts, Spades
    }
    public enum Rank
    {
        Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
        Jack, Queen, King
    }
    class Card
    {
        public Suit Suit { get; }
        public Rank Rank { get; }
        public string RankName { get; }       
        public int Value { get; set; }
        public char Sign { get; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
            switch (Suit)
            {
                case Suit.Clubs:
                    Sign = '♣';
                    break;
                case Suit.Spades:
                    Sign = '♠';
                    break;
                case Suit.Diamonds:
                    Sign = '♦';
                    break;
                case Suit.Hearts:
                    Sign = '♥';
                    break;
            }
            switch (Rank)
            {
                case Rank.Ten:
                    Value = 10;
                    RankName = "10";
                    break;
                case Rank.Jack:
                    Value = 10;
                    RankName = "J";
                    break;
                case Rank.Queen:
                    Value = 10;
                    RankName = "Q";
                    break;
                case Rank.King:
                    Value = 10;
                    RankName = "K";
                    break;
                case Rank.Ace:
                    Value = 11;
                    RankName = "A";
                    break;
                default:
                    Value = (int)rank + 1;
                    RankName = Value.ToString();
                    break;
            }
        }
        public void PrintCard()
        {
            Console.WriteLine($"{this.Sign}{this.RankName}");
        }
    }
}
