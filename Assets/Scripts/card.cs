using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
    public class Card : MonoBehaviour
    {
        [SerializeField]
        private CardSO currentCard = null;

        [SerializeField]
        private Image cardArt = null;

        [SerializeField]
        private GameObject holdText = null;

        // Initializes card from SO
        public void InitializeCard()
        {
            cardArt.sprite = currentCard.img;
            holdText.SetActive(false);

        }

        public void ToggleHoldText()
        {
            holdText.SetActive(!holdText.activeInHierarchy);

        }

        public void SetHoldText(bool b)
        {
            holdText.SetActive(b);

        }

        public int GetValue()
        {
            return currentCard.value;
        }
        public bool IsRoyal()
        {
            return currentCard.isRoyal;
        }
        public Suit GetSuit()
        {
            return currentCard.suit;
        }

        public void SetCurrentCard(CardSO cso)
        {
            currentCard = cso;
        }
    }
}


