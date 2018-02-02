/* Joel Salminen - 0401495 - joel.salminen@student.lut.fi - 10.12.2017 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Ball movement, picking up collectables, finishing a level */
public class BallController : MonoBehaviour {
	
	public float speed;
	public GameObject incoming;
	public GameObject playerExplosion;

	private Rigidbody rb;
	private GameObject myGameObject;
	private GameController gameController;


	void Start(){
		rb = GetComponent<Rigidbody>();

		/* Ignores collision between Ball and IncomingDrop -objects */
		Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), incoming.GetComponent<Collider>());
		GameObject myGameObject = GameObject.Find("GameController");
		gameController = myGameObject.GetComponent<GameController>();
	}
	
	void FixedUpdate(){
		/* Moving the ball with arrow keys and physics */
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other){
		/* Picking up collectables */
		if (other.gameObject.CompareTag("Collectable")){
			Destroy(other.transform.parent.gameObject);
			/* Informs GameController that a coin was collected*/
			gameController.CoinCollected();

			GetComponent<AudioSource>().Play();
			
		}

		/* Level Complete, activates when finish line is touched by the Ball */
		if (other.gameObject.CompareTag("Finish")){
			gameController.LevelComplete();
		}

		/* if outside game area is reached */
		if (other.gameObject.CompareTag("Lava")){
			Instantiate(playerExplosion, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject);
			gameController.GameOver();
		}
	}



}


