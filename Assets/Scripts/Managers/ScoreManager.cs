using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    public class ScoreManager : MonoBehaviour
    {
        [Header("Hand Settings")]
        [SerializeField]
        private int startBalance = 200;
        [SerializeField]
        private int BetCost = 5;

        private int currentBalance = 200;
        private List<ScoringItem> scoringItems = null;

        // Establish Getters and Setters
        public int CurrentBalance
        {
            get { return currentBalance; }
            set { currentBalance = value; }
        }

        public void InitializeScore()
        {
            currentBalance = startBalance;
            scoringItems = new List<ScoringItem>();
            InitializeScoringItems();
        }

        public void SubtractBet()
        {
            currentBalance -= startBalance;
        }

        void InitializeScoringItems()
        {
            scoringItems.Add(new ScoringItem("Royal Flush", IsRoyalFlush, 800));
            scoringItems.Add(new ScoringItem("Straight Flush", IsStraightFlush, 50));
            scoringItems.Add(new ScoringItem("Flush", IsFlush, 6));
            scoringItems.Add(new ScoringItem("Flush", IsStraight, 4));
            // Add more scoring items as needed
        }

        public void ScoreHand(List<Card> hand)
        {
            List<int> _values = new List<int>();
            List<Suit> _suits = new List<Suit>();

            foreach (Card card in hand)
            {
                _values.Add(card.GetValue());
                _suits.Add(card.GetSuit());
            }

            // Sort the values in ascending order
            _values.Sort();

            foreach (ScoringItem item in scoringItems)
            {
                if (item.condition(_values, _suits))
                {
                    Debug.Log("Scored " + item.points + " points for " + item.name);
                    break;
                }
            }
        }

        private bool IsRoyalFlush(List<int> values, List<Suit> suits)
        {

            List<int> _expectedValue = new List<int> { 1, 10, 11, 12, 13 };

            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] != _expectedValue[i])
                {
                    return false;
                }
            }

            return IsFlush(values,suits);
        }

        private bool IsStraightFlush(List<int> values, List<Suit> suits)
        {
            return IsFlush(values, suits) && IsStraight(values, suits);
        }

        private bool IsStraight(List<int> values, List<Suit> suits)
        {

            int _expectedValue = values[0] + 1;

            for (int i = 1; i < values.Count; i++)
            {
                if (values[i] != _expectedValue)
                {
                    return false;
                }
                _expectedValue++;
            }

            return true;
        }

        private bool IsFlush(List<int> values, List<Suit> suits)
        {
            for (int i = 1; i < suits.Count; i++)
            {
                if (suits[i] != suits[0])
                {
                    return false;
                }
            }

            return true;

        }


    }
}


