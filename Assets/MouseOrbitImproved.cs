﻿using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{
	private bool allowScroll =false;

	public Transform target;
	private float distance = 2.6f;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;

	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	public float distanceMin = .5f;
	public float distanceMax = 15f;

	private Rigidbody rigidbody;

	float x = 0.0f;
	float y = 0.0f;

	// Use this for initialization
	void Start()
	{
		Debug.Log ("I started_MouseOrbit");
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		rigidbody = GetComponent<Rigidbody>();

		// Make the rigid body not change rotation
		if (rigidbody != null)
		{
			rigidbody.freezeRotation = true;
		}
	}

	void Update()
	{
		//if (target && Input.GetKey(KeyCode.LeftAlt))
		bool motionSelect = Input.GetKey(KeyCode.LeftAlt) ||Input.GetMouseButton(0);
		if (target && motionSelect)
		{
			if(distance != 0 ){
				x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
			}
			else{
				x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			}
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
		}
		y = ClampAngle(y, yMinLimit, yMaxLimit);

		Quaternion rotation = Quaternion.Euler(y, x, 0);



		RaycastHit hit;
		if (Physics.Linecast(target.position, transform.position, out hit))
		{
			//  distance -= hit.distance;//what does this do?
		}
		Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
		Vector3 position = rotation * negDistance + target.position;

		transform.rotation = rotation;
		transform.position = position;
		if (allowScroll) {
			distance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
		}
	}


	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}