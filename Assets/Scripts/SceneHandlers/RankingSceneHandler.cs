using System.Collections.Generic;
using Horang.HorangUnityLibrary.Utilities;
using Managers;
using UI.Ranking;
using UnityEngine;

namespace SceneHandlers
{
    public class RankingSceneHandler : MonoBehaviour
    {
        private RankingUIManager _rankingUIManager;

        private void Awake()
        {
            _rankingUIManager = FindFirstObjectByType<RankingUIManager>();
        }

        private void Start()
        {
            FirebaseManager.Instance.GetAllData(new FirebaseManager.GetValueFirebaseCallback(
                onSuccess: OnSuccess
            ));
        }

        private void OnSuccess(object data)
        {
            if (data is not Dictionary<string, object> outer)
            {
                return;
            }

            List<KeyValuePair<string, int>> scores = new();

            foreach (var item in outer)
            {
                var innerData = item.Value as Dictionary<string, object>;

                var displayName = innerData!["DisplayName"] as string;
                var scoreString = innerData!["HighScore"] as string;

                if (string.IsNullOrEmpty(scoreString) || string.IsNullOrWhiteSpace(scoreString))
                {
                    Log.Print($"Invalid score data. -> name: {displayName} score: {scoreString}", LogPriority.Exception);

                    continue;
                }

                scores.Add(new KeyValuePair<string, int>(displayName, int.Parse(scoreString)));
            }

            _rankingUIManager.InjectData(scores);
        }
    }
}