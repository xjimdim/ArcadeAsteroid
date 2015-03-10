using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {


	public GameObject enemyPrefab;

	float enemyRate = 10f;
	float nextEnemy = 0f;  
	float spawnDistance = 12f;  // in Unity Units

	void Update () {
		nextEnemy -= Time.deltaTime;
		enemyRate *= 0.9f;

		if (enemyRate < 3) {
			enemyRate = 3;		
		}
		if (nextEnemy <= 0) {
			nextEnemy = enemyRate;

			Vector3 offset = Random.onUnitSphere;
			offset.z = 0;
			offset = offset.normalized * spawnDistance;

			Instantiate (enemyPrefab, transform.position + offset, Quaternion.identity);
		} 
	}


}
