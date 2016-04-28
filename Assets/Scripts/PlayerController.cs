using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//public Transform Saber;

	private Animator animator;
	private Rigidbody rigidBody;
	private SkillsController skillsController;
	private NewTargetController targetController;

	public float moveSpeedFoward = 6.0f;
	public float moveSpeedSides  = 6.0f;
	public float turnSpeedX = 60.0f;
	//public float turnSpeedY = 60.0f;

	private int mouseInvertX = 1;
	private int mouseInvertY = -1;

	private Vector3 moveDirection = Vector3.zero;
	//public float gravity = 20.0f;

	private int cheatStartBoss = 0;

	// Use this for initialization
	void Start () {
		animator  		 = gameObject.GetComponentInChildren<Animator> ();
		rigidBody 		 = gameObject.GetComponent<Rigidbody> ();
		skillsController = gameObject.GetComponent<SkillsController> ();
		targetController = gameObject.GetComponent<NewTargetController> ();
	}

	// Update is called once per frame
	void Update () {

		/**
		 * Movement animations
		 */
		if (Input.GetKeyDown("w")){
			animator.SetBool("isWalking", true);
		}

		if (Input.GetKeyUp("w")){
			animator.SetBool("isWalking", false);
		}
		//else if (Input.GetKeyUp("s"){
		// walk backwards
		//}

		/*
		 * Skills
		 */

		if (Input.GetKey (skillsController.magicOne)) {
			skillsController.UseSkill (SkillsController.LIGHT_ARROW);
			//animator.SetInteger(useSkill, 1);
		}

		if (Input.GetKeyDown (skillsController.magicTwo)) {
			skillsController.UseSkill (SkillsController.LIGHT_BALL);
			//animator.SetInteger(useSkill, 2);
		}

		if (Input.GetKeyDown (skillsController.magicThree)) {
			skillsController.UseSkill (SkillsController.LIGHT_CROSS);
			//animator.SetInteger(useSkill, 3);
		}

		if (Input.GetKeyDown (skillsController.magicFour)) {
			skillsController.UseSkill (SkillsController.LIGHT_SANCTUARY);
			//Aanimator.SetInteger(useSkill, 4);
		}

		if (Input.GetKeyDown (skillsController.magicFive)) {
			skillsController.UseSkill (SkillsController.ESSENCE_STEALER);
		}

		if (Input.GetKeyDown (NewTargetController.targetSwitch)) {
			targetController.UpdateTarget ();
		}

		if (Input.GetKeyDown ("escape"))
			PauseMenu.isPaused = !PauseMenu.isPaused;

		if (Input.GetKeyDown ("space"))
			gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3(0, 800.0f, 0));

		doTranslation ();
		doRotation ();

		/**
		 * Cheat !!!
		 */
		if (Input.GetKeyDown (KeyCode.Backspace))
			cheatStartBoss++;

		if (cheatStartBoss == 8) {
			//Debug.Log ("cheat!");
			GameObject.FindGameObjectWithTag ("BossShield").GetComponent<BossShieldScript> ().startBoss ();

			foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ItemLightBeacon")){
				Destroy (obj);
			}

			cheatStartBoss++;
		}
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

	// Returns true if player is in walking or running animation
	//private bool shouldMove(){
	//	return animator.GetCurrentAnimatorStateInfo (0).IsName ("Walking") ||
	//		   animator.GetCurrentAnimatorStateInfo (0).IsName ("Running");
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
