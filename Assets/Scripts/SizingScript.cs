using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SizingScript : MonoBehaviour
{
    public GameObject soi;

    public float mass;
    public float radius;
    CelestialBody thisCB;
    CelestialBody sun;

    public float soiRad;

    private void Awake()
    {
        thisCB = GetComponent<CelestialBody>();
        mass = thisCB.mass;
        radius = thisCB.radius;
        sun = GameObject.Find("Sun").GetComponent<CelestialBody>();
    }

    private void Update()
    {
        if (thisCB != sun)
        {
            soiRad = Vector2.Distance(transform.position, sun.transform.position) * Mathf.Pow(mass / sun.mass, (float)2 / 5) / radius;
            soi.transform.localScale = new Vector2(2 * soiRad, 2 * soiRad);
        }
    }
}
