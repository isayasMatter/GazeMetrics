using UnityEngine;
using System.Text;
namespace GazeMetrics{    
    public class TargetMetrics{
        public float targetId { get; set; }   
        public Vector3 localMarkerPosition { get; set; }
        public Vector3 worldMarkerPosition { get; set; }  
        public float AverageAccuracy { get; set; }
        public Vector3 SdPrecision { get; set; }
        public float RmsPrecision { get; set; }
        public int SampleCount { get; set; }
        public int ValidSamples { get; set; }
        public int ExcludedSamples { get; set; }   

        public string ToCSVString(){
            StringBuilder sb = new StringBuilder();
            sb.Append(targetId).Append(", ");
            sb.Append(Helpers.StringFromVector(localMarkerPosition)).Append(", ");
            sb.Append(Helpers.StringFromVector(worldMarkerPosition)).Append(", ");
            sb.Append(AverageAccuracy).Append(", ");
            sb.Append(Helpers.StringFromVector(SdPrecision)).Append(", ");
            sb.Append(RmsPrecision).Append(", ");
            sb.Append(SampleCount).Append(", ");
            sb.Append(ValidSamples).Append(", ");
            sb.Append(ExcludedSamples).Append(", ");

            return sb.ToString();
        }     

         public static string ToCSVHeader(){
            StringBuilder sb = new StringBuilder();

            sb.Append("TargetID").Append(", ");
            sb.Append("LocalMarkerPosition.X").Append(", ").Append("LocalMarkerPosition.Y").Append(", ").Append("LocalMarkerPosition.Z").Append(", ");
            sb.Append("WorldMarkerPosition.X").Append(", ").Append("WorldMarkerPosition.Y").Append(", ").Append("WorldMarkerPosition.Z").Append(", ");
            sb.Append("AverageAccuracy").Append(", ");
            sb.Append("SdPrecision.X").Append(", ").Append("SdPrecision.Y").Append(", ").Append("SdPrecision.Z").Append(", ");
            sb.Append("RmsPrecision").Append(", ");
            sb.Append("SampleCount").Append(", ");
            sb.Append("ValidSamples").Append(", ");
            sb.Append("ExcludedSamples").Append(", ");

            return sb.ToString();
         } 
    }

    
}