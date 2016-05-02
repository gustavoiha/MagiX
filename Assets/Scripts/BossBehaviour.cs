using UnityEngine;
using System.Collections;

public class BossBehaviour : MonoBehaviour {

	public enum State {
		Idle,
		Follow,
		Attacking,
		Die,
	}

	private State state;

	private Animator animator;
	private Rigidbody rigidBody;

	//Objeto que o inimigo irá seguir
	public Transform target;

	//Velocidade do inimigo
	public float moveSpeed = 3.0f;
	public float rotateSpeed = 3.0f;

	//Quanto ele chegará perto do player
	public float followRange = 10.0f;

	public float attackRange = 2.0f;

	//Distância da sensibilidade do player (deve ser menor ou igual a followRange)
	public float idleRange = 10.0f;

	public float gravityIncrement = 2.0f;

	IEnumerator IdleState() {

		animator.SetInteger ("State", 0);

		while(state == State.Idle)
		{
			//OnUpdate

			MakePerpendicular ();

			if (GetDistance () < followRange)
				state = State.Follow;

			yield return 0;
		}

		//Debug.Log ("Saiu do idle");

		GoToNextState ();
	}

	IEnumerator FollowState() {

		animator.SetInteger ("State", 1);

		while (state == State.Follow)
		{
			followPlayer ();

			if (GetDistance () > idleRange)
				state = State.Idle;
			yield return 0;
		}

		//Debug.Log ("Saiu do follow");

		GoToNextState ();
	}

	IEnumerator DieState() {

		Destroy(this.gameObject);
		yield return 0;
	}

	IEnumerator AttackingState() {

		animator.SetInteger ("State", 2);
		gameObject.GetComponentInChildren<SphereCollider> ().enabled = true;

		while (state == State.Attacking)
		{
			followPlayer ();
			RotateTowardsTarget ();

			if (GetDistance () > attackRange)
				state = State.Follow;

			yield return 0;
		}

		gameObject.GetComponentInChildren<SphereCollider> ().enabled = false;
		GoToNextState ();
	}

	void Start()
	{

		animator  = gameObject.GetComponentInChildren<Animator> ();
		rigidBody = gameObject.GetComponent<Rigidbody> ();

		target = GameObject.FindGameObjectWithTag ("Player").transform;

		GoToNextState ();
	}

	void FixedUpdate(){
		rigidBody.AddForce (Physics.gravity * rigidBody.mass * gravityIncrement);
	}

	private void GoToNextState ()
	{
		string methodName = state.ToString () + "State";
		System.Reflection.MethodInfo info = GetType ().GetMethod (methodName,
			System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
		StartCoroutine ((IEnumerator)info.Invoke (this, null));
	}

	public float GetDistance()
	{
		if (target != null)
			return (transform.position - target.transform.position).magnitude;
		else
			return Mathf.Infinity;
	}

	private void RotateTowardsTarget()
	{
		transform.rotation = Quaternion.Slerp (transform.rotation,
			Quaternion.LookRotation (target.position - transform.position), rotateSpeed * Time.deltaTime);
	}

	private void MakePerpendicular(){
		// Stoping object from rotating automatially because of the rigdbody component
		rigidBody.angularVelocity = new Vector3(0,0,0);

		// Making sure the object is always perpendicular
		Quaternion quaternion  = new Quaternion ();
		quaternion.eulerAngles = new Vector3 (0, gameObject.transform.rotation.eulerAngles.y, 0);
	}

	private void followPlayer(){

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
