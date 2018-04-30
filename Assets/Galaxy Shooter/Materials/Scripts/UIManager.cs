using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Sprite[] lives;
	public Image livesImageDisplay, gameTitle;
	public Text scoreText;
	public int score;
	public Image titleScreen;

	public void UpdateLives(int currentLives){
		Debug.Log("Player Lives: " + currentLives);
		livesImageDisplay.sprite = lives[currentLives];
	}

	public void UpdateScore(){
		score += 10;
		scoreText.text = "Score: " + score;
	}

	public void ShowTitleScreen(){
		titleScreen.enabled = true;
	}

	public void HideTitleScreen(){
		titleScreen.enabled = false;
		scoreText.text = "Score: ";
	}
}
