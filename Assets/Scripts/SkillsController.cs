﻿using UnityEngine;
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
	private SoundController  soundController;

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
    Vector3 direction; 

	//localização temporária para atirar magia(colocar a mão/cajado aqui futuramente)
    Vector3 location;

    // Skills cooldowns
	private float[] timeTilNext;
    public float[]  coolDown;
	public float[]  manaUse;

	//[Serializable] public class coolDown{
	//	public float[] coolDownArray;
	//}

    // Use this for initialization
    void Start () {

		healthController = gameObject.transform.parent.gameObject.gameObject.GetComponent<HealthController> ();
		targetController = gameObject.transform.parent.gameObject.GetComponent<TargetController> ();
		soundController =  GameObject.FindGameObjectWithTag ("Sound").GetComponent<SoundController> ();

        timeTilNext = new float[5];

		for (int i = 0; i < coolDown.Length; i++) {
            timeTilNext[i] = 0;
        }
	}

    // Update is called once per frame
    void Update() {

		for(int i = 0; i < timeTilNext.Length; i++) {
            //Resets cooldowns
            timeTilNext[i] -= Time.deltaTime;
        }

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

		if (!targetController.HasTarget())
			targetController.UpdateTarget ();

		target = targetController.GetTargetTransform();

		location = transform.position + Vector3.forward * 3; //Coloca a posição um pouco a frente

		if (target == null) { //If there is no target at all, calculations will be based on the mouse position
			var worldPosition = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)); //Calcula local do mouse em relação a tela
			direction = worldPosition - transform.position; //Calcula direção baseada entre o personagem e o mouse
			direction.Normalize();//Normaliza o vetor
		}
		else { //If there is a target, calculations will be based on the target's position
			direction = targetController.GetTargetTransform().position - location;
			direction.Normalize();
		}
		
		healthController.DecreaseMana (manaUse [skillID]);
		timeTilNext [skillID] = coolDown [skillID];

		switch (skillID) {

		case BASIC_ATTACK:
			UseBasicAttack ();
			soundController.PlayerSounds (SoundController.BASIC_ATTACK, true);
			break;
		case LIGHT_ARROW:
			UseLightArrow();
			soundController.PlayerSounds (SoundController.LIGHT_ARROW, true);
			break;
		case LIGHT_BALL:
			UseLightBall ();
			soundController.PlayerSounds (SoundController.LIGHT_BALL, true);
			break;
		case LIGHT_SANCTUARY:
			UseLightSanctuary ();
			soundController.PlayerSounds (SoundController.LIGHT_SANCTUARY, true);
			break;
		case DEFENCE_DOME:
			soundController.PlayerSounds (SoundController.DEFENCE_DOME, true);
			UseDefenceDome ();
			break;
		}

		return;
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

	private void UseBasicAttack(){
		
	}

    private void UseLightArrow() {

        //Instantiate novos projéteis (Euler foi necessário para endireitar o prefab)
		GameObject projectile = Instantiate(lightArrow, transform.position + Vector3.up * 8.0f, Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 180)) as GameObject;
        projectile.GetComponent<Rigidbody>().useGravity = false; //Evita a gravidade
        projectile.GetComponent<Rigidbody>().AddForce(direction * lightArrowForce); //Coloca uma força para arremessar a magia

    }

	private void UseLightBall() {

        //Mesma coisa acima, sem o ajuste, já q se trata de uma esfera
		GameObject projectile = Instantiate(lightBall, transform.position + Vector3.up * 8.0f, Quaternion.LookRotation(direction)) as GameObject;
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

}
