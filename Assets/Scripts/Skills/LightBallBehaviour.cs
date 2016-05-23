using UnityEngine;
using System.Collections;

public class LightBallBehaviour : MonoBehaviour {

    public float scale = 1.0f; //Usado para aumentar a esfera
    public float growthRate = 1.0f; //Taxa em q a esfera cresce
    public float radius = 1;//Raio da explosão
    public float explosionForce = 100.0f; //Força da explosão
    public float damage = 3.0f;

    public ParticleSystem particleS; //Particulas
    public ForceMode forceMode;//Tipo de força que será aplicada

	public GameObject Explosion;

    //private bool detonation;//A esfera detona quando toca algo
	private float explosionRadius;

    // Use this for initialization
    void Start()
    {
		explosionRadius = radius;
        Destroy(gameObject, 8.0f);//Auto detonação
        //detonation = false;//Começa falso e fica verdadeiro quanto toca algo
    }

    // Update is called once per frame
    void Update() {
		
        //if (detonation) {
		//	Detonate ();
        //}
        //else {
			
            transform.localScale = Vector3.one * scale; //Faz a esfera crescer de acordo com o scale
            particleS.transform.localScale = Vector3.one * scale; //E as particulas tbm
            scale += growthRate * Time.deltaTime; //Aumenta o scale conforme o tempo passa
			explosionRadius = radius * particleS.transform.localScale.x;//Aumenta o raio conforme aumenta a esfera
			//Debug.Log("Raio de explosão: " + explosionRadius);
        //}
    }

    void OnTriggerEnter(Collider other)
    {
		//Ao tocar algo, ela explode
		if (other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Boss"))
        	//detonation = true;
			Detonate ();
    }

	private void Detonate(){
		
		Collider[] colliders = Physics.OverlapSphere (transform.position, explosionRadius);

		foreach (Collider hit in colliders) {
			
			Rigidbody rigidbody = hit.GetComponent<Rigidbody> ();

			if (rigidbody != null && !hit.gameObject.tag.Equals("Player")) {
				rigidbody.AddExplosionForce (explosionForce, transform.position, explosionRadius, 1.0f, forceMode);
				//Debug.Log ("Added force!");
			}

			if (hit.gameObject.tag.Equals ("Enemy") || hit.gameObject.tag.Equals ("Boss"))
				hit.gameObject.GetComponent<HealthController> ().TakeDamage (damage);
		}

		GameObject explosion = Instantiate (Explosion, transform.position, Quaternion.identity) as GameObject;
		Destroy (explosion, 5.0f);
		//Destrói a esfera
		Destroy(gameObject);
	}
}
