using UnityEngine;
using System.Collections;

public class LightSanctuaryBehaviour : MonoBehaviour {

    public Transform player; //Localização do player

    public float range; //Distância do círculo

    public static bool toogleSanctuary; //Ativar ou desativar santuário

	// Use this for initialization
	void Start () {
        toogleSanctuary = false;//Santuário começa desativado
        range = GetComponent<ParticleSystem>().transform.localScale.x; // Alcance terá mesma distância do Círculo de partículas
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.position; //O santuário se manterá no local do jogador(?)
        if (toogleSanctuary)//Ao acionar o santuário
        {
            GetComponent<ParticleSystem>().Play();//Aciona as partículas
            foreach (Collider col in Physics.OverlapSphere(player.position, range))//Todos no alcance do santuário sofrerão algum efeito
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
