using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager: MonoBehaviour {
    AudioSource aud;

    public float countdownTimer;
	public GameObject[] lands;
	public GameObject[] shells;
	public GameObject[] otters;
	public GameObject player;
	public Text timer;
	public Text score;
	public int targetSocre;
	public bool gameover;
	bool flooded;
    private int theOtterScore;
    private int totalOtters;

    private int levelSelect;
    public AudioClip gameOversqueak;
    public bool overSqueaksounded = false; //Did the game over squeak play already?


	public GameObject levelClearedPanel;
	public GameObject gameOverPanel;
	void Start(){
		lands = GameObject.FindGameObjectsWithTag ("Land");
		otters = GameObject.FindGameObjectsWithTag ("Otter");
		gameover = false;
		flooded = false;
        theOtterScore = player.GetComponent<PlayerController>().otterScore;
        aud = GetComponent<AudioSource>();
        totalOtters = otters.Length;

        levelSelect = 0;    //start at level 0, "Main"
    }

	// Update is called once per frame
	void Update () {
        theOtterScore = player.GetComponent<PlayerController>().otterScore;
        foreach (GameObject otter in otters) {
			if (!otter.GetComponent<Otter> ().isAlive ()) {
				gameover = true;
                break;
			}
		}

		if (!gameover) {
			if (theOtterScore == targetSocre) {
                if (SceneManager.GetActiveScene().name == "level5") {   //if we're on the last level,
                    gameover = true;
                    levelClearedPanel.SetActive(true);
                }
				else {
                    levelSelect++;
                    SceneManager.LoadScene(levelSelect);
                }
			}



			if (countdownTimer > 0 && !flooded) {
				countdownTimer -= Time.deltaTime;
			} else {
				countdownTimer = 0;
			}
			if (countdownTimer < 1 && !flooded) {
				foreach (GameObject go in lands) {
					go.GetComponent<Land> ().flood ();

				}
				shells = GameObject.FindGameObjectsWithTag ("Collectable");
				foreach (GameObject shell in shells) {
					shell.GetComponent<Wave> ().Flood ();
				}
			}
			//update ui
			timer.text = "" + (int)countdownTimer;
			score.text = theOtterScore + " / " + totalOtters;
		} else {
            gameOverPanel.SetActive (true);
		}
        if ((gameover == true) && (overSqueaksounded == false))
        {
            aud.PlayOneShot(gameOversqueak);
            overSqueaksounded = true;
        }

    }
}
