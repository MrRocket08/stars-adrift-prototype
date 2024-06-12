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

		if (burnFraction != 0)
			heat += Mathf.Abs(burnFraction * mainAccel) / 1000;
		else if (heat > 0)
			heat -= Time.deltaTime;
		else
			heat = 0;

        wetMass = dryMass + fuel;

		telemetry.text = "Δv		" + (effectiveExhaustVelocity * Mathf.Log(wetMass / dryMass) / 1000).ToString("F3") + " KM / S" +
			"\n\nVEL     " + velocity.magnitude.ToString("F2") + " M / S" +
			"\n--VELX  " + velocity.x.ToString("F2") + " M / S" +
			"\n--VELY  " + velocity.y.ToString("F2") + " M / S" +

			"\n\nFUEL    " + fuel.ToString("F1") + " / " + maxFuel.ToString("F1") +
			"\n--%		" + (fuel / maxFuel * 100).ToString("F2") + " %" +

			"\n\nKTC     " + ktcAmmo.ToString("F0") + " / " + maxKtcAmmo.ToString("F0") +
			"\n--%		" + (ktcAmmo / maxKtcAmmo * 100).ToString("F2") + " %" +

			"\n\nMSL     " + mslAmmo.ToString("F0") + " / " + maxMslAmmo.ToString("F0") +
			"\n--%		" + (mslAmmo / maxMslAmmo * 100).ToString("F2") + " %" +

			"\n\nHEAT    " + heat.ToString("F1") + " / " + heatCapacity.ToString("F1") + " MW" +
			"\n--%		" + (heat / heatCapacity * 100).ToString("F2") + " %";
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
