using UnityEngine;
using System.Collections;

public class ArraySpawnerCartesian : MonoBehaviour {
    [SerializeField]
    Transform thing;
	[SerializeField]
	float xmax;
	[SerializeField]
	float xIncrementor;
	[SerializeField]
	float ymax;
	[SerializeField]
	float yIncrementor;
	[SerializeField]
	float zmax;
	[SerializeField]
	float zIncrementor;

    // Use this for initialization
    void Start () {
	//	Debug.Log ("I started_arr");
	//	for (int radInc = 0; radInc < radii.Length; radInc++) {
	//		for (float z = zmin; z < zmax; z+=zIncrementor) {
	//			for (int phi=0; phi < 360; phi+=phiIncrementor) {
	//				Instantiate (thing, new Vector3 (radii[radInc] * Mathf.Cos (phi * Mathf.Deg2Rad), z, radii[radInc] * Mathf.Sin (phi * Mathf.Deg2Rad)), Quaternion.identity);
		Debug.Log ("I started_arr");
		for (float x = -xmax; x <= xmax; x = x+xIncrementor) {
			for (float y = -ymax; y <= ymax-yIncrementor; y = y+yIncrementor) {
				for (float z = -zmax; z <= zmax; z = z+zIncrementor) {
					Instantiate (thing, new Vector3 (x, y, z), Quaternion.identity);

				}
			}
		}
	}
}