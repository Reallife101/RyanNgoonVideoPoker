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


        // For every card in hand, draw a new card
        // if no hard rest, only replace non held cards
        public void DrawHand(bool hardReset = false)
        {
            foreach (Card card in hand)
            {
                if (!card.IsHeld || hardReset)
                {
                    CardSO so = gameManager.DrawCard();
                    card.SetCurrentCard(so);
                    card.InitializeCard();
                }
            }
        }

        // Given a CardSO, set a card at index and initialize it
        public void setCardSOAtIndex(int i, CardSO c)
        {
            hand[i].SetCurrentCard(c);
            hand[i].InitializeCard();
        }

        //-//////////////////////////////////////////////////////////////////////
        /// Establish Getters and Setters
        /// -//////////////////////////////////////////////////////////////////////
        public List<Card> Hand
        {
            get { return hand; }
        }
    }
}

