using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heatsink", menuName = "Heatsink", order = 6)]
public class Heatsink : Module
{
	public enum ReservoirMaterial { solid, water, supercoolant } // three main types of possible heat reservoir material
	[SerializeField]
	ReservoirMaterial rm;

	public float heatCapacity; // heat capcity, in MJ, of the heatsink

	private new void Awake()
	{
		// for testing
		status = Status.off;
	}
}
