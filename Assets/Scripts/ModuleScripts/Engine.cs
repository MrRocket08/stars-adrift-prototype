using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Engine", menuName = "Engine", order = 4)]
public class Engine : Module
{
    public float fuelConsumption; // fuel consumption in kg/s
    public float eev; // effective exhaust velocity
    public float thrust;
}
