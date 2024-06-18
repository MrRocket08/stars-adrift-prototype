using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SensorSuite", menuName = "SensorSuite", order = 8)]
public class SensorSuite : Module
{
    enum Type { radar, lidar } // mainly determines patterning / stealth
    Type type;
    float range; // theoretical max detection range
    float swingTime; // how fast the radar array completes a 360 degree rotation
    int detectors; // how many raycasts the sensor suite should send out per ping
    float patterning; // how quickly detection quality increases as tracks get closer in range
    float maxTracks; // maximum tracks possible at one time
                     // Track[] tracks;
}