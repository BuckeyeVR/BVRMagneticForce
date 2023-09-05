using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VectorThingCurrent : MonoBehaviour {
    private GameObject [] currents;
	private Vector3 [] oldPos;
	private bool didChange = true;
	private GameObject [] otherVectors; 
	private float xscale;
	private float yscale;
	private float zscale;
	private float currentStepsFloat=200.0f;
	private int currentStepsInt = 200;
	private Vector3 [] currentCenters;
	private Vector3 [] dls;
	//private Vector3 origin;
	//private float jtemp=5.0f;

	
    // Use this for initialization
    void Start () {
		//Debug.Log ("I started_VectorThing");
        //transform.GetChild(0).localScale = new Vector3(0.15f, 1f, 0.15f);
		currents = GameObject.FindGameObjectsWithTag ("Charge");
		didChange = true;
		otherVectors = GameObject.FindGameObjectsWithTag ("Vector");
		oldPos = new Vector3[currents.Length];
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
			for(int i=0; i<currents.Length; i++){
				xscale = currents[i].transform.localScale.x;
				yscale = currents[i].transform.localScale.y;
				zscale = currents[i].transform.localScale.z;
				currentCenters = new Vector3[currentStepsInt];
				dls = new Vector3[currentStepsInt];
				
				float totalScale = Mathf.Sqrt((xscale*xscale)+(yscale*yscale)+(zscale*zscale));
				for(int j = 0; j < currentStepsInt; j++){
					dls[j] = currents[i].transform.up;
					currentCenters[j] = new Vector3 (currents[i].transform.position.x-(totalScale*dls[j].x/2.0f)+(totalScale*dls[j].x*j/currentStepsFloat),currents[i].transform.position.y-(totalScale*dls[j].y/2.0f)+(totalScale*dls[j].y*j/currentStepsFloat),currents[i].transform.position.z-(totalScale*dls[j].z/2.0f)+(totalScale*dls[j].z*j/currentStepsFloat));
					oldPos[i] = currents[i].transform.position;
					float kq = currents[i].transform.gameObject.GetComponent<ChargeStrength>().getChargeStrength()/currentStepsFloat;
					//float muI = currents[i].transform.gameObject.GetComponent<Current>().getCurrent();
					//kq = kq/currentStepsFloat;
					float r = Mathf.Sqrt(((transform.position.x - currentCenters[j].x)*(transform.position.x - currentCenters[j].x))+((transform.position.y - currentCenters[j].y)*(transform.position.y - currentCenters[j].y))+((transform.position.z - currentCenters[j].z)*(transform.position.z - currentCenters[j].z)));
					//Vector3 tmp = new Vector3(((kq/(r*r))*((transform.position.x - currentCenters[j].x)/r)),((kq/(r*r))*((transform.position.y - currentCenters[j].y)/r)),((kq/(r*r))*((transform.position.z - currentCenters[j].z)/r)));
					float dBx = (currentCenters[j].z-transform.position.z)*dls[j].y-(currentCenters[j].y-transform.position.y)*dls[j].z;
					float dBy = -(currentCenters[j].z-transform.position.z)*dls[j].x+(currentCenters[j].x-transform.position.x)*dls[j].z;
					float dBz = (currentCenters[j].y-transform.position.y)*dls[j].x-(currentCenters[j].x-transform.position.x)*dls[j].y;
					//Vector3 tmp = new Vector3(100f*dls[j].x,100f*dls[j].y,100f*dls[j].z);
					float rcubed = 1f*r*r*r;
					Vector3 tmp = new Vector3(kq*dBx/rcubed,kq*dBy/rcubed,kq*dBz/rcubed);
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
			for(int i=0; i<currents.Length;i++){
				if(currents[i].transform.position == oldPos[i]){
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
