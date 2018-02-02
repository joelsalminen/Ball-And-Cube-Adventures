/* Joel Salminen - 0401495 - joel.salminen@student.lut.fi - 10.12.2017 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* Controls the flow of the game
	- Starts the game
	- keeps track of coin and button interaction, opens doors when required
	- spawns coins, player objects, buttons
	- handles game over and level complete situations */

public class GameController : MonoBehaviour {

	public GameObject ball;
	public GameObject cube;
	public GameObject coin;
	public GameObject yellowDoor;
	public GameObject redDoor;
	public GameObject coinHolder;
	public GameObject buttonPlate;
	public GameObject hazardController;
	public Text coinText;
	public Text buttonText;
	public Text gameOverText;
	public Text victoryText;
	public Text levelCountText;
	public Text levelCountText2;
	public Light lt;
	public AudioClip victory;


	private int coins = 0;
	private int buttonsDown = 0;
	private int levelCount = 0;
	private int buttonCount;
	private int coinCount;

	private Vector3[] buttonPositions;
	private AudioSource audioSource;
	private CameraController mainCamera;
	private GameObject myGameObject;
	private GameObject hc;


	void Start(){
		coins = 0;
		buttonsDown = 0;
		audioSource = GetComponent<AudioSource>();
		myGameObject = GameObject.FindGameObjectWithTag("CameraController");
		mainCamera = myGameObject.GetComponent<CameraController>();
		mainCamera.ZoomIn();

		StartHazardController();
		SpawnButtons();
		SpawnCoins();
		SetTextFields();
		SpawnPlayerObjects();
		SpawnDoors();

	}

	void SpawnDoors(){
		Instantiate(yellowDoor, new Vector3(0, 1, 15.2f), Quaternion.identity);
		Instantiate(redDoor, new Vector3(0, 1.5f, 16f), Quaternion.identity);
	}

	void StartHazardController(){
		hc = Instantiate(hazardController, new Vector3(0, 0, 0), Quaternion.identity);
	}


	void SpawnPlayerObjects(){
		Instantiate(ball, new Vector3(2, 0.5f, 0), Quaternion.identity);
		Instantiate(cube, new Vector3(-2, 0.5f, 0), Quaternion.identity);
	}


	void SetTextFields(){
		coinText.text = "Coins left: " + coinCount;
		buttonText.text = "Buttons left: " + buttonCount;
		gameOverText.text = "";
		victoryText.text = "";
		levelCountText2.text = "";
		if (levelCount == 0){
			levelCountText.text = "";
		}

	}


	void Update(){
		DoorController();
	}


	/* Open the doors, if all coins are collected or buttons pushed down */
	void DoorController(){
		if (coins == coinCount){
			GameObject.FindGameObjectWithTag("YellowDoor").gameObject.SetActive(false);
		}

		if (buttonsDown == buttonCount){
			GameObject.FindGameObjectWithTag("RedDoor").gameObject.SetActive(false);
		}
	}


	/* Updates coin counters */
	public void CoinCollected(){
		coins++;
		coinText.text = "Coins left: " + (coinCount - coins);

	}


	/* Updates button counters */
	public void ButtonPushed(){
		buttonsDown++;
		buttonText.text = "Buttons left: " + (buttonCount - buttonsDown);
	}


