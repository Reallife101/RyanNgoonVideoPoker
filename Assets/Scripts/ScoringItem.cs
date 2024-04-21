using System;
using System.Collections.Generic;

namespace VideoPoker
{
    [System.Serializable]
    public class ScoringItem
    {
        public string name; // Name of the scoring item
        public Func<List<int>, List<Suit>, Dictionary<int, int>, bool> condition; // Condition to check for scoring
        public int points; // Points awarded if condition is met

        public ScoringItem(string _name, Func<List<int>, List<Suit>, Dictionary<int, int>, bool> _condition, int _points)
        {
            name = _name;
            condition = _condition;
            points = _points;
        }

    }
}