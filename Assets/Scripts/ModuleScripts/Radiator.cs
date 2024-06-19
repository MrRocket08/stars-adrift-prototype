using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Radiator", menuName = "Radiator", order = 5)]
public class Radiator : Module
{
	// DEPRECATE ASAP
	[SerializeField]
	float emissivity; // how much heat the radiators wick away
	[SerializeField]
	float surfaceArea; // radiator surface area in m^2

	public float emittedHeat; // heat, in kJ, emitted per second
	public float deployTime; // how long it takes to retract/extend the rads
	public float vulnerability; // how much more vulnerable the radiators are when deployed compared to when stowed

	bool isDeployed;
	float heatToRadiate;

	public bool IsDeployed()
	{
		return isDeployed;
	}

	// mutator methods

	// utilizing the Stefan-Boltzmann law of radiation
	// 5.67f * Mathf.Pow(10, -14)
	public void RadiateHeat()
	{
		heatToRadiate = 5.67f * Mathf.Pow(10, -14) * emissivity * surfaceArea * Mathf.Pow(temperature, 4);
		temperature += ChangeTemp(heatToRadiate);
	}

	public void Deploy()
	{
		isDeployed = true;
	}

	public void Undeploy()
	{
		isDeployed = false;
	}
}