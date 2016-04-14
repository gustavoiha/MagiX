using UnityEngine;
using System.Collections;

public class LightBallBehaviour : MonoBehaviour {

    public float scale = 1.0f; //Usado para aumentar a esfera
    public float growthRate = 1.0f; //Taxa em q a esfera cresce
    public float radius = 1;//Raio da explosão
    public float explosionForce = 100.0f; //Força da explosão

    public ParticleSystem particleS; //Particulas
    public ForceMode forceMode;//Tipo de força que será aplicada

    bool detonation;//A esfera detona quando toca algo

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 10.0f);//Auto detonação
        detonation = false;//Começa falso e fica verdadeiro quanto toca algo
    }

    // Update is called once per frame
    void Update()
    {
        if (detonation)
        {
            //Cria uma esfera que afetará quem estiver na explosão
            foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
            {
                //Coloquei a parte de empurrão como teste, vai ter empurrão ou só dano?
                if (col.GetComponent<Rigidbody>() != null) //Se o objeto puder ser empurrado, ele será
                    col.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, radius, 0, forceMode);
            }
            Destroy(gameObject);//Destrói a esfera
        }
        else {
            transform.localScale = Vector3.one * scale; //Faz a esfera crescer de acordo com o scale
            particleS.transform.localScale = Vector3.one * scale; //E as particulas tbm
            scale += growthRate * Time.deltaTime; //Aumenta o scale conforme o tempo passa
            radius = 2 * particleS.transform.localScale.x;//Aumenta o raio conforme aumenta a esfera
        }
    }

    void OnTriggerEnter(Collider other)
    {
        detonation = true;//Ao tocar algo, ela explode
    }
}
