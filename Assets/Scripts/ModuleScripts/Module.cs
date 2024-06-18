using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Module : ScriptableObject
{
	public enum Status { on, off, destroyed }
	Status status;

	[SerializeField]
	protected int size; // integer from 1-5, weapons only have 1-3
	[SerializeField]
	protected float mass; // mass, in metric tonnes, of the module
	[SerializeField]
	protected float passiveHeat; // heat generated when status = off (in MJ)
	[SerializeField]
	protected float activeHeat; // heat generated when status = on (in MJ)
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

	protected void Awake()
	{
		temperature = 0f;
		status = Status.on;
	}

	// class methods
	protected float ChangeTemp(float heatInput)
	{
		Debug.Log(heatInput * 10 / (mass * shc));
		return heatInput * 10 / (mass * shc); // because heat generation is in MJ
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

	public float GetMass()
	{
		return mass;
	}

	public float GetSHC()
	{
		return shc;
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
	public void SetTemperature(float newTemp)
	{
		temperature = newTemp;
	}

	public void ClampTemperature()
	{
		if (temperature > maxTemperature)
		{
			temperature = maxTemperature;
		}
	}

	public void GenerateHeat()
	{
		if (status == Status.on)
		{
			temperature += ChangeTemp(activeHeat);
			ClampTemperature();
		}
		else
		{
			temperature += ChangeTemp(passiveHeat);
			ClampTemperature();
		}
	}

	public void ReceiveHeat(float _heat)
	{
		temperature += ChangeTemp(_heat);
		ClampTemperature();
	}
}

public class Armor : Module
{
	float thickness;
	bool doesSpall; // spalling armor can cause internal damage even if it is not penetrated
}