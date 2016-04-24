using UnityEngine;
using System.Collections;
using System;

public class SkillsController : MonoBehaviour {

	//Botões
    public KeyCode magicOne;
    public KeyCode magicTwo;
    public KeyCode magicThree;
    public KeyCode magicFour;
    public KeyCode magicFive;
    public KeyCode targetSwitch;

    //Target acquired at ENEMY BEHAVIOUR script
    public GameObject target = null;
    //Array with all enemies around
    GameObject[] targets = null;

    ///
    /// Skills' prefabs
    ///

    //Prefab da flecha de luz
	public GameObject lightArrow; 
	//Prefab da bola de luz
	public GameObject lightBall;
	//Prefab da LightCross
	public GameObject lightCross;
    //Prefab EssenceStealer
	public GameObject essenceStealer;

	// Skills' id's. Pass them as arguments int UseSkill() method
	public const string LIGHT_ARROW = "lightArrow";
	public const string LIGHT_BALL  = "lightBall";
	public const string LIGHT_CROSS = "lightCross";

	//Força aplicada na flecha
    public float lightArrowForce = 100.0f; 

	// Força aplicada na esfera
    public float lightBallForce = 50.0f; 

	// Força aplicada no raio
    public float lightCrossForce = 10.0f;

    // Radius to search for a target (in case none is selected)
    public float radius = 0.5f;
    //Maximum for radius to reach
    public float radiusMax = 100;

	//Direção em q o mouse aponta
    Vector3 direction; 

	//localização temporária para atirar magia(colocar a mão/cajado aqui futuramente)
    Vector3 location;

    // Skills cooldowns
    public float[] timeTilNext;
    public float[] cd;

    // Use this for initialization
    void Start () {
        targetSwitch = KeyCode.Tab;
        //Define each cooldown
        timeTilNext = new float[5];
        cd = new float[5];
        cd[0] = 1.0f; 
        cd[1] = 2.0f;
        cd[2] = 3.0f;
        cd[3] = 2.0f;
        cd[4] = 5.0f;
        for(int i=0; i<5; i++)
        {
            timeTilNext[i] = 0;
        }
	}

    // Update is called once per frame
    void Update() {

        target = GetComponent<TargetController>().GetTarget();

        //If enemy has no health, the target will become null
        if (GetComponent<TargetController>().TargetIsDead())
        	target = null;

        location = transform.position + Vector3.forward * 3; //Coloca a posição um pouco a frente

        if (!GetComponent<TargetController>().HasTarget()) { //If there is no target at all, calculations will be based on the mouse position
            var worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)); //Calcula local do mouse em relação a tela
            direction = worldPosition - transform.position; //Calcula direção baseada entre o personagem e o mouse
            direction.Normalize();//Normaliza o vetor
        } else { //If there is a target, calculations will be based on the target's position
            direction = target.transform.position - location;
            direction.Normalize();
        }

        if (Input.GetKey(magicOne) && timeTilNext[0] <= 0) { //Ao apertar o botão 1, usa a primeira magia
            UseLightArrow();
            timeTilNext[0] = cd[0];
        }

        if (Input.GetKeyDown(magicTwo) && timeTilNext[1] <= 0) {//Ao apertar o botão 2, usa a segunda
            UseLightBall();
            timeTilNext[1] = cd[1];
        }

        if (Input.GetKeyDown(magicThree) && timeTilNext[2] <=0) {//Ao apertar o botão 3, usa a terceira
            UseLightCross();
            timeTilNext[2] = cd[2];
        }

        if (Input.GetKeyDown(magicFour) && timeTilNext[3] <=0) {//Ao apertar o botão 4, acionamos ou desativamos santuário
            LightSanctuaryBehaviour.toogleSanctuary = !LightSanctuaryBehaviour.toogleSanctuary;
            timeTilNext[3] = cd[3];
        }

        if (Input.GetKeyDown(magicFive) && timeTilNext[4]<=0) {//Ao apertar o botão 5, acionamos EssenceStealer
            //If there is no target, we'll pick the closest one
            target = GetComponent<TargetController>().FindNearTarget(radius, radiusMax);
            UseEssenceStealer();
            timeTilNext[4] = cd[4];
        }

        for(int i = 0; i<5; i++) {
            //Resets cooldowns
            timeTilNext[i] -= Time.deltaTime;
        }

        if (Input.GetKeyDown(targetSwitch)) {
            GetComponent<TargetController>().SwitchTarget(targets);
        }

        //Creates an array with all enemies
        targets = GetComponent<TargetController>().PickNearbyTarget(radiusMax);
        //print(targets.Length);

        //Seria bom colocar um static pra quando coletar os outros amuletos destrancar as magias
    }

	/// <summary>
	/// Call his method to use a desired skill
	/// </summary>
	/// <param name="skill">Skill.</param>
	public void UseSkill (string skillID){

		// Check if null or empty to continue
		if (string.IsNullOrEmpty(skillID))
			return;
		
		switch (skillID) {

		case LIGHT_ARROW:
			UseLightArrow ();
			break;
		case LIGHT_BALL:
			UseLightBall ();
			break;
		case LIGHT_CROSS:
			UseLightCross ();
			break;
		}

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

	private void UseLightCross() {
		//Cria uma LightCross no local do player
		GameObject newLightCross = Instantiate(lightCross, transform.position, transform.rotation) as GameObject;
		newLightCross.transform.parent = gameObject.transform;
    }

    private void UseEssenceStealer() {
        GameObject essence = Instantiate(essenceStealer, target.transform.position, Quaternion.LookRotation(-direction)) as GameObject;
    }
}
