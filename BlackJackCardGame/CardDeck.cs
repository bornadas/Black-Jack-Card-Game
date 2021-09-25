using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCardGame
{
    class CardDeck
    {
        private List<Card> cardDeck;
        public CardDeck()
        {
            cardDeck = new List<Card>(52);
            GetCardDeck();
            ShuffleDeck();
        }
        private void GetCardDeck()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 13; i++)
                    cardDeck.Add(new Card((Suit)j, (Rank)i));
            }
        }
        public Card DrawCard(Player person)
        {
            Card card;
            card = cardDeck[0];
            if (person.GetHandValue() + card.Value == 21 && person.Hand.Count == 1)
                // Check natural black jack immediately after receiving first 2 cards.            
                person.IsNaturalBlackJack = true;
            else if (person.GetHandValue() + card.Value > 21 && card.Rank == Rank.Ace)
                card.Value = 1;

            person.Hand.Add(card);
            cardDeck.Remove(card);
            return card;
        }
        private void ShuffleDeck()
        {
            Random rand = new Random();
            int c = cardDeck.Count;
            while (c > 1)
            {
                c--;
                int k = rand.Next(c + 1);
                Card card = cardDeck[k];
                cardDeck[k] = cardDeck[c];
                cardDeck[c] = card;
            }
        }
        public void ShowRemainingDeckCount()
        {
            Console.WriteLine("\nRemaining cards in the deck:" + GetRemainingDeckCount());
        }
        public int GetRemainingDeckCount()
        {
            return cardDeck.Count;
        }
    }
}
