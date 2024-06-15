using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    public bool operating = true;
    public float passiveHeat;
    public float activeHeat;

    public float GenerateHeat()
    {
        if (operating)
        {
            return activeHeat;
        } else
        {
            return passiveHeat;
        }
    }
}

public class Radiator : Module
{
    public float radiatedHeat;

    public float RadiateHeat()
    {
        return radiatedHeat;
    }
}
