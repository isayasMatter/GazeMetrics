using System;
using System.Collections.Generic;
using UnityEngine;

#if TOBII_SDK
using Tobii.XR;
#endif

namespace GazeMetrics
{
    
    public class TobiiXRProvider : GazeDataProvider {
        private EyeGazeData _eyeGazeData = new EyeGazeData();
        public EyeGazeData LocalEyeGazeData {get {return _eyeGazeData;}} 
        #if TOBII_SDK
            public bool Initialize(){
                var settings = new TobiiXR_Settings();
                          
                return TobiiXR.Start(settings);
            }
         
            public void GetGazeData() { 
                // Get eye tracking data in local space
                var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.Local);

                _eyeGazeData.Timestamp = eyeTrackingData.Timestamp;
                _eyeGazeData.isRayValid = eyeTrackingData.GazeRay.IsValid;
                _eyeGazeData.Origin = eyeTrackingData.GazeRay.Origin;
                _eyeGazeData.Direction = eyeTrackingData.GazeRay.Direction;
                _eyeGazeData.Distance = eyeTrackingData.ConvergenceDistance;
                _eyeGazeData.isDistanceValid = eyeTrackingData.ConvergenceDistanceIsValid;
            }

            public void Destroy() { }
        #else
            public bool Initialize() 
            {
                Debug.LogError("HTC Vive SR Anipal SDK not detected.");
                return false;
            }
            public void GetGazeData() { }
            public void Destroy() { }
       #endif
    }
}