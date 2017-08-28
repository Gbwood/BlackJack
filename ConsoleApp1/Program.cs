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
                Console.WriteLine("============= New Game =============");
                Console.WriteLine("You have : " + user.MoneyValue);
                Console.Write("How much do you bet : ");


                //error checking add here
                //check value > 1
                int bet = Convert.ToInt32(Console.ReadLine());
                user.Bet(bet);
                dealer.Bet(bet);


                //pull first two cards
                String[,] pulled = game_deck.PullCard();
                String[,] pulled2 = game_deck.PullCard();
                user.AddtoHand(pulled);
                user.AddtoHand(pulled2);

                //replace with get card?
                Console.WriteLine("your hand = " + pulled[0, 0] + " " + pulled2[0, 0] + " ,   Hand Value = " + user.GetHandValue());

                pulled = game_deck.PullCard();
                dealer.AddtoHand(pulled);
                pulled = game_deck.PullCard();
                dealer.AddtoHand(pulled);

                Console.WriteLine("Dealer's hand = " + pulled[0, 0] + " XX");


                Console.Write("Do you want to surrender (Y or N) ? : ");

                if (Console.ReadLine() == "y" || Console.ReadLine() == "Y")
                {
                    game3 = false;
                    user.surrender();
                    Console.WriteLine("You Surrender: $" +  "goes to Dealer");
                    RoundOver(user);
                    //.AddMoney();
                    //do something else
                }
                else
                {
                    while (game3 == true)
                    {

                        //if surrender game over and lose bet
                        //if continue ask to hit or stay

                        //hit or stay
                        //if stay game3 = false;


                        game3 = false;
                    }
                }
                
            }

            
        }
        public static void RoundOver(Player user)
        {
            Console.WriteLine("You have won " + user.Won + " times, Lost " + user.Lost + " times, and tied " + user.Tied + "times");
            Console.WriteLine("You have " + user.MoneyValue);
        }

    }
}
