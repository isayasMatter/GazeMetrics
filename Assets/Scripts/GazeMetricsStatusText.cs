using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GazeMetrics
{
    [RequireComponent(typeof(GazeMetricsController))]
    public class GazeMetricsStatusText : MonoBehaviour
    {
        public Text statusText;

        private GazeMetricsController gazeMetricsController;

        void Awake()
        {
            SetStatusText("Not connected");
            gazeMetricsController = GetComponent<GazeMetricsController>();
        }

        void OnEnable()
        {            
            gazeMetricsController.OnCalibrationStarted += OnCalibrationStarted;
            gazeMetricsController.OnCalibrationRoutineDone += OnCalibrationRoutineDone;
            gazeMetricsController.OnCalibrationSucceeded += CalibrationSucceeded;
            gazeMetricsController.OnCalibrationFailed += CalibrationFailed;
        }

        void OnDisable()
        {            
            gazeMetricsController.OnCalibrationStarted -= OnCalibrationStarted;
            gazeMetricsController.OnCalibrationRoutineDone -= OnCalibrationRoutineDone;
            gazeMetricsController.OnCalibrationSucceeded -= CalibrationSucceeded;
            gazeMetricsController.OnCalibrationFailed -= CalibrationFailed;
        }

        private void OnConnected()
        {
            string text = "Connected";
            text += "\n\nPlease warm up your eyes and press 'C' to start the calibration or 'P' to preview the calibration targets.";
            SetStatusText(text);
        }

        private void OnCalibrationStarted()
        {
            statusText.enabled = false;
        }

        private void OnCalibrationRoutineDone()
        {
            statusText.enabled = true;
            SetStatusText("Calibration routine is done. Waiting for results ...");
        }

        private void CalibrationSucceeded()
        {
            statusText.enabled = true;
            SetStatusText("Calibration succeeded.");

            StartCoroutine(DisableTextAfter(1));
        }

        private void CalibrationFailed()
        {
            statusText.enabled = true;
            SetStatusText("Calibration failed.");

            StartCoroutine(DisableTextAfter(1));
        }

        private void SetStatusText(string text)
        {
            if (statusText != null)
            {
                statusText.text = text;
            }
        }

        IEnumerator DisableTextAfter(float delay)
        {
            yield return new WaitForSeconds(delay);
            statusText.enabled = false;
        }
    }
}
