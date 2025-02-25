using Horang.HorangUnityLibrary.Utilities.CustomAttribute;
using UnityEngine;

namespace SceneHandlers
{
    [InspectorHideScriptField]
    public class CommonSceneHandler : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus is false)
            {
                return;
            }

            PlayerPrefs.Save();
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.Save();
        }
    }
}