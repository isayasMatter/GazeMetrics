using System;
using System.Collections.Generic;
using UnityEngine;

namespace GazeMetrics
{

    [CreateAssetMenu(fileName = "Square GazeMetrics Targets", menuName = "GazeMetrics/SquareGazeMetricsTargets", order = 3)]
    public class SquareCalibrationTargets : GazeMetricsTargets
    {
        [System.Serializable]
        public struct Square
        {
            public Vector3 center;
            public float width;
        }

        public List<Square> squares = new List<Square>();

        private List<Vector3> sqs = new List<Vector3>();
        
        [Tooltip("Points per square. This should be a perfect square.")] public int points = 9;

        int pointIdx;
        int squareIdx;

        public SquareCalibrationTargets(){
           
            sqs.Add(new Vector3(-0.3f,0.3f,1f));
            sqs.Add(new Vector3(-0.3f,0f,1f));
            sqs.Add(new Vector3(-0.3f,-0.3f,1f));
            sqs.Add(new Vector3(0f,0.3f,1f));
            sqs.Add(new Vector3(0,0,1f));
            sqs.Add(new Vector3(0f,-0.3f,1f));
            sqs.Add(new Vector3(0.3f,0.3f,1f));
            sqs.Add(new Vector3(0.3f,0f,1f));
            sqs.Add(new Vector3(0.3f,-0.3f,1));
        }

        public override int GetTargetCount()
        {
            return points * squares.Count;
        }

        public override Vector3 GetLocalTargetPosAt(int idx)
        {
            pointIdx = (int)Mathf.Floor((float)idx / (float)squares.Count);
            squareIdx = idx % squares.Count;

            return UpdateCalibrationPoint();
        }

        private Vector3 UpdateCalibrationPoint()
        {
            Square square = squares[squareIdx];
            Vector3 position = new Vector3(square.center.x, square.center.y, square.center.z);

            if (pointIdx >= 0 && pointIdx < points)
            {
                // int dimension = (int)Mathf.Sqrt(points);
                // float edges = square.width/2.0f;
                // float increment = square.width/dimension;
                
                // position.x += 0;
                // position.y += 0;
                position = sqs[pointIdx];
            }

            return position;
        }
    }
}