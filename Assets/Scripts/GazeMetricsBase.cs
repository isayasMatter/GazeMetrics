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
        public event Action OnMetricsCalculated;
        //members
        
        GazeMetricsSettings settings;
     
        List<TargetMetrics> targetMetricsList = new List<TargetMetrics>();
        List<SampleData> targetDataList = new List<SampleData>();
        List<SampleData> experimentDataList = new List<SampleData>();
        
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
            experimentDataList.Clear();
            targetMetricsList.Clear();
        }

        public void AddCalibrationPointReferencePosition(SampleData sampleData)
        {            
            targetDataList.Add(sampleData);
            experimentDataList.Add(sampleData);
        }

        public void SendCalibrationReferenceData()
        {
            Debug.Log("Calculate metrics");            

            targetMetricsList.Add(MetricsCalculator.CalculateTargetMetrics(targetDataList));
            
            if(OnMetricsCalculated != null){
                OnMetricsCalculated();
            }
            //Clear the current target data, so we can proceed to the next target if there is any.
            targetDataList.Clear();
        }

        public void StopCalibration()
        {
            Debug.Log("Calibration should stop");

            IsCalibrating = false;

            DataExporter.SamplesToCsv(settings.outputFolder, settings.experimentID, experimentDataList);
            DataExporter.MetricsToCsv(settings.outputFolder, settings.experimentID, targetMetricsList);           
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