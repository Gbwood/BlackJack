using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("        Welcome to Blackjack        ");
            bool game = true;


            while (game == true)
            {
                Console.WriteLine("============= New Game =============");
                Deck game_deck = new Deck();
                Player user = new Player(false);
                Player dealer = new Player(true);


                //loop here
                Console.WriteLine("You have : " + user.MoneyValue());
                Console.Write("How much do you bet : ");
                //error checking add here
                int bet = Convert.ToInt32(Console.ReadLine());
                user.Bet(bet);
                dealer.Bet(bet);


                    // loop here


            }
           
        }
    }
}
