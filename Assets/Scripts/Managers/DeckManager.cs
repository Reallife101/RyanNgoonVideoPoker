using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    public class DeckManager : MonoBehaviour
    {
        [Header("Serialized Variables")]
        [SerializeField]
        private List<CardSO> cards = null;

        private int deckIndex = 0;

        public void InitializeDeck()
        {
            deckIndex = 0;
            ShuffleDeck();
        }

        // Draw a card from the Deck, shuffling the deck if none are left
        public CardSO drawCard()
        {
            if (deckIndex >= cards.Count)
            {
                ShuffleDeck();
                deckIndex = 0;
            }

            CardSO _retCard = cards[deckIndex];
            deckIndex++;

            return _retCard;
        }

        // Utilize Fisher-Yates shuffle Algorithm
        private void ShuffleDeck()
        {
            for (int _i = cards.Count - 1; _i > 0; _i--)
            {
                int _rnd = Random.Range(0, _i);
                CardSO value = cards[_i];
                cards[_i] = cards[_rnd];
                cards[_rnd] = value;
            }
        }
    }
}
