using UnityEngine;


namespace VideoPoker
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Cards/Card SO")]
    public class CardSO : ScriptableObject
    {
        public bool isRoyal;
        public Suit suit;
        public int value;
        public Sprite img; // Assuming you're using Sprite for the image

    }
}
