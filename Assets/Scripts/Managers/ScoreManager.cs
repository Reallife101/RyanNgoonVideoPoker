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

        // Establish Getters and Setters
        public int CurrentBalance
        {
            get { return currentBalance; }
            set { currentBalance = value; }
        }

        public void InitializeScore()
        {
            currentBalance = startBalance;
        }

        public void SubtractBet()
        {
            currentBalance -= startBalance;
        }
    }
}


