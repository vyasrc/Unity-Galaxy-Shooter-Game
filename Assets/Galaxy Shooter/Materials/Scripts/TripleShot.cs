using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private float _speed = 10.0f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * _speed * Time.deltaTime);
		if (transform.position.y >= 6){
			if (transform.parent != null){
				Destroy(transform.parent.gameObject);
			}
			Destroy(this.gameObject);
		}
	}
}
