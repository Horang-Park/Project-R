using UnityEngine;
using UnityEngine.UI;

namespace UI.Main.Settings.Graphics
{
    public abstract class BaseGraphicController : MonoBehaviour
    {
        protected Toggle Toggle;

        private void Awake()
        {
            Toggle = GetComponentInChildren<Toggle>();
            Toggle.onValueChanged.AddListener(OnGraphicSetting);
        }

        public abstract void OnShowSettings();
        protected abstract void OnGraphicSetting(bool isOn);
    }
}