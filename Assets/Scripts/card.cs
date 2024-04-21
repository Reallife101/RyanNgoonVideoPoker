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

        private bool isHeld = false;

        // Initializes card from SO
        public void InitializeCard()
        {
            cardArt.sprite = currentCard.img;
            holdText.SetActive(false);
            isHeld = false;

        }

        public void ToggleHeld()
        {
            // Reasoning: We should never hold a card and not display the text, since it is miscommunication to the player
            isHeld = !isHeld;
            holdText.SetActive(!holdText.activeInHierarchy);

        }

        public void SetHeld(bool b)
        {
            holdText.SetActive(b);
            isHeld = b;

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

        // Establish Getters
        public bool IsHeld
        {
            get { return isHeld; }
        }
    }
}


