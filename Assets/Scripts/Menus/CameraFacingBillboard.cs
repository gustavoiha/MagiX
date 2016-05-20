using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour {

	private Camera myCamera;

	void Start () {
		myCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () {
		transform.LookAt (transform.position + myCamera.transform.rotation * Vector3.forward, myCamera.transform.rotation * Vector3.up);
	}
}
