using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particletriggercontrol : MonoBehaviour {

    ParticleSystem particlesystem;

    public ParticleSystem sparkle;
    public ParticleSystem heart;
    public bool holdGet = false;
    public bool heldAlready = false;

	// Use this for initialization
	void Start () {
    particlesystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (holdGet == true)
        {
            sparkle.Play();
            holdGet = false;
        }

        if (heldAlready == true)
        {
            heart.Play();
            heldAlready = false;
        }
		
	}
}
