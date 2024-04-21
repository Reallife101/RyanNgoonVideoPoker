using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
    public class card : MonoBehaviour
    {
        [SerializeField]
        private CardSO currentCard = null;

        [SerializeField]
        private Image cardArt = null;

        // Initializes card from SO
        public void InitializeCard()
        {
            cardArt.sprite = currentCard.img;
        }
    }
}


