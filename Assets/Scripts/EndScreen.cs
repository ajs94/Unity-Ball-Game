using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndScreen : MonoBehaviour {

    public Text scoreText;

    private int score;

    void Start () {
        GameObject thePlayer = GameObject.Find("PlayerBall");
        PlayerController playerScript = thePlayer.GetComponent<PlayerController>();
        score = playerScript.score;

        SetScoreText();
    }
	
	void Update () {
		
	}

    void SetScoreText()
    {
        scoreText.text = "Final Score: " + score.ToString();
    }
}
