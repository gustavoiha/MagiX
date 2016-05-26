using UnityEngine;
using System.Collections;
using System.Linq;

public class FootStepsController : MonoBehaviour {

	public AudioClip[] footsteps;

	public string[] tagsToFootsteps;

	public AudioClip[] footstepsFromTags;

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

	private string standingOnTag = "";

	public bool isOnObject {
		get {
			RaycastHit raycastHit;

			Vector3 position = transform.position;
			position.y = GetComponent<CharacterController> ().bounds.min.y + 0.1f;
			float length = isGroundedRayLength + 0.1f;

			/*Physics.Raycast (position, Vector3.down, out raycastHit, length);

			if (raycastHit.transform.gameObject != null) {
				standingOnTag = raycastHit.transform.gameObject.tag;
				return true;
			}*/

			if (Physics.Raycast (position, Vector3.down, out raycastHit, length)) {
				if (!string.IsNullOrEmpty (raycastHit.transform.gameObject.tag)) {
					standingOnTag = raycastHit.transform.gameObject.tag;
					return true;
				}
			}

			return false;
		}
	}

	// Use this for initialization
	void Start () {
		audioSource = gameObject.AddComponent<AudioSource> () as AudioSource;
	}

	public  void PlayNextStep (){

		// Footstep sounds will only play when walking on terrain

		if (isOnObject && tagsToFootsteps.Contains (standingOnTag)) {
			audioSource.clip = footstepsFromTags[System.Array.IndexOf (tagsToFootsteps, standingOnTag) * 2 + stepCounter];
			audioSource.Play ();

			stepCounter = (stepCounter == 0) ? 1 : 0;
			return;
		}

		if (!isOnTerrain)
			return;

		audioSource.clip = footsteps[currentStepSound + stepCounter];
		audioSource.Play ();

		stepCounter = (stepCounter == 0) ? 1 : 0;

		//Debug.Log ("Footstep!! Sound: " + stepCounter.ToString ());
	}
}
