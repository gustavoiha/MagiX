using UnityEngine;
using System.Collections;

public class LightRayBehaviour : MonoBehaviour {

    public float growth = 1.1f; //Constante em que o raio crescerá
    public GameObject player; //Localização do Player
    Vector3 scale; //Vetor para a escala atual do raio

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1.0f); //Auto-destruição
        scale = transform.localScale; //Escala atual do raio
	}
	
	// Update is called once per frame
	void Update () {
        float yScale = transform.localScale.y; //Cria um float para segurar a posição Y que fará o raio crescer
        yScale *= growth; //Aumenta esse float de acordo com a constante
        transform.localScale = new Vector3(0,1,0) * yScale + scale; //Nova escala será a parte y somado com a parte antiga
	}

}
