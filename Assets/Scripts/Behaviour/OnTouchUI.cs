using Horang.HorangUnityLibrary.Modules.AudioModule;
using Horang.HorangUnityLibrary.Utilities.CustomAttribute;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Behaviour
{
    [InspectorHideScriptField]
    public class OnTouchUI : MonoBehaviour
    {
        private EventSystem _eventSystem;

        private void Awake()
        {
            _eventSystem = GetComponent<EventSystem>();
        }

        private void Update()
        {
            if (_eventSystem.currentSelectedGameObject is null || !_eventSystem.currentSelectedGameObject)
            {
                return;
            }

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                AudioModule.Play("select");
            }
#else
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				AudioModule.Play("select");
			}
#endif
        }
    }
}