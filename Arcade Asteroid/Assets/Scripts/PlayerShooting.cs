using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

	public GameObject bulletPrefab;
	public Vector3 bulletOffset = new Vector3(0, 0.5f, 0);



	public float fireDelay = 0.25f;
	float cooldownTimer = 0;
	int bulletLayer;

	void Start() {
		bulletLayer = gameObject.layer;  //bullet gets the player layer 
	}

	void Update () {
		cooldownTimer -= Time.deltaTime;

		if (Input.GetButton ("Fire1") && cooldownTimer <= 0) {
 
			cooldownTimer = fireDelay;

			Vector3 offset = transform.rotation * bulletOffset;
			GameObject bulletGO = (GameObject) Instantiate (bulletPrefab, transform.position + offset, transform.rotation);
			
			bulletGO.layer = bulletLayer;
		}
	}
}
