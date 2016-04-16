using UnityEngine;
using System.Collections;

public class SkillsController : MonoBehaviour {

    public KeyCode magicOne;//Botões
    public KeyCode magicTwo;
    public KeyCode magicThree;
    public KeyCode magicFour;

	public GameObject lightArrow; //Prefab da flecha de luz
    public GameObject lightBall; //Prefab da bola de luz
    public GameObject lightCross; //Prefab da LightCross

    public float lightArrowForce = 100.0f; //Força aplicada na flecha
    public float lightBallForce = 50.0f; // Força aplicada na esfera
    public float lightCrossForce = 10.0f;

    Vector3 direction; //Direção em q o mouse aponta
    Vector3 location; //localização temporária para atirar magia(colocar a mão/cajado aqui futuramente)

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        location = transform.position + Vector3.forward*3; //Coloca a posição um pouco a frente
        var worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)); //Calcula local do mouse em relação a tela
        direction = worldPosition - transform.position; //Calcula direção baseada entre o personagem e o mouse
        direction.Normalize();//Normaliza o vetor
        if (Input.GetKey(magicOne)) //Ao apertar o botão 1, usa a primeira magia
            UseLightArrow();
        if (Input.GetKeyDown(magicTwo))//Ao apertar o botão 2, usa a segunda
            UseLightBall();
        if (Input.GetKeyDown(magicThree))//Ao apertar o botão 3, usa a terceira
            UseLightCross();
        if (Input.GetKeyDown(magicFour))//Ao apertar o botão 4, acionamosu desativamo santuário
            LightSanctuaryBehaviour.toogleSanctuary = !LightSanctuaryBehaviour.toogleSanctuary;

        //Seria bom colocar um static pra quando coletar os outros amuletos destrancar as magias
    }

    void UseLightArrow()
    {
        //Instantiate novos projéteis (Euler foi necessário para endireitar o prefab)
        GameObject projectile = Instantiate(lightArrow, location, Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 180)) as GameObject;
        projectile.GetComponent<Rigidbody>().useGravity = false; //Evita a gravidade
        projectile.GetComponent<Rigidbody>().AddForce(direction * lightArrowForce); //Coloca uma força para arremessar a magia
    }
    void UseLightBall()
    {
        //Mesma coisa acima, sem o ajuste, já q se trata de uma esfera
        GameObject projectile = Instantiate(lightBall, location, Quaternion.LookRotation(direction)) as GameObject;
        projectile.GetComponent<Rigidbody>().useGravity = false;
        projectile.GetComponent<Rigidbody>().AddForce(direction * lightBallForce);
    }
    void UseLightCross()
    {
        //Cura aqui
       Instantiate(lightCross, transform.position, Quaternion.LookRotation(direction)); //Cria uma LightCross no local do player
    }
}
