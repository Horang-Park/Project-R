using Horang.HorangUnityLibrary.Utilities.PlayerPrefs;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main.Settings.Graphics
{
    public class UsePostProcessing : MonoBehaviour
    {
        private Toggle _toggle;

        public void Initialize()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(PostProcessing);
        }

        private void PostProcessing(bool isOn)
        {
            SetPlayerPrefs.Int("Use Post Processing", isOn ? 1 : 0);
        }
    }
}