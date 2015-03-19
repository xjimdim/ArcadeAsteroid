using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class FBHolder : MonoBehaviour {

	public static FBHolder instance;
	public GameObject UIFBIsLoggedIn;
	public GameObject UIFBIsNotLoggedIn;
	public GameObject UIFBAvatar;
	public GameObject UIFBUserName;

	public GameObject ScoreEntryPanel;
	public GameObject ScoreScrollList;

	private List<object> scoresList = null;

	private List<object> userHighScore = null;

	private Dictionary<string, string> profile = null;



	void Awake(){
		instance = this;
		DealWithFBMenus (false);
		FB.Init (SetInit, OnHideUnity);
	}

	private void SetInit(){
		Debug.Log ("FB Init Done");

		if (FB.IsLoggedIn) {
			Debug.Log ("FB Logged in");
			DealWithFBMenus(true);	
		}
		else{
			DealWithFBMenus(false);	
		}
	}

	private void OnHideUnity (bool isGameShown){

		if (!isGameShown) {
			Time.timeScale = 0;			

		} 
		else {
			Time.timeScale = 1;
		}

	}

	public void FBLogin(){
		FB.Login ("email, user_friends, publish_actions", AuthCallback);
	}

	void AuthCallback(FBResult result){
		if (FB.IsLoggedIn) {
			Debug.Log ("FB login worked");	
			DealWithFBMenus(true);
		}
		else{
			Debug.Log("FB Login Fail");
			DealWithFBMenus(false);
		}
	}

	void DealWithFBMenus(bool isLoeggedIn){
		if (isLoeggedIn) {
			UIFBIsLoggedIn.SetActive(true);
			UIFBIsNotLoggedIn.SetActive(false);


			//get profile picture
			FB.API (Util.GetPictureURL("me", 128, 128),Facebook.HttpMethod.GET, DealWithProfilePicture);


			//get user name
			FB.API ("/me?fields=id,first_name", Facebook.HttpMethod.GET, DealWithUserName);

			QueryScores();

		} 
		else {
			UIFBIsLoggedIn.SetActive(false);
			UIFBIsNotLoggedIn.SetActive(true);
		}
	}

	void DealWithProfilePicture(FBResult result){
		if (result.Error != null) {
			Debug.Log("Problem with getting profile picture");	
			FB.API (Util.GetPictureURL("me", 128, 128),Facebook.HttpMethod.GET, DealWithProfilePicture);
			return;
		}

		Image UserAvatar = UIFBAvatar.GetComponent<Image> (); 
		UserAvatar.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 (0, 0));

	}

	void DealWithUserName(FBResult result){
		if (result.Error != null) {
			Debug.Log("Problem with user name");	
			FB.API ("/me?fields=id,first_name", Facebook.HttpMethod.GET, DealWithUserName);
			return;
		}
		profile = Util.DeserializeJSONProfile (result.Text);

		Text UserMsg = UIFBUserName.GetComponent<Text> ();
		UserMsg.text = "Hello, " + profile ["first_name"]; 

		
	}

	public void ShareWithFriends(){

		FB.Feed (
			linkCaption: "This is the best game I have ever played. PERIOD",
			picture: "https://uberfacts.files.wordpress.com/2013/02/asteroid.jpg",
			linkName: "BEST GAME EVER",
			link: "http://apps.facebook.com/"+ FB.AppId + "/?challenge_brack=" + (FB.IsLoggedIn ? FB.UserId : "guest")
			);
	}

	public void InviteFriends(){
		
		FB.AppRequest (
			message: "Hey, come play this epic game!",
			title: "Invite your friends to join you!!"
			);
	}

	//Scores handling: 

	public void QueryScores(){
		FB.API ("/app/scores?fields=score,user.limit(40)", Facebook.HttpMethod.GET, ScoresCallback);
	}

	private void ScoresCallback(FBResult result) {
		Debug.Log ("Scores result: " + result.Text);

 		
		scoresList = Util.DeserializeScores(result.Text);

		foreach (Transform child in ScoreScrollList.transform) {
			GameObject.Destroy(child.gameObject);
		}


		foreach (object score in scoresList) {
			var entry = (Dictionary<string,object>) score;
			var user = (Dictionary<string,object>) entry["user"];


		
			GameObject ScorePanel;
			ScorePanel = Instantiate(ScoreEntryPanel) as GameObject;
			ScorePanel.transform.parent = ScoreScrollList.transform;

			Transform ThisScoreName = ScorePanel.transform.FindChild("NamePanelBG").FindChild("FriendName");
			Transform ThisScoreScore = ScorePanel.transform.FindChild("ScorePanelBG").FindChild("FriendScore");;

			Text ScoreName = ThisScoreName.GetComponent<Text>();
			Text ScoreScore = ThisScoreScore.GetComponent<Text>(); 


			string temp = user["name"].ToString();
			string temp2 = temp.Substring(0, temp.IndexOf(" ")+1);  //get only the name
			ScoreName.text = temp2.ToString();
			ScoreScore.text = entry["score"].ToString();

			Transform TheUserAvatar = ScorePanel.transform.Find("FriendAvatar");
			Debug.Log ("found: " + TheUserAvatar.name);
			Image UserAvatar = TheUserAvatar.GetComponent<Image>();

			FB.API (Util.GetPictureURL(user["id"].ToString(), 128, 128), Facebook.HttpMethod.GET, delegate(FBResult pictureResult) {
				if(pictureResult.Error != null) { // in case there was an error
					Debug.Log (pictureResult.Error);
				}
				else { //we got the image
					UserAvatar.sprite = Sprite.Create(pictureResult.Texture, new Rect (0, 0, 128, 128), new Vector2 (0, 0));
				}
			});

		}
	
	}

	public void SetScore(int scr){

		FB.API ("/me/scores", Facebook.HttpMethod.GET, delegate(FBResult getResult) {
			if(getResult.Error != null){
				Debug.Log("ERROR GETTING USER HIGH SCORE");
			} 
			else{

				userHighScore = Util.DeserializeScores (getResult.Text);
				try{
					object firstScoreFound = userHighScore[0]; 
					var entry2 = (Dictionary<string,object>) firstScoreFound; // now entry2["score"] has the highscore
					
					//compare highscore and current scr 
					
					if(scr > int.Parse(entry2["score"].ToString())){
						var scoreData = new Dictionary<string,string> ();
						scoreData ["score"] = scr.ToString();
						
						FB.API("/me/scores", Facebook.HttpMethod.POST, delegate(FBResult result) {
							Debug.Log ("High Score sumbit result: "+result.Text);
						},scoreData);

						PlayerSpawner.instance.GameOverTextAndButtonGO.transform.Find("NewHighScoreImg").gameObject.SetActive(true); 
					}
					else {
						PlayerSpawner.instance.GameOverTextAndButtonGO.transform.Find("NewHighScoreImg").gameObject.SetActive(false); 
					}
				}
				catch(Exception e) {
					var scoreData = new Dictionary<string,string> ();
					scoreData ["score"] = scr.ToString();
					
					FB.API("/me/scores", Facebook.HttpMethod.POST, delegate(FBResult result) {
						Debug.Log ("High Score sumbit result: "+result.Text);
					},scoreData);
				}

			}
		});


	}

	public void PlayGame(){
		DontDestroyOnLoad(gameObject);
		Application.LoadLevel("_scene");

	}


}
