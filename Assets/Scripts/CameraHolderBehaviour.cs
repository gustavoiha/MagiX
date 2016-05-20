using UnityEngine;
using System.Collections;

public class CameraHolderBehaviour : MonoBehaviour {

	private Transform player;

	// Height of the center of the camera's rotation (for example, the player body's center
	public float CameraPivot = 2.5f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = player.position + player.up * CameraPivot;
	}
}
