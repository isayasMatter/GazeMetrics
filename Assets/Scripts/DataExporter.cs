using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GazeMetrics{
    public static class DataExporter{
        public static bool SamplesToCsv(string FolderName, string FileName, List<SampleData> SampleDataList){
                StringBuilder sb = new StringBuilder();
                sb.Append(SampleData.ToCSVHeader()).Append("\n");
                foreach (SampleData _sampleData in SampleDataList){
                    sb.Append(_sampleData.ToCSVString()).Append("\n");
                }
                
                string _fileName = Path.Combine(FolderName, (FileName + "_experiment_data_raw.csv") );
                StreamWriter writer = new StreamWriter(_fileName);
                writer.Write(sb.ToString());
                writer.Close();
                return true;
        }
        public static bool MetricsToCsv(string FolderName, string FileName, List<TargetMetrics> MetricsDataList){
                StringBuilder sb = new StringBuilder();
                sb.Append(TargetMetrics.ToCSVHeader()).Append("\n");
                foreach (TargetMetrics _targetMetrics in MetricsDataList){
                    sb.Append(_targetMetrics.ToCSVString()).Append("\n");
                }
                
                string _fileName = Path.Combine(FolderName, (FileName + "_experiment_data_metrics.csv") );
                StreamWriter writer = new StreamWriter(_fileName);
                writer.Write(sb.ToString());
                writer.Close();
                return true;
        }
    }
}