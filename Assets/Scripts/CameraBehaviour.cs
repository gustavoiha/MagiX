using UnityEngine;
using System.Collections;

/**
*
* Camera control
*
**/

public class CameraBehaviour : MonoBehaviour {

	private Transform camera;

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

	// Camera's rotation speed in the Y axis
	public float turnSpeedY = 80.0f;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}

	// Update is called once per frame
	void Update () {
		updateCameraCoordinatesRegular ();
	}

	private void updateCameraCoordinatesRegular (){

		float turnY = Input.GetAxis ("Mouse Y") * Mathf.Sign(mouseInvertY) + Input.GetAxis ("VerticalRotation");

		float camPosY = CameraPivot + CameraDistance * Mathf.Sin(camera.rotation.eulerAngles.x * Mathf.Deg2Rad) * Mathf.Cos(camera.rotation.eulerAngles.z * Mathf.Deg2Rad);
		float camPosZ = -CameraDistance * Mathf.Cos(camera.rotation.eulerAngles.x * Mathf.Deg2Rad) * Mathf.Cos(camera.rotation.eulerAngles.z * Mathf.Deg2Rad);;

		camera.Rotate (turnY * turnSpeedY * Time.deltaTime, 0, 0);
		camera.localPosition = new Vector3 (0, camPosY, camPosZ);

		//Debug.Log (camera.localRotation.eulerAngles);

		// Angle to limit the camera's rotation
		float newCamAngleX;

		if (camera.rotation.eulerAngles.x <= 180)
			newCamAngleX = Mathf.Min (camera.localRotation.eulerAngles.x, CameraAngleLimitUpper);
		else
			newCamAngleX = Mathf.Max (camera.localRotation.eulerAngles.x, 360 + CameraAngleLimitLower);

		Quaternion quaternion = new Quaternion ();
		quaternion.eulerAngles = new Vector3 (newCamAngleX, 0, 0);
		camera.localRotation = quaternion;
	}

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
