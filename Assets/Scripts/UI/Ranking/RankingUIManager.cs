using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Ranking
{
    public class RankingUIManager : MonoBehaviour
    {
        private List<KeyValuePair<string, int>> _currentRankingData;

        public void PushRankingData(List<KeyValuePair<string, int>> data)
        {
            _currentRankingData = data.OrderBy(kv => kv.Value).ToList();
        }
    }
}