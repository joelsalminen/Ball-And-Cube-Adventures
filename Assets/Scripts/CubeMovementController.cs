/* Joel Salminen - 0401495 - joel.salminen@student.lut.fi - 10.12.2017 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handling the movement of the Cube */
public class CubeMovementController : MonoBehaviour {
	private Transform transfor;

	void Start () {
		transfor = GetComponent<Transform>();
	}


	/* Starts a coroutine */
	public void MovementManager(string s){
		StartCoroutine(WaitUntilAnimationFinishes(s));
	}


	/* Waits until the jump animation of the Cube is finished, then 
	calls the movement function*/
	IEnumerator WaitUntilAnimationFinishes(string s){
		yield return new WaitForSeconds((0.6834f));
		MoveCube(s);
	}


	/* Moves the cube to te correct direction depending on which button
	was pushed */ 
	void MoveCube(string s){
		if (s.Equals("forward")){
			transfor.position += new Vector3(0, 0, 2);
		}
		if (s.Equals("left")){
			transfor.position += new Vector3(-2, 0, 0);
		}
		if (s.Equals("down")){
			transfor.position += new Vector3(0, 0, -2);
		}
		if (s.Equals("right")){
			transfor.position += new Vector3(2, 0, 0);
		}
		
	}


	/* Prevents the Cube from moving outside the walls of the game area */
	public bool CheckPosition(string s){

		if (s.Equals("forward") && transfor.position.z < 14){
			return true;
		}
		else if (s.Equals("left") && transfor.position.x > -14){
			return true;
		}
		else if (s.Equals("down") && transfor.position.z > -14){
			return true;
		}
		else if (s.Equals("right") && transfor.position.x < 14){
			return true;
		}

		return false;
	}

}

