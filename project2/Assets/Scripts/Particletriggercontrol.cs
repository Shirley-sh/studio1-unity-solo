using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particletriggercontrol : MonoBehaviour {

    ParticleSystem particlesystem;

    public ParticleSystem sparkleRight;
    public ParticleSystem sparkleLeft;
    public ParticleSystem heart;
    public bool holdGetright = false;
    public bool holdGetleft = false;
    public bool heldAlready = false;

	// Use this for initialization
	void Start () {
    particlesystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (holdGetright == true)
        {
            sparkleRight.Play();
            holdGetright = false;
            Debug.Log("Particles should play");
        }

        if (holdGetleft == true)
        {
            sparkleLeft.Play();
            holdGetleft = false;
            Debug.Log("Particles should play");
        }

        if (heldAlready == true)
        {
            heart.Play();
            heldAlready = false;
        }
		
	}
}
