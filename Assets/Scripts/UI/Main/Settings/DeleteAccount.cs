using Managers;
using UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main.Settings
{
    public class DeleteAccount : MonoBehaviour
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
                CommonCanvasManager.Instance.ShowPopup(new PopupController.Data(
                    Context: "이 동작은 되돌릴 수 없습니다.<br>정말 삭제하시겠습니까?",
                    Title: "계정삭제",
                    RightButtonAction: () =>
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
                    }
                ));
            });
        }
    }
}