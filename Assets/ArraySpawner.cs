using UnityEngine;
using System.Collections;

public class ArraySpawner : MonoBehaviour {
    [SerializeField]
    Transform thing;
	[SerializeField]
	float[] radii;
	[SerializeField]
	int thetaIncrementor;
	[SerializeField]
	int phiIncrementor;

    // Use this for initialization
    void Start () {
		Debug.Log ("I started_arr");
		for (int radInc = 0; radInc < radii.Length; radInc++) {
			for (int theta = 0; theta < 360; theta+=thetaIncrementor) {
				for (int phi=0; phi < 360; phi+=phiIncrementor) {
					Instantiate (thing, new Vector3 (radii[radInc] * Mathf.Sin (theta * Mathf.Deg2Rad) * Mathf.Cos (phi * Mathf.Deg2Rad), radii[radInc] * Mathf.Sin (theta * Mathf.Deg2Rad) * Mathf.Sin (phi * Mathf.Deg2Rad), radii[radInc] * Mathf.Cos (theta * Mathf.Deg2Rad)), Quaternion.identity);
				}
			}
		}
	}
}