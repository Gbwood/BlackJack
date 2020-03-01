using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_CodeQuality
{
    class Card
    {
        public char Suit { get; set; } 
        public int Rank { get; set; }
        public bool FaceUp { get; set; }

        public Card(int CardRank,char CardSuit)
        {
            Rank = CardRank;
            Suit = CardSuit;
            FaceUp = true;
        }

        



        public override string ToString()
        {
            if (FaceUp == false)
            {
                return "XX";
            }
            else
            {
                if (Rank == 11) { return "J" + Suit.ToString(); }
                if (Rank == 12) { return "Q" + Suit.ToString(); }
                if (Rank == 13) { return "K" + Suit.ToString(); }
                if (Rank == 14) { return "A" + Suit.ToString(); }
                return (Rank.ToString() + Suit.ToString());
            }
        }
    }
}
