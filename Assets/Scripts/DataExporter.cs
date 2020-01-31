using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GazeMetrics{
    public static class DataExporter{
        public static bool SamplesToCsv(string FileName, List<SampleData> SampleDataList){
                StringBuilder sb = new StringBuilder();
                sb.Append(SampleData.ToCSVHeader()).Append("\n");
                foreach (SampleData _sampleData in SampleDataList){
                    sb.Append(_sampleData.ToCSVString()).Append("\n");
                }
                
                StreamWriter writer = new StreamWriter(FileName);
                writer.Write(sb.ToString());
                writer.Close();
                return true;
        }
    }
}