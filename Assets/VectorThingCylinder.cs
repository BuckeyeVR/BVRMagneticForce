using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VectorThingCylinder : MonoBehaviour {
    private GameObject [] charges;
	private Vector3 [] oldPos;
	private bool didChange = true;
	private GameObject [] otherVectors; 
	private float yscale;
	private float chargeStepsFloat=10.0f;
	private int chargeStepsInt = 10;
	private Vector3 [] chargeCenters;
	//private Vector3 origin;
	//private float jtemp=5.0f;

	
    // Use this for initialization
    void Start () {
		//Debug.Log ("I started_VectorThing");
        //transform.GetChild(0).localScale = new Vector3(0.15f, 1f, 0.15f);
		charges = GameObject.FindGameObjectsWithTag ("Charge");
		didChange = true;
		otherVectors = GameObject.FindGameObjectsWithTag ("Vector");
		oldPos = new Vector3[charges.Length];
		for(int i=0; i<otherVectors.Length;i++){
			if((transform.root.position == otherVectors[i].transform.root.position) && (transform.root != otherVectors[i].transform.root)){
				Destroy(otherVectors[i]);
			}
		}
    }

	// Update is called once per frame
	void Update () {
		if(didChange==true){
			didChange=false;
			Vector3 realVector = new Vector3 ();
			for(int i=0; i<charges.Length; i++){
				yscale = charges[i].transform.localScale.y;
				//origin = charges[i].transform.position;
				chargeCenters = new Vector3[chargeStepsInt];
				for(int j = 0; j < chargeStepsInt; j++){
					//jtemp = 5.0f;
					//chargeCenters[j] = new Vector3 (charges[i].transform.position.x,charges[i].transform.position.y-yscale/2.0f+yscale*j/(2.0f*chargeStepsFloat),charges[i].transform.position.z);
					chargeCenters[j] = new Vector3 (charges[i].transform.position.x,charges[i].transform.position.y-yscale/2.0f+yscale*j/(1.0f*chargeStepsFloat),charges[i].transform.position.z);
					oldPos[i] = charges[i].transform.position;
					float kq = charges[i].transform.gameObject.GetComponent<ChargeStrength>().getChargeStrength();
					//kq = kq/chargeStepsFloat;
					float r = Mathf.Sqrt(((transform.position.x - chargeCenters[j].x)*(transform.position.x - chargeCenters[j].x))+((transform.position.y - chargeCenters[j].y)*(transform.position.y - chargeCenters[j].y))+((transform.position.z - chargeCenters[j].z)*(transform.position.z - chargeCenters[j].z)));
					Vector3 tmp = new Vector3(((kq/(r*r))*((transform.position.x - chargeCenters[j].x)/r)),((kq/(r*r))*((transform.position.y - chargeCenters[j].y)/r)),((kq/(r*r))*((transform.position.z - chargeCenters[j].z)/r)));
					realVector.x += tmp.x;
					realVector.y += tmp.y;
					realVector.z += tmp.z;
				}
			}
			transform.LookAt(realVector);
			transform.Rotate(90, 0, 0);
			if(float.IsNaN(realVector.magnitude)==false){
				transform.GetChild(0).localScale = new Vector3(0.15f, realVector.magnitude/1500, 0.15f);
			}
			else{
				transform.GetChild(0).localScale = new Vector3(0f, 0f, 0f);
			}
		}
		else{
			for(int i=0; i<charges.Length;i++){
				if(charges[i].transform.position == oldPos[i]){
					//nothing changed
				}
				else{
					didChange=true;
					break;
				}
			}
		}
	}
}
