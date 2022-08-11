using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class SaveData
    {
        public int MostKills { get; set; }
        public int TotalKills { get; set; }
        public int MostTimeSurvived { get; set; }
        public int TotalTimeSurived { get; set; }
    }
}