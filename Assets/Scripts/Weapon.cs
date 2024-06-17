using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 1)]
public class Weapon : Module
{
    public enum Side { left, right, center }
    Side side;

    [SerializeField]
    float turnSpeed;
    [SerializeField]
    float range; // weapon range (in km)
    [SerializeField]
    float ammo; // current ammo
    [SerializeField]
    float maxAmmo; // maximum ammo the weapon can have
    [SerializeField]
    float clip; // current ammo in the clip
    [SerializeField]
    float clipSize; // how much ammo the clip can hold
    [SerializeField]
    float fireTime; // time in between firing two shots
    [SerializeField]
    float reloadTime; // time it takes to reload the clip

    // Track target;	// current target of the weapon (targetList[0])
    // Track[] targetList; // targets it can potentially attack

    // accessor methods

    // mutator methods
    public void SetSide(Side _side)
    {
        side = _side;
    }
}