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
    public GameObject target;
    //Array with all enemies around
    GameObject[] targets = null;
    //Variable to switch between enemies
    public int switchT = 0;

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

        //If enemy has no health, the target will become null
        if (target != null)
            if (target.GetComponent<HealthController>().health <= 0)
                target = null;

        location = transform.position + Vector3.forward * 3; //Coloca a posição um pouco a frente

        if (target == null) //If there is no target at all, calculations will be based on the mouse position
        {
            var worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)); //Calcula local do mouse em relação a tela
            direction = worldPosition - transform.position; //Calcula direção baseada entre o personagem e o mouse
            direction.Normalize();//Normaliza o vetor
        } else //If there is a target, calculations will be based on the target's position
        {
            direction = target.transform.position - location;
            direction.Normalize();
        }
        if (Input.GetKey(magicOne) && timeTilNext[0] <= 0) //Ao apertar o botão 1, usa a primeira magia
        {
            UseLightArrow();
            timeTilNext[0] = cd[0];
        }
        if (Input.GetKeyDown(magicTwo) && timeTilNext[1] <= 0)//Ao apertar o botão 2, usa a segunda
        {
            UseLightBall();
            timeTilNext[1] = cd[1];
        }
        if (Input.GetKeyDown(magicThree) && timeTilNext[2] <=0)//Ao apertar o botão 3, usa a terceira
        {
            UseLightCross();
            timeTilNext[2] = cd[2];
        }
        if (Input.GetKeyDown(magicFour) && timeTilNext[3] <=0)//Ao apertar o botão 4, acionamos ou desativamos santuário
        {
            LightSanctuaryBehaviour.toogleSanctuary = !LightSanctuaryBehaviour.toogleSanctuary;
            timeTilNext[3] = cd[3];
        }
        if (Input.GetKeyDown(magicFive) && timeTilNext[4]<=0)//Ao apertar o botão 5, acionamos EssenceStealer
        {
            //If there is no target, we'll pick the closest one
            if (target == null)
                while (target == null)
                {
                    //Create a globe around the character
                    foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
                    {
                        if (col.tag == "Enemy") // If there's an enemy around, we'll pick them as a target
                            target = col.gameObject;
                        radius += 0.1f;// Otherwise, we'll increase the globe until we find one.
                    }
                    if (radius > radiusMax) //If no enemy is found in 1000 units, we'll break the while loop
                    {
                        print("No enemy around");
                        return;
                    }
                }
            UseEssenceStealer();
            timeTilNext[4] = cd[4];
        }
        for(int i = 0; i<5; i++) {
            //Resets cooldowns
            timeTilNext[i] -= Time.deltaTime;
        }

        if (Input.GetKeyDown(targetSwitch)) {
            //In case we kill too many enemies and the current int is bigger than the new array, we reset it
            if (switchT >= targets.Length)
                switchT = 0;
            //When we press the key, we will change targets according to the array we created
            print(targets.Length);
            target = targets[switchT];
            //If we reach the maximum of the array, we reset the int
            if (switchT >= targets.Length - 1)
            {
                switchT = 0;
            } else //If we don't reach the end, we add one to it
                switchT++;
        }

        //Creates an array with all enemies
        targets = GameObject.FindGameObjectsWithTag("Enemy");
        //Creates an array for close objects
        Collider[] colTargets = Physics.OverlapSphere(transform.position, radiusMax);
        //Compare enemies to close objects
        for (int i = 0; i < targets.Length; i++) {
            bool same = false;
            for (int o = 0; o < colTargets.Length; o++) {
                //If there's a match, same becomes true
                if (targets[i] == colTargets[o].gameObject) {
                    same = true;
                    break;
                }
            }
            //If there's no match, the enemy is removed from the array
            if (!same)
                targets[i] = null;
        }
        //Here we resize the enemy array, removing the null ones
        for(int i = 0; i < targets.Length; i++) {
            //If there's a null space
            if(targets[i] == null) {
                //We remove it by pulling all the array back by 1
                for (int a = i; a < targets.Length - 1; a++) {
                    targets[a] = targets[a + 1];
                }
                //When it ends, we reduce its size by one
                Array.Resize<GameObject>(ref targets, targets.Length - 1);
                //Check if the new one is also null;
                i--;
            }
        }
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
