using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageHandler : MonoBehaviour {

	public int health = 3;

	public float invulnPeriod = 0;
	float invulnTimer = 0;
	int correctLayer;
	float invulnAnimTimeStart = 0.5f;
	float invulnAnimTime;
	bool changecolorflag = false;

	SpriteRenderer spriteRend; 


	void Start(){

		correctLayer = gameObject.layer;
		Debug.Log ("Correct layer for " + gameObject.name + ": " + correctLayer);

		//this only gets the renderer on the parent object so only player
		spriteRend = GetComponent<SpriteRenderer> ();

		if (spriteRend == null) {
			spriteRend = transform.GetComponentInChildren<SpriteRenderer>();
			if(spriteRend == null) {
				Debug.LogError("Object '"+gameObject.name+"' has no sprite renderer.");	
			}	
		}

		invulnAnimTime = invulnAnimTimeStart;
	}
	void OnTriggerEnter2D(Collider2D other){

		health--;
		Debug.Log ("Health of layer " + gameObject.layer + " and name " + gameObject.name +": " + health);

		if (gameObject.layer == 8 && gameObject.name == "PlayerShip"){ //thats definately our player) 
						invulnTimer = invulnPeriod;
						gameObject.layer = 10;  //players gets invulnerable for invulPeriod seconds
						changeInvulnColors (true);
		}

		if (other.name == gameObject.name && other.gameObject.layer == 9 && gameObject.layer == 8) {  //that means we hit an enemy bullter with our bullet thats 30 points!!
			Debug.Log("collider name: " + other.name  + " our gameobject name " + gameObject.name + " layer of collider: " +other.gameObject.layer+" layer of our go: " + gameObject.layer  );
			PlayerSpawner.instance.increaseScore(30);
		}

	}



	void changeInvulnColors(bool change){
		if (change) {
			spriteRend.color = new Color32(255,122,122,255);		
		}
		else {
			spriteRend.color = Color.white;
		}
	}


	void Update(){


		//UI STUFF


		if (health > 0 && invulnTimer > 0) {
			invulnTimer -= Time.deltaTime;
			if (invulnTimer <= 0) {
				gameObject.layer = correctLayer;
				changeInvulnColors(false); //go back to normal
			}
			else{
				invulnAnimTime -= Time.deltaTime;
				if(invulnAnimTime <= 0){
					changecolorflag = !changecolorflag;
					changeInvulnColors(changecolorflag);
					invulnAnimTime = invulnAnimTimeStart;
				}
			}

		}
		else {
			if(gameObject.layer == 10){
				gameObject.layer = correctLayer;
				changeInvulnColors(false);
			}
		}


		if (health <= 0) {
			Die ();		
		}
	}
	void Die(){
		Destroy (gameObject);
		Debug.Log ("die function running for layer " + gameObject.layer + " and name " + gameObject.name);
		if(gameObject.layer == 8 && gameObject.name == "PlayerShip"){ //thats definately our player
			PlayerSpawner.instance.numOfPlayers--;
			FBHolder.instance.SetScore (PlayerSpawner.instance.getScore());
		}

		if(gameObject.layer == 9 && gameObject.name == "Enemy01(Clone)"){ //we destroyed an enemy, thats 20points;	
			PlayerSpawner.instance.increaseScore(20);
		}


	}
}
