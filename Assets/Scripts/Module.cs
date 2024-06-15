using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Module : MonoBehaviour
{
	public enum Status { on, off, destroyed }
	Status status;

	protected int size; // integer from 1-5, weapons only have 1-3
	protected float passiveHeat; // heat generated when status = off
	protected float activeHeat; // heat generated when status = on
	protected float maxHeat; // heat the module can have before it starts taking damage
	protected float health; // module health
	protected float powerUse; // module power consumption

	protected Image image; // module image

	protected void Awake()
	{
		status = Status.on;
	}

	public float GenerateHeat()
	{
		if (status == Status.on)
		{
			return activeHeat;
		} else
		{
			return passiveHeat;
		}
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
	float deployTime; // how long it takes to retract/extend the rads
	float vulnerability; // how much more vulnerable the radiators are when deployed compared to when stowed
	float conductivity; // how quickly other modules' heat reaches the radiator
}

public class Heatsink : Module
{
	float conductivity; // how quickly other modules' heat reaches the heatsink
}

public class SensorSuite : Module
{
	enum Type { radar, lidar } // mainly determines patterning / stealth
	Type type;
	float range; // theoretical max detection range
	float patterning; // how quickly detection quality increases as tracks get closer in range
	float maxTracks; // maximum tracks possible at one time
	// Track[] tracks;
}

// may remove this one
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

	public ModuleSlot(int _size)
	{
		size = _size;
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
}