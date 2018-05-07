using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

	private GameManager _gameManager;

	public void LoadSinglePlayerGame(){
		SceneManager.LoadScene("Single_Player");
		Time.timeScale = 1;
	}

	public void LoadCoOpMode(){
		SceneManager.LoadScene("Co-Op_Mode");
		Time.timeScale = 1;
	}
}
