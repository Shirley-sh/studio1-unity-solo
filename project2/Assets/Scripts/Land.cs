using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour {
	public float speed;
	public float size;
	bool flooded;
	public bool finished;

	float timer;
	// Use this for initialization
	void Start () {
		timer = 0.0f;
		flooded = false;
		finished = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!flooded) {
			timer += Time.deltaTime;
			transform.localScale *= 1 + Mathf.Sin (timer * speed / 2) * (size / 100);
		} else {
			transform.localScale -= Vector3.Lerp(new Vector3(0,0,0),transform.localScale,Time.deltaTime/3);
		}

		if (transform.localScale.magnitude < 0.01) {
			finished = true;
		}


	}

	public void flood(){
		flooded = true;
	}
}
