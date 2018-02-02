/* Joel Salminen - 0401495 - joel.salminen@student.lut.fi - 10.12.2017 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Randomly spawns bombs that destroy the player if touched */
public class HazardSpawner : MonoBehaviour {
	public GameObject hazard;
	public GameObject bigHazard;
	public GameObject incomingDropZone;
	public GameObject incomingBigDropZone;
	public float waveDelay; /* delay between waves */ 


	void Start(){
		StartCoroutine(SpawnHazards());
	}


	/* Randomly spawns one of the hazard events once per 'waveDelay' */
	IEnumerator SpawnHazards(){
		yield return new WaitForSeconds(5.0f);
		int hazardType;
		while (true){
			hazardType = Random.Range(1, 5);

			if (hazardType == 1){
				StartCoroutine(SpawnBigHazards());
				yield return new WaitForSeconds(10.0f);
			}
			else if(hazardType == 2){
				StartCoroutine(LineHazardTopToBottom());
				StartCoroutine(LineHazardLeftToRight());
			}
			else if(hazardType == 3){
				StartCoroutine(LineHazardBottomToTop());
				StartCoroutine(LineHazardRightToLeft());
			}
			else{
				StartCoroutine(SpawnSmallHazards());
				
			}
	 		yield return new WaitForSeconds(waveDelay);
			
		}
	
	}


	/* An event that spawns large fields of explosives every four seconds */
	IEnumerator SpawnBigHazards(){
		for (int i=0; i<4; i++){
			BigHazard();
			yield return new WaitForSeconds(4.0f);
		}
		
		
	}



	void BigHazard(){
		/* Choosing the location of the dropzone at random */
		Vector3 randomPos = new Vector3(Random.Range(-1, 2)*7, 0.009f, Random.Range(-1, 2)*7);
		
		/* Instantiating a large dangerzone */
		Instantiate (incomingBigDropZone, randomPos, Quaternion.identity);

		/* Instantiating the bombs to be dropped on top of the danger zone */
		Vector3 position = randomPos + new Vector3(-5.0f, 90, 5.4f);
		for (int i=0; i<5; i++ ){
			Instantiate (bigHazard, position, Quaternion.Euler(new Vector3(90, 90, 0)));
			Instantiate (bigHazard, position + new Vector3 (5, 0, 0), Quaternion.Euler(new Vector3(90, 90, 0)));
			Instantiate (bigHazard, position + new Vector3 (10, 0, 0), Quaternion.Euler(new Vector3(90, 90, 0)));
			position += new Vector3(0, 0, -2.7f);
		}

	}


	/* An event that spawns a random amount of small bombs in random locations, 3,3 times a second */
	IEnumerator SpawnSmallHazards(){
		for (int i=0; i < Random.Range(13, 20); i++){
			Vector3 spawnPosition = new Vector3 (Random.Range(-3, 4)*4, 50, Random.Range(-3, 4)*4);
	 		SmallHazard(spawnPosition);
			yield return new WaitForSeconds(0.3f);
		}
		
	}


	/* An event that spawns a line of bombs, from bottom to top */
	IEnumerator LineHazardBottomToTop(){
		Vector3 spawnPosition = new Vector3(Random.Range(-3,4)*4, 50, -12);
		for(int i=0; i<7; i++){
			SmallHazard(spawnPosition);
			spawnPosition += new Vector3(0, 0, 4.0f);
			yield return new WaitForSeconds(0.3f);
		}
	}


	/* an event that spawns a line of bombs, from top to bottom */
	IEnumerator LineHazardTopToBottom(){
		Vector3 spawnPosition = new Vector3(Random.Range(-3,4)*4, 50, 12);
		for(int i=0; i<7; i++){
			SmallHazard(spawnPosition);
			spawnPosition += new Vector3(0, 0, -4.0f);
			yield return new WaitForSeconds(0.3f);
		}
	}


	/* An event that spawns a line of bombs, right to left */
	IEnumerator LineHazardRightToLeft(){
		Vector3 spawnPosition = new Vector3(12, 50, Random.Range(-3,4)*4);
		for(int i=0; i<7; i++){
			SmallHazard(spawnPosition);
			spawnPosition += new Vector3(-4.0f, 0, 0);
			yield return new WaitForSeconds(0.3f);
		}
	}


	/* An event that spawns a line of bombs, left to right */
	IEnumerator LineHazardLeftToRight(){
		Vector3 spawnPosition = new Vector3(-12, 50, Random.Range(-3,4)*4);
		for(int i=0; i<7; i++){
			SmallHazard(spawnPosition);
			spawnPosition += new Vector3(4.0f, 0, 0);
			yield return new WaitForSeconds(0.3f);
		}
	}


	/* Spawns a small bomb and a danger zone below it */ 
	void SmallHazard(Vector3 spawnPosition){
		Vector3 onGroundPosition = spawnPosition - new Vector3(0, 49.999f, 0);
		Instantiate (hazard, spawnPosition, Quaternion.identity);
		Instantiate (incomingDropZone, onGroundPosition, Quaternion.identity);
	}

}