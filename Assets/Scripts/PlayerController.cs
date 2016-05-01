using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//public Transform Saber;

	private Animator animator;
	private Rigidbody rigidBody;
	private SkillsController skillsController;
	private TargetController targetController;

	public float moveSpeedFoward = 6.0f;
	public float moveSpeedSides  = 6.0f;
	public float turnSpeedX = 60.0f;
	//public float turnSpeedY = 60.0f;

	private int mouseInvertX = 1;
	private int mouseInvertY = -1;

	private Vector3 moveDirection = Vector3.zero;
	//public float gravity = 20.0f;

	private int cheatStartBoss = 0;

	// States in animator
	private int walkingID;

	// Use this for initialization
	void Start () {
		animator  		 = gameObject.GetComponentInChildren<Animator> ();
		rigidBody 		 = gameObject.GetComponent<Rigidbody> ();
		skillsController = gameObject.GetComponentInChildren<SkillsController> ();
		targetController = gameObject.GetComponent<TargetController> ();

		walkingID = Animator.StringToHash ("isWalking");
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape))
			PauseMenu.isPaused = !PauseMenu.isPaused;

		/**
		 * Movement animations
		 */
		//if (Input.GetKeyDown("w")){
		float HorizontalAxis = Input.GetAxis("VerticalTranslation");

		if (HorizontalAxis != 0.0f) {
			//if (HorizontalAxis > 0.0f)
			animator.SetBool ("isWalking", true);
			//else
			// Move backwards
		} else {
			animator.SetBool("isWalking", false);
		}

		//else if (Input.GetKeyUp("s"){
		// walk backwards
		//}

		/*
		 * Skills
		 */

		// Basic Attack
		if (Input.GetKey (skillsController.basicAttack) && skillsController.CanUseSkill(SkillsController.BASIC_ATTACK)) {
			//skillsController.UseSkill (SkillsController.BASIC_ATTACK);
			animator.SetInteger ("skill", 1);
		}

		if (Input.GetKeyUp (skillsController.basicAttack))
			animator.SetInteger ("skill", 0);

		// Light Arrow
		if (Input.GetKey (skillsController.magicOne) && skillsController.CanUseSkill(SkillsController.LIGHT_ARROW)) {
			animator.SetInteger ("skill", 2);
		}

		if (Input.GetKeyUp (skillsController.magicOne))
			animator.SetInteger ("skill", 0);

		// Light Ball
		if (Input.GetKeyDown (skillsController.magicTwo) && skillsController.CanUseSkill(SkillsController.LIGHT_BALL)) {
			skillsController.UseSkill (SkillsController.LIGHT_BALL);
			animator.SetInteger ("skill", 3);
		}

		if (Input.GetKeyUp (skillsController.magicTwo))
			animator.SetInteger ("skill", 0);

		// Sanctuary
		if (Input.GetKeyDown (skillsController.magicThree) && skillsController.CanUseSkill(SkillsController.LIGHT_SANCTUARY)) {
			//skillsController.UseSkill(SkillsController.LIGHT_SANCTUARY);
			animator.SetInteger ("skill", 5);
		}

		//if (Input.GetKeyDown (skillsController.magicThree))
		//	animator.SetInteger ("skill", 0);

		// Defense ball
		if (Input.GetKeyDown (skillsController.magicFour) && skillsController.CanUseSkill(SkillsController.DEFENCE_DOME)) {
			skillsController.UseSkill (SkillsController.DEFENCE_DOME);
		}

		//if (Input.GetKeyUp (skillsController.magicFour))
		//	animator.SetInteger ("skill", 0);

		if (Input.GetKeyDown (TargetController.targetSwitch)) {
			targetController.UpdateTarget ();
		}

		if (Input.GetKeyDown ("space"))
			gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3(0, 800.0f, 0));

		// Mode if animator is in walking mode
		if (animator.GetBool (walkingID))
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

	private void doTranslation (){
		//if (shouldMove()) {
			//if (controler.isGrounded) {
			moveDirection = Vector3.zero;
			moveDirection += transform.forward * Input.GetAxis ("VerticalTranslation")   * moveSpeedFoward;
			moveDirection += transform.right   * Input.GetAxis ("HorizontalTranslation") * moveSpeedSides;

			transform.position += moveDirection * Time.deltaTime;
			//}
		//}
	}

	private void doRotation (){

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
