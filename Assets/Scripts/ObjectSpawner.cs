using UnityEngine;
using System.Collections;

public class EnemySpawnerBehaviour : MonoBehaviour {

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
	public float enableRandomRange;

	/// <summary>
	///  A random value smaller than this variable (inclusive) will increment the spawn range in each spawn.
	/// </summary>
	public float randomIncrementInSpawnRange;

	private float timeFromLastSpawn;

	// Use this for initialization
	void Start () {
		
		// Standard values
		spawnInterval = 2.0f;
		SpawnRange 	  = 10.0f;
		enableRandomInterval = true;
		enableRandomRange    = true;
		randomIncrementInSpawnInterval = 1.0f;
		randomIncrementInSpawnRange    = SpawnRange / 4.0f;

		// Start the time counter
		timeFromLastSpawn = spawnInterval;

	}
	
	// Update is called once per frame
	void Update () {

		if (timeFromLastSpawn > 0.0f) {
			timeFromLastSpawn -= Time.deltaTime;
			return;
		} 
		else {
			// Spawn object
			Transform newObject = Instantiate(objectToSpawn, spawnPosition, gameObject.transform.rotation) as Transform;
		}

	}

	private void updateSpawnTimeCounter(){
		timeFromLastSpawn = spawnInterval + Random.value * randomIncrementInSpawnInterval;
	}

	private void spawnPosition(){
		
		float spawnAngle = Random.value * 360.0f;

		Vector3 spawnPosition = gameObject.transform.position;

		Vector3 incrementPosition = Vector3.zero;

	}
}
