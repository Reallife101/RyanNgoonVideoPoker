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


        public void DrawHand()
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

        public List<Card> Hand
        {
            get { return hand; }
        }

    }
}

