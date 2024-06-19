using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SensorSuite", menuName = "SensorSuite", order = 8)]
public class SensorSuite : Module
{
	// DEPRECATE ASAP
	enum Type { radar, lidar } // mainly determines patterning / stealth
	[SerializeField]
	Type type;

	public float detectionRange; // theoretical max detection range
	public float swingTime; // how fast the radar array completes a 360 degree rotation
	public int detectors; // how many raycasts the sensor suite should send out per ping
	public float patterning; // how quickly detection quality increases as tracks get closer in range
	public float maxTracks; // maximum tracks possible at one time
					 // Track[] tracks;
}