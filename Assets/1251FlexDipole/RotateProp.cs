using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateProp : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
/*		transform.Rotate(
			xSpeed * Time.deltaTime,
			ySpeed * Time.deltaTime,
			zSpeed * Time.deltaTime
		);*/
		transform.Rotate(
			0f,
			200f* Time.deltaTime,
			0f
		 
		);
		
	}
}
