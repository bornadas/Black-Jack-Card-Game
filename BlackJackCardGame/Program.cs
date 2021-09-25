using System;

namespace BlackJackCardGame
{
    class Program
    {
        //How to beat the dealer?

        //        By drawing a hand value that is higher than the dealer’s hand value
        //        By the dealer drawing a hand value that goes over 21.
        //        By drawing a hand value of 21 (its called natural blackjack) on your first two cards, when the dealer does not.

        //How to lose to the dealer?

        //       Your hand value exceeds 21.
        //       The dealers hand has a greater value than yours at the end of the round.

        //How to find a hand’s Total Value?

        //       2 through 10 count at rank value, i.e.a 2 counts as two, a 9 counts as nine.
        //       Rank cards (J, Q, K) count as 10.
        //       Ace can count as a 1 or an 11 depending on which value helps the hand the most.

        //Rules of the game

        //       Blackjack is played with a conventional deck of 52 playing cards and suits don’t matter.
        //       2 cards are dealt initially.(2 cards each for players and 2 cards for the dealer)
        //       On the 1st round players shows both the cards while dealer shows 1 card and hold the another till the players declare STAND.
        //       If the players require cards after the 1st round they say HIT and they are dealt 1 card in each round after the 1st round.

        static void Main(string[] args)
        {
            Console.Write("--------------BlackJack Game---------------- ");
            PlayGame game = new PlayGame();
            game.play();
            Console.ReadKey();
        }
    }
}
