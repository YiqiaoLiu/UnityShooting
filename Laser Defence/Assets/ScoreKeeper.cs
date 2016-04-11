using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public int playerScore;
	public Text playerScoreText;

	// Use this for initialization
	void Start () {
		playerScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//playerScore += 100;
		//Score (playerScore);
		playerScoreText.text = playerScore.ToString();
	}	

	public void Score(int points){

	}

	public void Reset(){
		playerScore = 0;
	}
}
