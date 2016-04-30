using UnityEngine;
using System.Collections;

public class BossDomeBehaviour : MonoBehaviour {

    public int y = 90;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, y, 0) * Time.deltaTime);
	}
}
