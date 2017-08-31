using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Deck
    {
        String[] hand = { "2S", "2H", "2D", "2C", "3S", "3H", "3D", "3C", "4S", "4H", "4D", "4C", "5S", "5H", "5D", "5C", "6S", "6H", "6D", "6C", "7S", "7H", "7D", "7C", "8S", "8H", "8D", "8C",
        "9S", "9H", "9D", "9C","10S", "10H", "10D", "10C","JS", "JH", "JD", "JC","QS", "QH", "QD", "QC","KS", "KH", "KD", "KC","AS", "AH", "AD", "AC"};
        int[] handValues = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };
        String[,] playerHand;
        public Deck()
        {
            int secondIndex = 0;
            int counter = 0;
            playerHand = new String[hand.Length, 3];
            for (int i = 0; i < hand.Length; i++)
            {
                if (counter == 4)
                {
                    secondIndex++;
                    counter = 0;
                }
                playerHand[i, 0] = hand[i];
                playerHand[i, 1] = Convert.ToString(handValues[secondIndex]);
                playerHand[i, 2] = "TRUE";
                counter++;
            }
         }

        public bool IsValid(int card)
        {
            if (playerHand[card, 2] == "FALSE")
            {
                return false;
            }
            else
            {
                return true;
            }

            
        }



        public String[,] PullCard()
        {
            Random r = new Random();
            bool valid = false;
            int card = 0;
            int[] cards = new int[52];

            while (valid == false) {
                card = r.Next(0,51);
                
                if (IsValid(card) == true)
                {
                    valid = true;
                }
                else
                {
                    //we need to check if we've pulled all the cards in the deck so we don't get caught in an infinite loop
                    //for (int i = 0; i < 52; i++)
                    //{
                      //  if ()
                //}
                }
            }
            String[,] Pulled = new String[1, 2];
            Pulled[0, 0] = playerHand[card, 0];
            Pulled[0, 1] = playerHand[card, 1];
            playerHand[card, 2] = "FALSE";
            return Pulled;
        }
    }
}
