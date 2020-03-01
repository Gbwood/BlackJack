using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_CodeQuality
{
    class BJPlayer
    {
        protected Card[] playerHand;
        protected StringBuilder handString;
        private int topCardIndex;
        private int numAces;
        private Deck deck;
        public int HandValue { get; set; }
        public decimal Money { get; set; }
        
       
        public BJPlayer(decimal startingMoney, Deck gameDeck) 
        {
            Money = startingMoney;
            playerHand = new Card[10];
            numAces = 0;
            HandValue = 0;
            handString = new StringBuilder();
            deck = gameDeck;
            topCardIndex = 0;
        }

        public void draw(string s, bool faceUp)
        {
            s = (topCardIndex + 1).ToString() + s;
            playerHand[topCardIndex] = deck.Draw(s);
            if (!faceUp) { playerHand[topCardIndex].FaceUp = false; }
            if (playerHand[topCardIndex].Rank == 14) { numAces++; }
            topCardIndex++;
        }

        public void ReturnCardsToDeck()
        {
            for (int i = 0; i < topCardIndex; i++)
            {
                deck.ReturnCard(playerHand[i]);
                playerHand[i] = null;
                numAces = 0;
            }
            topCardIndex = 0;
        }

        public string DisplayCards()
        {
            handString.Clear();
            int i = 0;
            while (playerHand[i] != null)
            {
                handString.Append(playerHand[i].ToString() + " ");
                i++;
            }
            CalculateHandValue();
            return handString.ToString();
       
        }
        private void CalculateHandValue()
        {
            HandValue = 0;
            int i = 0;
            while (playerHand[i] != null)
            {
                if (playerHand[i].Rank == 11 || playerHand[i].Rank == 12 || playerHand[i].Rank == 13) { HandValue += 10; }
                else if (playerHand[i].Rank == 14) { HandValue += 11; }
                else{ HandValue += playerHand[i].Rank; }
                i++;
            }
            for (i = 0; i < numAces; i++) {
                if (HandValue > 21) { HandValue -= 10; }
                else { break; }
            }
        }
       
    }

    class BJCustomer : BJPlayer
    {

        public int NumWins { get; set; }
        public int NumLosees { get; set; }
        public int NumTimes { get; set; }
        public BJCustomer(decimal startingMoney, Deck gameDeck) : base(startingMoney, gameDeck)
        {

        }
    }


    class BJDealer : BJPlayer
    {
        public BJDealer(decimal startingMoney, Deck gameDeck) : base(startingMoney, gameDeck)
        {

        }


        public void flipCard(int Index)
        {
            if (playerHand[Index].FaceUp == false) { playerHand[Index].FaceUp = true; }
            else { playerHand[Index].FaceUp = false; }
        }
    }

}
