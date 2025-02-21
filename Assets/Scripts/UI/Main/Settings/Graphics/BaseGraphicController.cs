using UnityEngine;
using UnityEngine.UI;

namespace UI.Main.Settings.Graphics
{
    public abstract class BaseGraphicController : MonoBehaviour
    {
        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponentInChildren<Toggle>();
            _toggle.onValueChanged.AddListener(OnGraphicSetting);
        }

        protected abstract void OnGraphicSetting(bool isOn);
    }
}