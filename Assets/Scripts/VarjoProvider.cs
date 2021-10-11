using System;
using System.Collections.Generic;
using UnityEngine;

#if VARJO_SDK
using Varjo.XR;
#endif

namespace GazeMetrics
{

    public class VarjoProvider : GazeDataProvider
    {
        private EyeGazeData _eyeGazeData = new EyeGazeData();
        public EyeGazeData LocalEyeGazeData { get { return _eyeGazeData; } }
        #if VARJO_SDK
        public bool Initialize()
        {
            InitializeVarjo();
            return true;
        }

        private void InitializeVarjo()
        {
            if (!VarjoEyeTracking.IsGazeCalibrated())
            {
                Debug.LogError("Varjo ET is not calibrated; no ET data will be available.");
            }
        }

        public void GetGazeData()
        {
            VarjoEyeTracking.GazeData eyeTrackingData = VarjoEyeTracking.GetGaze();

            _eyeGazeData.Timestamp = 1e-6f * eyeTrackingData.captureTime;
            _eyeGazeData.isRayValid = eyeTrackingData.status == VarjoEyeTracking.GazeStatus.Valid;
            _eyeGazeData.Origin = eyeTrackingData.gaze.origin;
            _eyeGazeData.Direction = eyeTrackingData.gaze.forward;
            float dist = eyeTrackingData.focusDistance;
            _eyeGazeData.Distance = dist;
            _eyeGazeData.isDistanceValid = 0.01f < dist && dist < 2f;
        }

        public void Destroy() { }
        #else
        public bool Initialize()
        {
            Debug.LogError("Varjo could not be initialized.");
            return false;
        }
        public void GetGazeData() { }
        public void Destroy() { }
        #endif
    }
}