using UnityEngine;
using UnityEngine.UI;

namespace VideoPoker
{
	//-//////////////////////////////////////////////////////////////////////
	///
	/// Manages UI including button events and updates to text fields
	/// 
	public class UIManager : MonoBehaviour
	{
		[Header("Other Managers")]
		[SerializeField]
		private GameManager gameManager = null;

		[Header("Serialized Fields")]
		[SerializeField]
		private Text currentBalanceText = null;

		[SerializeField]
		private Text winningText = null;

		[SerializeField]
		private Button betButton = null;

		[SerializeField]
		private Text betButtonText = null;

		private bool isBetting = true;

		private string CHOOSECARDSTOTOSS = "Choose Cards to hold or toss!";

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Awake()
		{
		}

		//-//////////////////////////////////////////////////////////////////////
		/// 
		void Start()
		{
			betButton.onClick.AddListener(OnBetButtonPressed);
			gameManager.SetWinText(CHOOSECARDSTOTOSS);
		}

		//-//////////////////////////////////////////////////////////////////////
		///
		/// Event that triggers when bet button is pressed
		/// 
		private void OnBetButtonPressed()
		{
			if (isBetting)
            {
				gameManager.BetHand();
				betButtonText.text ="Next";

			}
			else
            {
				gameManager.DrawNewHand();
				betButtonText.text = "Bet";
				gameManager.SetWinText(CHOOSECARDSTOTOSS);

			}
		}

		//-//////////////////////////////////////////////////////////////////////
		///
		/// Getters, Setters and Other Functions
		///

		public bool IsBetting
		{
			get { return isBetting; }
			set { isBetting = value; }
		}

		public void SetWinText(string s)
        {
			winningText.text = s;
        }

		public void SetBalanceText(string s)
		{
			currentBalanceText.text = s;
		}

		public void ToggleIsBetting()
        {
			isBetting = !isBetting;
        }

	}
}