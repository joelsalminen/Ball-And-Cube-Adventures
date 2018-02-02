/* Joel Salminen - 0401495 - joel.salminen@student.lut.fi - 10.12.2017 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Cube movement control, activating buttons */
public class CubeController : MonoBehaviour {
	
	public CubeMovementController cubeMove;
	public Animator anim;

	private float timestamp;
	private float delay = 0.70f; /* delay between movement */
	private GameObject myGameObject;
	private GameController gameController;


	void Start(){
		anim = GetComponent<Animator>();
		GameObject myGameObject = GameObject.Find("GameController");
		gameController = myGameObject.GetComponent<GameController>();
	}


	void Update(){
		/* Cube movement is done by playing an animation and then manually moving the cube forward.
		Controlled by WASD-keys */
		if ((Time.time >= timestamp) ){ /* delay between movement commands */
			if((Input.GetKey (KeyCode.W)) && (cubeMove.CheckPosition("forward"))){

				anim.Play("cubeJumpForward"); /* play the animation */
				cubeMove.MovementManager("forward"); /* move the Cube forward */
				timestamp = Time.time + delay; /* update movement command delay*/
			}
			if((Input.GetKey (KeyCode.A)) && (cubeMove.CheckPosition("left"))){

				anim.Play("cubeJumpLeft");
				cubeMove.MovementManager("left");
				timestamp = Time.time + delay;
			}
			if((Input.GetKey (KeyCode.S)) && (cubeMove.CheckPosition("down"))){

				anim.Play("cubeJumpDown");
				cubeMove.MovementManager("down");
				timestamp = Time.time + delay;
			}
			if((Input.GetKey (KeyCode.D)) && (cubeMove.CheckPosition("right"))){

				anim.Play("cubeJumpRight");
				cubeMove.MovementManager("right");
				timestamp = Time.time + delay; 
			}
		}
		
	}


	/* Button interaction */ 
	void OnTriggerEnter (Collider other){
		if(other.gameObject.CompareTag("Button")){
			GetComponent<AudioSource>().Play();
			other.gameObject.SetActive(false);
			gameController.ButtonPushed();
		}

	}

}