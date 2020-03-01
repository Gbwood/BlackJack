using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_CodeQuality
{
    class BlackJack
    {

        private Deck deck;
        private BJDealer Dealer;
        private BJCustomer Customer;
        private decimal bet;
        private string response;
        // It also performs all the I/O operations (called a boundary object)
        public BlackJack()
        {
            deck = new Deck();

            Dealer = new BJDealer(250, deck);
            Customer = new BJCustomer(100, deck);
        }


        //Code acquired from professor Masaaki Mizuno
        public void Go()
        {
            bool bankrupt;
            while (true)
            {
                playOneGame();
                Customer.ReturnCardsToDeck();
                Dealer.ReturnCardsToDeck();
                displayStat(out bankrupt);
                // displayStat() sets "bankrupt" to true if either player bunkrupts
                if (bankrupt) break;
                if (!CheckMoreGame()) break;
                // checkMoreGmae() return true if the customer wants another game
                // false otherwise
            }
            
        }
        private void GetBetFromUser()
        {
            while (true)
            {
                Console.Write("How much do you bet : ");
                try { bet = Convert.ToDecimal(Console.ReadLine()); }
                catch { Console.WriteLine("Improper character input"); }
               
                if (bet > 0 && bet <= Customer.Money) { break; }
            }
            Console.WriteLine("You bet $" + bet);
        }

        //Code acquired from professor Masaaki Mizuno
        private void playOneGame()
        {
            bool bust;

            Console.WriteLine("============= New Game =============");
            Console.WriteLine("You have : " + Customer.Money);

            GetBetFromUser();

            deck.Shuffle();

            DealCards();

            if (TestForNatural21()) { return; }
            
            if (CheckSurrender()) { return; }

            CustomerTurn(out bust); { if (bust) return; }
            //if true, break
            DealerTurn(out bust); { if (bust) return; }
            //if true break
            DetermineWinner();
            //determines who had the higher score
        }
        private void CustomerTurn(out bool bust)
        {
            while (true)
            {
                //loop to ensure the proper letter is input
                while (true)
                {
                    Console.Write("Would you like to HIT or STAY (H or S) ? ");
                    response = Console.ReadLine();
                    if (response == "S" || response == "s") { bust = false; return; }
                    if (response == "H" || response == "h") { break; }
                }
                
                Customer.draw("Customer" , true);
                Console.WriteLine("Your hand is " + Customer.DisplayCards() + " , Hand Value = " + Customer.HandValue);
                
                if (Customer.HandValue > 21) { bust = true; Customer.NumLosees++; Customer.Money -= bet; Dealer.Money += bet; Console.WriteLine("You bust");  return; }
            }
        }

        private void DealerTurn(out bool bust)
        {
            while (true)
            {
                Console.WriteLine("Dealer hand is " + Dealer.DisplayCards() + " , Hand Value = " + Dealer.HandValue);
                if (Dealer.HandValue > 21) { bust = true; Customer.NumWins++; Customer.Money += bet; Dealer.Money -= bet; Console.WriteLine("Dealer Busts"); return; }
                if (Dealer.HandValue > 17) { bust = false;  return; }

                //dealer represents who is calling it during debugging 
                Dealer.draw("Dealer", true);
            }
        }

        private bool CheckSurrender()
        {
            while (true)
            {
                Console.Write("Do you want to surrender (Y or N) ? : ");
                response = Console.ReadLine();
                if (response == "n" || response == "N") { return false; }
                if (response == "y" || response == "Y")
                {
                    Console.WriteLine("You Surrender: $" + bet / 2 + " goes to Dealer");
                    Customer.Money += bet / 2;
                    Dealer.Money += bet / 2; 
                    return true;
                }
            }
        }

        private bool TestForNatural21()
        {
            if (Customer.HandValue == 21 && Dealer.HandValue == 21) { Customer.NumTimes++; Console.WriteLine("It's a standoff"); return true; }
            if (Customer.HandValue == 21) { Customer.NumWins++; Customer.Money += bet * Convert.ToDecimal(2.5); Dealer.Money -= bet * Convert.ToDecimal(2.5); Console.WriteLine("You got natural 21"); return true; }
            if (Dealer.HandValue == 21) { Customer.NumLosees++; Customer.Money -= bet * Convert.ToDecimal(2.5); Dealer.Money += bet * Convert.ToDecimal(2.5); Console.WriteLine("Dealer has Natural 21"); return true; }
            return false;
        }

        private void displayStat(out bool bankrupt)
        {
            
            if (Dealer.Money <= 0) { bankrupt = true; Console.WriteLine("You made the Dealer go bankrupt"); return;}
            if (Customer.Money <= 0) { bankrupt = true; Console.WriteLine("You're out of money"); return; }
            bankrupt = false;

            Console.WriteLine("You have won " + Customer.NumWins + " times, Lost " + Customer.NumLosees + " times, and tied " + Customer.NumTimes + " times");
            Console.WriteLine("You have : $" + Customer.Money);
        }

        private bool CheckMoreGame()
        {
            while (true)
            {
                Console.Write("More Game (Y or N) ? : ");
                response = Console.ReadLine();
                if (response == "Y" || response == "y") { return true;}
                if (response == "N" || response == "n") { return false;}
            }
        }

        private void DealCards()
        {
            response = "Customer";
            Customer.draw(response, true);
            Customer.draw(response, true);
            Console.WriteLine("Your hand is " + Customer.DisplayCards() + " , Hand Value = " + Customer.HandValue);

            response = "Dealer";
            Dealer.draw(response, false);
            Dealer.draw(response, true);
            Console.WriteLine("Dealer hand is " + Dealer.DisplayCards());
            Dealer.flipCard(0);
        }

        private void DetermineWinner()
        {
            if (Customer.HandValue > Dealer.HandValue)
            {
                Console.WriteLine("You won and got $" + bet + " from Dealer");
                Customer.NumWins++;
                Customer.Money += bet;
                Dealer.Money -= bet;
                return;
            }
            if (Dealer.HandValue > Customer.HandValue)
            {
                Console.WriteLine("Dealer won and Got $" + bet + " from user");
                Customer.NumLosees++;
                Customer.Money -= bet;
                Dealer.Money += bet;
                return;
            }
            Console.WriteLine("It's a standoff");
            Customer.NumTimes++;
        }
    }

}
