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
		[SerializeField]
		private UIManager ui = null;

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
			DrawNewHand();
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		public void DrawNewHand()
        {
			score.SubtractBet();
			hand.DrawHand(true);
			ui.IsBetting = true;
		}

		public CardSO DrawCard()
		{
			return deck.drawCard();
		}

		public void BetHand()
        {
			hand.DrawHand();
			score.ScoreHand(hand.Hand);
			ui.IsBetting = false;
		}

		public void EditScore(int delta)
        {
			score.CurrentBalance += delta;
        }

		public void SetWinText(string s)
        {
			ui.SetWinText(s);
        }

		public void SetBalanceText(string s)
		{
			ui.SetBalanceText(s);
		}
	}
}