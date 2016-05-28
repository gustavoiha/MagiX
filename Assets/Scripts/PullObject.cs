using UnityEngine;
using System.Collections;

public class PullObject : MonoBehaviour {

	public float speed;

	// Stop pulling if distance is smaller than minDist
	public float minDist = 10.0f;

	public float maxDist = 80.0f;

	public Transform pullObject;
	
	// Update is called once per frame
	void Update () {

		Vector3 delta = transform.position - pullObject.position;

		if (delta.magnitude <= minDist || delta.magnitude >= maxDist)
			return;

		delta.Normalize ();

		pullObject.position += delta * speed * Time.deltaTime;
	}
}
