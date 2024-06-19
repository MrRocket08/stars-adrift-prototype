using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleSlot : MonoBehaviour
{
	public Transform transform;
	public int moduleSlotSize;
	[SerializeField] Module module;

	// module data fields
	public enum Status { on, off, destroyed }
	[SerializeField]
	private Status status;

    // MODULE
    #region Module Data
    [Header("Module Data")]
	[SerializeField]
	private int moduleSize; // integer from 1-5, weapons only have 1-3
	[SerializeField]
	private float mass; // mass, in metric tonnes, of the module
	[SerializeField]
	private float passiveHeat; // heat generated when status = off (in MJ)
	[SerializeField]
	private float activeHeat; // heat generated when status = on (in MJ)
	[SerializeField]
	private float shc; // specific heat capacity of the module
	[SerializeField]
	private float temperature; // temperature the module currently has
	[SerializeField]
	private float maxTemperature; // temperature the module can have before it starts taking damage
	[SerializeField]
	private float health; // module health
	[SerializeField]
	private float powerUse; // module power consumption (in MW)
	[SerializeField]
	private Sprite sprite; // module sprite
    #endregion

    // WEAPON
    #region Weapon Data
    public enum wepType { kinetic, missile, energy }
	wepType weptype;

    [Header("Weapon Data")]
	[SerializeField]
	float turnSpeed;
	[SerializeField]
	float fireRange; // weapon range (in km)
	[SerializeField]
	int ammo; // current ammo
	[SerializeField]
	int maxAmmo; // maximum ammo the weapon can have
	[SerializeField]
	int clip; // current ammo in the clip
	[SerializeField]
	int clipSize; // how much ammo the clip can hold
	[SerializeField]
	float fireTime; // time in between firing two shots
	[SerializeField]
	float reloadTime; // time it takes to reload the clip
    #endregion

    // REACTOR
    #region Reactor Data
    [Header("Reactor Data")]
	[SerializeField]
	float powerGeneration; // amount of energy, in MW, that the reactor generates per second
    #endregion

    // ENGINE
    #region Engine Data
    [Header("Engine Data")]
	[SerializeField]
	float fuelConsumption; // fuel consumption in kg/s
	[SerializeField]
	float eev; // effective exhaust velocity
	[SerializeField]
	float thrust; // thrust in newtons
	#endregion

	// HEATSINK
	#region Heatsink Data
	[Header("Heatsink Data")]
	[SerializeField]
	float heatCapacity;
    #endregion

    // RADIATOR
    #region Radiator Data
    [Header("Radiator Data")]
	[SerializeField]
	float emittedHeat;
	[SerializeField]
	float deployTime; // how long it takes to retract/extend the rads
	[SerializeField]
	float vulnerability; // how much more vulnerable the radiators are when deployed compared to when stowed
    #endregion

    // SENSOR SUITE
    #region Sensor Suite Data
    [Header("Sensor Suite Data")]
	[SerializeField]
	float detectionRange; // theoretical max detection range (in km)
	[SerializeField]
	float swingTime; // how fast the radar array completes a 360 degree rotation
	[SerializeField]
	int detectors; // how many raycasts the sensor suite should send out per ping
	[SerializeField]
	float patterning; // how quickly detection quality increases as tracks get closer in range
	[SerializeField]
	float maxTracks; // maximum tracks possible at one time
                     // Track[] tracks;
    #endregion

    #region Module Slot Data
    public enum Side { left, right, center }
	Side side;

	[SerializeField] Image image;
	[SerializeField] Gradient dmgGradient;
	float heatReceived;

	ModuleSlot[] connections; // heat connections this module slot has with other module slots
    #endregion

    private void Awake()
	{
		mass = module.mass;
		passiveHeat = module.passiveHeat;
		activeHeat = module.activeHeat;
		health = module.health;
		powerUse = module.powerUse;
		sprite = module.sprite;

		if (module is Weapon w)
		{
			turnSpeed = w.turnSpeed;
			fireRange = w.fireRange;
			maxAmmo = w.maxAmmo;
			clipSize = w.clipSize;
			fireTime = w.fireTime;
			reloadTime = w.reloadTime;

			ammo = maxAmmo;
			clip = clipSize;
		}
		else if (module is Reactor r)
		{
			powerGeneration = r.powerGeneration;
		}
		else if (module is Engine e)
		{
			fuelConsumption = e.fuelConsumption;
			eev = e.eev;
			thrust = e.thrust;
		}
		else if (module is Heatsink h)
		{
			heatCapacity = h.heatCapacity;
		}
		else if (module is Radiator rad)
		{
			emittedHeat = rad.emittedHeat;
			deployTime = rad.deployTime;
			vulnerability = rad.vulnerability;
		}
		else if (module is SensorSuite s)
		{
			detectionRange = s.detectionRange;
			swingTime = s.swingTime;
			detectors = s.detectors;
			patterning = s.patterning;
			maxTracks = s.maxTracks;
		}

		connections = FindObjectsOfType<ModuleSlot>();

		image = GetComponent<Image>();

		var colors = new GradientColorKey[3];
		colors[0] = new GradientColorKey(Color.green, 0.0f);
		colors[1] = new GradientColorKey(Color.yellow, 0.5f);
		colors[2] = new GradientColorKey(Color.red, 1.0f);

		var alphas = new GradientAlphaKey[2];
		alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
		alphas[1] = new GradientAlphaKey(1.0f, 1.0f);

		dmgGradient.SetKeys(colors, alphas);
	}

	// accessor methods
	public Module GetModule()
	{
		return module;
	}

	// mutator methods
	public bool RecieveModule(Module _module)
	{
		if (_module.size > moduleSlotSize)
		{
			return false;
		}
		else
		{
			module = _module;
			image.sprite = module.sprite;
			return true;
		}
	}

	public void RemoveModule()
	{
		module = null;
		image = null;
	}

	private void OnValidate()
	{
		image.sprite = module.sprite;
	}
}