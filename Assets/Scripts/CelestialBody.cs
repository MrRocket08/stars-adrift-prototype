using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    [Range(1f, 20000f)]
    public float mass;
    [Range(.5f, 30f)]
    public float radius;
    public Vector2 initialVelocity;
    public SizingScript sc;

    protected Vector2 currentVelocity;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().mass = mass;
        currentVelocity = initialVelocity;

        sc = GetComponent<SizingScript>();

        transform.localScale = new Vector2(2 * radius, 2 * radius);
    }

    public void UpdateVelocity(CelestialBody[] allBodies, float timeStep)
    {
        foreach (var body in allBodies)
        {
            if (body != this)
            {
                float sqrDist = (body.transform.position - transform.position).sqrMagnitude;
                Vector2 forceDir = (body.transform.position - transform.position).normalized;
                Vector2 force = body.mass * UniverseMaster.universalGravity * mass * forceDir / sqrDist;
                Vector2 accel = force / mass;
                currentVelocity += accel * timeStep;
            }
        }
    }

    public void UpdatePosition (float timeStep)
    {
        GetComponent<Rigidbody2D>().position += currentVelocity * timeStep;
    }

    private void OnValidate()
    {
        GetComponent<Rigidbody2D>().mass = mass;
        transform.localScale = new Vector2(2 * radius, 2 * radius);

        sc.radius = radius;
        sc.mass = mass;
    }
}
