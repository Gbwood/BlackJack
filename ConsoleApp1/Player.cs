using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Player
    {
        private double money;
        private double bet;
        public Player(bool dealer)
        {
            if (dealer == false)
            {
                money = 100;
                bet = 0;
            }
            else
            {
                money = 250;
                bet = 0;
            }
        }


        public void Bet(int value)
        {
            bet = value;
        }

        public double MoneyValue()
        {
            return money;
        }

        public void AddMoney()
        {
            money += bet;
            bet = 0;
        }

        public void SubtractMoney()
        {
            money -= bet;
            bet = 0;

        }

        public void TwentyOne()
        {
            money += (bet * 2.5);
            bet = 0;
        }
    }
}
