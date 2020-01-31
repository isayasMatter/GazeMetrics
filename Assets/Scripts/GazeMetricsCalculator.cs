using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static GazeMetrics.LINQExtension;

namespace GazeMetrics{
    
    public class MetricsCalculator
    {
        public static void CalculateSampleMetrics(ref SampleData sampleDataPoint, Vector3 previousGazeDirection){ 
                   
            //direction of marker relative to eye position in world cordinates
            Vector3 markerDirection = sampleDataPoint.worldMarkerPosition - sampleDataPoint.worldGazeOrigin;

            //The accuracy of this single sample measured by offset angle
            float offsetAngle = Vector3.Angle(markerDirection,sampleDataPoint.worldGazeDirection);                  

            //The angular distance between this sample and the previous sample
            float interSampleAngle = Vector3.Angle(previousGazeDirection, sampleDataPoint.worldGazeDirection);                    
            
            Ray r = new Ray(sampleDataPoint.worldGazeOrigin, sampleDataPoint.worldGazeDirection);

            sampleDataPoint.wolrdGazePoint = r.GetPoint(sampleDataPoint.worldGazeDistance);
            sampleDataPoint.OffsetAngle = offsetAngle;                 
            sampleDataPoint.interSampleAngle = interSampleAngle; 
        }        

        public static void CalculateTargetMetrics(List<SampleData> sampleDataList){
            
            var targetMetricsQuery = from sampledata in sampleDataList 
                where sampledata.exclude == false && sampledata.isValid == true 
                group sampledata by sampledata.targetId into sampleGroup
                select new TargetMetrics
                    {
                        targetId = sampleGroup.Key,
                        AverageAccuracy = (from sm in sampleGroup where sm.exclude == false && sm.isValid == true select sm).Average(p => p.OffsetAngle),
                        RmsPrecision = (from sm in sampleGroup where sm.exclude == false && sm.isValid == true select sm).RootMeanSquare(),
                        SdPrecision = 0 , //(from sm in sampleGroup where sm.exclude == false && sm.isValid == true select sm).StandardDeviation(),
                        SampleCount = sampleGroup.Count(), 
                        ValidSamples = (from sm in sampleGroup where sm.isValid == true select sm).Count(),
                        ExcludedSamples = (from sm in sampleGroup where sm.exclude == true select sm).Count()                                               
                    };

                foreach (TargetMetrics tm in targetMetricsQuery){
                    Debug.Log(tm.targetId + ", " + tm.AverageAccuracy);
                }
                
        }      

        
    }

    public static class LINQExtension
    {
        public static float RootMeanSquare(this IGrouping<float,SampleData> source){
            if (source.Count() < 2)
                throw new System.InvalidOperationException("Source must have at least 2 elements");

            double s = source.Aggregate(0.0, (x, d) => x += Mathf.Pow((float)d.interSampleAngle, 2));

            return Mathf.Sqrt((float)(s / source.Count()));
        }

        public static float RootMeanSquare(this IEnumerable<SampleData> source){
            if (source.Count() < 2)
                throw new System.InvalidOperationException("Source must have at least 2 elements");

            double s = source.Aggregate(0.0, (x, d) => x += Mathf.Pow((float)d.interSampleAngle, 2));

            return Mathf.Sqrt((float)(s / source.Count()));
        }

        public static float Variance(this IEnumerable<float> source) 
        { 
            int n = 0;
            float mean = 0;
            float M2 = 0;

            foreach (float x in source)
            {
                n = n + 1;
                float delta = x - mean;
                mean = mean + delta / n;
                M2 += delta * (x - mean);
            }
            return M2 / (n - 1);
        }

        public static float StandardDeviation(this IEnumerable<SampleData> source) 
        { 
            float sdx = Mathf.Sqrt((float)source.Select(t => t.wolrdGazePoint.x).Variance()); 
            float sdy = Mathf.Sqrt((float)source.Select(t => t.wolrdGazePoint.y).Variance());   
            float sdz = Mathf.Sqrt((float)source.Select(t => t.wolrdGazePoint.z).Variance());  

            return Mathf.Sqrt((Mathf.Pow(sdx,2) + Mathf.Pow(sdy,2)));      
        }

    }
}