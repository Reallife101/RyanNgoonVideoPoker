using UnityEngine;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	/// 
	/// The main game manager
	/// 
	public class GameManager : MonoBehaviour
	{
		[Header("Other Managers")]
		[SerializeField]
		private DeckManager deck = null;
		[SerializeField]
		private HandManager hand = null;
		[SerializeField]
		private ScoreManager score = null;

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Awake()
		{

		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Start()
		{
			deck.InitializeDeck();
			score.InitializeScore();
			score.SubtractBet();
			hand.BeginHand();
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Update()
		{
		}

		public CardSO DrawCard()
		{
			return deck.drawCard();
		}

		public void BetHand()
        {
			hand.BetHand();
        }

		public void EditScore(int delta)
        {
			score.CurrentBalance += delta;
        }
	}
}