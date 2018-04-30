using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private GameObject _enemyShipPrefab;
	[SerializeField]
	private GameObject[] powerups;
	private GameManager _gameManager;

	void Start () {
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		
	}

	public void StartSpawnRoutine(){
		StartCoroutine(EnemySpawnRoutine());
		StartCoroutine(PowerUpSpawnRoutine());
	}

	public IEnumerator EnemySpawnRoutine(){
		while(_gameManager.gameOver == false){
			float randomX = Random.Range(-9.0f,9.0f); 
			Instantiate(_enemyShipPrefab, new Vector3(randomX, 6.0f, 0), Quaternion.identity);
			yield return new WaitForSeconds(2.0f);
		}
	}

	public IEnumerator PowerUpSpawnRoutine(){
		while(_gameManager.gameOver == false){
			int randomPowerUp = Random.Range(0,3);
			float randomX = Random.Range(-9.0f,9.0f); 
			Instantiate(powerups[randomPowerUp], new Vector3(randomX, 6.0f, 0), Quaternion.identity);
			yield return new WaitForSeconds(5.0f);
		}
		
	}
}
