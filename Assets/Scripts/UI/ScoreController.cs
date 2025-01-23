using Stores;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI
{
	public class ScoreController : MonoBehaviour
	{
		private TMP_Text _text;

		private void Awake()
		{
			_text = GetComponent<TMP_Text>();
		}

		private void Start()
		{
			OneCycleRecordStore.Score.Subscribe(ScoreUpdate);
		}

		private void ScoreUpdate(int score)
		{
			_text.text = score.ToString("# ##0");
		}
	}
}