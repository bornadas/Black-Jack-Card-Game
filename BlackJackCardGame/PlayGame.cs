using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCardGame
{
    class PlayGame
    {
        private CardDeck cardDeck;
        public List<Player> players = new List<Player>();
        Player player = new Player();
        public bool continuePlaying = false;
        public void play()
        {
            bool continuePlaying = true;
            Console.WriteLine();
            Console.Write($"Enter name of the 1st player: ");
            var player1 = new Player(Console.ReadLine());
            Console.Write($"Enter name of the 2nd player: ");
            var player2 = new Player(Console.ReadLine());

            var dealerComputer = new Player();

            cardDeck = new CardDeck();

            while (continuePlaying)
            {
                player1.ResetPlayerHand();
                player2.ResetPlayerHand();
                dealerComputer.ResetPlayerHand();

                // Create a new deck if remaining cards are less than 20
                if (cardDeck.GetRemainingDeckCount() < 20)
                {
                    cardDeck = new CardDeck();
                    cardDeck.ShowRemainingDeckCount();
                }
                cardDeck.ShowRemainingDeckCount();

                // Deal first two cards to player
                cardDeck.DrawCard(player1);
                cardDeck.DrawCard(player1);

                // Show player's hand 
                player1.ShowUpCards();

                cardDeck.DrawCard(player2);
                cardDeck.DrawCard(player2);

                player2.ShowUpCards();

                // Deal first two cards to dealer
                cardDeck.DrawCard(dealerComputer);
                cardDeck.DrawCard(dealerComputer);

                //Show dealer's hand    
                dealerComputer.ShowUpCards(true);

                // Check natural black jack
                if (CheckNaturalBlackJack(player1, player2, dealerComputer) == false)
                {
                    TakeAction(player1);
                    TakeAction(player2);
                    TakeAction(dealerComputer, player1.IsBusted);

                    WinnerForThisRound(player1, player2, dealerComputer);
                }
                Console.WriteLine("---------------This Round Is Over---------------");
                string playAgain = "";
                do
                {
                    Console.Write("\nPlay again? Y or N? ");
                    playAgain = Console.ReadLine().ToUpper();
                    switch (playAgain)
                    {
                        case "Y":
                            continuePlaying = true;
                            break;
                        case "N":
                            continuePlaying = false;
                            break;
                        default:
                            Console.WriteLine("Please enter the correct command");
                            break;
                    }
                } while (!(playAgain == "Y") && !(playAgain == "N"));
            }
            DeclareEndingTheGame(player1, player2, dealerComputer);
        }
        private void TakeAction(Player currentPlayer, bool isPlayerBusted = false)
        {
            string choose = "";
            currentPlayer.Turn = true;

            Console.WriteLine($"\n{currentPlayer.Name}'s turn. ");

            while (currentPlayer.Turn)
            {
                if (currentPlayer.Name.Equals("Dealer"))
                {
                    if (currentPlayer.GetHandValue() <= 16)
                        choose = "H";
                    else if (isPlayerBusted)
                        choose = "S";
                    else
                        choose = "S";
                }
                else
                {
                    Console.Write("Hit (H) or Stand (S): ");
                    choose = Console.ReadLine();
                }
                switch (choose.ToUpper())
                {
                    case "H":
                        currentPlayer.Hit(cardDeck);
                        currentPlayer.ShowHandValue();
                        break;
                    case "S":
                        currentPlayer.Stand();
                        break;
                    default:
                        Console.WriteLine("Please enter the correct command");
                        break;
                }
                CheckingPlayerCards(currentPlayer);
            }
            Console.WriteLine($"{currentPlayer.Name}'s turn is over.");
        }
        private void CheckingPlayerCards(Player currentPlayer)
        {
            if (currentPlayer.GetHandValue() > 21)
            {
                Console.WriteLine("Bust!");
                currentPlayer.IsBusted = true;
                currentPlayer.ShowUpCards();
                currentPlayer.Turn = false;
            }
            else if (currentPlayer.Hand.Count >= 7)
            {
                Console.WriteLine($"{currentPlayer.Name} got 6 cards in hand.");
                currentPlayer.Turn = false;
            }
        }
        private bool CheckNaturalBlackJack(Player player1, Player player2, Player dealer)
        {

            if (dealer.IsNaturalBlackJack)
            {
                Console.WriteLine($"{dealer.Name} got natural BlackJack. {dealer.Name} won!");
                dealer.ShowUpCards();
                dealer.AddWin();
                return true;
            }
            else if (player1.IsNaturalBlackJack)
            {
                Console.WriteLine($"{player1.Name} got natural BlackJack. {player1.Name} won!");
                player1.ShowUpCards();
                player1.AddWin();
                dealer.ShowUpCards();
                return true;
            }
            else if (player2.IsNaturalBlackJack)
            {
                Console.WriteLine($"{player2.Name} got natural BlackJack. {player2.Name} won!");
                player2.ShowUpCards();
                player2.AddWin();
                dealer.ShowUpCards();
                return true;
            }
            return false;
        }
        private void WinnerForThisRound(Player player1, Player player2, Player dealer)
        {
            if (!dealer.IsBusted && !player1.IsBusted && !player2.IsBusted)
            {
                if (player1.GetHandValue() > dealer.GetHandValue() && player1.GetHandValue() > player2.GetHandValue())
                {
                    Console.WriteLine($"{player1.Name} won.");
                    player1.AddWin();
                }
                else if (dealer.GetHandValue() > player1.GetHandValue() && dealer.GetHandValue() > player2.GetHandValue())
                {
                    Console.WriteLine($"{dealer.Name} won.");
                    dealer.AddWin();
                }
                else if (player2.GetHandValue() > dealer.GetHandValue() && player2.GetHandValue() > player1.GetHandValue())
                {
                    Console.WriteLine($"{player2.Name} won.");
                    player2.AddWin();
                }
                else
                {
                    Console.WriteLine("Tie game.");
                }
            }
            else if (!dealer.IsBusted && !player1.IsBusted && player2.IsBusted)
            {
                if (player1.GetHandValue() > dealer.GetHandValue())
                {
                    Console.WriteLine($"{player1.Name} won.");
                    player1.AddWin();
                }
                else if (player1.GetHandValue() < dealer.GetHandValue())
                {
                    Console.WriteLine($"{dealer.Name} won.");
                    dealer.AddWin();
                }
                else
                {
                    Console.WriteLine("Tie Game");
                }
            }
            else if (!dealer.IsBusted && player1.IsBusted && !player2.IsBusted)
            {
                if (player2.GetHandValue() > dealer.GetHandValue())
                {
                    Console.WriteLine($"{player2.Name} won.");
                    player2.AddWin();
                }
                else if (player2.GetHandValue() < dealer.GetHandValue())
                {
                    Console.WriteLine($"{dealer.Name} won.");
                    dealer.AddWin();
                }
                else
                {
                    Console.WriteLine("Tie Game");
                }
            }
            else if (dealer.IsBusted && !player1.IsBusted && !player2.IsBusted)
            {
                if (player1.GetHandValue() > player2.GetHandValue())
                {
                    Console.WriteLine($"{player1.Name} won.");
                    player1.AddWin();
                }
                else if (player1.GetHandValue() < player2.GetHandValue())
                {
                    Console.WriteLine($"{player2.Name} won.");
                    player2.AddWin();
                }
                else
                {
                    Console.WriteLine("Tie Game");
                }
            }
            else if (dealer.IsBusted && player1.IsBusted && !player2.IsBusted)
            {
                Console.WriteLine($"{player2.Name} won.");
                player2.AddWin();
            }
            else if (dealer.IsBusted && !player1.IsBusted && player2.IsBusted)
            {
                Console.WriteLine($"{player1.Name} won.");
                player1.AddWin();
            }
            else if (!dealer.IsBusted && player1.IsBusted && player2.IsBusted)
            {
                Console.WriteLine($"{dealer.Name} won.");
                dealer.AddWin();
            }
        }
        private void DeclareEndingTheGame(Player player1, Player player2, Player dealerComputer)
        {
            Console.WriteLine($"{player1.Name} won {player1.TotalWins} times.");
            Console.WriteLine($"{player2.Name} won {player2.TotalWins} times.");
            Console.WriteLine($"{dealerComputer.Name} won {dealerComputer.TotalWins} times.");
            Console.WriteLine("----------------Game over. Thank you for playing Black Jack!!-------------------");
        }
    }
}
