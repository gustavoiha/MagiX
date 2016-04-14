using UnityEngine;
using System.Collections;

public class SkillsController : MonoBehaviour {

<<<<<<< Updated upstream
    public GameObject lightArrow; //Prefab da flecha de luz
    public GameObject lightBall; //Prefab da bola de luz

    public float lightArrowForce = 100.0f; //Força aplicada na flecha
    public float lightBallForce = 50.0f; // Força aplicada na esfera

    Vector3 direction; //Direção em q o mouse aponta
    Vector3 location; //localização temporária para atirar magia(colocar a mão/cajado aqui futuramente)

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        location = transform.position + Vector3.forward * 3; //Coloca a posição um pouco a frente
        var worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)); //Calcula local do mouse em relação a tela
        direction = worldPosition - transform.position; //Calcula direção baseada entre o personagem e o mouse
        direction.Normalize();//Normaliza o vetor
        if (Input.GetKey(KeyCode.Alpha1)) //Ao apertar o botão 1, usa a primeira magia
=======
	public GameObject lightArrow;
	public GameObject lightBall;

    public float lightArrowForce = 100.0f;
    public float lightBallForce  = 50.0f;
    public float angleX = 90.0f;
    public float angleY = 0.0f;
    public float angleZ = 180.0f;

    private Vector3 direction;

    private Vector3 location;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
		
        location = transform.position + Vector3.forward * 3;
        var worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        direction = worldPosition - transform.position;
        direction.Normalize();
        if (Input.GetKey(KeyCode.Alpha1))
>>>>>>> Stashed changes
            UseLightArrow();
        if (Input.GetKeyDown(KeyCode.Alpha2))//Ao apertar o botão 2, usa a segunda
            UseLightBall();

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
}
