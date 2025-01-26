using Stores;
using TMPro;
using UniRx;
using UnityEngine;

namespace UI.Game.Fever
{
    public class FeverMultiplierController : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            OneCycleRecordStore.CurrentFeverMultiplier
                .Subscribe(MultiplierUpdater)
                .AddTo(this);
        }

        private void MultiplierUpdater(int multiplier)
        {
            _text.text = $"x{multiplier * 0.1f:F1}";
        }
    }
}
