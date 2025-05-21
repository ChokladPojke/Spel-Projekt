using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {

	public float speed;
	private float x;
	public float destination;
	public float original;

	// Moves the background to the left
	// and resets it to the original position
	void Update(){
		x = transform.position.x;
		x += speed * Time.deltaTime;
		transform.position = new Vector3 (x, transform.position.y, transform.position.z);
		if (x <= destination)
		{
			x = original;
			transform.position = new Vector3 (x, transform.position.y, transform.position.z);
		}
	}
}
