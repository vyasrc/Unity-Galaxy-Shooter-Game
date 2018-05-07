using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public bool gameOver = true;
	public bool isCoOpMode = false;
	public Image background;

	[SerializeField]
	private GameObject _player;
	
	[SerializeField]
	private GameObject _coOpPlayers;

	[SerializeField]
	private GameObject _pauseMenuPanel;

	private UIManager _uiManager;
	private SpawnManager _spawnManager;
	private Animator _pauseAnimator;

	private void Start(){
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
		_pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
		_pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
		_pauseMenuPanel.SetActive(false);
	}

	public void ResumeGame(){		
		background.enabled = false;
		_pauseAnimator.SetBool("isPaused", false);
		Time.timeScale = 1;		
	}
		
	void Update(){
		if (gameOver == true){
			if (Input.GetKeyDown(KeyCode.S)){
				if (isCoOpMode == false){
					Instantiate(_player, Vector3.zero, Quaternion.identity);
				}
				else{
					Instantiate(_coOpPlayers, _coOpPlayers.transform.position, Quaternion.identity);
				}
				gameOver = false;	
				_uiManager.HideTitleScreen();
				if (isCoOpMode == true){
					_uiManager.InitializeText();
				}
				_spawnManager.StartSpawnRoutine();
			}
		}	
		else if (Input.GetKey(KeyCode.Escape)){
			SceneManager.LoadScene("Main_Menu");
		}  
		if (Input.GetKeyDown(KeyCode.P)){
			_pauseMenuPanel.SetActive(true);
			background.enabled = true;
			_pauseAnimator.SetBool("isPaused", true);
			Time.timeScale = 0;		
		}
	}
}
