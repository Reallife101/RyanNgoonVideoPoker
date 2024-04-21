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

        public void ToggleHold()
        {
            holdText.SetActive(!holdText.activeInHierarchy);

        }
    }
}


