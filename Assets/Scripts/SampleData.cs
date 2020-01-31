using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
namespace GazeMetrics{
    
    public class SampleData{
        public float timeStamp { get; set; }
        public bool isValid { get; set; }
        public bool exclude { get; set; }
        public float targetId { get; set; }     
        public Vector3 cameraPosition { get; set; }
        public Vector3 localGazeOrigin { get; set; }
        public Vector3 localGazeDirection { get; set; }
        public Vector3 worldGazeOrigin { get; set; }
        public Vector3 worldGazeDirection { get; set; }
        public float worldGazeDistance { get; set; }
        public Vector3 wolrdGazePoint { get; set; }
        public Vector3 localMarkerPosition { get; set; }
        public Vector3 worldMarkerPosition { get; set; }       
        public float OffsetAngle { get; set; }
        public float interSampleAngle { get; set; }
        
        public string ToCSVString(){

            StringBuilder sb = new StringBuilder();
            sb.Append(timeStamp).Append(", ");
            sb.Append(isValid?"Valid":"Invalid").Append(", ");
            sb.Append(exclude?"Excluded":"Included").Append(", ");
            sb.Append(targetId).Append(", ");
            sb.Append(Helpers.StringFromVector(cameraPosition)).Append(", ");
            sb.Append(Helpers.StringFromVector(localGazeOrigin)).Append(", ");
            sb.Append(Helpers.StringFromVector(localGazeDirection)).Append(", ");
            sb.Append(Helpers.StringFromVector(worldGazeOrigin)).Append(", ");
            sb.Append(Helpers.StringFromVector(worldGazeDirection)).Append(", ");
            sb.Append(worldGazeDistance).Append(", ");
            sb.Append(Helpers.StringFromVector(localMarkerPosition)).Append(", ");
            sb.Append(Helpers.StringFromVector(worldMarkerPosition)).Append(", ");
            sb.Append(OffsetAngle).Append(", ");
            sb.Append(interSampleAngle);
            
            return sb.ToString();
            
        }

        public static string ToCSVHeader(){
            StringBuilder sb = new StringBuilder();

            sb.Append("timeStamp").Append(", ");
            sb.Append("Validity").Append(", ");
            sb.Append("SampleInclusion").Append(", ");
            sb.Append("TargetId").Append(", ");
            sb.Append("CameraPosition.X").Append(", ").Append("CameraPosition.Y").Append(", ").Append("CameraPosition.Z").Append(", ");
            sb.Append("LocalGazeOrigin.X").Append(", ").Append("LocalGazeOrigin.Y").Append(", ").Append("LocalGazeOrigin.Z").Append(", ");
            sb.Append("LocalGazeDirection.X").Append(", ").Append("LocalGazeDirection.Y").Append(", ").Append("LocalGazeDirection.Z").Append(", ");
            sb.Append("WorldGazeOrigin.X").Append(", ").Append("WorldGazeOrigin.Y").Append(", ").Append("WorldGazeOrigin.Z").Append(", ");
            sb.Append("WorldGazeDirection.X").Append(", ").Append("WorldGazeDirection.Y").Append(", ").Append("WorldGazeDirection.Z").Append(", ");
            sb.Append("WorldGazeDistance").Append(", ");
            sb.Append("LocalMarkerPosition.X").Append(", ").Append("LocalMarkerPosition.Y").Append(", ").Append("LocalMarkerPosition.Z").Append(", ");
            sb.Append("WorldMarkerPosition.X").Append(", ").Append("WorldMarkerPosition.Y").Append(", ").Append("WorldMarkerPosition.Z").Append(", ");
            sb.Append("OffsetAngle").Append(", ");
            sb.Append("InterSampleAngle");
            
            return sb.ToString();
        }

    }
}
