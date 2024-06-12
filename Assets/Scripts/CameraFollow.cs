using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public float FollowSpeed = 5f;
	public Transform target;

	public float maxLeft;
	public float maxRight;
	public float maxUp;
	public float maxDown;

	private Vector3 newPos;
	private Camera cam;
	CelestialBody[] bodies;
	int bodyIndex = 0;

	float scrollMoment;

	void Start()
	{
		cam = Camera.main;
		bodies = FindObjectsOfType<CelestialBody>();
		target = GameObject.Find("Ship").GetComponent<Transform>();

		//target = bodies[bodyIndex].GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		newPos = new Vector3(target.position.x, target.position.y, -1f);

		scrollMoment = Input.GetAxisRaw("Zoom");
		Debug.Log(scrollMoment);

		/*
		if(Input.GetKeyDown("right"))
		{
			if(bodyIndex < bodies.Length - 1)
			{
				bodyIndex++;
				target = bodies[bodyIndex].GetComponent<Transform>();
			} else
			{
				bodyIndex = 0;
				target = bodies[bodyIndex].GetComponent<Transform>();
			}
		}

		if (Input.GetKeyDown("left"))
		{
			if (bodyIndex > 0)
			{
				bodyIndex--;
				target = bodies[bodyIndex].GetComponent<Transform>();
			}
			else
			{
				bodyIndex = bodies.Length - 1;
				target = bodies[bodyIndex].GetComponent<Transform>();
			}
		} */
	}

	void FixedUpdate()
	{
		transform.position = newPos;

		cam.orthographicSize += scrollMoment;

		if(cam.orthographicSize <= 1)
		{
			cam.orthographicSize = 1;
		}
	}
}
