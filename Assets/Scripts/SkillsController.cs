using UnityEngine;
using System.Collections;
using System;

public class SkillsController : MonoBehaviour {

	//Botões
	public KeyCode basicAttack;
    public KeyCode magicOne;
    public KeyCode magicTwo;
    public KeyCode magicThree;
    public KeyCode magicFour;

    //Target acquired at ENEMY BEHAVIOUR script
	private Transform target;

	private HealthController healthController;
	private TargetController targetController;
	private SoundManager 	 soundManager;
	//private Animator 		 animator;

    ///
    /// Skills' prefabs
    ///

    //Prefab da flecha de luz
	public GameObject lightArrow; 
	//Prefab da bola de luz
	public GameObject lightBall;
	//Prefab da LightSanctuary
	public GameObject lightSanctuary;
    //Prefab EssenceStealer
	public GameObject defenseDome;

	// Skills' id's. Pass them as arguments int UseSkill() method
	public const int BASIC_ATTACK    = 0;
	public const int LIGHT_ARROW 	 = 1;
	public const int LIGHT_BALL  	 = 2;
	public const int LIGHT_SANCTUARY = 3;
	public const int DEFENCE_DOME    = 4;

	//Força aplicada na flecha
    public float lightArrowForce = 100.0f; 

	// Força aplicada na esfera
    public float lightBallForce = 50.0f; 

	//Direção em q o mouse aponta
    private Vector3 direction; 

	//localização temporária para atirar magia(colocar a mão/cajado aqui futuramente)
    private Vector3 location;

	public Vector3 targetPivot;

    // Skills cooldowns
	private float[] timeTilNext;
	public float[] TimeTilNext {
		get { 
			return timeTilNext;
		}
	}

    public float[]  coolDown;
	public float[]  manaUse;

	public  bool[] skillsToStopMovement;
	public  bool[] skillsToRotateToTarget;
	private bool[] isUsingSkill;
	public bool[] IsUsingSkill {
		get { 
			return isUsingSkill;
		}
		set { 
			isUsingSkill = value;
		}
	}

	public bool autoTargetNearestEnemy = false;

	public static SkillsController _instance;

	void Awake (){
		_instance = this;
	}

    // Use this for initialization
    void Start () {

		healthController = /*gameObject.transform.parent.*/gameObject.GetComponent<HealthController> ();
		targetController = /*gameObject.transform.parent.*/gameObject.GetComponent<TargetController> ();
		soundManager     = gameObject.GetComponent<SoundManager> ();
		//animator  		 = gameObject.GetComponent/*InChildren*/<Animator> ();

        timeTilNext  = new float[5];
		isUsingSkill = new bool[5];

		for (int i = 0; i < coolDown.Length; i++) {
            timeTilNext[i] = 0.0f;
        }
	}

    // Update is called once per frame
    void Update() {

		for (int i = 0; i < timeTilNext.Length; i++) {
            //Resets cooldowns
			if (timeTilNext[i] >= 0.0f)
            	timeTilNext[i] -= Time.deltaTime;
        }

		// Rotate towards target when using skills if skillsToRotateToTarget
		if (RotateToTarget ()) {
			FPSWalkerEnhanced._instance.RotateTowards (Direction ());
		}

		// For safety:
		/*bool usingAnySkill = false;

		foreach (bool skill in isUsingSkill) {
			usingAnySkill = usingAnySkill || skill;
		}

		if (!usingAnySkill)
			FPSWalkerEnhanced.movementEnabled = true;*/

    }

	void FixedUpdate (){
		// For safety:
		bool usingAnySkill = false;

		foreach (bool skill in isUsingSkill) {
			usingAnySkill = usingAnySkill || skill;
		}

		if (!usingAnySkill)
			FPSWalkerEnhanced.movementEnabled = true;
	}

