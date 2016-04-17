using UnityEngine;
using System.Collections;

public class LightSanctuaryBehaviour : MonoBehaviour {

	//Localização do player
    public Transform player;

	//Distância do círculo
    public float range;

	//Ativar ou desativar santuário
    public static bool toogleSanctuary;

	// Use this for initialization
	void Start () {
		
		//Santuário começa desativado
        toogleSanctuary = false;

		// Alcance terá mesma distância do Círculo de partículas
        range = GetComponent<ParticleSystem>().transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {

		//O santuário se manterá no local do jogador(?)
        transform.position = player.position;

		//Ao acionar o santuário
        if (toogleSanctuary) {

			//Aciona as partículas
            GetComponent<ParticleSystem>().Play();

			//Todos no alcance do santuário sofrerão algum efeito
            foreach (Collider col in Physics.OverlapSphere(player.position, range))
            {
                if (col.tag == "Enemy")//Inimigos levarão dano
                {

                }
                if (col.tag == "Player")//E o jogador será curado
                {

                }
            }
        }
        else
        {
            GetComponent<ParticleSystem>().Stop();//Ao desativar o santuário, desligamos as partículas
        }
    }
}
