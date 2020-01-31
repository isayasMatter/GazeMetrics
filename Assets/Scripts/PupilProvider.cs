using System;
using System.Collections.Generic;
using UnityEngine;

#if PUPIL_SDK
using PupilLabs;
#endif

namespace GazeMetrics
{
    
    public class PupilProvider : GazeDataProvider {
        private EyeGazeData _localEyeGazeData = new EyeGazeData();        
        public EyeGazeData LocalEyeGazeData {get {return _localEyeGazeData;}}         
        #if PUPIL_SDK
        private GameObject _pupilGameObject;

        public bool Initialize(){
            InitilizePupilLabs();
            return true;
        }        

        private void InitilizePupilLabs(){
            if (_pupilGameObject != null) return;
            _pupilGameObject = new GameObject("PupilGazeController");

            var requestController = _pupilGameObject.AddComponent<RequestController>();
            var subscriptionsController = _pupilGameObject.AddComponent<SubscriptionsController>();
            subscriptionsController.requestCtrl = requestController;
            subscriptionsController.enabled = true;
            var gazeController = _pupilGameObject.AddComponent<GazeController>();
            gazeController.subscriptionsController = subscriptionsController;
            gazeController.enabled = true;

            Debug.Log("Created object: " + _pupilGameObject.name);
            //gazeController.OnReceive3dGaze += ReceiveGaze;
        }

        public void GetGazeData() { 
            
        }
        public void Destroy() { }
        #else
            public bool Initialize() 
            {
                // Debug.LogError("PupilLabs SDK not detected.");
                return false;
            }
            public void GetGazeData() { }
            public void Destroy() { }
       #endif
    }  
}