/* Joel Salminen - 0401495 - joel.salminen@student.lut.fi - 10.12.2017 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Camera animation control */
public class CameraController : MonoBehaviour {

	public Animator anim;

	void Start(){
		anim = GetComponent<Animator>();
	}

	public void ZoomIn(){
		anim.Play("ZoomIn");
	}

	public void ZoomOut(){
		anim.Play("ZoomOut");
	}
}
