using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ship : MonoBehaviour
{
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

	public float turnAccel;
	public float mainAccel;

	Module[] modules = new Module[8];

	// OTHER FIELDS
	public enum Alignment { ally, neutral, enemy }

	TextMeshProUGUI telemetry;

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
		rb = GetComponent<Rigidbody2D>();
		wetMass = rb.mass;

		maneuvers[0] = new maneuver(Vector2.up, 10f);

		telemetry = GameObject.Find("TelemetryInfo").GetComponentInChildren<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update()
	{
		burnFraction = Input.GetAxisRaw("Vertical");
		turnFraction = Input.GetAxis("Horizontal");
		fuel -= Mathf.Abs(burnFraction) / 1000; // mass flow rate in METRIC TONNES
		fuel -= Mathf.Abs(turnFraction) / 1000;

		heat += AccumulateHeat();
		heat -= RadiateHeat();

        wetMass = dryMass + fuel;
	}

	private float AccumulateHeat()
	{
		float sumHeat = 0f;

		foreach (Module c in modules)
		{
			sumHeat += c.GenerateHeat();
		}

		return sumHeat;
	}

	private float RadiateHeat()
	{
		float radiatedHeat = 0f;

		foreach (Module c in modules)
		{
			if (c is Radiator)
			{
				radiatedHeat += ((Radiator)c).RadiateHeat();
			}
		}

		return radiatedHeat;
	}

	void FixedUpdate()
	{
		// all forces in METRIC TONNES
		rb.AddForce(burnFraction * effectiveExhaustVelocity * Mathf.Abs(burnFraction) * transform.up, ForceMode2D.Force);
		rb.AddTorque(-turnFraction * effectiveExhaustVelocity * Mathf.Abs(turnFraction));

		velocity = rb.velocity;
	}

	// ACCESSOR METHODS
	public maneuver GetManeuver() { return maneuvers[0]; }
}