	/// <summary>
	/// Call his method to use a desired skill
	/// </summary>
	/// <param name="skill">Skill.</param>
	public void UseSkill (int skillID){

		// Check if ok to continue
		if (skillID < 0 || skillID >= manaUse.Length)
			return;

		if (!CanUseSkill(skillID))
			return;

		//if (autoTargetNearestEnemy)
		//	targetController.UpdateTarget ();

		//target = targetController.GetTargetTransform();

		//location = transform.position + transform.forward * 10.0f + transform.up * 8.0f; //Coloca a posição um pouco à frente

		//direction = Direction ();

		//if (target != null)
		//	FPSWalkerEnhanced._instance.doRotation (Quaternion.LookRotation (direction));
		
		healthController.DecreaseMana (manaUse [skillID]);
		timeTilNext [skillID] = coolDown [skillID];

		switch (skillID) {

		case BASIC_ATTACK:
			UseBasicAttack ();
			soundManager.PlaySound (0);
			break;
		case LIGHT_ARROW:
			UseLightArrow();
			soundManager.PlaySound (1);
			break;
		case LIGHT_BALL:
			location = transform.position + transform.forward * 1.4f + transform.up * 12.2f;
			direction = Direction ();
			UseLightBall ();
			soundManager.PlaySound (2);
			break;
		case LIGHT_SANCTUARY:
			UseLightSanctuary ();
			soundManager.PlaySound (3);
			break;
		case DEFENCE_DOME:
			soundManager.PlaySound (4);
			UseDefenceDome ();
			break;
		}

		return;
	}

	public void PrepareSkill (int skillID){
		
		if (autoTargetNearestEnemy)
			targetController.UpdateTarget ();

		target = targetController.GetTargetTransform();

		location = transform.position + transform.forward * 10.0f + transform.up * 8.0f; //Coloca a posição um pouco à frente

		direction = Direction ();

		FPSWalkerEnhanced.movementEnabled = !SkillsController._instance.skillsToStopMovement [skillID];
		SkillsController._instance.IsUsingSkill [skillID] = true;
		FPSWalkerEnhanced.movementEnabled = !SkillsController._instance.skillsToStopMovement[skillID];
	}

	public void EndedSkill (int skillID){
		isUsingSkill [skillID] = false;
		FPSWalkerEnhanced.movementEnabled = true;
	}

	public bool CanUseSkill (int skillID){
		
		// Check if ok to continue
		if (skillID < 0 || skillID >= manaUse.Length)
			return false;

		return healthController.HasMana (manaUse [skillID]) && (timeTilNext [skillID] <= 0.0f);
	}

	public bool HasSkillMana (int skillID){
		// Check if ok to continue
		if (skillID < 0 || skillID >= manaUse.Length)
			return false;

		return healthController.HasMana (manaUse [skillID]);
	}

	public Vector3 Direction (){

		Vector3 newDirection = new Vector3 ();

		if (target == null) { //If there is no target at all, calculations will be based on the mouse position
			newDirection = transform.forward;
		}
		else { //If there is a target, calculations will be based on the target's position
			newDirection = targetController.GetTargetTransform().position + targetPivot - location;
			newDirection.Normalize();
		}

		return newDirection;
	}

	private void UseBasicAttack(){
		
	}

    private void UseLightArrow() {

        //Instantiate novos projéteis (Euler foi necessário para endireitar o prefab)
		GameObject projectile = Instantiate(lightArrow, location, Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 180)) as GameObject;
        projectile.GetComponent<Rigidbody>().useGravity = false; //Evita a gravidade
        projectile.GetComponent<Rigidbody>().AddForce(direction * lightArrowForce); //Coloca uma força para arremessar a magia

    }

	private void UseLightBall() {

        //Mesma coisa acima, sem o ajuste, já q se trata de uma esfera
		GameObject projectile = Instantiate(lightBall, location, Quaternion.LookRotation(direction)) as GameObject;
        projectile.GetComponent<Rigidbody>().useGravity = false;
        projectile.GetComponent<Rigidbody>().AddForce(direction * lightBallForce);
    }

	private void UseLightSanctuary() {

		//Vector3 position = gameObject.transform.position;

		Quaternion rotation  = new Quaternion ();
		rotation.eulerAngles = new Vector3 (0, 0, 0);

		Instantiate(lightSanctuary, gameObject.transform.position, rotation);
	}

    private void UseDefenceDome() {

		GameObject newDefenseDome = Instantiate(defenseDome, gameObject.transform.position, Quaternion.identity) as GameObject;

		newDefenseDome.transform.localScale *= 3;

		newDefenseDome.transform.parent = gameObject.transform;

		newDefenseDome.transform.localPosition = new Vector3 (0, 2.7f, 0);

    }

	public bool UsingSkill (int skillID){
		return isUsingSkill [skillID];
	}

	public bool RotateToTarget () {
		bool rotateToTarget = false;

		for (int a = 0; a < isUsingSkill.Length; a++) {
			rotateToTarget = rotateToTarget || (isUsingSkill [a] && skillsToRotateToTarget [a]);
		}

		return rotateToTarget;
	}
}
