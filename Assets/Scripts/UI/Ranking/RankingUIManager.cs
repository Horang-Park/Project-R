using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Ranking
{
    public class RankingUIManager : MonoBehaviour
    {
        [Header("Top Area")]
        [SerializeField] private Button back;
        [Header("Scroll Area")]
        [SerializeField] private GameObject rankingPiecePrefab;
        [SerializeField] private Transform instantiateTargetParent;

        private readonly List<RankingPiece> _pieces = new();

        private void Awake()
        {
            back.onClick.AddListener(() =>
                FullFadeManager.Instance.FadeOut(() =>
                {
                    SceneManager.LoadSceneAsync(1).ToUniTask();
                }));
        }

        private void Start()
        {
            FullFadeManager.Instance.FadeIn();
        }

        public void InjectData(List<KeyValuePair<string, int>> data)
        {
            var arrayData = data.ToArray();

            Array.Sort(arrayData, (pair, valuePair) => valuePair.Value.CompareTo(pair.Value));

            for (var instantiateTimes = _pieces.Count; instantiateTimes < arrayData.Length; instantiateTimes++)
            {
                var component = Instantiate(rankingPiecePrefab, instantiateTargetParent).GetComponent<RankingPiece>();

                component.Initialize();

                _pieces.Add(component);
            }

            for (var rank = 0; rank < arrayData.Length; rank++)
            {
                _pieces[rank].InjectionData(rank + 1, arrayData[rank].Key, arrayData[rank].Value);
            }
        }
    }
}