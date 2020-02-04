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
        private GameObject _tobiiXRGameObject;
            public bool Initialize(){
                // var settings = new TobiiXR_Settings();
                // settings.FieldOfUse = FieldOfUse.Analytical;
                
                // return TobiiXR.Start(settings);
                InitializeTobiiXR();
                return true;
            }

            private void InitializeTobiiXR(){
                
                //if (_tobiiXRGameObject != null) return;

                Debug.Log("Inside initialize tobiixr");
                var settings = new TobiiXR_Settings();
                settings.FieldOfUse = FieldOfUse.Interactive;
                
                _tobiiXRGameObject = new GameObject("Tobii XR Initializer");              
                var _tobiixr = _tobiiXRGameObject.AddComponent<TobiiXR_Initializer>();
                _tobiixr.Settings = settings;
                
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
                Debug.LogError("TobiXR could not be initialized.");
                return false;
            }
            public void GetGazeData() { }
            public void Destroy() { }
       #endif
    }
}