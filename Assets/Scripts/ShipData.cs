using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "ShipData", order = 1)]
public class ShipData : ScriptableObject
{
    [SerializeField]
    List<ModuleSlot> modules = new List<ModuleSlot>();

    public float dryMass;
    public float effectiveExhaustVelocity;

    public float fuel;
    public float maxFuel;

    public float heat;
    public float heatCapacity;

    public float turnAccel;
    public float mainAccel;

    public float conductivity;
}
