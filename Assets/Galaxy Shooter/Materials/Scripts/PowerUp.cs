using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private float _speed = -1.0f;

	[SerializeField]
	private int _powerupID; // 0 tripleshot 1 speed boost 2 shields

	[SerializeField]
	private AudioClip _clip; 

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);
		if (transform.position.y < -6.0f){
			Destroy(this.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D other){
		AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
		Debug.Log("Collision with " + other.name);
		if (other.tag == "Player"){
			Player player = other.GetComponent<Player>();
			if (player != null){
				if ( _powerupID  == 0){
					player.TripleShotPowerUpOn();
				}
				else if (_powerupID == 1){
					player.SpeedBoostPowerUpOn();
					}
				else if(_powerupID == 2){
					player.EnableShields();
				}	
			}
			Destroy(this.gameObject);
		}
		
	}
}
