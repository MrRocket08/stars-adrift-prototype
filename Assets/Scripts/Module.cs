using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Module : MonoBehaviour
{
	public enum Status { on, off, destroyed }
	Status status;

	protected int size; // integer from 1-5, weapons only have 1-3
	protected int mass; // mass, in kg, of the module
	protected float passiveHeat; // heat generated when status = off
	protected float activeHeat; // heat generated when status = on
	protected float shc; // specific heat capacity of the module
	protected float temperature; // temperature the module currently has
	protected float maxTemperature; // temperature the module can have before it starts taking damage
	protected float health; // module health
	protected float powerUse; // module power consumption

	protected Image image; // module image

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

	public Image GetImage()
	{
		return image;
	}

	public int GetSize()
	{
		return size;
	}

	public float GetTemperature()
	{
		return temperature;
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

public class Weapon : Module
{
	public enum Side { left, right, center }

	Side side;

	float turnSpeed;
	float range;

	float ammo; // current ammo
	float maxAmmo; // maximum ammo the weapon can have
	float clip; // current ammo in the clip
	float clipSize; // how much ammo the clip can hold

	float fireTime; // time in between firing two shots
	float reloadTime; // time it takes to reload the clip

	// Track target;	// current target of the weapon (targetList[0])
	// Track[] targetList; // targets it can potentially attack
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

public class ModuleSlot : MonoBehaviour
{
	int size;
	Module module;
	Image image;
	float heatReceived;

	protected List<ModuleSlot> connections = new List<ModuleSlot>(); // heat connections this module slot has with other module slots

	public ModuleSlot(int _size)
	{
		size = _size;
	}

	// utilizes the simple equation from the second law of thermodynamics
	// to transfer heat to all connected modules
	public void FlowHeat(float conductivity)
	{
		float distance;

		foreach (ModuleSlot ms in connections)
		{
			distance = Vector2.Distance(transform.position, ms.transform.position);

			// heat received by the other object
			ms.ReceiveHeat(-conductivity * (ms.GetModule().GetTemperature() - GetModule().GetTemperature()) / distance);
			// heat lost by this object
			ReceiveHeat(conductivity * (ms.GetModule().GetTemperature() - GetModule().GetTemperature()) / distance);
		}
	}

	// accessor methods
	public Module GetModule()
	{
		return module;
	}

	// mutator methods
	public bool RecieveModule(Module _module)
	{
		if (_module.GetSize() > size) {
			return false;
		} else {
			module = _module;
			image = module.GetImage();
			return true;
		}
	}

	public void RemoveModule()
	{
		module = null;
		image = null;
	}

	public void ReceiveHeat(float _heatReceived)
	{
		heatReceived += _heatReceived;
	}

	public void GiveModuleHeat()
	{
		module.ReceiveHeat(heatReceived);
		heatReceived = 0;
	}
}