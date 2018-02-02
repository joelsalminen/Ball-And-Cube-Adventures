/* Joel Salminen - 0401495 - joel.salminen@student.lut.fi - 10.12.2017 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Destroys an object after some time */
public class DestroyByTime : MonoBehaviour {

	/* https://unity3d.com/learn/tutorials/projects/space-shooter/spawning-waves?playlist=17147 */
	public float lifetime;
	void Start () {
		Destroy(gameObject, lifetime);
	}
}
