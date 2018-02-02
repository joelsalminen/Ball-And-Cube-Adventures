/* Joel Salminen - 0401495 - joel.salminen@student.lut.fi - 10.12.2017 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Blowing up bombs and players */
public class BombController : MonoBehaviour {
	public GameObject playerExplosion;
	public GameObject bombExplosion;
	private GameObject myGameObject;
	private GameController gameController;


	void Start(){
		GameObject myGameObject = GameObject.Find("GameController");
		gameController = myGameObject.GetComponent<GameController>();
	}

/* https://answers.unity.com/questions/440371/how-to-run-function-in-another-script-with-prefabs.html */

	void OnTriggerEnter(Collider other){
		/* Exploding a player object */
		if (other.gameObject.CompareTag("Player_cube") || other.gameObject.CompareTag("Player_ball")){
			Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
			other.gameObject.SetActive(false);
			gameController.GameOver();
		}

		/* Bombs are destroyed whenever they touch the GameBoard */
		if (other.gameObject.CompareTag("GameBoard")){
			Instantiate(bombExplosion, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
