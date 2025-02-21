using UI.Main.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main
{
    public class SettingsButtonController : MonoBehaviour
    {
        [SerializeField] private SettingsUIManager settingsUIManager;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(settingsUIManager.Show);
        }
    }
}