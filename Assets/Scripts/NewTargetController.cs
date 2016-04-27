using UnityEngine;
using System.Collections;

public class NewTargetController : MonoBehaviour {

	private Transform target;

	private Transform oldTarget;

	// max distance for enemy to be detected
	public float maxDistance;

	public const KeyCode targetSwitch = KeyCode.Tab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.Tab))
			UpdateTarget ();
	}

	// Tells TargetController to find nearest available target
	public void UpdateTarget(){

		if (target != null)
			setTargetProjectorState (target, false);

		target = FindNearestTarget ();

		if (target != null)
			setTargetProjectorState (target, true);

	}

	// Checks if there is a target
	public bool HasTarget(){
		return (target != null) ? true : false;
	}

	// Get target transform
	public Transform GetTargetTransform(){
		return target;
	}

	private void setTargetProjectorState(Transform transform, bool newState){
		if (transform == null)
			return;

		OgreProjectorController mOgreProjectorController = transform.gameObject.GetComponentInChildren<OgreProjectorController>() as OgreProjectorController;

		if (mOgreProjectorController != null)
			mOgreProjectorController.setState (newState);
	}

	private Transform FindNearestTarget(){
		Collider[] collisionTargets = Physics.OverlapSphere(transform.position, maxDistance);

		Transform bestTarget = null;

		float closestDistanceSqr = Mathf.Infinity;

		Vector3 currentPosition = transform.position;

		foreach (Collider potentialTargetCollider in collisionTargets){

			Transform potentialTarget = potentialTargetCollider.gameObject.transform;

			Vector3 directionToTarget = potentialTarget.position - currentPosition;

			float dSqrToTarget = directionToTarget.sqrMagnitude;

			if (dSqrToTarget < closestDistanceSqr && potentialTarget.tag.Equals("Enemy")) {
				closestDistanceSqr = dSqrToTarget;
				bestTarget = potentialTarget;
			}

		}

		return bestTarget;
	}
}
