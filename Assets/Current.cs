using UnityEngine;
using System.Collections;

public class Current : MonoBehaviour {
	[SerializeField]
	public float current = 0;
	[SerializeField]
	public Material positiveMaterial;
	[SerializeField]
	public Material negativeMaterial;
	[SerializeField]
	public Material neutralMaterial;
	// Use this for initialization
	void Start () {
		Debug.Log ("I started_ChargeStrength");
		if (current > 0) {
			transform.GetComponent<Renderer>().material = positiveMaterial;
		} else if (current < 0) {
			transform.GetComponent<Renderer>().material = negativeMaterial;
		} else {
			transform.GetComponent<Renderer>().material = neutralMaterial;
		}
		GameObject controller = GameObject.Find ("DisplayController");
		controller.transform.GetComponent<DisplayControllerScript> ().addChargeToArray (transform);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float getCurrent(){
		return current;
	}
}
