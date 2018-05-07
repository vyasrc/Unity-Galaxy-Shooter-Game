using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public Sprite[] lives;
	public Image playerOneLivesImageDisplay, playerTwoLivesImageDisplay, gameTitle;
	public Text scoreText,  bestText, playerTwoScoreText, winnerDisplay;
	public int currentScore, bestScore, playerTwoCurrentScore;
	public Image titleScreen;

	private GameManager _gameManager;

	void Start () {
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		bestScore = PlayerPrefs.GetInt("HighScore", 0);
		if (_gameManager.isCoOpMode == false){
			bestText.text = "Best: " + bestScore;
		}	
	}

	public void UpdateLivesOne(int currentLives){
		Debug.Log("Player1 Lives: " + currentLives);
		playerOneLivesImageDisplay.sprite = lives[currentLives];		
	}

	public void UpdateLivesTwo(int currentLives){
		Debug.Log("Player2 Lives: " + currentLives);
		playerTwoLivesImageDisplay.sprite = lives[currentLives];		
	}

	public void UpdateScore(){
		currentScore += 10;
		scoreText.text = "Score: " + currentScore;
	}

	public void PlayerTwoUpdateScore(){
		playerTwoCurrentScore += 10;
		playerTwoScoreText.text = "Score: " + playerTwoCurrentScore;
	}

	public void CheckForBestScore(){
		if (currentScore > bestScore){
			bestScore = currentScore;
			PlayerPrefs.SetInt("HighScore", bestScore);
			bestText.text = "Best: " + bestScore;
		}
	}

	public void CheckForWinner(){
		if (currentScore > playerTwoCurrentScore){
			winnerDisplay.text = "Player1 Wins!!";
		}
		else if (currentScore < playerTwoCurrentScore){
			winnerDisplay.text = "Player2 Wins!!";
		}
		else{
			winnerDisplay.text = "It is a Tie!!";
		}
	}

	public void ShowTitleScreen(){
		CheckForBestScore();
		titleScreen.enabled = true;
		currentScore = 0;
		playerTwoCurrentScore = 0;
	}

	public void HideTitleScreen(){
		titleScreen.enabled = false;
		scoreText.text = "Score: ";		
	}

	public void InitializeText(){
		playerTwoScoreText.text = "Score: ";
		winnerDisplay.text = " ";
	}

	public void ResumePlay(){
		_gameManager.ResumeGame();	
	}

	public void BackToMainMenu(){
		SceneManager.LoadScene("Main_Menu");
	}
}
