using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicMove : MonoBehaviour {
	gameManager manager;
	playerControl playerC;
	Transform playerPos;
	public SpriteRenderer myRender;
	bool seen;
	float direction;

	void Start(){
		GameObject gameManager = GameObject.FindGameObjectsWithTag("manager")[0];
		GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];

		manager = gameManager.GetComponent<gameManager>();
		playerC = player.GetComponent<playerControl>();
		playerPos = player.transform;

		myRender = GetComponent<SpriteRenderer>();
		seen = false;
	}

	void Update(){
		if(manager.gameStarted){
			if(myRender.isVisible){
				seen = true;
				if(gameObject.tag != "enemy"){
					direction = Random.Range(0,2);
				}
			}
			if(seen){
				move();
			}
		}
	}

	void move(){
		if((gameObject.tag == "enemy" || gameObject.tag == "enemy2") && playerC.isGrounded){
			direction = playerPos.position.x - transform.position.x;
		}

		if(direction > 0){
			transform.position = new Vector3(transform.position.x+.005f, transform.position.y, -1f);
		}else{
			transform.position = new Vector3(transform.position.x-.005f, transform.position.y, -1f);
		}
	}

	ContactPoint2D[] list = new ContactPoint2D[1];
	void OnCollisionEnter2D(Collision2D col)
	{
		col.GetContacts(list);
		foreach(ContactPoint2D hitPos in list)
		{
			// Enemy collisions
			if(gameObject.tag == "enemy" || gameObject.tag == "enemy2"){

				if((col.gameObject.name == "player" && hitPos.normal.x == 1) ||
					(col.gameObject.name == "player" && hitPos.normal.x == -1) ||
					(col.gameObject.name == "player" && hitPos.normal.y == 1)){
					Debug.Log("hitMario");
					manager.marioState -= 1;
					if(manager.marioState == -1){
						manager.gameEnded();
					}
				}
				else if(col.gameObject.name == "player" && 
					hitPos.normal.y == -1){
					Destroy(gameObject);
				}else if(col.gameObject.tag == "fireball" && 
					gameObject.tag != "enemy2"){
					Destroy(gameObject);
				}

			}
			// Item collisions
			if(gameObject.tag != "enemy" && gameObject.tag != "enemy2"){

				if(col.gameObject.name == "player"){
					if (manager.marioState < 2){
						manager.marioState += 1;
					}

					Destroy(gameObject);
				}
				else if(hitPos.normal.x == 1){
					direction = 0;
				}
				else if(hitPos.normal.x == -1){
					direction = 1;
				}
			}
		}
	}
}
