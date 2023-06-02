using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour {
	GameObject myPlayer;
	SpriteRenderer fireRender;
	Vector3 direction;
	float speed;

	void Start () {
		direction = Vector3.right;

		myPlayer = GameObject.FindGameObjectsWithTag("Player")[0];
		if((myPlayer.transform.position.x - transform.position.x) > 0){
			direction = -Vector3.right;

			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

		speed = 0.03f;
		fireRender = GetComponent<SpriteRenderer>();
	}

	void Update () {
		transform.position += (direction*speed);

		if(!(fireRender.isVisible)){
			Destroy(gameObject);
		}
	}


	
}
