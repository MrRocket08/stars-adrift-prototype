using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseMaster : MonoBehaviour
{
    public static float physicsTimeStep = 0.05f;
    public static float universalGravity = 5f;

    CelestialBody[] bodies;

    private void Awake()
    {
        bodies = FindObjectsOfType<CelestialBody>();
        Time.fixedDeltaTime = UniverseMaster.physicsTimeStep;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdateVelocity(bodies, UniverseMaster.physicsTimeStep);
        }

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdatePosition(UniverseMaster.physicsTimeStep);
        }
    }
}
