using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TelemetryMenu : MonoBehaviour
{
	Ship ship;
	TextMeshProUGUI telemetry;

	// Start is called before the first frame update
	void Start()
	{
		ship = GameObject.Find("Ship").GetComponent<Ship>();
		telemetry = GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update()
	{
		telemetry.text = "Δv	" + (ship.effectiveExhaustVelocity * Mathf.Log(ship.wetMass / ship.dryMass) / 1000).ToString("F3") + " KM / S" +
			"\n\nVEL	" + ship.velocity.magnitude.ToString("F2") + " M / S" +
			"\n--VELX	" + ship.velocity.x.ToString("F2") + " M / S" +
			"\n--VELY	" + ship.velocity.y.ToString("F2") + " M / S" +

			"\n\nFUEL	" + ship.fuel.ToString("F1") + " / " + ship.maxFuel.ToString("F1") +
			"\n--%	" + (ship.fuel / ship.maxFuel * 100).ToString("F2") + " %" +

			"\n\nKTC	" + ship.ktcAmmo.ToString("F0") + " / " + ship.maxKtcAmmo.ToString("F0") +
			"\n--%	" + (ship.ktcAmmo / ship.maxKtcAmmo * 100).ToString("F2") + " %" +

			"\n\nMSL	" + ship.mslAmmo.ToString("F0") + " / " + ship.maxMslAmmo.ToString("F0") +
			"\n--%	" + (ship.mslAmmo / ship.maxMslAmmo * 100).ToString("F2") + " %" +

			"\n\nHEAT	" + ship.heat.ToString("F1") + " / " + ship.heatCapacity.ToString("F1") + " MW" +
			"\n--%	" + (ship.heat / ship.heatCapacity * 100).ToString("F2") + " %";
	}
}
