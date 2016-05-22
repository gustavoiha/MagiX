using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

	/// <summary>
	/// State of the spawner (enabled or stopped)
	/// </summary>
	private bool spawnEnabled = true;

	/// <summary>
	/// The object to spawn. An enemy prefab, for example
	/// </summary>
	public Transform objectToSpawn;

	/// <summary>
	/// The interval in seconds between two consecutive spawn.
	/// </summary>
	public float spawnInterval;

	/// <summary>
	/// If true, random time (randomIncrementInSpawnInterval) will be added to the spawn interval at every spawn.
	/// </summary>
	public bool enableRandomInterval;

	/// <summary>
	/// A random value smaller than this variable (inclusive) will increment the spawn interval in each spawn.
	/// </summary>
	public float randomIncrementInSpawnInterval;

	/// <summary>
	/// The spawn range from the center of this object.
	/// </summary>
	public float SpawnRange;

	/// <summary>
	/// If true, random distance (randomIncrementInSpawnRange) will be added to the spawn range at every spawn.
	/// </summary>
	public bool enableRandomRange;

	/// <summary>
	///  A random value smaller than this variable (inclusive) will increment the spawn range in each spawn.
	/// </summary>
	public float randomIncrementInSpawnRange;

	public bool useActivatorObject;

	/// <summary>
	/// The spawner will only work if an object with this tag is inside it's colliders and useActivatorObject is true.
	/// </summary>
	public string activatorObjectsTag;

	private float timeFromLastSpawn;

    public int spawnLimit;

    private int spawnCounter = 0;

	// Use this for initialization
	void Start () {

		// Standard values
		/*spawnInterval = 2.0f;
		SpawnRange    = 10.0f;
		enableRandomInterval = true;
		enableRandomRange    = true;
		randomIncrementInSpawnInterval = 1.0f;
		randomIncrementInSpawnRange    = SpawnRange / 4.0f;*/

		spawnEnabled = !useActivatorObject;

		// Start the time counter
		timeFromLastSpawn = spawnInterval;
        
	}
	
	void Update () {
        
        if (timeFromLastSpawn > 0.0f){
            timeFromLastSpawn -= Time.deltaTime;
            return;
        }

		if (objectToSpawn == null || spawnCounter >= spawnLimit)
			return;

        if (spawnEnabled)
        {
            // Spawn object
            /*Transform newObject = */
            Instantiate (objectToSpawn, SpawnPosition(), gameObject.transform.rotation) /*as Transform*/;
            spawnCounter++;
			//Debug.Log ("spawning");
        }

        UpdateSpawnTimeCounter ();
        
	}

	void OnTriggerEnter (Collider collider){
		if (activatorObjectsTag.Contains (collider.tag) && useActivatorObject)
			spawnEnabled = true;
	}

	void OnTriggerExit (Collider collider){
		if (activatorObjectsTag.Contains (collider.tag) && useActivatorObject)
			spawnEnabled = false;
	}


	// Use this to set spawn enabled (true) or stopped (false)
	public void setEnabled (bool newState){
		this.spawnEnabled = newState;
	}

	private void UpdateSpawnTimeCounter (){
		timeFromLastSpawn = spawnInterval + ((enableRandomInterval) ? Random.value * randomIncrementInSpawnInterval : 0);
	}

	private Vector3 SpawnPosition (){

		// Spawn angle
		float spawnAngle = Random.value * 360.0f;

		// Spawn distance
		float spawnDistance = SpawnRange + ((enableRandomRange) ? Random.value * randomIncrementInSpawnRange : 0);

		// Parent position
		Vector3 spawnPosition = gameObject.transform.position;

		// Equivalent to local position relative to parent object
		Vector3 deltaPosition = Vector3.zero;

		deltaPosition.x = spawnDistance * Mathf.Sin(spawnAngle * Mathf.Deg2Rad);
		deltaPosition.z = spawnDistance * Mathf.Cos(spawnAngle * Mathf.Deg2Rad);

		spawnPosition.x += deltaPosition.x;
		spawnPosition.y += deltaPosition.y;
		spawnPosition.z += deltaPosition.z;

		return spawnPosition;
	}
}