using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GazeMetrics
{
    [RequireComponent(typeof(GazeMetricsController))]
    public class GazeMetricsResultsText : MonoBehaviour
    {
        public Text accuracyText;
        public Text rmsPrecisionText;        
        public Text sdPrecisionText;
        public Text sampleText;
        public Text validSamplesText;
        private GazeMetricsController gazeMetricsController;

        void Awake()
        {            
            gazeMetricsController = GetComponent<GazeMetricsController>();
        }

        void OnEnable()
        {                     
            gazeMetricsController.OnMetricsCalculated += OnMetricsCalculated;
        }

        void OnDisable()
        {                
            gazeMetricsController.OnMetricsCalculated += OnMetricsCalculated;
        }

       
        private void OnMetricsCalculated(TargetMetrics metrics)
        {
            SetStatusText(metrics);
        }
       
        private void SetStatusText(TargetMetrics metrics)
        {
            if (accuracyText != null && rmsPrecisionText != null &&  sdPrecisionText != null && sampleText != null && validSamplesText != null)
             {
                accuracyText.text = metrics.AverageAccuracy.ToString();
                rmsPrecisionText.text = metrics.RmsPrecision.ToString();
                sdPrecisionText.text = String.Format("x: {0}\ty: {1}\tz: {2}", metrics.SdPrecision.x, metrics.SdPrecision.y, metrics.SdPrecision.z) ;
                sampleText.text = metrics.SampleCount.ToString();
                validSamplesText.text = ((float)metrics.ValidSamples/metrics.SampleCount).ToString();
             }
        }
        
    }
}
