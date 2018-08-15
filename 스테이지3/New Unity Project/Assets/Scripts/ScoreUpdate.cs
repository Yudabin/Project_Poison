using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreUpdate : MonoBehaviour {

    public static int score = 0;
    Text scoreLabel;

    void Awake()
    {
        score = 0;
        scoreLabel = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        //scoreLabel.text = ScoreManager.score.ToString();
        scoreLabel.text = "Phone : " + score.ToString();
	}
}
