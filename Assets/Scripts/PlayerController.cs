 using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//public Transform Saber;

	private Animator animator;
	//private Rigidbody rigidBody;
	private SkillsController skillsController;
	private TargetController targetController;
	//private Transform cameraTransform;

	public GameObject particleChargeBall;
	public GameObject particleChargeSanctuary;

	//public float moveSpeedFoward = 6.0f;
	//public float moveSpeedSides  = 6.0f;
	//public float turnSpeedY = 60.0f;
	//public float turnSpeedY = 60.0f;

	private int mouseInvertX = 1;
	private int mouseInvertY = -1;

	//private Vector3 moveDirection = Vector3.zero;
	//public float gravity = 20.0f;

	private int cheatStartBoss = 0;

	// States in animator
	//private int walkingID;
	//private int jumpingID;
	//private int skillID;
	//private int sanctuaryState;

    //private float isGroundedRayLength = 0.1f;
    //public LayerMask layerMaskForGrounded;

    /*public bool isGrounded {
		get {
		     Vector3 position = transform.position;
		     position.y = GetComponent<Collider>().bounds.min.y + 0.1f;
		     float length = isGroundedRayLength + 0.1f;
		     Debug.DrawRay (position, Vector3.down * length);
		     bool grounded = Physics.Raycast (position, Vector3.down, length, layerMaskForGrounded.value);
		     return grounded;
		}
	}*/

	// Use this for initialization
	void Start () {
		animator  		 = gameObject.GetComponent/*InChildren*/<Animator> ();
		//rigidBody 		 = gameObject.GetComponent<Rigidbody> ();
		skillsController = gameObject.GetComponent/*InChildren*/<SkillsController> ();
		targetController = gameObject.GetComponent<TargetController> ();
		//cameraBehaviour  = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraBehaviour>();
		//cameraTransform  = GameObject.FindGameObjectWithTag ("MainCamera").transform;

		//walkingID 	   = Animator.StringToHash ("isWalking");
		//jumpingID 	   = Animator.StringToHash ("isJumping");
		//  	   = Animator.StringToHash ("skill");
		//sanctuaryState = Animator.StringToHash ("Base Layer.Sanctuary");
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape))
			PauseMenu.isPaused = !PauseMenu.isPaused;

		// if died
		if (GetComponent<HealthController> ().health <= 0.0f) {
			animator.SetBool ("isDead", true);
			return;
		}

		/**
		 * Movement animations
		 */

		if (Input.GetAxis("VerticalTranslation") != 0.0f || Input.GetAxis("HorizontalTranslation") != 0.0f) {
			animator.SetBool ("isWalking", true);
			
		}
		else {
			animator.SetBool ("isWalking", false);
		}

		//else if (Input.GetKeyUp("s"){
		// walk backwards
		//}

		/*
		 * Skills
		 */

		if (Input.GetKey (KeyCode.Mouse0)) {
			if (skillsController.HasSkillMana (SkillsController.LIGHT_ARROW)) {
				animator.SetInteger ("skill", 2);
				skillsController.PrepareSkill (SkillsController.LIGHT_ARROW);
			}
			else if (skillsController.CanUseSkill (SkillsController.BASIC_ATTACK)) {
				animator.SetInteger ("skill", 1);
				skillsController.PrepareSkill (SkillsController.BASIC_ATTACK);
			}
		}

		if (Input.GetKey (KeyCode.Mouse1)) {
			if (skillsController.CanUseSkill (SkillsController.LIGHT_BALL)) {
				animator.SetInteger ("skill", 3);
				skillsController.PrepareSkill (SkillsController.LIGHT_BALL);
			}
			else if (skillsController.HasSkillMana (SkillsController.LIGHT_ARROW)) {
				animator.SetInteger ("skill", 2);
				skillsController.PrepareSkill (SkillsController.LIGHT_ARROW);
			}
			else if (skillsController.CanUseSkill (SkillsController.BASIC_ATTACK)) {
				animator.SetInteger ("skill", 1);
				skillsController.PrepareSkill (SkillsController.BASIC_ATTACK);
			}
		}

		if (Input.GetKeyUp (KeyCode.Mouse0) || Input.GetKeyUp (KeyCode.Mouse1))
			animator.SetInteger ("skill", 0);

		// Basic Attack
		if (Input.GetKey (skillsController.basicAttack) && skillsController.CanUseSkill(SkillsController.BASIC_ATTACK)) {
			//skillsController.UseSkill (SkillsController.BASIC_ATTACK);
			animator.SetInteger ("skill", 1);
			skillsController.PrepareSkill (SkillsController.BASIC_ATTACK);
		}

		if (Input.GetKeyUp (skillsController.basicAttack))
			animator.SetInteger ("skill", 0);

		// Light Arrow
		if (Input.GetKey (skillsController.magicOne) && skillsController.CanUseSkill(SkillsController.LIGHT_ARROW)) {
			animator.SetInteger ("skill", 2);
			skillsController.PrepareSkill (SkillsController.LIGHT_ARROW);
		}

		if (Input.GetKeyUp (skillsController.magicOne) && !Input.GetKey (KeyCode.Mouse0))
			animator.SetInteger ("skill", 0);

		// Light Ball
		if (Input.GetKeyDown (skillsController.magicTwo) && skillsController.CanUseSkill(SkillsController.LIGHT_BALL)) {
			
			GameObject particleCharge = Instantiate (particleChargeBall, gameObject.transform.position, Quaternion.identity) as GameObject;
			particleCharge.transform.parent = gameObject.transform;
			Destroy (particleCharge, 5.0f);

			animator.SetInteger ("skill", 3);
			skillsController.PrepareSkill (SkillsController.LIGHT_BALL);
		}

		if (Input.GetKeyUp (skillsController.magicTwo))
			animator.SetInteger ("skill", 0);

		// Sanctuary
		if (Input.GetKeyDown (skillsController.magicThree) && skillsController.CanUseSkill(SkillsController.LIGHT_SANCTUARY)) {
			
			GameObject particleCharge = Instantiate (particleChargeSanctuary, gameObject.transform.position, Quaternion.identity) as GameObject;
			particleCharge.transform.parent = gameObject.transform;
			Destroy (particleCharge, 5.0f);

			animator.SetInteger ("skill", 4);
			skillsController.PrepareSkill (SkillsController.LIGHT_SANCTUARY);
		}

		if (Input.GetKeyUp (skillsController.magicThree))
			animator.SetInteger ("skill", 0);

		// Defense ball
		if (Input.GetKeyDown (skillsController.magicFour) && skillsController.CanUseSkill(SkillsController.DEFENCE_DOME)) {
			animator.SetInteger ("skill", 5);
			skillsController.PrepareSkill (SkillsController.DEFENCE_DOME);
		}

		if (Input.GetKeyUp (skillsController.magicFour))
			animator.SetInteger ("skill", 0);

		if (Input.GetKeyDown (TargetController.targetSwitch)) {
			targetController.UpdateTarget ();
		}

		//if (Input.GetKeyDown ("space") && isGrounded) {
			//animator.SetBool ("isJumping", true);
			//gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 800.0f, 0));
		//}
		//else if (!isGrounded)
			//animator.SetBool ("isJumping", false);

		if (FPSWalkerEnhanced.jumping)
			animator.SetBool ("isJumping", true);
		else
			animator.SetBool ("isJumping", false);

		// Mode if animator is in walking mode
		//FPSWalkerEnhanced.movementEnabled = /*animator.GetBool (walkingID) && */animator.GetCurrentAnimatorStateInfo(0).fullPathHash != sanctuaryState;

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

	// Returns true if player is in walking or running animation
	//private bool shouldMove(){
	//	return animator.GetCurrentAnimatorStateInfo (0).IsName ("Walking") ||
	//		   animator.GetCurrentAnimatorStateInfo (0).IsName ("Running");
	//}

	/**
	 * Invert mouse directions regarding the camera's rotation
	 * pass "X" as argument to invert horizontal axis, and "Y" for vertical
	 */
	public void invertMouseDirection (string direction){
		if (direction.Equals ("X") || direction.Equals ("x"))
			mouseInvertX *= -1;
		else if (direction.Equals ("Y") || direction.Equals ("y"))
			mouseInvertY *= -1;
	}

}
