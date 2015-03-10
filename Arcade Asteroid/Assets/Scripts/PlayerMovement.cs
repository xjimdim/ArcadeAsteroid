using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


	public float maxSpeed = 5f;
	public float rotSpeed = 180f;
	float shipBoundaryRadius = 0.5f;



	void FixedUpdate () {

		//movement
		Quaternion rot = transform.rotation;

		float z = rot.eulerAngles.z;

		z -= Input.GetAxis ("Horizontal") * rotSpeed * Time.deltaTime;

		rot = Quaternion.Euler (0, 0, z);

		transform.rotation = rot; 

		Vector3 pos = transform.position;

		Vector3 velocity = new Vector3(0, Input.GetAxis ("Vertical") * maxSpeed * Time.deltaTime, 0);

		pos += rot * velocity;



		/*/restrict player

		if (pos.y + shipBoundaryRadius> Camera.main.orthographicSize) {
			pos.y = Camera.main.orthographicSize - shipBoundaryRadius;
		}

		if (pos.y - shipBoundaryRadius < -Camera.main.orthographicSize) {
			pos.y = -Camera.main.orthographicSize + shipBoundaryRadius;
		}

		float screenRatio = (float)Screen.width / (float)Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;

		if (pos.x + shipBoundaryRadius> widthOrtho) {
			pos.x = widthOrtho - shipBoundaryRadius;
		}
		
		if (pos.x - shipBoundaryRadius < -widthOrtho) {
			pos.x = -widthOrtho + shipBoundaryRadius;
		}*/

		if (pos.y + shipBoundaryRadius> Camera.main.orthographicSize) {
			if(PlayerSpawner.instance.numOfPlayers==1){
				GameObject pgo = (GameObject) Instantiate(gameObject, new Vector3 (-pos.x, -(Camera.main.orthographicSize + shipBoundaryRadius), 0), rot);
				pgo.name = "PlayerShip"; //remove the (clone)... hardcode = bad but works :P
				pgo.layer = 8;
				PlayerSpawner.instance.numOfPlayers++;
				PlayerSpawner.instance.playerInstance = pgo;

			}
			if (pos.y + shipBoundaryRadius> Camera.main.orthographicSize + shipBoundaryRadius + 0.5f) {
				Destroy(gameObject);
				PlayerSpawner.instance.numOfPlayers--;
			}
		}
        if (pos.y - shipBoundaryRadius < -Camera.main.orthographicSize) {
			if(PlayerSpawner.instance.numOfPlayers==1){
				GameObject pgo = (GameObject) Instantiate(gameObject, new Vector3 (-pos.x, (Camera.main.orthographicSize + shipBoundaryRadius), 0), rot);
				pgo.name = "PlayerShip";  
				pgo.layer = 8;
				PlayerSpawner.instance.numOfPlayers++;
				PlayerSpawner.instance.playerInstance = pgo;
			}
			if (pos.y - shipBoundaryRadius < -Camera.main.orthographicSize - shipBoundaryRadius - 0.5f) {
				Destroy(gameObject);
				PlayerSpawner.instance.numOfPlayers--;
			}
		}



		
		//if (pos.y - shipBoundaryRadius < -Camera.main.orthographicSize) {
			//pos.y = -Camera.main.orthographicSize + shipBoundaryRadius;
		//}
		
		float screenRatio = (float)Screen.width / (float)Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;
		
		if (pos.x + shipBoundaryRadius> widthOrtho) {
			if(PlayerSpawner.instance.numOfPlayers==1){
				GameObject pgo = (GameObject) Instantiate(gameObject, new Vector3 (-(widthOrtho+shipBoundaryRadius), -pos.y, 0), rot);
				pgo.name = "PlayerShip";
				pgo.layer = 8;
				PlayerSpawner.instance.numOfPlayers++;
				PlayerSpawner.instance.playerInstance = pgo;
			}
			if (pos.x + shipBoundaryRadius> widthOrtho + shipBoundaryRadius + 0.5f) {
				Destroy(gameObject);
				PlayerSpawner.instance.numOfPlayers--;
			}
		}
		
		if (pos.x - shipBoundaryRadius < -widthOrtho) {
			if(PlayerSpawner.instance.numOfPlayers==1){
				GameObject pgo = (GameObject) Instantiate(gameObject, new Vector3 ((widthOrtho+shipBoundaryRadius), -pos.y, 0), rot);
				pgo.name = "PlayerShip";
				pgo.layer = 8;
				PlayerSpawner.instance.numOfPlayers++;
				PlayerSpawner.instance.playerInstance = pgo;
			}
			if (pos.x + shipBoundaryRadius < - widthOrtho - shipBoundaryRadius - 0.5f) {
				Destroy(gameObject);
				PlayerSpawner.instance.numOfPlayers--;
			}
		}


		transform.position = pos;
	}
}
