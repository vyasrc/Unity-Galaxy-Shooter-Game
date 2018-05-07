using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	// Use this for initialization
	public bool canTripleShot = false;
	public bool isSpeedBoostActive = false;
	public bool shieldsActive = false;
	public int playeronelives = 3;
	public int playertwolives = 3;
	public bool isPlayerOne = false;
	public bool isPlayerTwo = false;

	[SerializeField]
	private GameObject _laserPrefab;

	[SerializeField]
	private GameObject _laserRedPrefab;

	[SerializeField]
	private GameObject _explosionPrefab;

	[SerializeField]
	private GameObject _tripleShotPrefab;

	[SerializeField]
	private GameObject _tripleShotRedPrefab;

	[SerializeField]
	private GameObject _shieldGameObject;

	[SerializeField]
	private float _fireRate = 0.25f;
	
	private float _canFire = 0.0f;

	[SerializeField]
	private float _speed = 5.0f;

	[SerializeField]
	private GameObject[] _engines;

	private UIManager _uiManager;
	private GameManager _gameManager;

	[SerializeField]
	private AudioClip _clip;

	private int hitCount;
	private int hitCountTwo;

	void Start () {
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (_gameManager.isCoOpMode == false){
			transform.position = new Vector3(0, 0, 0);
		}
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		if (_uiManager != null){
			_uiManager.UpdateLivesOne(playeronelives);
			if (_gameManager.isCoOpMode == true){
				_uiManager.UpdateLivesTwo(playertwolives);
			}
		}
		hitCount = 0;
		hitCountTwo = 0;
		_engines[0].SetActive(false);
		_engines[1].SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlayerOne == true){
			Movement();
			if (_gameManager.isCoOpMode == false){
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)){
					Shoot();
				}
			}
			else{
				if (Input.GetKeyDown(KeyCode.Space)){
					Shoot();
				}
			}
		}
		if (isPlayerTwo == true){
			PlayerTwoMovement();
			if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter)){
				PlayerTwoShoot();
			}
		}				
	}

	private void Shoot(){
		if (Time.time > _canFire){
			AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
			if (canTripleShot == true){
				Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
			}
			else{ 
				Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0),
					Quaternion.identity);
			}
			_canFire = Time.time + _fireRate;
		}
	}

	private void PlayerTwoShoot(){
		if (Time.time > _canFire){
			AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
			if (canTripleShot == true){
				Instantiate(_tripleShotRedPrefab, transform.position, 
					Quaternion.identity);
			}
			else{
				Instantiate(_laserRedPrefab, 
					transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
			}
			_canFire = Time.time + _fireRate;
		}
	}

	private void Movement(){
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		if (isSpeedBoostActive == true){
			transform.Translate(Vector3.right  * _speed * horizontalInput * Time.deltaTime * 1.5f);
			transform.Translate(Vector3.up  * _speed * verticalInput * Time.deltaTime * 1.5f);
		}
		else{
			transform.Translate(Vector3.right  * _speed * horizontalInput * Time.deltaTime);
			transform.Translate(Vector3.up  * _speed * verticalInput * Time.deltaTime);
		}
		if (transform.position.x > 9.4f){
			transform.position = new Vector3(-9.4f, transform.position.y, 0);
		}
		if (transform.position.x < -9.4f){
			transform.position = new Vector3(9.4f, transform.position.y, 0);
		}
	}

	private void PlayerTwoMovement (){
		//float horizontalInput = Input.GetAxis("Horizontal");
		//float verticalInput = Input.GetAxis("Vertical");
		if (isSpeedBoostActive == true){
			if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.Keypad8)){
				transform.Translate(Vector3.up  * _speed  * 1.5f * Time.deltaTime);
			}

			if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.Keypad6)){
				transform.Translate(Vector3.right * _speed * 1.5f * Time.deltaTime);
			}

			if (Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.Keypad2)){
				transform.Translate(Vector3.down * _speed * 1.5f * Time.deltaTime);
			}

			if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.Keypad4)){
				transform.Translate(Vector3.left * _speed * 1.5f * Time.deltaTime);
			}
		}
		else{
			if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.Keypad8)){
				transform.Translate(Vector3.up  * _speed * Time.deltaTime);
			}

			if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.Keypad6)){
				transform.Translate(Vector3.right * _speed * Time.deltaTime);
			}

			if (Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.Keypad2)){
				transform.Translate(Vector3.down * _speed * Time.deltaTime);
			}

			if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.Keypad4)){
				transform.Translate(Vector3.left * _speed * Time.deltaTime);
			}
		}
		if (transform.position.x > 9.4f){
			transform.position = new Vector3(-9.4f, transform.position.y, 0);
		}
		if (transform.position.x < -9.4f){
			transform.position = new Vector3(9.4f, transform.position.y, 0);
		}
	}

	public void Damage(){		
		if (shieldsActive == true){
			shieldsActive = false;
			_shieldGameObject.SetActive(false);
			return;
		}
		hitCount++;
		Debug.Log("hitCount" + hitCount);
		if (hitCount == 1){
			int randomEngine = Random.Range(0,2);
			_engines[randomEngine].SetActive(true);
		}
		else if (hitCount == 2){
			if (_engines[0].activeSelf){
			_engines[1].SetActive(true);
			}
			else{
				_engines[0].SetActive(true);
			}
		}			
		playeronelives--;
		_uiManager.UpdateLivesOne(playeronelives);
		if (playeronelives < 1){
			Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
			_gameManager.gameOver = true;
			_uiManager.ShowTitleScreen();
			Destroy(this.gameObject);
		}
	}

	public void PlayerOneDamage(){		
		if (shieldsActive == true){
			shieldsActive = false;
			_shieldGameObject.SetActive(false);
			return;
		}
		hitCount++;
		//Debug.Log("hitCount" + hitCount);
		if (hitCount == 1){
			int randomEngine = Random.Range(0,2);
			_engines[randomEngine].SetActive(true);
		}
		else if (hitCount == 2){
			if (_engines[0].activeSelf){
			_engines[1].SetActive(true);
			}
			else{
				_engines[0].SetActive(true);
			}
		}			
		playeronelives--;
		_uiManager.UpdateLivesOne(playeronelives);
		if (playeronelives < 1){
			Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
			_gameManager.gameOver = true;
			_uiManager.CheckForWinner();
			_uiManager.ShowTitleScreen();
			Destroy(this.gameObject);
			Destroy(transform.parent.gameObject);
		}
	}

	public void PlayerTwoDamage(){		
		if (shieldsActive == true){
			shieldsActive = false;
			_shieldGameObject.SetActive(false);
			return;
		}
		hitCountTwo++;
		if (hitCountTwo == 1){
			int randomEngine = Random.Range(0,2);
			_engines[randomEngine].SetActive(true);
		}
		else if (hitCountTwo == 2){
			if (_engines[0].activeSelf){
			_engines[1].SetActive(true);
			}
			else{
				_engines[0].SetActive(true);
			}
		}			
		playertwolives--;
		_uiManager.UpdateLivesTwo(playertwolives);
		if (playertwolives < 1){
			Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
			_gameManager.gameOver = true;
			_uiManager.CheckForWinner();
			_uiManager.ShowTitleScreen();
			Destroy(this.gameObject);
			Destroy(transform.parent.gameObject);
		}
	}

	public void SpeedBoostPowerUpOn(){
		isSpeedBoostActive = true;
		StartCoroutine(SpeedBoostDownRoutine());
	}

	public void TripleShotPowerUpOn(){
		canTripleShot = true;
		StartCoroutine(TripleShotPowerDownRoutine());
	}

	public void EnableShields(){
		shieldsActive = true;
		_shieldGameObject.SetActive(true);
	}

	public IEnumerator TripleShotPowerDownRoutine(){
		yield return new WaitForSeconds(5.0f);
		canTripleShot = false;
	}

	public IEnumerator SpeedBoostDownRoutine(){
		yield return new WaitForSeconds(5.0f);
		isSpeedBoostActive = false;
	}
}
