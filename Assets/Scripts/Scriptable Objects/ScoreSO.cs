using System.Collections.Generic;
using UnityEngine;


namespace VideoPoker
{
    public delegate void ScoreLambda(List<Card> value);

    [CreateAssetMenu(fileName = "New Score", menuName = "Cards/Score SO")]
    public class ScoreSO : ScriptableObject
    {
        public float payout;

        public ScoreLambda scoreLambda;

        public void InvokeLambda(List<Card> value)
        {
            scoreLambda?.Invoke(value);
        }

    }
}
