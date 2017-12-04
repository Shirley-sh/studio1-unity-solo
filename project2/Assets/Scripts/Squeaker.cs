using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squeaker : MonoBehaviour {

    AudioSource audiosource;

    public bool newFriendsqueak;
    public bool alreadyFriendsqueak;
    public bool soundPlayed;
    public AudioClip newsqueak;
    public AudioClip alreadysqueak;

	// Use this for initialization
	void Start () {
        audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (newFriendsqueak == true) 
        {
            audiosource.PlayOneShot(newsqueak);
            newFriendsqueak = false;
        }

		if (alreadyFriendsqueak == true) 
        {
            audiosource.PlayOneShot(alreadysqueak);
            alreadyFriendsqueak = false;
        }
	}
}
