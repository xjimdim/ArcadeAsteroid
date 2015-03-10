![Arcade Asteroid by XjiMDim](https://github.com/xjimdim/ArcadeAsteroid/blob/master/screencaptures/logo.jpg)
***

This is the complete project of Arcade Asteroid (v1.0) made entirely in Unity 4.6 with full Facebook integration (using Facebook SDK 6.2.1).

## What is Arcade Asteroid?

Arcade Asteroid is a clone game of the original arcade game [Asteroids](http://en.wikipedia.org/wiki/Asteroids_%28video_game%29). The basic code (movement, shooting, and basic damage methods) and most of the sprites are from [quill18creates](https://www.youtube.com/user/quill18creates) (a great youtuber who does excellent Unity tutorials). 

So, I took over quill18's creation (which was mostly a showcase of some scripts and ideas) and made it a playable game. 

## Added features 
* Moving in and out of the screen "teleportation" (like the one in the original asteroid arcade game) 
* Health Points and multiple lives 
* A basic score system
* Invulnerability period for when you lose a health point
* Full Facebook integration through the Facebook SDK for Unity which includes a highscore system between friends, inviting people to the the game and sharing the application on the player's Facebook Timeline.

## Plans for future releases 

I am planning on adding a whole lot of extra features in the near future like: LOTS of different enemies, power ups that can give you different kinds of weapons, health packs and adding some actual asteroids :)

If you have any idea about a new feature please don't hesitate to contact me. 

## Facebook Integration 

This game is fully integrated with Facebook. However, due to Facebook policy, for the game to be available and published through Facebook's platform it must be first uploaded to an SSL secure web hosting server (something that I don't actually own at the moment). 

If you want to connect the project to your Facebook account and/or app do the following:

1. You will need a Facebook App ID so, you must create a new Facebook App from the website https://developers.facebook.com/ (My apps -> Add a new app) 
2. After you create your Facebook App and have your unique App ID generated, go back to Unity and (since the Facebook SDK is included in the project files) you will probably be able to see the Facebook menu at the top toolbar of Unity. Click there and then "Edit Settings" and add the App ID you just created at the Unity inspector.

You are set to go, you can upload the project to a secure SSL certified server and the game would be ready to be published.

### Testing with Facebook's Test Users

If however you want to test the game like I did, you will have to create some Test users and make them friends with each other. That can be easily done from the main page of your App (at Facebook Developers). 

Just go to the menu Roles (at the left) and then in the main toolbar click Test Users. 
Here you can add up to 5 Test Users at a time (I'm not really sure how many users you can actually have but I guess a lot). For every group of users you make don't forget to enable "Authorize Test Users for This App?" during activation and at Login Permissions to write publish_actions (if you don't have this enabled you will not be able to POST scores).

So you create those users and then you click the edit button next to every user and then "Manage this test user's friends" and make everyone friends with everyone. 

Then when you try to Play the game at Unity, click the Login with Facebook button and then you will get this screen:

![unity and facebook login](https://github.com/xjimdim/ArcadeAsteroid/blob/master/screencaptures/image%201.jpg)

The User Access Token is a unique token (that expires every hour) that you can get for every test user you just created. Just go to the edit button next to the user you want to login with and press "Get an access token for this test user". 

There you go, you are set to test this properly :)

Don't forget to let me know about any bugs you find or any ideas you may have!!

## Some more screens of the game: 

Login Screen (pretty simple, will probably get a visual remake at the future):
![Login Screen](https://github.com/xjimdim/ArcadeAsteroid/blob/master/screencaptures/image%202.jpg)

Gameplay:
![Arcade Asteroids Gameplay](https://github.com/xjimdim/ArcadeAsteroid/blob/master/screencaptures/image%203.jpg) 

![Arcade Asteroids Gameplay](https://github.com/xjimdim/ArcadeAsteroid/blob/master/screencaptures/image%204.jpg)
 
Game Over Screen: 
![Arcade Asteroids Game Over Screen](https://github.com/xjimdim/ArcadeAsteroid/blob/master/screencaptures/image%205.jpg)
 