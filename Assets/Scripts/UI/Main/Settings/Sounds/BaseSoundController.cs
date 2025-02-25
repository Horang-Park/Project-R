using Horang.HorangUnityLibrary.Modules.AudioModule;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main.Settings.Sounds
{
    public abstract class BaseSoundController : MonoBehaviour
    {
        protected Slider Slider;
        protected Toggle OnOff;
        private TMP_Text _percentage;

        private void Awake()
        {
            Slider = GetComponentInChildren<Slider>();
            OnOff = GetComponentInChildren<Toggle>();
            _percentage = GetComponentInChildren<TMP_Text>();
        }

        protected virtual void Start()
        {
            Slider.onValueChanged.AddListener(SetVolume);
            Slider.onValueChanged.AddListener(ShowPercentage);
            OnOff.onValueChanged.AddListener(OnSound);

            Slider.OnBeginDragAsObservable().Subscribe(_ => OnBeginDrag()).AddTo(this);
            Slider.OnEndDragAsObservable().Subscribe(_ => OnEndDrag()).AddTo(this);
        }

        private void OnDestroy()
        {
            Slider.onValueChanged.RemoveListener(ShowPercentage);
        }

        public abstract void OnShowSettings();
        protected abstract void OnSound(bool isOn);
        protected abstract void SetVolume(float volume);

        private void ShowPercentage(float value)
        {
            _percentage.text = $"{(int)(value * 100.0f)}%";
        }

        private static void OnBeginDrag()
        {
            AudioModule.Play("slider");
        }

        private static void OnEndDrag()
        {
            AudioModule.Stop("slider");
        }
    }
}