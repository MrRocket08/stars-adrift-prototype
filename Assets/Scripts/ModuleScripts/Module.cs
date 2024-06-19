using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Module : ScriptableObject
{
	public enum Status { on, off, destroyed }
	[SerializeField]
	protected Status status;

	// DEPRECATE ASAP
    public float shc; // specific heat capacity of the module
    public float temperature; // temperature the module currently has
    public float maxTemperature; // temperature the module can have before it starts taking damage

    public int size; // integer from 1-5, weapons only have 1-3
	public float mass; // mass, in metric tonnes, of the module
	public float passiveHeat; // heat generated when status = off (in MJ)
	public float activeHeat; // heat generated when status = on (in MJ)
	public float health; // module health
	public float powerUse; // module power consumption (in MW)
	public Sprite sprite; // module sprite

	protected void Awake()
	{
		temperature = 0f;
		status = Status.on;
	}

	// class methods

	// mutator methods

	public float GenerateHeat()
	{
		if (status == Status.on)
		{
			return Time.deltaTime * activeHeat;
		}
		else if (status == Status.off)
		{
			return Time.deltaTime * passiveHeat;
		} else
		{
			return 0;
		}
	}
}

public class Armor : Module
{
	float thickness;
	bool doesSpall; // spalling armor can cause internal damage even if it is not penetrated
}