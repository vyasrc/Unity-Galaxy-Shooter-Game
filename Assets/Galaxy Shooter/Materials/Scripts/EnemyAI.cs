using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	// Use this for initialization

	[SerializeField]
	private GameObject _enemyExplosionPrefab;

	private float _speed = 3.0f;

	private UIManager _uiManager;

	[SerializeField]
	private AudioClip _clip;


	void Start () {
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);
		//Random rnd = new Random();
		if (transform.position.y < -6.0f){
			float randomX = Random.Range(-9.0f,9.0f); 
			transform.position = new Vector3(randomX, 6.0f, 0);
		}
	}	

	private void OnTriggerEnter2D(Collider2D other){	
		AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
		if (other.tag == "Laser"){
			_uiManager.UpdateScore();
			if (other.transform.parent != null){
				Destroy(other.transform.parent.gameObject);
			}
			Destroy(other.gameObject);
			Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);	
			Destroy(this.gameObject);
		}
		else if (other.tag == "Laser_Red"){
			_uiManager.PlayerTwoUpdateScore();
			if (other.transform.parent != null){
				Destroy(other.transform.parent.gameObject);
			}
			Destroy(other.gameObject);
			Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);	
			Destroy(this.gameObject);
		}
		else if(other.tag == "Player"){
			Player player = other.GetComponent<Player>();
			if (player != null){
				player.Damage();
			}
			Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}		
		else if (other.tag == "Co-Op_Players"){
			Player player = other.GetComponent<Player>();
			if (player != null){
				if (other.name == "Player_One"){
					player.PlayerOneDamage();
				}
				else{
					player.PlayerTwoDamage();
				}
			}		
			Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
			Destroy(this.gameObject);	
		}
	}
}
