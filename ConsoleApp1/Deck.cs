using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_CodeQuality
{
    class Deck
    {
        private Card[] deck;
        private int topCardIndex;

        private Random r = new Random();

        //• Card[topCardIndex+1]~Card[51]: point to null (this is a requirement)

        public Deck()
        {
            deck = new Card[52];
            Char[] suits = { 'S', 'H', 'D', 'C' };
            int j = 0;
                for (int i = 0; i < 13; i++)
                {
                    for (int s = 0; s < 4; s++)
                    {
                    deck[j] = new Card(i + 2, suits[s]);
                    j++;
                    }
                }
            topCardIndex = 51;
        }

        // http://rosettacode.org/wiki/Knuth_shuffle
        public void Shuffle()
        {
            for (int i = 0; i < 52; i++)
            {
                Card temp = deck[i];
                int SwapIndex = r.Next(51);
                deck[i] = deck[SwapIndex];
                deck[SwapIndex] = temp;
            }
        }


        public Card Draw(string CardNumberFollowedByPlayer)
        {
            Card pulledCard;
#if DEBUG
            //first index of CardNumberFollowedByPlayer contains the nth card that is being chose is to the player
            //the rest of the string represents who is calling the method (dealers or Customers)
            while (true)
            {
                Console.Write("Input " + CardNumberFollowedByPlayer[0] + " card for " + CardNumberFollowedByPlayer.Substring(1) + " (3H, AD, TC, etc. or XX to draw from deck) : ");
                string card = Console.ReadLine().ToUpper();
                if (card == "XX")
                {
                    pulledCard = deck[topCardIndex];
                    deck[topCardIndex] = null;
                    topCardIndex--;
                    break;
                }
                else
                {
                    for (int i = topCardIndex; i >=0; i--)
                    {
                        if (deck[i].ToString() == card)
                        {
                            pulledCard = deck[i];
                            deck[i] = deck[topCardIndex];
                            deck[topCardIndex] = null;
                            topCardIndex--;
                            return pulledCard;
                        }
                    }
                }
            }

#else
            pulledCard = deck[topCardIndex];
            deck[topCardIndex] = null;
            topCardIndex--;
#endif
            return pulledCard;
        }
        //returns cards back into the deck and adjust the index
        public void ReturnCard(Card card)
        {
            topCardIndex++;
            deck[topCardIndex] = card;
        }

    }
}
