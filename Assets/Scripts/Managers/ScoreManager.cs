using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    public class ScoreManager : MonoBehaviour
    {
        [Header("Hand Settings")]
        [SerializeField]
        private int startBalance = 40;
        [SerializeField]
        private int BetCost = 1;
        [SerializeField]
        private int ScoreMultiplier = 1;

        [Header("Serialized Variables")]
        [SerializeField]
        private GameManager gameManager = null;

        private int currentBalance = 200;
        private List<ScoringItem> scoringItems = null;

        // Establish Getters and Setters
        public int CurrentBalance
        {
            get { return currentBalance; }
            set {
                if (currentBalance != value)
                {
                    currentBalance = value;
                    OnBalanceChanged(); // Call the function when balance changes
                }
            }
        }

        public void InitializeScore()
        {
            currentBalance = startBalance;
            scoringItems = new List<ScoringItem>();
            InitializeScoringItems();
            DisplayBalance();

        }

        public void SubtractBet()
        {
            CurrentBalance -= BetCost*ScoreMultiplier;
        }

        private void OnBalanceChanged()
        {
            DisplayBalance();
        }

        private void DisplayBalance()
        {
            gameManager.SetBalanceText("Current Balance: " + currentBalance + " Credits");

        }

        void InitializeScoringItems()
        {
            scoringItems.Add(new ScoringItem("Royal Flush", IsRoyalFlush, 800));
            scoringItems.Add(new ScoringItem("Straight Flush", IsStraightFlush, 50));
            scoringItems.Add(new ScoringItem("Four of A Kind", IsFourOfKind, 25));
            scoringItems.Add(new ScoringItem("Full House", IsFullHouse, 9));
            scoringItems.Add(new ScoringItem("Flush", IsFlush, 6));
            scoringItems.Add(new ScoringItem("Straight", IsStraight, 4));
            scoringItems.Add(new ScoringItem("Three of A Kind", IsThreeOfKind, 3));
            scoringItems.Add(new ScoringItem("Two Pairs", IsTwoPair, 2));
            scoringItems.Add(new ScoringItem("Jacks or Better", IsJacksOrBetter, 1));
            // Add more scoring items as needed
        }

        public void ScoreHand(List<Card> hand)
        {
            List<int> _values = new List<int>();
            List<Suit> _suits = new List<Suit>();
            Dictionary<int, int> _valueCounts = new Dictionary<int, int>();


            foreach (Card card in hand)
            {
                int _currentVal = card.GetValue();
                _values.Add(card.GetValue());
                _suits.Add(card.GetSuit());

                // Add to frequency table
                if (_valueCounts.ContainsKey(_currentVal))
                {
                    _valueCounts[_currentVal]++;
                }
                else
                {
                    _valueCounts[_currentVal] = 1;
                }
            }

            // Sort the values in ascending order
            _values.Sort();

            string s = "No Score!";

            foreach (ScoringItem item in scoringItems)
            {
                if (item.condition(_values, _suits, _valueCounts))
                {
                    s = "Scored " + item.points + " points for " + item.name + "!";
                    CurrentBalance += item.points * ScoreMultiplier;
                    break;
                }
            }
            
            gameManager.SetWinText(s);
        }

        private bool IsRoyalFlush(List<int> values, List<Suit> suits, Dictionary<int, int> valueCounts)
        {

            List<int> _expectedValue = new List<int> { 1, 10, 11, 12, 13 };

            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] != _expectedValue[i])
                {
                    return false;
                }
            }

            return IsFlush(values,suits, valueCounts);
        }

        private bool IsStraightFlush(List<int> values, List<Suit> suits, Dictionary<int, int> valueCounts)
        {
            return IsFlush(values, suits, valueCounts) && IsStraight(values, suits, valueCounts);
        }

        private bool IsStraight(List<int> values, List<Suit> suits, Dictionary<int, int> valueCounts)
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

        private bool IsFlush(List<int> values, List<Suit> suits, Dictionary<int, int> valueCounts)
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

        private bool IsNOfAKind(int n, Dictionary<int, int> valueCounts)
        {
            foreach (var count in valueCounts.Values)
            {
                if (count == n)
                {
                    return true;
                }
            }

            return false;
        }
        
        private bool IsFourOfKind(List<int> values, List<Suit> suits, Dictionary<int, int> valueCounts)
        {
            return IsNOfAKind(4, valueCounts);

        }
        
        private bool IsThreeOfKind(List<int> values, List<Suit> suits, Dictionary<int, int> valueCounts)
        {
            return IsNOfAKind(3, valueCounts);

        }

        private bool IsPair(List<int> values, List<Suit> suits, Dictionary<int, int> valueCounts)
        {
            return IsNOfAKind(2, valueCounts);

        }

        private bool IsFullHouse(List<int> values, List<Suit> suits, Dictionary<int, int> valueCounts)
        {

            return IsPair(values, suits, valueCounts) && IsThreeOfKind(values, suits, valueCounts);

        }

        private bool IsTwoPair(List<int> values, List<Suit> suits, Dictionary<int, int> valueCounts)
        {
            int pairCount = 0;
            foreach (var count in valueCounts.Values)
            {
                if (count == 2)
                {
                    pairCount++;
                }
            }

            return pairCount>=2;

        }

        private bool IsJacksOrBetter(List<int> values, List<Suit> suits, Dictionary<int, int> valueCounts)
        {
            foreach (KeyValuePair<int, int> pair in valueCounts)
            {
                if (pair.Value == 2)
                {
                    if (pair.Key>=11 || pair.Key==1)
                    {
                        return true;
                    }
                }
            }

            return false;

        }


    }
}


