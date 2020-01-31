using System;
using System.Collections.Generic;
using UnityEngine;

#if VIVE_SDK
using ViveSR.anipal.Eye;
#endif

namespace GazeMetrics
{
    
    public class SranipalProvider : GazeDataProvider {
        private EyeGazeData _localEyeGazeData = new EyeGazeData();
        public EyeGazeData LocalEyeGazeData {get {return _localEyeGazeData;}} 

        #if VIVE_SDK
        // private static EyeData _eyeData = new EyeData();
        private GameObject _sranipalGameObject;
            public bool Initialize(){  

                if (!SRanipal_Eye_API.IsViveProEye()) return false;
                InitializeSranipal();

                return (SRanipal_Eye_Framework.Status == SRanipal_Eye_Framework.FrameworkStatus.WORKING);                
            }

            private void InitializeSranipal()
    {
                if (_sranipalGameObject != null) return;
                _sranipalGameObject = new GameObject("EyeFramework");              
                var sranipal = _sranipalGameObject.AddComponent<SRanipal_Eye_Framework>();
                sranipal.StartFramework();
                if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING){
                    sranipal.StartFramework();
                }  
            }
            
            public void GetGazeData(){
            
                if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
                            SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT) return;
                
                _localEyeGazeData.Timestamp = Time.unscaledTime;
                _localEyeGazeData.isRayValid = SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out _localEyeGazeData.Origin, out _localEyeGazeData.Direction);
                // _localEyeGazeData.Distance = _eyeData.verbose_data.combined.convergence_distance_mm/1000;
                // _localEyeGazeData.isDistanceValid = _eyeData.verbose_data.combined.convergence_distance_validity;
                            
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