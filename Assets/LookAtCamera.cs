using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
	private int updatesWithoutCam=0;
	[SerializeField]
    Transform myCam;
	// Use this for initialization
	void Start () {
		Debug.Log ("I started_LookAtCam");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (myCam != null) {
			transform.LookAt (myCam);
			transform.Rotate (0, 90, 0);
		} 
		else {
			updatesWithoutCam++;
			if(updatesWithoutCam>=6){
				newCam(GameObject.Find ("Main Camera").transform);
			}
		}
    }

	public void newCam(Transform newCam){
		myCam = newCam;
	}
}
