/* Joel Salminen - 0401495 - joel.salminen@student.lut.fi - 10.12.2017 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Dangerzone animation control */
public class ColorShaker : MonoBehaviour {

	/* https://answers.unity.com/questions/430298/how-to-cycle-through-color-spectrum-with-material.html */
	
	Material render;
	void Start () {
		render = GetComponent<Renderer>().material;
	}
	
	public Color lerpedColor = Color.white;

    void Update() {
       render.color = Color.Lerp(Color.yellow, new Color(1.0f, 0.3f, 0.0f, 0.5f), Mathf.PingPong(Time.time, 1));
    }
}
