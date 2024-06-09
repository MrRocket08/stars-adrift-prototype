using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class OrbitRenderer : MonoBehaviour
{
	public int numVertices = 120;

	public Vector2 currentManeuverDir;
	public float currentManeuverMag;

	struct VirtualShip
	{
		public Vector2 position;
		public Vector2 velocity;
		public float mass;

		public VirtualShip(Vector2 _position, Vector2 _velocity, float _mass)
		{
			position = _position;
			velocity = _velocity;
			mass = _mass;
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
		if(Application.isPlaying)
		{
			HideOrbits();
		}
	}

	private void Update()
	{
		if(!Application.isPlaying)
		{
			RenderOrbits();
		}
	}

	private void RenderOrbits()
	{
		Ship[] ships = FindObjectsOfType<Ship>();
		var renderPoints = new Vector2[ships.Length][];

		for (int i = 0; i < ships.Length; i++)
		{
			VirtualShip vs = new VirtualShip((Vector2)ships[i].transform.position, (Vector2)ships[i].GetComponent<Rigidbody2D>().velocity, ships[i].mass);

			renderPoints[i] = new Vector2[numVertices];

			Vector2 newPos = vs.position;
			renderPoints[i][0] = newPos;

			for (int vertex = 1; vertex < numVertices; vertex++)
			{
				newPos = Vector2.zero;

				newPos += vs.velocity * UniverseMaster.physicsTimeStep;

				renderPoints[i][vertex] = newPos;

				vs.velocity = CalculateVelocity(vs);
			}
		}
	}

	private Vector2 CalculateVelocity(VirtualShip _vs)
	{
		Vector2 currentVelocity = _vs.velocity;

		return Vector2.zero;
	}

	private void HideOrbits()
	{

	}
}
