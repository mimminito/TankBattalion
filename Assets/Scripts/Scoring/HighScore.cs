using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTankBattalion.Scoring
{
    [System.Serializable]
    public class HighScore
    {
        /// <summary>
        /// The players score
        /// </summary>
        [SerializeField]
        public int Score;
        
        /// <summary>
        /// The players name
        /// </summary>
        [SerializeField]
        public string PlayerName;
    }
}