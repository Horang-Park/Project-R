using TMPro;
using UnityEngine;

namespace UI.Ranking
{
    public class RankingPiece : MonoBehaviour
    {
        private TMP_Text _rank;
        private TMP_Text _displayName;
        private TMP_Text _score;

        public void Initialize()
        {
            var components = GetComponentsInChildren<TMP_Text>();

            foreach (var component in components)
            {
                switch (component.gameObject.name)
                {
                    case "Rank":
                        _rank = component;
                        break;
                    case "Name":
                        _displayName = component;
                        break;
                    case "Score":
                        _score = component;
                        break;
                }
            }
        }

        public void InjectionData(int rank, string displayName, int score)
        {
            _rank.text = rank.ToString("000");
            _displayName.text = displayName;
            _score.text = score.ToString("#0");
        }
    }
}