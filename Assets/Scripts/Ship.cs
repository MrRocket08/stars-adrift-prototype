using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ship : MonoBehaviour
{
	// shipdata slot
	public ShipData sd;

	// SHIP DATA FIELDS
	public float wetMass;
	public float dryMass;
	public Vector2 velocity;
	public float effectiveExhaustVelocity;

	public float fuel;
	public float maxFuel;

	public float ktcAmmo;
	public float maxKtcAmmo;

	public float mslAmmo;
	public float maxMslAmmo;

	public float heat;
	public float heatCapacity;
	public float conductivity; // thermal conductivity of the ship's heat transfer systems

	public float turnAccel;
	public float mainAccel;

	// OTHER FIELDS
	ModuleSlot[] modules; // list of all the ship's modules


	public enum Alignment { ally, neutral, enemy }

	// MANEUVER
	[System.Serializable]
	public struct maneuver
	{
		public Vector2 direction;
		public float magnitude;

		public maneuver(Vector2 _direction, float _magnitude)
		{
			direction = _direction;
			magnitude = _magnitude;
		}
	}

	public maneuver[] maneuvers = new maneuver[5];

	Rigidbody2D rb;
	float burnFraction;
	float turnFraction;
	// Start is called before the first frame update
	void Awake()
	{
		dryMass = sd.dryMass;
		effectiveExhaustVelocity = sd.effectiveExhaustVelocity;
		fuel = sd.fuel;
		maxFuel = sd.maxFuel;
		heat = sd.heat;
		heatCapacity = sd.heatCapacity;
		conductivity = sd.conductivity;
		turnAccel = sd.turnAccel;
		mainAccel = sd.mainAccel;

		rb = GetComponent<Rigidbody2D>();
		wetMass = rb.mass;

		maneuvers[0] = new maneuver(Vector2.up, 10f);

		modules = FindObjectsOfType<ModuleSlot>();

		if (modules != null)
		{
			foreach (ModuleSlot ms in modules)
			{
				Module m = ms.GetModule();

				if (m is Weapon weapon)
				{
					if (weapon.GetWepType() == Weapon.wepType.kinetic)
					{
						ktcAmmo += weapon.GetAmmo() + weapon.GetClip();
						maxKtcAmmo += weapon.GetAmmo() + weapon.GetClip();
					}
					else if (weapon.GetWepType() == Weapon.wepType.missile)
					{
						mslAmmo += weapon.GetAmmo() + weapon.GetClip();
						maxMslAmmo += weapon.GetAmmo() + weapon.GetClip();
					}
				} else if (m is Radiator radiator)
				{
					radiator.Deploy();
				} else if (m is Heatsink h)
				{
					heatCapacity = h.heatCapacity;
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		burnFraction = Input.GetAxisRaw("Vertical");
		turnFraction = Input.GetAxis("Horizontal");
		fuel -= Mathf.Abs(burnFraction) / 1000; // mass flow rate in kilograms
		fuel -= Mathf.Abs(turnFraction) / 1000;

		wetMass = dryMass + fuel;
	}

	private void AccumulateHeat()
	{
		foreach (ModuleSlot ms in modules)
		{
			ms.GetModule().GenerateHeat();
		}
	}

	private void DisperseHeat()
	{
		foreach (ModuleSlot ms in modules)
		{
			if (ms.GetModule() is Radiator radiator && radiator.IsDeployed())
			{
				radiator.RadiateHeat();
			}
		}
	}

	void FixedUpdate()
	{
		// all forces in METRIC TONNES
		rb.AddForce(burnFraction * effectiveExhaustVelocity * Mathf.Abs(burnFraction) * transform.up, ForceMode2D.Force);
		rb.AddTorque(-turnFraction * effectiveExhaustVelocity * Mathf.Abs(turnFraction));

		velocity = rb.velocity;

		AccumulateHeat();
		DisperseHeat();
	}

	// ACCESSOR METHODS
	public maneuver GetManeuver() { return maneuvers[0]; }
}
