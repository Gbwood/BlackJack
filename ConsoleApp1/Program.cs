using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        Deck game_decl;
        Player user;
        Player dealer;
        static void Main(string[] args)
        {
            Console.WriteLine("        Welcome to Blackjack        ");
            //these three will have to get moved down probably

            bool game2 = true;
            Deck game_deck = new Deck();
            Player user = new Player(false);
            Player dealer = new Player(true);
            

            while (game2 == true)
            {
                bool game3 = true;
                user.EmptyHand();
                dealer.EmptyHand();
                Console.WriteLine("============= New Game =============");
                Console.WriteLine("You have : " + user.MoneyValue);
                Console.Write("How much do you bet : ");


                //bet needs to be a double
                //check value > 1
                double bet = Convert.ToDouble(Console.ReadLine());
                while (bet <= 0)
                {
                    Console.Write("How much do you bet : ");
                    bet = Convert.ToInt32(Console.ReadLine());
                }
                user.Bet(bet);
                dealer.Bet(bet);


                //pull first two cards
                String[,] pulled = game_deck.PullCard();
                String[,] pulled2 = game_deck.PullCard();
                user.AddtoHand(pulled);
                user.AddtoHand(pulled2);

               
                Console.WriteLine("your hand = " + pulled[0, 0] + " " + pulled2[0, 0] + " ,   Hand Value = " + user.GetHandValue());

                pulled = game_deck.PullCard();
                dealer.AddtoHand(pulled);
                pulled = game_deck.PullCard();
                dealer.AddtoHand(pulled);

                Console.WriteLine("Dealer's hand = " + pulled[0, 0] + " XX");

                //check for natural21
                bool Natural21 = false;
                if (user.GetHandValue() == 21)
                {
                    double num = (2.5 * user.GetBet);
                    Console.WriteLine("You got natural21; $" + num + " goes to you from the dealer");
                    user.TwentyOne();
                    dealer.DealerNatural21();
                    Natural21 = true;
                    user.Won++;
                    Console.WriteLine("You have won " + user.Won + " times, Lost " + user.Lost + " times, and tied " + user.Tied + "times");
                    Console.WriteLine("You have : $" + user.MoneyValue);
                }
                
                if (Natural21 == false)
                {
                    //Surrender?
                    Console.Write("Do you want to surrender (Y or N) ? : ");
                    //error checking needs added
                    String temp = Console.ReadLine();
                    while (temp != "y" && temp != "Y" && temp != "n" && temp != "N")
                    {
                        Console.Write("Do you want to surrender (Y or N) ? : ");
                        temp = Console.ReadLine();
                    }
                    if (temp == "y" || temp == "Y")
                    {
                        game3 = false;
                        user.surrender();
                        Console.WriteLine("You Surrender: $" + "goes to Dealer");
                        user.surrender();
                        dealer.DealerSurrender();
                    }
                    else
                    {
                        while (game3 == true)
                        {
                            Console.Write("Would you like to HIT or STAY (H or S) ? ");
                            temp = Console.ReadLine();
                            if (temp == "H" || temp == "h")
                            {
                                pulled = game_deck.PullCard();
                                user.AddtoHand(pulled);
                                Console.Write("Your hand is ");
                                for (int i = 0; i <= user.count; i++)
                                {
                                    Console.Write(user.getCard(i) + " ");
                                }
                                Console.WriteLine(" , Hand Value = " + user.GetHandValue());
                                if (user.GetHandValue() > 21)
                                {
                                    RoundOver(user, dealer, game_deck);
                                    game3 = false;
                                }
                            }
                            else if (temp == "S" || temp == "s")
                            {
                                game3 = false;
                                RoundOver(user, dealer, game_deck);
                            }
                        }
                    }



                    //would you like to play again?
                    Console.Write("More Game (Y or N) ? : ");
                    bool valid = false;
                    while (valid == false)
                    {
                        String text = Console.ReadLine();
                        if (text == "Y" || text == "y" || text == "N" || text == "n")
                        {
                            valid = true;
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.Write("More Game (Y or N) ? : ");
                        }

                    }
                }
                
                
            }

            
        }
        public static void RoundOver(Player user, Player dealer, Deck game_deck)
        {
            if (user.GetHandValue() > 21)
            {
                Console.WriteLine("You Bust");
                user.SubtractMoney();
                dealer.AddMoney();
                user.Lost++;
                
            }
            else
            {
                Console.WriteLine("Now, Dealers turn");
                Console.Write("Dealer hand is ");
                for (int i = 0; i <= dealer.count; i++)
                {
                    Console.Write(dealer.getCard(i) + " ");
                }
                Console.WriteLine(" , Hand Value : " + dealer.GetHandValue());
                while (dealer.GetHandValue() < 17)
                {
                    dealer.AddtoHand(game_deck.PullCard());
                    Console.Write("Dealer hand is ");
                    for (int i = 0; i <= dealer.count; i++)
                    {
                        Console.Write(dealer.getCard(i) + " ");
                    }
                    Console.WriteLine(" , Hand Value : " + dealer.GetHandValue());
                }
                if (dealer.GetHandValue() > 21)
                {
                    Console.WriteLine("Dealer Busts");
                    user.Won++;
                    user.AddMoney();
                    dealer.SubtractMoney();
                }
                else if (dealer.GetHandValue() == user.GetHandValue())
                {
                    Console.WriteLine("It's a standoff");
                    user.Tied++;
                }
                else if (dealer.GetHandValue() > user.GetHandValue())
                {
                    Console.WriteLine("Dealer won and Got $" + user.GetBet + " from user");
                    user.Lost++;
                    dealer.AddMoney();
                    user.SubtractMoney();
                }
                else
                {
                    Console.WriteLine("You won and got $" + user.GetBet + " from Dealer");
                    user.AddMoney();
                    dealer.SubtractMoney();
                    user.Won++;
                }
                
            }

            Console.WriteLine("You have won " + user.Won + " times, Lost " + user.Lost + " times, and tied " + user.Tied + " times");
            Console.WriteLine("You have : $" + user.MoneyValue);
        }
        //can't bet more than you have
        //infite loop when cards run out
    }
}
