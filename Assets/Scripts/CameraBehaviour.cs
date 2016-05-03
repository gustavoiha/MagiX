using UnityEngine;
using System.Collections;

/**
*
* Camera control
*
**/

public class CameraBehaviour : MonoBehaviour {

	// Height of the center of the camera's rotation (for example, the player body's center
	public float CameraPivot = 2.5f;

	public float CameraAngle = 35.0f;

	// Camera rotation limits. Upper limit should be positive, and Lower limit should be negative!
	public float CameraAngleLimitUpper = 85.0f;
	public float CameraAngleLimitLower = -10.0f;

	// Camera's distance from Pivot
	public float CameraDistance = 9.5f;

	// switch these values so the mouse's movement turns the camera in the other direction
	private int mouseInvertX = 1;
	private int mouseInvertY = -1;

	// Camera's rotation speed in the X axis
	public float turnSpeedX = 80.0f;
	public float turnSpeedY = 80.0f;

	// Use this for initialization
	void Start () {

		float[] distances = gameObject.GetComponent<Camera>().layerCullDistances;

		for (int i = 0; i < distances.Length; i++){
			distances[i] = 800;
		}

		distances[15] = 10000;

		gameObject.GetComponent<Camera>().layerCullDistances = distances;

	}

	void Update(){

		UpdateCameraCoordinatesRegular ();
	}

	public void UpdateCameraCoordinatesRegular (){

		transform.rotation = Quaternion.LookRotation (- transform.localPosition);


		float turnX = Input.GetAxis ("VerticalRotation");
		float turnY = Input.GetAxis ("HorizontalRotation");

		gameObject.transform.Rotate (turnX * turnSpeedX * Time.deltaTime, turnY * turnSpeedY * Time.deltaTime, 0);

		// Angle to limit the camera's rotation
		float newCamAngleX;

		if (gameObject.transform.localRotation.eulerAngles.x <= 180)
			newCamAngleX = Mathf.Min (transform.localRotation.eulerAngles.x, CameraAngleLimitUpper);
		else
			newCamAngleX = Mathf.Max (transform.localRotation.eulerAngles.x, 360 + CameraAngleLimitLower);

		//float moveX = Input.GetAxis ("HorizontalTranslation");
		//float moveZ = Input.GetAxis ("VerticalTranslation");

		//float extraAngleY = - Mathf.Atan2 (moveX, moveZ) * Mathf.Rad2Deg;

		Quaternion quaternion  = new Quaternion ();
		quaternion.eulerAngles = new Vector3 (newCamAngleX, transform.rotation.eulerAngles.y, 0);
		transform.localRotation   = quaternion;

		float camPosX = - CameraDistance * Mathf.Sin (transform.localRotation.eulerAngles.y * Mathf.Deg2Rad);
		float camPosY = CameraPivot + CameraDistance * Mathf.Sin (transform.localRotation.eulerAngles.x * Mathf.Deg2Rad);
		float camPosZ = - CameraDistance * Mathf.Cos (transform.localRotation.eulerAngles.x * Mathf.Deg2Rad) * Mathf.Cos (transform.localRotation.eulerAngles.y * Mathf.Deg2Rad);

		//Debug.Log (camera.localRotation.eulerAngles);

		transform.localPosition = new Vector3 (camPosX, camPosY, camPosZ);
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

	/**
	 * Invert mouse directions regarding the camera's rotation
	 * pass "X" as argument to invert horizontal axis, and "Y" for vertical
	 */
	public void invertMouseDirection(string direction){
		if (direction == "X")
			mouseInvertX *= -1;
		else if (direction == "Y")
			mouseInvertY *= -1;
	}
}
