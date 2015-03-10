using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {

	public GameObject bulletPrefab;
	public Vector3 bulletOffset = new Vector3(0, 0.5f, 0);
	
	public float fireDelay = 0.50f;
	float cooldownTimer = 0;
	int bulletLayer;


	Transform player;

	void Start() {
		bulletLayer = gameObject.layer;  // bullet gets the enemy layer
	}

	void Update () {

		if (player == null) {
			GameObject go = GameObject.FindWithTag("Player");
			
			if(go != null){
				player = go.transform;
			}
		}	
		cooldownTimer -= Time.deltaTime;


		if (cooldownTimer <= 0 && player != null && Vector3.Distance(transform.position, player.position)<4) {
			 
			cooldownTimer = fireDelay;
			
			Vector3 offset = transform.rotation * bulletOffset;
			GameObject bulletGO = (GameObject) Instantiate (bulletPrefab, transform.position + offset, transform.rotation);

			bulletGO.layer = bulletLayer;
			bulletGO.GetComponent<SpriteRenderer>().color = Color.red;
		}
	}
}
