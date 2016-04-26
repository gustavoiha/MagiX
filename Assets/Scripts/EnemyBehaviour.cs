using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public enum State
	{
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

	//Distância da sensibilidade do player (deve ser menor ou igual a followRange)
	public float idleRange = 10.0f;

	public float health = 100.0f;
	public float currentHealth;

	//Valor do dano dado pelo inimigo
	public float maxHit = 5;
	private float giveDamage;

	public void TakeDamage()
	{
		float damageToDo = 100.0f - (GetDistance () * 5);
		if (damageToDo < 0)
			damageToDo = 0;
		if (damageToDo > health)
			damageToDo = health;
		currentHealth -= damageToDo;
		if (currentHealth <= 0)
			state = State.Die;
		else
		{
			followRange = Mathf.Max (GetDistance (), followRange);
			state = State.Follow;
		}

		print ("Current Health: " + currentHealth.ToString ());
	}

	public void GiveDamage()
	{
		giveDamage = maxHit * Random.value;

		//Debug.Log ("Tomou " + giveDamage + " seu otário");
	}

	IEnumerator IdleState()
	{
		
		//Debug.Log("Ta em idle");

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

	IEnumerator FollowState()
	{
		//Debug.Log ("Começou o Follow");

		animator.SetInteger ("State", 1);

		while (state == State.Follow)
		{
			transform.position = Vector3.MoveTowards (transform.position, target.position, Time.deltaTime * moveSpeed);

			MakePerpendicular ();

			RotateTowardsTarget ();

			if (GetDistance () > idleRange)
				state = State.Idle;
			yield return 0;
		}

		//Debug.Log ("Saiu do follow");

		GoToNextState ();
	}

	IEnumerator DieState()
	{
		//Debug.Log ("MORREU");
		Destroy(this.gameObject);
		yield return 0;
	}

	/*IEnumerator AttackingState(){

		animator.SetInteger ("State", 2);

		// Attack

		GoToNextState ();
	}*/

	void Start()
	{
		
		animator  = gameObject.GetComponent<Animator> ();
		rigidBody = gameObject.GetComponent<Rigidbody> ();

		GoToNextState ();
		currentHealth = health;
		giveDamage = maxHit;
	}

	void GoToNextState ()
	{
		string methodName = state.ToString () + "State";
		System.Reflection.MethodInfo info = GetType ().GetMethod (methodName,
			                                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
		StartCoroutine ((IEnumerator)info.Invoke (this, null));
	}

	public float GetDistance()
	{
		return (transform.position - target.transform.position).magnitude;
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

}
