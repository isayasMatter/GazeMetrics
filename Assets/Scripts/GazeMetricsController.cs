using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace GazeMetrics
{
    public enum ProvidersList{
        HTCViveSranipal,
        TobiiXR,
        PupiLabs

    }
    public class GazeMetricsController : MonoBehaviour
    {
        [Header("Eye Tracker Type")]
        
        public ProvidersList ProviderSDK;

        [Header("Scene References")]
        public new Camera camera;
        public Transform marker;

        [Header("Settings")]
        public GazeMetricsSettings settings;
        public GazeMetricsTargets targets;
        
        public bool showPreview;

        public bool IsCalibrating { get { return calibration.IsCalibrating; } }

        private const string SranipalProviderCompilerFlagString = "VIVE_SDK";
        private const string TobiiXRProviderCompilerFlagString = "TOBII_SDK";
        private const string PupilProviderCompilerFlagString = "PUPIL_SDK";
        private string _compilerFlagString;
        private EyeGazeData _gazeData;
        private GazeDataProvider _gazeProvider;

        //events
        public event Action OnCalibrationStarted;
        public event Action OnCalibrationRoutineDone;
        public event Action OnCalibrationFailed;
        public event Action OnCalibrationSucceeded;

        //members
        GazeMetricsBase calibration = new GazeMetricsBase();

        int targetIdx;
        int targetSampleCount;
        Vector3 currLocalTargetPos;

        float tLastSample = 0;
        float tLastTarget = 0;
        List<GameObject> previewMarkers = new List<GameObject>();

        bool previewMarkersActive = false;
        bool _isSampleExcluded;
        void OnEnable()
        {
            calibration.OnCalibrationSucceeded += CalibrationSucceeded;
            calibration.OnCalibrationFailed += CalibrationFailed;

            if (marker == null || camera == null || settings == null || targets == null)
            {
                Debug.LogWarning("Required components missing.");
                enabled = false;
                return;
            }
                        
            Time.fixedDeltaTime = (float)1/settings.samplingRate;
            
            InitPreviewMarker();
            InitGazeProvider();
        }

        void OnDisable()
        {
            calibration.OnCalibrationSucceeded -= CalibrationSucceeded;
            calibration.OnCalibrationFailed -= CalibrationFailed;

            if (calibration.IsCalibrating)
            {
                StopCalibration();
            }
        }

        void Update()
        {
            if (showPreview != previewMarkersActive)
            {
                SetPreviewMarkers(showPreview);
            }

            if (calibration.IsCalibrating)
            {
                //UpdateCalibration();                
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                ToggleCalibration();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                showPreview = !showPreview;
            }
        }
        int counter = 0;
        void FixedUpdate(){
            if (calibration.IsCalibrating)
            {                
                UpdateCalibration(); 
                _gazeProvider.GetGazeData();               
            }
        }

        public void ToggleCalibration()
        {
            if (calibration.IsCalibrating)
            {
                StopCalibration();
            }
            else
            {
                StartCalibration();
            }
        }

        public void StartCalibration()
        {
            if (!enabled)
            {
                Debug.LogWarning("Component not enabled!");
                return;
            }
           

            Debug.Log("Starting Calibration");
            Debug.Log((_gazeProvider.GetType().ToString()));

            showPreview = false;

            targetIdx = 0;
            targetSampleCount = 0;

            UpdatePosition();

            marker.gameObject.SetActive(true);

            calibration.StartCalibration(settings);
            Debug.Log($"Sample Rate: {settings.samplingRate}");

            if (OnCalibrationStarted != null)
            {
                OnCalibrationStarted();
            }

            //abort process on disconnecting
            //subsCtrl.OnDisconnecting += StopCalibration;
        }

        public void StopCalibration()
        {
            if (!calibration.IsCalibrating)
            {
                Debug.Log("Nothing to stop.");
                return;
            }

            calibration.StopCalibration();

            marker.gameObject.SetActive(false);

            if (OnCalibrationRoutineDone != null)
            {
                OnCalibrationRoutineDone();
            }

            //subsCtrl.OnDisconnecting -= StopCalibration;
        }

        void OnApplicationQuit()
        {
            //calibration.Destroy();
        }

        private void UpdateCalibration()
        {
            UpdateMarker();

            float tNow = Time.time;
            // if (tNow - tLastSample >= 1f / settings.SampleRate - Time.deltaTime / 2f)
            // {
                _isSampleExcluded = false;
                if (tNow - tLastTarget < settings.ignoreInitialSeconds - Time.deltaTime / 2f)
                {
                    _isSampleExcluded = true;
                }

                tLastSample = tNow;

                //Adding the calibration reference data to the list that will be passed on, once the required sample amount is met.
                // double sampleTimeStamp = timeSync.ConvertToPupilTime(Time.realtimeSinceStartup);
                AddSample();

                targetSampleCount++;//Increment the current calibration sample. (Default sample amount per calibration point is 120)

                if (tNow - tLastTarget >= settings.secondsPerTarget)
                {
                    calibration.SendCalibrationReferenceData();

                    if (targetIdx < targets.GetTargetCount())
                    {
                        targetSampleCount = 0;

                        UpdatePosition();
                    }
                    else
                    {
                        StopCalibration();
                    }
                }
            // }
        }

        private void CalibrationSucceeded()
        {
            if (OnCalibrationSucceeded != null)
            {
                OnCalibrationSucceeded();
            }
        }

        private void CalibrationFailed()
        {
            if (OnCalibrationFailed != null)
            {
                OnCalibrationFailed();
            }
        }

        private void AddSample()
        {
            SampleData pointData = new SampleData();
            
            pointData.timeStamp = _gazeProvider.LocalEyeGazeData.Timestamp;
            pointData.isValid = _gazeProvider.LocalEyeGazeData.isRayValid;  
            pointData.exclude = _isSampleExcluded; 
            pointData.targetId = targetIdx;
            pointData.localMarkerPosition = currLocalTargetPos;
            pointData.worldMarkerPosition = marker.position;
            pointData.cameraPosition = camera.transform.position;
            pointData.localGazeOrigin = _gazeProvider.LocalEyeGazeData.Origin;
            pointData.localGazeDirection = _gazeProvider.LocalEyeGazeData.Direction;             
            pointData.worldGazeOrigin =  camera.transform.localToWorldMatrix.MultiplyPoint(_gazeProvider.LocalEyeGazeData.Origin);           
            pointData.worldGazeDirection = camera.transform.localToWorldMatrix.MultiplyVector(_gazeProvider.LocalEyeGazeData.Direction); 
            pointData.worldGazeDistance = _gazeProvider.LocalEyeGazeData.Distance; 
            
            //Calculate sample metrics
            MetricsCalculator.CalculateSampleMetrics(ref pointData, Vector3.zero);            

            calibration.AddCalibrationPointReferencePosition(pointData);
        }

        private void UpdatePosition()
        {
            currLocalTargetPos = targets.GetLocalTargetPosAt(targetIdx);

            targetIdx++;
            tLastTarget = Time.time;
        }

        private void UpdateMarker()
        {
            marker.position = camera.transform.localToWorldMatrix.MultiplyPoint(currLocalTargetPos);
            marker.LookAt(camera.transform.position);
        }

        void InitPreviewMarker()
        {

            var previewMarkerParent = new GameObject("Calibration Targets Preview");
            previewMarkerParent.transform.SetParent(camera.transform);
            previewMarkerParent.transform.localPosition = Vector3.zero;
            previewMarkerParent.transform.localRotation = Quaternion.identity;

            for (int i = 0; i < targets.GetTargetCount(); ++i)
            {
                var target = targets.GetLocalTargetPosAt(i);
                var previewMarker = Instantiate<GameObject>(marker.gameObject);
                previewMarker.transform.parent = previewMarkerParent.transform;
                previewMarker.transform.localPosition = target;
                previewMarker.transform.LookAt(camera.transform.position);
                previewMarker.SetActive(true);
                previewMarkers.Add(previewMarker);
            }

            previewMarkersActive = true;
        }

        void SetPreviewMarkers(bool value)
        {
            foreach (var marker in previewMarkers)
            {
                marker.SetActive(value);
            }

            previewMarkersActive = value;
        }

        private void InitGazeProvider(){
            if (_gazeProvider != null) return;
            Debug.Log("Initializing provider: " + ProviderSDK);
            _gazeProvider = GetProvider();

            if (_gazeProvider != null){
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, _compilerFlagString);
            }
            
            _gazeProvider.Initialize();
        }
        
        private GazeDataProvider GetProvider(){
            string providerName = "";
            
            switch(ProviderSDK){
                case ProvidersList.HTCViveSranipal:
                    providerName = "GazeMetrics.SranipalProvider";
                    _compilerFlagString = SranipalProviderCompilerFlagString;
                    break;
                case ProvidersList.PupiLabs:
                    providerName = "GazeMetrics.PupilProvider";
                    _compilerFlagString = PupilProviderCompilerFlagString;
                    break;
                case ProvidersList.TobiiXR:
                    providerName = "GazeMetrics.TobiiXRProvider";
                    _compilerFlagString = TobiiXRProviderCompilerFlagString;
                    break;
                default:
                    return null;                    
            }
            Debug.Log("Initializing provider for: " + providerName);
            return GetProviderFromName(providerName);            
        }

        private GazeDataProvider GetProviderFromName(string ProviderName)
        {
            Type providerType = Type.GetType(ProviderName);
            if (providerType == null){
                Debug.Log("provider type not found");
                return null;
            } 
            try
            {
                return Activator.CreateInstance(providerType) as GazeDataProvider;
            }
            catch (Exception) {
                Debug.LogError("There was an error instantiating the gaze provider: " + ProviderName);
             }
            return null;
        }

    }
}
