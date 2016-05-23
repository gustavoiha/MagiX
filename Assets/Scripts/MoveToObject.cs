using UnityEngine;
using System.Collections;

public class MoveToObject : MonoBehaviour {

	public string tagsToFollow;

	public bool useTrigger = true;

	public float moveSpeed = 20.0f;

	public Vector3 wobbleAmplitude;

	public Vector3 wobbleFrequency;

	private Vector3 previousWobble;

	private bool follow = false;

	public Vector3 targetPivot;

	private Transform target;

	// Use this for initialization
	void Start () {
		follow = !useTrigger;

		previousWobble = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!follow)
			return;

		Vector3 direction = target.position + targetPivot - transform.position;
		direction.Normalize ();

		Vector3 wobble = Vector3.zero;
		wobble.x = wobbleAmplitude.x * Mathf.Sin (Time.time * wobbleFrequency.x);
		wobble.y = wobbleAmplitude.y * Mathf.Sin (Time.time * wobbleFrequency.y);
		wobble.z = wobbleAmplitude.z * Mathf.Sin (Time.time * wobbleFrequency.z);

		transform.position += direction * moveSpeed * Time.deltaTime + wobble - previousWobble;

		previousWobble = wobble;

	}

	void OnTriggerEnter (Collider collider){
		
		if (!tagsToFollow.Contains (collider.tag))
			return;

		target = collider.gameObject.transform;
		follow = true;
	}
}
