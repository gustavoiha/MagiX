using UnityEngine;
using System.Collections;

public class CameraHolderBehaviour : MonoBehaviour {

	private Transform player;

	// Height of the center of the camera's rotation (for example, the player body's center
	public float CameraPivot = 2.5f;

	// Camera rotation limits. Upper limit should be positive, and Lower limit should be negative!
	public float CameraAngleLimitUpper = 85.0f;
	public float CameraAngleLimitLower = -10.0f;

	public float minTurnSpeed = 20.0f;
	public float maxTurnSpeed = 100.0f;

	private float turnSpeed;

	// Use this for initialization
	void Start () {

		turnSpeed = (minTurnSpeed + maxTurnSpeed) / 2.0f;

		player = GameObject.FindGameObjectWithTag ("Player").transform;
		transform.position = player.position + player.up * CameraPivot;
		transform.rotation = player.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = player.position + player.up * CameraPivot;

		transform.Rotate (Vector3.up * turnSpeed * Input.GetAxis ("HorizontalRotation") * Time.deltaTime, Space.World);

		if ((Input.GetAxis ("VerticalRotation") > 0 && transform.rotation.eulerAngles.x >= CameraAngleLimitUpper && transform.rotation.eulerAngles.x < 180.0f) ||
			(Input.GetAxis ("VerticalRotation") < 0 && transform.rotation.eulerAngles.x <= 360.0f + CameraAngleLimitLower && transform.rotation.eulerAngles.x >= 180.0f))
			return;

		transform.Rotate (Vector3.right * turnSpeed * Input.GetAxis ("VerticalRotation") * Time.deltaTime);

	}

	public void AdjustTurnSpeed (float newSpeedPercent){
		turnSpeed = minTurnSpeed + newSpeedPercent * (maxTurnSpeed - minTurnSpeed);
	}
}
