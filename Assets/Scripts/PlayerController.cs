using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//public Transform Saber;

	private Animator animator;
	private Rigidbody rigidBody;

	public float moveSpeedFoward = 6.0f;
	public float moveSpeedSides  = 6.0f;
	public float turnSpeedX = 60.0f;
	//public float turnSpeedY = 60.0f;

	private int mouseInvertX = 1;
	private int mouseInvertY = -1;

	private Vector3 moveDirection = Vector3.zero;
	//public float gravity = 20.0f;

	// Use this for initialization
	void Start () {
		animator  = gameObject.GetComponentInChildren<Animator> ();
		rigidBody = gameObject.GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {

		//if (Input.GetKeyDown ("b") && !animator.GetBool("hasSaber")) {
			//Instantiate (Saber, Vector3.zero, Quaternion.identity);
			//animator.SetInteger ("AnimParam", 2);
		//}

		/**
		 * Movement animations
		 */
		if (Input.GetKeyDown("w")){
			animator.SetBool("isWalking",true);
		}

		if (Input.GetKeyUp("w")){
			animator.SetBool("isWalking",false);
		}
		//else if (Input.GetKeyUp("s"){
		// walk backwards
		//}

		//if (Input.GetKeyDown ("w")/* && !isDrawingSaber*/) {
		//	animator.SetInteger ("AnimParam", 1);
		//}
		if (Input.GetKeyDown ("escape"))
			PauseMenu.isPaused = !PauseMenu.isPaused;

		if (Input.GetKeyDown ("space"))
			gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3(0,800.0f,0));

		doTranslation ();
		doRotation ();
	}

	private void doTranslation(){
		//if (shouldMove()) {
			//if (controler.isGrounded) {
			moveDirection = Vector3.zero;
			moveDirection += transform.forward * Input.GetAxis ("VerticalTranslation")   * moveSpeedFoward;
			moveDirection += transform.right   * Input.GetAxis ("HorizontalTranslation") * moveSpeedSides;

			transform.position += moveDirection * Time.deltaTime;
			//}
		//}
	}

	private void doRotation(){

		// Stoping player from rotating automatially because of the rigdbody component
		rigidBody.angularVelocity = new Vector3(0,0,0);

		// Making sure the player is always perpendicular
		Quaternion quaternion  = new Quaternion ();
		quaternion.eulerAngles = new Vector3 (0, transform.rotation.eulerAngles.y, 0);

		float turnX = Input.GetAxis ("Mouse X") * Mathf.Sign(mouseInvertX) + Input.GetAxis ("HorizontalRotation");

		transform.rotation = quaternion;
		transform.Rotate (0, turnX * turnSpeedX * Time.deltaTime, 0);
	}

	// returns true if player is in walking or running animation
	//private bool shouldMove(){
	//	return animator.GetCurrentAnimatorStateInfo (0).IsName ("walking") ||
	//		animator.GetCurrentAnimatorStateInfo (0).IsName ("running") ||
	//		animator.GetCurrentAnimatorStateInfo (0).IsName ("walkingSaber") ||
	//		animator.GetCurrentAnimatorStateInfo (0).IsName ("runningSaber");
	//}

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
