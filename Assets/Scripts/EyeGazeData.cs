using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GazeMetrics
{
    public class EyeGazeData
    {
        public float Timestamp;
        public Vector3 Origin;
        public Vector3 Direction;
        public float Distance;  
        public bool isDistanceValid;
        public bool isRayValid;
        
    }
}
