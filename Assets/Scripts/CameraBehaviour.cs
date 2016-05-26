using UnityEngine;
using System.Collections;

/**
*
* Camera control
*
**/

public class CameraBehaviour : MonoBehaviour {

	public float CameraAngle = 35.0f;

	// Camera rotation limits. Upper limit should be positive, and Lower limit should be negative!
	public float CameraAngleLimitUpper = 85.0f;
	public float CameraAngleLimitLower = -10.0f;

	// Camera's distance from Pivot
	public float CameraDistance = 9.5f;

	// Camera's rotation speed in the X axis
	//public float turnSpeedX = 80.0f;
	//public float turnSpeedY = 80.0f;

	public float minTurnSpeed = 20.0f;
	public float maxTurnSpeed = 100.0f;

	private float turnSpeed;

	public bool isStatic = false;

	// Use this for initialization
	void Start () {

		turnSpeed = (minTurnSpeed + maxTurnSpeed) / 2.0f;

		float[] distances = gameObject.GetComponent<Camera>().layerCullDistances;

		for (int i = 0; i < distances.Length; i++){
			distances[i] = 800;
		}

		distances[15] = 10000;

		gameObject.GetComponent<Camera>().layerCullDistances = distances;

		if (!isStatic) {
			transform.localPosition = new Vector3 (CameraDistance, 0, 0);

			transform.rotation = Quaternion.LookRotation (-transform.localPosition);
		}
	}

	void Update(){

		if (!isStatic)
			UpdateCameraCoordinatesRegular ();
	}

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

	}

	public void AdjustTurnSpeed (float newSpeedPercent){
		turnSpeed = minTurnSpeed + newSpeedPercent * (maxTurnSpeed - minTurnSpeed);
	}

	/*private void UpdateCameraCoordinatesRotate (Vector3 playerPosition, Vector3 playerRotation){

		float turnX = Input.GetAxis ("VerticalRotation");

		gameObject.transform.Rotate (turnX * turnSpeedX * Time.deltaTime, 0, 0);

		// Angle to limit the camera's rotation
		float newCamAngleX;

		if (gameObject.transform.localRotation.eulerAngles.x <= 180)
			newCamAngleX = Mathf.Min (transform.localRotation.eulerAngles.x, CameraAngleLimitUpper);
		else
			newCamAngleX = Mathf.Max (transform.localRotation.eulerAngles.x, 360 + CameraAngleLimitLower);

		float moveX = Input.GetAxis ("HorizontalTranslation");
		float moveZ = Input.GetAxis ("VerticalTranslation");

		float extraAngleY = Mathf.Atan2 (moveX, moveZ) * Mathf.Rad2Deg;

		Quaternion quaternion  = new Quaternion ();
		quaternion.eulerAngles = new Vector3 (newCamAngleX, extraAngleY, 0);
		transform.localRotation   = quaternion;

		float camPosX = - CameraDistance * Mathf.Sin (extraAngleY * Mathf.Deg2Rad);
		float camPosY = CameraPivot + CameraDistance * Mathf.Sin (transform.localRotation.eulerAngles.x * Mathf.Deg2Rad);
		float camPosZ = - CameraDistance * Mathf.Cos (transform.localRotation.eulerAngles.x * Mathf.Deg2Rad) * Mathf.Cos (extraAngleY * Mathf.Deg2Rad);

		//Debug.Log (camera.localRotation.eulerAngles);

		transform.localPosition = new Vector3 (0, camPosY, camPosZ);
	}*/

}
