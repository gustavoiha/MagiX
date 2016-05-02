using UnityEngine;
using System.Collections;

public class BossBehaviour : MonoBehaviour {

	public enum State {
		Idle,
		Walking,
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

	public float gravityIncrement = 2.0f;


}
