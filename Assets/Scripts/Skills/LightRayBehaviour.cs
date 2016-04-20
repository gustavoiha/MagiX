using UnityEngine;
using System.Collections;

public class LightRayBehaviour : MonoBehaviour {

    public float growth = 1.1f; //Constante em que o raio crescerá

    public GameObject player; //Localização do Player
    Vector3 scale; //Vetor para a escala atual do raio
    public float damage = 0.5f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 1.0f); //Auto-destruição
        scale = transform.localScale; //Escala atual do raio

		originalPosition = gameObject.transform.position;
		originalLength   = gameObject.GetComponent<Renderer> ().bounds.size.z;
	}
	
	// Update is called once per frame
	void Update () {

		//Cria um float para segurar a posição Y que fará o raio crescer
        float yScale = transform.localScale.y;

        yScale *= growth; //Aumenta esse float de acordo com a constante

		//Increases the local dimension y of the game object and moves it forward to avoid duplicate objects in same space 
		//gameObject.transform.position += transform.forward * gameObject.GetComponent<Renderer>().bounds.size.y * (growth - 1.0f)/2.0f;

		float deltaLength = gameObject.GetComponent<Renderer> ().bounds.size.z - originalLength;

		gameObject.transform.position = originalPosition - deltaLength * transform.forward / 2.0f;

        transform.localScale = new Vector3(0,1,0) * yScale + scale; //Nova escala será a parte y somado com a parte antiga

	}

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Enemy")
        {
            collider.GetComponent<HealthController>().TakeDamage(damage);
        }
    }
}
