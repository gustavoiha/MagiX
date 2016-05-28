using UnityEngine;
using System.Collections;

/**
*
* Camera control
*
**/

public class CameraBehaviour : MonoBehaviour {

	//public float CameraAngle = 35.0f;

	// Camera rotation limits. Upper limit should be positive, and Lower limit should be negative!
	//public float CameraAngleLimitUpper = 85.0f;
	//public float CameraAngleLimitLower = -10.0f;

	// Camera's distance from Pivot
	public float startingDistance = 9.5f;

	public float minDistance = 4.0f;

	public float maxDistance = 20.0f;

	// Camera's rotation speed in the X axis
	//public float turnSpeedX = 80.0f;
	//public float turnSpeedY = 80.0f;

	//public float minTurnSpeed = 20.0f;
	//public float maxTurnSpeed = 100.0f;

	//private float turnSpeed;

	public bool isStatic = false;

	public float scrollSpeed = 5.0f;

	// Use this for initialization
	void Start () {

		//turnSpeed = (minTurnSpeed + maxTurnSpeed) / 2.0f;

		float[] distances = gameObject.GetComponent<Camera>().layerCullDistances;

		for (int i = 0; i < distances.Length; i++){
			distances[i] = 800;
		}

		distances[15] = 10000;

		gameObject.GetComponent<Camera>().layerCullDistances = distances;

		if (!isStatic) 
			transform.localPosition = new Vector3 (0, 0, -startingDistance);

		//transform.rotation = Quaternion.LookRotation (-transform.localPosition);
	}

	void Update(){

		if (isStatic)
			return;

		Vector3 position = transform.localPosition;

		position.z += Input.GetAxis ("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

		position.z = - Mathf.Max (minDistance, Mathf.Min (maxDistance, - position.z));

		transform.localPosition = position;

	}

/*
	public void UpdateCameraCoordinatesRegular (){

		transform.rotation = Quaternion.LookRotation (- transform.localPosition);

		transform.RotateAround (transform.parent.transform.position, Vector3.up, turnSpeed * Input.GetAxis ("HorizontalRotation") * Time.deltaTime);

		//if ((Input.GetAxis ("VerticalRotation") < 0 || transform.rotation.eulerAngles.x < CameraAngleLimitUpper)
		//	&&
		//	(Input.GetAxis ("VerticalRotation") > 0 || transform.rotation.eulerAngles.x > 360.0f + CameraAngleLimitLower))

		if ((Input.GetAxis ("VerticalRotation") > 0 && transform.rotation.eulerAngles.x >= CameraAngleLimitUpper && transform.rotation.eulerAngles.x < 180.0f) ||
			(Input.GetAxis ("VerticalRotation") < 0 && transform.rotation.eulerAngles.x <= 360.0f + CameraAngleLimitLower && transform.rotation.eulerAngles.x >= 180.0f))
			return;

		transform.RotateAround (transform.parent.transform.position, - transform.right, - turnSpeed * Input.GetAxis ("VerticalRotation") * Time.deltaTime);

	}*/

	//public void AdjustTurnSpeed (float newSpeedPercent){
	//	turnSpeed = minTurnSpeed + newSpeedPercent * (maxTurnSpeed - minTurnSpeed);
	//}

}
