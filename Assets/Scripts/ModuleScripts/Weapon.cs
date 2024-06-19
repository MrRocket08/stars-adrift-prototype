using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 2)]
public class Weapon : Module
{
	public enum wepType { kinetic, missile, energy }
	public wepType weptype;
	
	public float turnSpeed;
	public float fireRange; // weapon range (in km)
	public int ammo; // current ammo
	public int maxAmmo; // maximum ammo the weapon can have
	public int clip; // current ammo in the clip
	public int clipSize; // how much ammo the clip can hold
	public float fireTime; // time in between firing two shots
	public float reloadTime; // time it takes to reload the clip

	// Track target;	// current target of the weapon (targetList[0])
	// Track[] targetList; // targets it can potentially attack

	// accessor methods
	public wepType GetWepType()
	{
		return weptype;
	}

	public int GetAmmo()
	{
		return ammo;
	}

	public int GetMaxAmmo()
	{
		return maxAmmo;
	}

	public int GetClip()
	{
		return clip;
	}

	public int GetClipSize()
	{
		return clipSize;
	}

	// mutator methods
}