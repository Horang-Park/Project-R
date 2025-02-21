using Horang.HorangUnityLibrary.ComponentValueProviders.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main.Settings.Sounds
{
    public abstract class BaseSoundController : MonoBehaviour
    {
        private Slider _slider;
        private TMP_Text _percentage;
        private Toggle _onOff;

        private void Awake()
        {
            _slider = GetComponentInChildren<Slider>();
            _percentage = GetComponentInChildren<TMP_Text>();
            _onOff = GetComponentInChildren<Toggle>();
        }

        protected virtual void Start()
        {
            _slider.onValueChanged.AddListener(SetVolume);
            _slider.Subscribe(ShowPercentage);
            _onOff.onValueChanged.AddListener(OnSound);
        }

        private void OnDestroy()
        {
            _slider.Unsubscribe(ShowPercentage);
        }

        protected abstract void OnSound(bool isOn);
        protected abstract void SetVolume(float volume);

        private void ShowPercentage(float value)
        {
            _percentage.text = $"{(int)(value * 100.0f)}%";
        }
    }
}