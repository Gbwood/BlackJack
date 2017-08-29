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
        private String[,] hand;
        private int nextIndex;
        private int won;
        private int lost;
        private int tied;
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
            hand = new String[10, 2];
            nextIndex = 0;
            won = 0;
            lost = 0;
            tied = 0;
        }
        public double GetBet
        {
            get
            {
                return bet;
            }
        }
        public int count
        {
            get
            {
                return nextIndex;
            }
        }
         public int Won
        {
            get
            {
                return won;
            }
            set
            {
                won = value;
            }
        }

        public int Lost
        {
            get
            {
                return lost;
            }
            set
            {
                lost = value;
            }
        }

        public int Tied
        {
            get
            {
                return tied;
            }
            set
            {
                tied = value;
            }
        }

        public double MoneyValue
        {
            get
            {
                return money;
            }
        }



        public void Bet(double value)
        {
            bet = value;
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

        public void AddtoHand(String[,] cardpair)
        {
            hand[nextIndex, 0] = cardpair[0, 0];
            hand[nextIndex, 1] = cardpair[0, 1];
            nextIndex++;
        }

        public int GetHandValue()
        {
            int value = 0;
            int AceCounter = 0;

            for (int i = 0; i <= nextIndex - 1; i++)
            {
                if (hand[i,1] == "11")
                {
                    AceCounter++;
                    
                }
                value += Convert.ToInt32(hand[i, 1]);
            }

            while (AceCounter > 0)
            {
                if (value > 21)
                {
                    value -= 10;
                    AceCounter--;
                }
                else
                {
                    break;
                }
            }
            

            return value;
        }

        public String getCard(int index)
        {
            return hand[index,0];
        }

        public void surrender()
        {
            
            money -= (bet / 2);
            bet = 0;
           
        }


        public void DealerSurrender()
        {
            money += (bet / 2);
            bet = 0;
        }

        public void DealerNatural21()
        {
            money -= (bet * 2.5);
            bet = 0;
        }

        public void EmptyHand()
        {
            
            for (int i = 0; i < nextIndex; i++)
            {
                hand[i, 0] = null;
                hand[i, 1] = null;
            }
            nextIndex = 0;
        }
    }
}
