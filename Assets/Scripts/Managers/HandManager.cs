using System.Collections.Generic;
using UnityEngine;


namespace VideoPoker
{
    public class HandManager : MonoBehaviour
    {
        [Header("Serialized Variables")]
        [SerializeField]
        private GameManager gameManager = null;
        
        [SerializeField]
        private List<Card> hand = null;

        // Starts the Round
        public void BeginHand()
        {
            DrawHand();
        }


        private void DrawHand()
        {
            foreach (Card card in hand)
            {
                if (!card.IsHeld)
                {
                    CardSO so = gameManager.DrawCard();
                    card.SetCurrentCard(so);
                    card.InitializeCard();
                }
            }
        }

        // Click Bet
        public void BetHand()
        {
            DrawHand();
            ScoreHand();
        }

        private void ScoreHand()
        {

        }
    }
}

