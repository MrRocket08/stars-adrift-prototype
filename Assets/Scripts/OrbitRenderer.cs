using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class OrbitRenderer : MonoBehaviour
{
	public int numVertices = 120;

	public float width;

	Ship[] ships;
	Vector3[][] renderPastPoints;
	Vector3[][] renderFuturePoints;
	CelestialBody sun;

	class VirtualShip
	{
		public Vector2 position;
		public Vector2 velocity;
		public float mass;

		public VirtualShip(Ship ship)
		{
			position = ship.transform.position;
			velocity = ship.GetComponent<Rigidbody2D>().velocity;
			mass = ship.wetMass;
		}

		public void setPos(Vector2 _position)
		{
			position = _position;
		}

		public void setVel(Vector2 _velocity)
		{
			velocity = _velocity;
		}
	}

	private void Start()
	{
		ships = FindObjectsOfType<Ship>();
		sun = GameObject.Find("Sun").GetComponent<CelestialBody>();

		if(!Application.isPlaying)
		{
			HideOrbits();
		}
	}

	private void Update()
	{
		if(Application.isPlaying)
		{
			RenderOrbits();
		}
	}

	private void RenderOrbits()
	{
		renderFuturePoints = new Vector3[ships.Length][];

		for (int i = 0; i < ships.Length; i++)
		{
			VirtualShip vs = new VirtualShip(ships[i]);

			renderFuturePoints[i] = new Vector3[numVertices];

			Vector2 newPos = vs.position;
			renderFuturePoints[i][0] = newPos;

			for (int vertex = 1; vertex < numVertices; vertex++)
			{
				newPos = renderFuturePoints[i][vertex - 1];

				newPos += vs.velocity * UniverseMaster.physicsTimeStep;

				renderFuturePoints[i][vertex] = newPos;

				vs.velocity += CalculateVelocity(vs, ships[i].GetManeuver()) * UniverseMaster.physicsTimeStep;
			}
		}

		for (int shipIndex = 0; shipIndex < ships.Length; shipIndex++)
		{
			//var pathColour = ships[shipIndex].gameObject.GetComponent<SpriteRenderer>().color;

            var lineRenderer = ships[shipIndex].gameObject.GetComponent<LineRenderer>();
            lineRenderer.enabled = true;
            lineRenderer.positionCount = renderFuturePoints[shipIndex].Length;
            lineRenderer.SetPositions(renderFuturePoints[shipIndex]);
            //lineRenderer.startColor = pathColour;
            //lineRenderer.endColor = pathColour;
            lineRenderer.widthMultiplier = width;
        }
	}

	private Vector2 CalculateVelocity(VirtualShip _vs, Ship.maneuver man)
	{
		return man.direction * man.magnitude;
	}

    private void HideOrbits()
    {
        Ship[] ships = FindObjectsOfType<Ship>();

        // Draw paths
        for (int shipIndex = 0; shipIndex < ships.Length; shipIndex++)
        {
            var lineRenderer = ships[shipIndex].gameObject.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
        }
    }
}
