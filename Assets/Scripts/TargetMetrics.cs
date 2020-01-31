using UnityEngine;
namespace GazeMetrics{    
    public class TargetMetrics{
        public float targetId { get; set; }   
        public Vector3 localMarkerPosition { get; set; }
        public Vector3 worldMarkerPosition { get; set; }  
        public float AverageAccuracy { get; set; }
        public float SdPrecision { get; set; }
        public float RmsPrecision { get; set; }
        public int SampleCount { get; set; }
        public int ValidSamples { get; set; }
        public int ExcludedSamples { get; set; }         
    }
}