using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VideoPoker
{
    public class HandManager : MonoBehaviour
    {
        [Header("Hand Settings")]
        [SerializeField]
        private int betCost = 5;

        [Header("Serialized Variables")]
        [SerializeField]
        private List<ScoreSO> scoring = null;
        
        [SerializeField]
        private List<Card> hand = null;

        [SerializeField]
        private int currentBalance = 200;

        // Establish Getters and Setters
        public int CurrentBalance
        {
            get { return currentBalance; }
            set { currentBalance = value; }
        }

        // Starts the Round
        public void BeginHand()
        {
            SubtractBet();
            DrawHand();
        }

        private void SubtractBet()
        {
            currentBalance -= betCost;
        }

        private void DrawHand()
        {
            foreach (Card card in hand)
            {
                if (card.IsHeld)
                {
                    CardSO so = new CardSO();
                    card.SetCurrentCard(so);
                }
            }
        }
    }
}

