using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main.Test
{
    public class SignOut : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(() =>
            {
                FirebaseManager.Instance.SignOut();

                FullFadeManager.Instance.FadeOut(() =>
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                });
            });
        }
    }
}