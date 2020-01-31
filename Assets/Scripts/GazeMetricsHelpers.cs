using System;
using System.Collections.Generic;
using UnityEngine;

namespace GazeMetrics
{
    public class Helpers
    {                   
        public static string StringFromVector(Vector3 vector){
            return vector.ToString("F6").Trim(new Char[] {'(', ')'});
        }
    }
}