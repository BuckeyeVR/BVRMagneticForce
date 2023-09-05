using UnityEngine;
using System.Collections;

public class MoveAlongCurve : MonoBehaviour
{

    public Camera camera;
    public BezierCurve curve;
	public float duration = 2;

	private float progress;

    void Start()
    {
        progress = 0;
    }

    // move along curve and follow its direction.
	private void Update () {
		progress += Time.deltaTime / duration;
		if (progress > 1f) {
			progress = 1f;
		}
        transform.localPosition = curve.GetPoint(progress);
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, curve.GetDirection(progress));
	}
}
