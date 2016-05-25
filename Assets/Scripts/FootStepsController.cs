using UnityEngine;
using System.Collections;

public class FootStepsController : MonoBehaviour {

	public AudioClip[] footSteps;

	private AudioSource audioSource;

	private int stepCounter = 0;

	//private bool isMoving = false;

	//0, 2, 4 ... bacause each step sound has two audio clips (left and right feet)
	private int CurrentStepSound = 0;
	public  int currentStepSound {
		get {
			return CurrentStepSound;
		}

		set {
			CurrentStepSound = value;
		}
	}

	// Check if above terrain
	private float isGroundedRayLength = 0.3f;
	public LayerMask layerMaskForTerrain;
	public bool isOnTerrain {
		get {
			Vector3 position = transform.position;
			position.y = GetComponent<CharacterController> ().bounds.min.y + 0.1f;
			float width = GetComponent<CharacterController> ().bounds.size.x;
			float length = isGroundedRayLength + 0.1f;
			Debug.DrawRay (position, Vector3.down * length);
			bool grounded = Physics.Raycast (position, Vector3.down, length, layerMaskForTerrain.value)
				|| Physics.Raycast (position + transform.right * width, Vector3.down, length + width / 2.0f, layerMaskForTerrain.value)
				|| Physics.Raycast (position - transform.right * width, Vector3.down, length + width / 2.0f, layerMaskForTerrain.value)
				|| Physics.Raycast (position + transform.forward * width, Vector3.down, length + width / 2.0f, layerMaskForTerrain.value)
				|| Physics.Raycast (position - transform.forward * width, Vector3.down, length + width / 2.0f, layerMaskForTerrain.value);
			return grounded;
		}
	}

	// Use this for initialization
	void Start () {
		audioSource = gameObject.AddComponent<AudioSource> () as AudioSource;
	}

	public  void PlayNextStep (){

		// Footstep sounds will only play when walking on terrain
		if (!isOnTerrain)
			return;

		audioSource.clip = footSteps[currentStepSound + stepCounter];
		audioSource.Play ();

		stepCounter = (stepCounter == 0) ? 1 : 0;

		//Debug.Log ("Footstep!! Sound: " + stepCounter.ToString ());
	}
}
