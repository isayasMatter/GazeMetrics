namespace GazeMetrics {
    using UnityEngine;
    public interface GazeDataProvider{
        EyeGazeData LocalEyeGazeData {get;}       
        bool Initialize();
        void GetGazeData();
        void Destroy();

    }
    

}