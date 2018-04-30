using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	public bool canTripleShot = false;
	public bool isSpeedBoostActive = false;
	public bool shieldsActive = false;
	public int lives = 3;

	[SerializeField]
	private GameObject _laserPrefab;

	[SerializeField]
	private GameObject _explosionPrefab;

	[SerializeField]
	private GameObject _tripleShotPrefab;

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
	private SpawnManager _spawnManager;
	private AudioSource _audioSource;
	private int hitCount;

	void Start () {
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
		if (_uiManager != null){
			_uiManager.UpdateLives(lives);
		}
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
		if (_spawnManager != null){
			_spawnManager.StartSpawnRoutine();
		}
		_audioSource = GetComponent<AudioSource>();
		hitCount = 0;
		_engines[0].SetActive(false);
		_engines[1].SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		Movement();

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)){
			Shoot();
		}
		
	}

	

	private void Shoot(){
		if (Time.time > _canFire){
			_audioSource.Play();
			if (canTripleShot == true){
				Instantiate(_tripleShotPrefab, transform.position,
					Quaternion.identity);
			}
			else{
				Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0),
				Quaternion.identity);
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

	public void Damage(){
		
		if (shieldsActive == true){
			shieldsActive = false;
			_shieldGameObject.SetActive(false);
			return;
		}
		hitCount++;
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
		lives--;
		_uiManager.UpdateLives(lives);
		if (lives < 1){
			Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
			_gameManager.gameOver = true;
			_uiManager.ShowTitleScreen();
			Destroy(this.gameObject);
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
