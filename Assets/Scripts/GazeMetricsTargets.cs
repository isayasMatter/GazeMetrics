using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GazeMetrics
{
    public abstract class GazeMetricsTargets : ScriptableObject
    {
        public abstract int GetTargetCount();
        public abstract Vector3 GetLocalTargetPosAt(int idx); //unity camera space 
    }
}
