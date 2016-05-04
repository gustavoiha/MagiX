using UnityEngine;
using System.Collections;

public class BossBehaviour : MonoBehaviour {

	private Animator animator;
	private Rigidbody rigidBody;

	//Objeto que o inimigo irá seguir
	private Transform target;

	public GameObject SkillRing;
	public GameObject SkillPull;

	//Velocidade do inimigo
	public float moveSpeed = 3.0f;
	public float rotateSpeed = 3.0f;

	//Quanto ele chegará perto do player
	public float followRange = 100.0f;

	public float meeleAttackRange = 6.0f;

	public float gravityIncrement = 2.0f;

	public float intervalBetweenSkills = 5.0f;

	public float randomIncrementRange = 5.0f;

	private float timeLeftToSkill;

	private const int MEELE_ATTACK = 1;
	private const int SKILL_PULL   = 2;
	private const int SKILL_RING   = 3;

	void Start (){

		animator  = gameObject.GetComponentInChildren<Animator> ();
		rigidBody = gameObject.GetComponent<Rigidbody> ();

		target = GameObject.FindGameObjectWithTag ("Player").transform;

		timeLeftToSkill = intervalBetweenSkills;
	}

	void Update (){

		MakePerpendicular ();

		if (GetDistance () <= meeleAttackRange) {
			UseSkill (MEELE_ATTACK);
			FollowPlayer ();
			RotateTowardsTarget ();
		}
		else if (GetDistance () <= followRange) {
			if (timeLeftToSkill <= 0.0f) {
				UseSkillSort ();
				timeLeftToSkill = intervalBetweenSkills + Random.value * randomIncrementRange;
			}
			else {
				animator.SetBool("isWalking", true);
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

	private void UseSkillSort (){
		float sortValue = Random.value;

		if (sortValue <= 0.5)
			UseSkill (SKILL_PULL);
		else
			UseSkill (SKILL_RING);
	}

	private void UseSkill (int skillID){
		
		switch (skillID) {
		case MEELE_ATTACK:
			animator.SetInteger ("skill", 1);
			break;
		case SKILL_PULL:
			Instantiate (SkillPull, transform.position, transform.rotation);
			animator.SetInteger ("skill", 2);
			break;
		case SKILL_RING:
			Instantiate (SkillRing, transform.position, transform.rotation);
			animator.SetInteger ("skill", 3);
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


}