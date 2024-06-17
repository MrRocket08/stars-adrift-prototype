using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Module", menuName = "Module", order = 1)]
public class Module : ScriptableObject
{
	public enum Status { on, off, destroyed }
	Status status;

	[SerializeField]
	protected int size; // integer from 1-5, weapons only have 1-3
    [SerializeField]
    protected float mass; // mass, in metric tonnes, of the module
    [SerializeField]
    protected float passiveHeat; // heat generated when status = off (in kJ)
    [SerializeField]
    protected float activeHeat; // heat generated when status = on (in kJ)
    [SerializeField]
    protected float shc; // specific heat capacity of the module
    [SerializeField]
    protected float temperature; // temperature the module currently has
    [SerializeField]
    protected float maxTemperature; // temperature the module can have before it starts taking damage
    [SerializeField]
    protected float health; // module health
    [SerializeField]
    protected float powerUse; // module power consumption (in MW)
    [SerializeField]
    protected Sprite sprite; // module sprite

	/*
	public Module(
		int _size,
		float _mass,
		float _passiveHeat,
		float _activeHeat,
		float _shc,
		float _temperature,
		float _maxTemperature,
		float _health,
		float _powerUse
		)
	{
		size = _size;
		mass = _mass;
		passiveHeat = _passiveHeat;
		activeHeat = _activeHeat;
		shc = _shc;
		temperature = _temperature;
		maxTemperature = _maxTemperature;
		health = _health;
		powerUse = _powerUse;
	} */

	protected void Awake()
	{
		status = Status.on;
	}

	// class methods
	public float ChangeTemp(float heatInput)
	{
		return heatInput / (mass * shc);
	}

	// accessor methods
	public Status GetStatus()
	{
		return status;
	}

	public Sprite GetSprite()
	{
		return sprite;
	}

	public int GetSize()
	{
		return size;
	}

	public float GetTemperature()
	{
		return temperature;
	}

	public float GetMaxTemperature()
	{
		return maxTemperature;
	}

	// mutator methods
	public void GenerateHeat()
	{
		if (status == Status.on)
		{
			temperature += ChangeTemp(activeHeat);
		}
		else
		{
			temperature += ChangeTemp(passiveHeat);
		}
	}

	public void ReceiveHeat(float _heat)
	{
		temperature += ChangeTemp(_heat);
	}
}

public class Reactor : Module
{
	float powerGeneration;
}

public class Engine : Module
{
	float fuelConsumption;
	float eev; // effective exhaust velocity
	float thrust;
}

public class Radiator : Module
{
	float radiatedHeat; // how much heat the radiators wick away
	bool isDeployed;
	float deployTime; // how long it takes to retract/extend the rads
	float vulnerability; // how much more vulnerable the radiators are when deployed compared to when stowed

	public bool IsDeployed()
	{
		return isDeployed;
	}

	public void RadiateHeat()
	{
		temperature += ChangeTemp(-radiatedHeat);
	}
}

public class Heatsink : Module {
	public enum ReservoirMaterial { solid, water, supercoolant } // three main types of possible heat reservoir material
	ReservoirMaterial rm;
	float reservoirMass;
	float reservoirSHC; // specific heat capacity of the reservoir material--determined by the material
}

public class Utility : Module
{

}

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

public class Armor : Module
{
	float thickness;
	bool doesSpall; // spalling armor can cause internal damage even if it is not penetrated
}