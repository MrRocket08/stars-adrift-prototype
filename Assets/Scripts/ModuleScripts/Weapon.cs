using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 2)]
public class Weapon : Module
{
    public enum wepType { kinetic, missile, energy }
    wepType weptype;

    [SerializeField]
    float turnSpeed;
    [SerializeField]
    float range; // weapon range (in km)
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