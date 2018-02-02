/* Joel Salminen - 0401495 - joel.salminen@student.lut.fi - 10.12.2017 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Exits the game */
public class ExitOnClick : MonoBehaviour {

	/* https://unity3d.com/learn/tutorials/topics/user-interface-ui/creating-main-menu */
	public void Quit(){
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
				Application.Quit();
		#endif
	}
}
