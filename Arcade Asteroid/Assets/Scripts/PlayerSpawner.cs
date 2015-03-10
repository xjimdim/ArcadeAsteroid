using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour {

	// this is also the game manager script (kinda)


	public static PlayerSpawner instance;
	public GameObject playerPrefab;
	public GameObject playerInstance;
	GameObject[] errorPlayers;

	public int numLives = 4; // its getting decreased at the first player swawn so its actually 3 lives 


	float respawnTimer = 3.5f;

	public int numOfPlayers = 0;
	private int score; 

	//UI Stuff
	Text LivesLeftText;
	public GameObject GameOverTextAndButtonGO;

	Text HealthText;
	Image Health1;
	Image Health2;
	Image Health3;

	Text ScoreText;
	Text FinalScoreText;


	void Awake(){
		instance = this; 
	}


	void Start () {
		SpawnPlayer ();

		//UI initialisation
		LivesLeftText = GameObject.Find ("LivesLeftText").GetComponent<Text>();
		GameOverTextAndButtonGO = GameObject.Find ("GameOverTextAndButton");
		FinalScoreText = GameObject.Find ("FinalScoreText").GetComponent<Text>();
		PlayerSpawner.instance.GameOverTextAndButtonGO.transform.Find("NewHighScoreImg").gameObject.SetActive(false); 
		GameOverTextAndButtonGO.SetActive (false);


		HealthText = GameObject.Find ("Health Text").GetComponent<Text> ();
		Health1 = GameObject.Find("Health 1").GetComponent<Image>();
		Health2 = GameObject.Find("Health 2").GetComponent<Image>();
		Health3 = GameObject.Find("Health 3").GetComponent<Image>();	

		ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();	
		score = 0;

	}

	public void PlayAgain(){
		/*
		score = 0;
		numLives = 4;
		numOfPlayers = 0;
		respawnTimer = 3.5f;
		GameOverTextAndButtonGO.SetActive (false);
		SpawnPlayer ();
		*/

		// i think thats better
		Application.LoadLevel ("_scene");
	} 

		

	void Update () {

		//UI Stuff

		//lives ui and gameover
		if (numLives > 0 || playerInstance != null) {
			LivesLeftText.text =  "Lives Left: " + numLives;
		}
		else{
			GameOverTextAndButtonGO.SetActive(true);
			FinalScoreText.text = "Your score: " + score; 

		}

		//health ui:
		if (playerInstance != null) {

			HealthText.text = "Health: ";

			if(playerInstance.GetComponent<DamageHandler>().health == 3){
				Health1.enabled = true;
				Health2.enabled = true;
				Health3.enabled = true;
			}
			else if(playerInstance.GetComponent<DamageHandler>().health == 2){
				Health1.enabled = true;
				Health2.enabled = true;
				Health3.enabled = false;
			}
			else if(playerInstance.GetComponent<DamageHandler>().health == 1){
				Health1.enabled = true;
				Health2.enabled = false;
				Health3.enabled = false;
			}
			else if(playerInstance.GetComponent<DamageHandler>().health == 0){
				Health1.enabled = false;
				Health2.enabled = false;
				Health3.enabled = false;
			}	
		}
		else{
			if(numOfPlayers==0){
				if(respawnTimer.ToString ("0") != "0"){ // dint like the 0 at the end 
					HealthText.text = "Respawning in " + respawnTimer.ToString ("0");
				}
			}
		}

		//score ui:

		ScoreText.text = "Score: " + score;







		//weird glitch handling: 
		errorPlayers = GameObject.FindGameObjectsWithTag ("Player");
		if (errorPlayers != null) {
			foreach (GameObject pl in errorPlayers){
				if(pl.GetComponent<PlayerMovement>().enabled == false){
					Destroy(pl);
					PlayerSpawner.instance.numOfPlayers--;
				}
			}		
		}


		if (playerInstance == null && numLives > 0 && numOfPlayers == 0) {
			Debug.Log ("runnin respawn timer");
			respawnTimer -= Time.deltaTime;

			if(respawnTimer <= 0.5){
				SpawnPlayer();	
			}	
		}
		else if(playerInstance == null){
			//Debug.Log ("number of lives " + numLives + " numofplayer: " + numOfPlayers);
		}
	}

	public void increaseScore(int value){
		score = score + value;
	}

	void SpawnPlayer(){
		numLives--;
		respawnTimer = 3.5f;
		playerInstance = (GameObject) Instantiate (playerPrefab, transform.position, Quaternion.identity);
		playerInstance.name = "PlayerShip";
		numOfPlayers++;
		
	}

	public int getScore(){
		return score;
	}


}
