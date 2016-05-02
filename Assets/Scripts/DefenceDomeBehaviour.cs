using UnityEngine;
using System.Collections;

public class DefenceDomeBehaviour : MonoBehaviour {

    public int y = 90;

	public float duration = 8.0f;

	void Start(){
		Destroy(gameObject, duration);
	}

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, y, 0) * Time.deltaTime);
	}
}