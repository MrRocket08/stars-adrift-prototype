using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Engine", menuName = "Engine", order = 4)]
public class Engine : Module
{
    [SerializeField]
    float fuelConsumption; // fuel consumption in kg/s
    [SerializeField]
    float eev; // effective exhaust velocity
    [SerializeField]
    float thrust;
}
