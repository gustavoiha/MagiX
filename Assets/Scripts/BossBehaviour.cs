using UnityEngine;
using System.Collections;

public class BossBehaviour : MonoBehaviour {

	private Animator animator;
	private Rigidbody rigidBody;
	private SoundManager soundManager;

	//Objeto que o inimigo irá seguir
	private Transform target;

	public GameObject SkillRing;
	public GameObject SkillPull;
	public GameObject SkillStorm;

	//Velocidade do inimigo
	public float moveSpeed = 3.0f;
	public float rotateSpeed = 3.0f;

	//Quanto ele chegará perto do player
	public float followRange = 100.0f;

	public float meeleAttackRange = 6.0f;

	public float middleSkillAttackRange = 12.0f;

	public float gravityIncrement = 2.0f;

	public float intervalBetweenSkills = 5.0f;

	public float randomIncrementRange = 5.0f;

	private float timeLeftToSkill;

	private const int MEELE_ATTACK = 1;
	private const int SKILL_PULL   = 2;
	private const int SKILL_RING   = 3;
	private const int SKILL_STORM  = 4;

	public float StormSpeedGain = 10.0f;

	private TrailRenderer swordTrail;

	private bool isUsingSkill = false;

	void Start (){

		animator        = gameObject.GetComponentInChildren<Animator> ();
		rigidBody       = gameObject.GetComponent<Rigidbody> ();
		swordTrail      = gameObject.GetComponentInChildren<TrailRenderer> ();
		soundManager    = gameObject.GetComponent<SoundManager> ();

		target = GameObject.FindGameObjectWithTag ("Player").transform;

		timeLeftToSkill = intervalBetweenSkills;

		SetSwordTrail (false);
	}

	void Update (){

		//Debug.Log (GetDistance ());
		MakePerpendicular ();

		if (GetDistance () <= meeleAttackRange) {
			
			FollowPlayer ();
			RotateTowardsTarget ();

			UseSkill (MEELE_ATTACK);
			soundManager.PlaySound (0);

		}
		else if (GetDistance () <= middleSkillAttackRange) {
			FollowPlayer ();
			RotateTowardsTarget ();

			if (timeLeftToSkill <= 0.0f) {
				UseSkillSort (SKILL_STORM, SKILL_RING);
				timeLeftToSkill = intervalBetweenSkills + Random.value * randomIncrementRange;
				Debug.Log ("girando");
			}
			else {
				animator.SetBool ("isRunning", true);
				animator.SetInteger ("Skill", 0);
			}
		}
		else if (GetDistance () <= followRange) {
			
			if (timeLeftToSkill <= 0.0f) {
				
				UseSkill (SKILL_PULL);
				timeLeftToSkill = intervalBetweenSkills + Random.value * randomIncrementRange;
				animator.SetBool("isRunning", false);
			}
			else {
				animator.SetBool("isRunning", true);
				animator.SetInteger ("Skill", 0);
				FollowPlayer ();
			}
		}
		else {
			animator.SetBool ("Idle", true);
		}

		timeLeftToSkill -= Time.deltaTime;
	}

	void FixedUpdate (){
		rigidBody.AddForce (Physics.gravity * rigidBody.mass * gravityIncrement);
	}

	private void UseSkillSort (int skillID1, int skillID2){
		float sortValue = Random.value;

		if (sortValue <= 0.7f)
			UseSkill (skillID1);
		else
			UseSkill (skillID2);
	}

	private void UseSkill (int skillID){

		animator.SetInteger ("Skill", skillID);

		switch (skillID) {
		case SKILL_PULL:
			soundManager.PlaySound (2);
			Instantiate (SkillPull, transform.position, transform.rotation);
			break;
		case SKILL_RING:
			soundManager.PlaySound (1);
			Instantiate (SkillRing, transform.position, transform.rotation);
			break;
		case SKILL_STORM:
			soundManager.PlaySound (3);
			GameObject particle = Instantiate (SkillStorm, transform.position, transform.rotation) as GameObject;
			Destroy (particle, 8.0f);
			break;
		}
	}

	private float GetDistance (){
		if (target != null)
			return (transform.position - target.transform.position).magnitude;
		else
			return Mathf.Infinity;
	}

	private void RotateTowardsTarget (){
		transform.rotation = Quaternion.Slerp (transform.rotation,
			Quaternion.LookRotation (target.position - transform.position), rotateSpeed * Time.deltaTime);
	}

	private void MakePerpendicular (){
		// Stoping object from rotating automatially because of the rigdbody component
		rigidBody.angularVelocity = new Vector3(0,0,0);

		// Making sure the object is always perpendicular
		Quaternion quaternion  = new Quaternion ();
		quaternion.eulerAngles = new Vector3 (0, gameObject.transform.rotation.eulerAngles.y, 0);
	}

	private void FollowPlayer (){

		if (gameObject == null || target == null)
			return;

		Vector3 delta = target.position - gameObject.transform.position;

		delta.Normalize ();

		Quaternion rot = Quaternion.Slerp (transform.rotation,
			Quaternion.LookRotation (delta), rotateSpeed * Time.deltaTime);

		Quaternion rotation = Quaternion.Euler (new Vector3 (0, rot.eulerAngles.y , 0));

		this.transform.rotation = rotation;

		transform.position += delta * moveSpeed * Time.deltaTime;
	}

	public void setStormSkillState (int i){
		bool newState = (i >= 0) ? true : false;
		SetSwordTrail (newState);

		moveSpeed += StormSpeedGain * Mathf.Sign (i);
	}

	public void SetSwordTrail (bool newState){
		swordTrail.enabled = newState;
	}

	public void UsingSkill (int i){
		isUsingSkill = (i >= 0) ? true : false;
	}
}