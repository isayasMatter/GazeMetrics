[![Generic badge](https://img.shields.io/badge/Maintained-No-green.svg)](https://shields.io/)
[![Generic badge](https://img.shields.io/badge/Software-C%23-blue.svg)](https://shields.io/)
[![Generic badge](https://img.shields.io/badge/License-MIT-red.svg)](https://shields.io/)
[![DOI](https://img.shields.io/badge/DOI-10.1145%2F3379156.3391374-yellowgreen)](https://doi.org/10.1145/3379156.3391374)

# What is GazeMetrics

GazeMetrics is a an Open-Source Tool for Measuring the Data Quality of head mounted display (HMD) based Eye Trackers. Here are some of it's functions:

* It measures spatial accuracy and spatial precision. 
* It supports multiple VR HMD based eye trackers including the Vive Pro Eye integrated eye tracker, Pupil Labs eye trackers and Tobii eye trackers.
* It streams raw eye gaze data and the derived metrics during the measurement procedure to external files.
* It provides an extensible framework that allows developers to expand the functionality of the tool.

# Required software

* Unity 2019.1.* (Working on updating this)
* An eye tracking SDK. 
    GazeMetrics currently supports Vive's SRanipal, Tobii XR and PupilLabs Unity SDKs. 

Latest release can be found in the the "Releases" page.


# Getting started with GazeMetrics

## Open a Unity project

1. Create a new Unity project, or open an existing project. If you are creating a new project make sure to select "3D" as the template type.
2. (**Optional**)To get the most accurate spatial accuracy and precision readings, the virtual scene should be free of visual distractions during the measurement procedure. Therefore it is preferred to create an empty scene and add GazeMetrics to that scene. You can learn more about scenes in Unity [here](https://docs.unity3d.com/Manual/CreatingScenes.html)

## Add an eye tracking SDK to your project

Add and initialize an eye tracking SDK in your project. You can refer to the specific SDK's documentation for more information. Information for the PupilLabs SDK can be found [here](https://github.com/pupil-labs/hmd-eyes/blob/master/docs/Developer.md), and information for Vive's SRAnipal SDK can be found [here](https://developer.vive.com/resources/vive-sense/sdk/vive-eye-tracking-sdk-sranipal/). 

If you created an empty scene for GazeMetrics make sure you have a reference to your SDK in the new scene (not just your experiment scenes).

If you are working on an eye tracking project in Unity, you will most probably have your preferred SDK added to your project. In that case you can safely skip this step.


## Import GazeMetrics Unity package into your Unity project

1. Go to the [GazeMetrics release page](https://github.com/isayasMatter/GazeMetrics/releases).
2. Under assets download **GazeMetrics_v[version number].unitypackage**
3. Import the **GazeMetrics_v[version number].unitypackage** you downloaded by going into "Asset -> Import Package -> Custom Package", select the .unitypackage file, ensure all items to import are checked, and then select "Import".

# Configuring GazeMetrics with your scene

1. Make sure you have imported the GazeMetrics package and your preferred eye tracking Unity SDK into your scene following the steps above.
2. Drag the GazeMetricsController prefab from your assets folder, in the project panel, into your scene in the hierarchy panel.
3. Select the GazeMetricsController Game Object in the Hierarchy panel to access the tool's setting in your inspector panel.
4. Configure the settings on the inspector panel. For more information about the settings refer to the settings section below.   
5. Run the scene.
6. Click "p" to preview the calibration points or click "c" to start the measurement procedure.
7. After the procedure has ended you should find two files inside the output folder you entered above. For more information about the output files refer to the section [below][GazeMetrics output format].

## GazeMetrics Settings

1. Select your eye tracking SDK from the Provider SDK drop down list.
2. Under "Scene references" set your main scene Camera in the "Camera" setting. You can also drag and drop your main Camera Game Object from the hierarchy panel in to this field.
3. Under "Settings" double click each settings object and select your preferred settings. Ensure that you have entered an output folder and an experiment ID under settings.
4. Gaze Metrics Settings allow you to set the following options:
    * **Seconds per target** (Default 2 seconds): Defines how long each target will be shown before proceeding to the next target.
    * **Ignore initial seconds** (Default 0.8 seconds): After each target is displayed how long do you want to wait before including the samples in your calculations. For example, if you use the default settings, about 1.2 seconds of data (2 - 0.8 seconds) of data for each target will be included in calculating the precision and accuracy metrics.
    * **Sampling rate** (Default 120): How many samples of eye tracking data to read per seconds. This is recommended to be equal to be equal to your eye tracker's sampling rate.
    * **Background color**: The background color of the scene during the measurement procedure.
    * **Target color**: The color of the stimulus target. Make sure the background color and the target color are contrasting to ensure the target is clearly visible.
    * **Target center color**: The color of the target's center.
    * **Experiment ID**: Identifier for this user/experiment. This value is written the file name and to one of the columns of the output CSV files. Make sure you change this for every experiment or you risk your data getting overwritten by later samples.
    * **Output folder**: The output folder for the calculations and the raw data. Two csv files will be exported for each measurement session. One file contains the raw data while the other file contains the calculated metrics for each stimulus target location.
5. Target settings allow you to select different option related to the number and placement of targets on the scene. You can choose either a Circular arrangement or Square arrangement by inserting the resective scriptable object into the Target field. Then to customize the options for the targets you can click on the scribtable object and set the following options:
    *  **Circles**: You can choose the number of circles you want to appear in the scene by changing this setting.
    *  **Center**: Allows you to chose where the center of the circle, around which the targets will appear, will be. If you choose more than one circle make sure you set the 'Z' cordinate of each circle to a different value to allow you to measure precision and accuracy at different depths.
    *  **Radius**: Allows you to set the radius of the circle.
    *  **Points**: This option allows you to set the number of targets per each circle. For example, if you chose 3 circles and 9 points, you will have 27 targets in total. If you have selected 2 *seconds per target* then the whole procedure would take about 54 seconds.

# Running GazeMetrics

If you have followed the instructions above, you can now preview your stimulus targets by pressing "P". When you are ready to start the measurement procedure you can press "C" on the keyboard. The procedure will start and you will see the targets appear one by one. After the last target is shown the procedure will end, and you will find your output data in the output folder you selected.

# Citation

If you used GazeMetrics in your research project, please cite the following article.

```
@inproceedings{10.1145/3379156.3391374,
    author = {B. Adhanom, Isayas and Lee, Samantha C. and Folmer, Eelke and MacNeilage, Paul},
    title = {GazeMetrics: An Open-Source Tool for Measuring the Data Quality of HMD-Based Eye Trackers},
    year = {2020},
    isbn = {9781450371346},
    publisher = {Association for Computing Machinery},
    address = {New York, NY, USA},
    url = {https://doi.org/10.1145/3379156.3391374},
    doi = {10.1145/3379156.3391374},
    articleno = {19},
    numpages = {5},
    keywords = {eye movements, precision, Eye tracking, eye tracker data quality, virtual reality, accuracy},
    location = {Stuttgart, Germany},
    series = {ETRA '20 Short Papers}
}
```