	/* Spawns a random amount of coins in random locations in the beginning of the level */
	void SpawnCoins(){
		coinCount = Random.Range(15, 25);

		Vector3 spawnPosition;

		/* An array that keeps track of coin locations so that two coins will not be placed on 
		the same position */
		Vector3[] coinPositions = new Vector3[coinCount];

		for (int i = 0; i < coinCount; i++){
			spawnPosition = new Vector3(Random.Range(-14, 14), 0.5f, Random.Range(-14, 14));

			/* Calculates a random locations until a valid location is calculated. 
			The middle of the screen and locations that already have a coin are invalid locations */
			while ((spawnPosition.z < 3 && spawnPosition.z >-3) || (spawnPosition.x < 6 && spawnPosition.x >-6) || CheckIfExists(coinPositions, spawnPosition)){
				spawnPosition = new Vector3(Random.Range(-14, 14), 0.5f, Random.Range(-14, 14));
			}

			/* coins placed on top of buttons are raised by 1.0f */
			if (CheckIfExists(buttonPositions, spawnPosition)){
				spawnPosition.y =+ 1.0f;
			}

			/* Add location on the list and instantiate coin in that location */ 
			coinPositions[i] = spawnPosition;
			var coinholder = Instantiate (coinHolder, spawnPosition, Quaternion.identity);
			var coinSprite = Instantiate (coin, new Vector3(0, 0, 0), Quaternion.identity);
			coinSprite.transform.parent = coinholder.transform;
		}
	}


	/* Spawns buttons in random locations */
	void SpawnButtons(){ 
		buttonCount = Random.Range(3, 6);
		/* An array that keeps track of button locations so that two buttons will not be placed on 
		the same position */
		buttonPositions = new Vector3[buttonCount]; 


		Vector3 spawnPosition;
		for (int i = 0; i < buttonCount; i++){
			spawnPosition = new Vector3(Random.Range(-7, 7)*2, 0, Random.Range(-7, 7)*2);

			/* Calculates a random locations until a valid location is calculated. 
			The middle of the screen and locations that already have a button are invalid all invalid */
			while ((spawnPosition.z < 3 && spawnPosition.z >-3) || (spawnPosition.x < 6 && spawnPosition.x >-6) || CheckIfExists(buttonPositions, spawnPosition)){
				spawnPosition = new Vector3(Random.Range(-7, 7)*2, 0, Random.Range(-7, 7)*2);
			}
			
			/* add buttons location to the list, instantiate button*/
			buttonPositions[i] = spawnPosition;
			Instantiate (buttonPlate, spawnPosition, Quaternion.identity);
		}
	}


	/* Actions that are taken when level is completed */
	public void LevelComplete(){
		victoryText.text = "LEVEL COMPLETE";
		levelCount++;
		levelCountText.text = "Levels completed: " + levelCount;
		audioSource.PlayOneShot(victory, 1.0F);
		
		ClearBoard();
		StartCoroutine(LoadNextLevel());
	}


	/* Clears all objects in preparation for next level */
	void ClearBoard(){
		/* Disable cube in order to avoid inintentional deaths */
		GameObject.FindGameObjectWithTag("Player_cube").gameObject.SetActive(false);

		/* Destory hazardController */
		Destroy(hc.gameObject);

		/* Despawn all buttons */
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Pedestal&Button");
		for(int i=0; i < buttonCount; i++){
			GameObject.Destroy(gameObjects[i]);
		}

	}


	/* Coroutine because delay is wanted here */
	IEnumerator LoadNextLevel(){
		yield return new WaitForSeconds((5.0f));
		/* Next level is started by calling Start() function again */
		Start();
	}


	/* Checks if a Vector3 is in an array */ 
	bool CheckIfExists(Vector3[]list, Vector3 position){
		for(int i=0; i<list.Length; i++){
			if ((position.x == list[i].x) && (position.z == list[i].z)){
				return true;
			}
		}
		return false;

	}


	/* Called when one of the player objects dies */
	public void GameOver(){
		gameOverText.text = "GAME OVER";
		levelCountText.text = "";
		levelCountText2.text = "LEVELS COMPLETED: " + levelCount;
		mainCamera.ZoomOut();
		StartCoroutine(DimmerLights());
		StartCoroutine(LoadScene(0));
	}


	/* Darkes the lights for dramatic effect */
	IEnumerator DimmerLights(){
		while(lt.intensity > 0){
			lt.intensity = lt.intensity - 0.025f;
			yield return new WaitForSeconds((0.05f));
		}

	}

	IEnumerator LoadScene(int sceneIndex){
		yield return new WaitForSeconds((8.0f));
		SceneManager.LoadScene(sceneIndex);
	}

}
