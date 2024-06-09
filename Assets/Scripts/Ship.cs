using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public enum Alignment { ally, neutral, enemy }

    public float mass;
    public float turnAccel;
    public float mainAccel;
    public float fuel;

    // MANEUVER
    public struct maneuver
    {
        Vector2 direction;
        float magnitude;

        public maneuver(Vector2 _direction, float _magnitude)
        {
            direction = _direction;
            magnitude = _magnitude;
        }
    }

    maneuver[] maneuvers = new maneuver[5];

    Rigidbody2D rb;
    float burnFraction;
    float turnFraction;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        maneuvers[0] = new maneuver(Vector2.up, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        burnFraction = Input.GetAxisRaw("Vertical");
        turnFraction = Input.GetAxis("Horizontal");
        fuel -= Mathf.Abs(burnFraction * mainAccel);
        fuel -= Mathf.Abs(turnFraction);
    }

    void FixedUpdate()
    {
        rb.AddForce(burnFraction * mainAccel * transform.up, ForceMode2D.Force);
        rb.AddTorque(-turnFraction * turnAccel);
    }

    // ACCESSOR METHODS
    public maneuver GetManeuver() { return maneuvers[0]; }
}
