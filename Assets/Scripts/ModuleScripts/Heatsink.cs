using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heatsink", menuName = "Heatsink", order = 6)]
public class Heatsink : Module
{
	public enum ReservoirMaterial { solid, water, supercoolant } // three main types of possible heat reservoir material
	[SerializeField]
	ReservoirMaterial rm;
	[SerializeField]
	float reservoirMass;
	[SerializeField]
	float reservoirSHC; // specific heat capacity of the reservoir material--determined by the material

	private new void Awake()
	{
		if (rm == ReservoirMaterial.solid)
		{
			reservoirSHC = 2.5f;
		} else if (rm == ReservoirMaterial.water)
		{
			reservoirSHC = 4.2f;
		} else if (rm == ReservoirMaterial.supercoolant)
		{
			reservoirSHC = 6.0f;
		}
	}

    protected new float ChangeTemp(float heatInput)
    {
        return heatInput / ((mass + reservoirMass) * (shc + reservoirSHC));
    }

	public new float GetMass()
	{
		return mass + reservoirMass;
	}

	public new float GetSHC()
	{
		return shc + reservoirSHC;
	}
}
