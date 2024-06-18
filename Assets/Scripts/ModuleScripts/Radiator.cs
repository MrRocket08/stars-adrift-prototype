using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Radiator", menuName = "Radiator", order = 5)]
public class Radiator : Module
{
    [SerializeField]
    float radiatedHeat; // how much heat the radiators wick away
    [SerializeField]
    float deployTime; // how long it takes to retract/extend the rads
    [SerializeField]
    float vulnerability; // how much more vulnerable the radiators are when deployed compared to when stowed
    bool isDeployed;

    public bool IsDeployed()
    {
        return isDeployed;
    }

    // mutator methods
    public void RadiateHeat()
    {
        temperature += ChangeTemp(-radiatedHeat);
    }

    public void Deploy()
    {
        isDeployed = true;
    }

    public void Undeploy()
    {
        isDeployed = false;
    }
}