using System;
using System.Collections.Generic;
using UnityEngine;

namespace GazeMetrics
{
    public class GazeMetricsBase
    {
        //events
        public event Action OnCalibrationStarted;
        public event Action OnCalibrationSucceeded;
        public event Action OnCalibrationFailed;
        //members
        
        GazeMetricsSettings settings;

        List<Dictionary<string, object>> calibrationData = new List<Dictionary<string, object>>();

        List<SampleData> targetDataList = new List<SampleData>();
        
        public bool IsCalibrating { get; set; }

        public void StartCalibration(GazeMetricsSettings settings)
        {
            this.settings = settings;            

            if (OnCalibrationStarted != null)
            {
                OnCalibrationStarted();
            }

            IsCalibrating = true;                    

            Debug.Log("Calibration Started");

            targetDataList.Clear();
        }

        public void AddCalibrationPointReferencePosition(SampleData targetData)
        {            
            targetDataList.Add(targetData);
            //Debug.Log(targetData.ToCSVString());
        }

        public void SendCalibrationReferenceData()
        {
            Debug.Log("Send CalibrationReferenceData");            

            MetricsCalculator.CalculateTargetMetrics(targetDataList);
            //Clear the current calibration data, so we can proceed to the next point if there is any.
            calibrationData.Clear();
        }

        public void StopCalibration()
        {
            Debug.Log("Calibration should stop");

            IsCalibrating = false;

            DataExporter.SamplesToCsv("samplefile.csv", targetDataList);

            //Send(new Dictionary<string, object> { { "subject", "calibration.should_stop" } });
        }

        
        private void ReceiveSuccess(string topic, Dictionary<string, object> dictionary, byte[] thirdFrame)
        {
            if (OnCalibrationSucceeded != null)
            {
                OnCalibrationSucceeded();
            }

            CalibrationEnded(topic);
        }

        private void ReceiveFailure(string topic, Dictionary<string, object> dictionary, byte[] thirdFrame)
        {
            if (OnCalibrationFailed != null)
            {
                OnCalibrationFailed();
            }

            CalibrationEnded(topic);
        }

        private void CalibrationEnded(string topic)
        {
            Debug.Log($"Calibration response: {topic}");
            
        }
    }
}