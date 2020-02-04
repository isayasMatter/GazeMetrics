using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GazeMetrics
{
    [CreateAssetMenu(fileName = "GazeMetricsSettings", menuName = "GazeMetrics/GazeMetricsSettings", order = 1)]
    public class GazeMetricsSettings : ScriptableObject
    {

        [Header("Time and sample amount per target")]
        public float secondsPerTarget = 1f;
        public float ignoreInitialSeconds = 0.1f;
        public int samplingRate = 120;

        [Header("Environment and target settings")]
        public Color backgroundColor;
        public Color targetColor;
        public Color targetCenterColor;

        [Header("Results and Output")]
        public string experimentID;
        public string outputFolder;
        

    }
}