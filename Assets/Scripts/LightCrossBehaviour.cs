using UnityEngine;
using System.Collections;

public class LightCrossBehaviour : MonoBehaviour {

    public GameObject lightRay; //Prefab dos raios
    public Transform player; //Localização do player
	
	// Update is called once per frame
	void Update () {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(player.position.x, 0, player.position.z), Vector3.forward); //Adquire a rotação do player
            CreateLightRay(rotation); //Cria os raios a partir da rotação do player
            Destroy(gameObject); //Destroi esfera
        }
	
	

    void CreateLightRay(Quaternion rot)
    {
        //Cria raios rotacionando eles de 15 em 15°
        Instantiate(lightRay, transform.position, rot);
        Instantiate(lightRay, transform.position, rot * Quaternion.Euler(15, 0, 0));
        Instantiate(lightRay, transform.position, rot * Quaternion.Euler(30, 0, 0));
        Instantiate(lightRay, transform.position, rot * Quaternion.Euler(45, 0, 0));
        Instantiate(lightRay, transform.position, rot * Quaternion.Euler(60, 0, 0));
        Instantiate(lightRay, transform.position, rot * Quaternion.Euler(75, 0, 0));
        Instantiate(lightRay, transform.position, rot * Quaternion.Euler(90, 0, 0));
        Instantiate(lightRay, transform.position, rot * Quaternion.Euler(105, 0, 0));
        Instantiate(lightRay, transform.position, rot * Quaternion.Euler(120, 0, 0));
        Instantiate(lightRay, transform.position, rot * Quaternion.Euler(135, 0, 0));
        Instantiate(lightRay, transform.position, rot * Quaternion.Euler(150, 0, 0));
        Instantiate(lightRay, transform.position, rot * Quaternion.Euler(165, 0, 0));
    }
}
