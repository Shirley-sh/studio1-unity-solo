using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
	float timer;
	Rigidbody2D rb;
	public Vector2 waveForce;
	public Vector2 floodForce;
	public float speed;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		timer = Random.Range (-1, 1);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer += Time.deltaTime;
		if (Mathf.Sin (timer*speed) >= 0.9999) {
			rb.AddForce (waveForce);
		}

		if (Mathf.Sin (timer*speed)  <= -0.9999) {
			rb.AddForce (waveForce*-1);
		}

	}

	public void Flood(){
		waveForce = floodForce;
	}
}
