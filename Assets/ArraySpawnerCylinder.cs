using UnityEngine;
using System.Collections;

public class ArraySpawnerCylinder : MonoBehaviour {
    [SerializeField]
    Transform thing;
	[SerializeField]
	float[] radii;
	[SerializeField]
	float zmin;
	[SerializeField]
	float zmax;
	[SerializeField]
	float zIncrementor;
	[SerializeField]
	int phiIncrementor;
	public float zOffSet=0f;

    // Use this for initialization
    void Start () {
		Debug.Log ("I started_arr");
		for (int radInc = 0; radInc < radii.Length; radInc++) {
			for (float z = zmin; z <= zmax; z+=zIncrementor) {
				for (int phi=0; phi < 360; phi+=phiIncrementor) {
					Instantiate (thing, new Vector3 (radii[radInc] * Mathf.Cos (phi * Mathf.Deg2Rad), z,radii[radInc] * Mathf.Sin (phi * Mathf.Deg2Rad)-zOffSet), Quaternion.identity);
				}
			}
		}
	}
}