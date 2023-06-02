using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {

	private LayerMask theGroundLayer;
	public bool isGrounded;
	private Rigidbody2D playerRB;
	private float moveSmooth;
	private float jumpForce;
	private Vector3 velocity = Vector3.zero;

	public float raycastRadius;
	public Transform playerPos;

	// Use this for initialization
	void Start () {
		theGroundLayer = LayerMask.GetMask("Default");
		raycastRadius = .2f;
		playerRB = GetComponent<Rigidbody2D>();
		moveSmooth = .1f;
		jumpForce = 230f;
	}
	
	// Update is called once per frame
	void Update () {

		bool wasGrounded = isGrounded;
		isGrounded = false;


		Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPos.position, raycastRadius, theGroundLayer);
		for(int i = 0; i < colliders.Length; i++){
			if(colliders[i].gameObject != gameObject){
				isGrounded = true;
			}
		}

	}

	public void Move(float movement, bool jump){
		Vector3 tVelocity = new Vector2(movement * 1f, playerRB.velocity.y);

		playerRB.velocity = Vector3.SmoothDamp(playerRB.velocity, tVelocity, ref velocity, moveSmooth);

		if(isGrounded && jump){
			isGrounded = false;
			playerRB.AddForce(new Vector2(0f, jumpForce));
		}

	}
}
