/* Joel Salminen - 0401495 - joel.salminen@student.lut.fi - 10.12.2017 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Start the game from the main menu */
public class LoadSceneOnClick : MonoBehaviour {

	/* https://unity3d.com/learn/tutorials/topics/user-interface-ui/creating-main-menu*/ 
	public void LoadByIndex(int sceneIndex){
		SceneManager.LoadScene(sceneIndex);
	}
}
