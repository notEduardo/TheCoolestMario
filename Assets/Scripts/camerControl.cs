using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerControl : MonoBehaviour {
	public gameManager manager;
	public float camPos;
	public float max = 100f;
	public Camera myCam; 
	public float camBackEnd;
	private float min = 2.2f;
	private float yLevel = 0f; 


	void Start () {
		startPos();
	}
	
	// Update is called once per frame
	void Update () {
		if(manager.gameStarted){
			transform.position = new Vector3(Mathf.Clamp(transform.position.x+.003f, min, max), yLevel, -10);
			camPos = transform.position.x;
		}

		findBackEnd();
	}

	public void startPos(){
		transform.position = new Vector3(min, yLevel, -10);
	}

	void findBackEnd(){
		float camWidth = myCam.orthographicSize*myCam.aspect;
		camBackEnd = camPos - camWidth;
	}
}
