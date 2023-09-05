using UnityEngine;
using System.Collections;

public class PictureLookAtCamera : MonoBehaviour {
	[SerializeField]
	Transform focusPoint;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.LookAt(focusPoint);
		transform.Rotate(90, 0, 0);
	}
}
