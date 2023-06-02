using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour {

	public playerControl pController;
	public gameManager manager;
	public Animator myAnim;
	public GameObject fireball;

	float hMove;
	bool jump;
	bool facingRight;
	float shootTimer;
	bool shooting;

	public float sprintSpeed;
	public float walkSpeed;

	void Start () {
		sprintSpeed = 2;
		walkSpeed = 1.5f;
		facingRight = true;

		shootTimer = 1;
		shooting = false;
	}

	void Update () {
		if(manager.gameStarted){
			bool running;

			// basic controls
			hMove = Input.GetAxisRaw("Horizontal");

			jump = false;
			if(Input.GetButtonDown("Jump")){
				jump = true;
			}

			if(Input.GetKey(KeyCode.LeftShift)){
				hMove = hMove * sprintSpeed;
				running = true;
			}
			else{
				hMove = hMove * walkSpeed;
				running = false;
			}

			if(pController.isGrounded){
				myAnim.SetBool("isJumping", false);
				if(hMove == 0){
					myAnim.SetInteger("movement", 0);
				}else if(running){
					myAnim.SetInteger("movement", 2);
				}else if(!running){
					myAnim.SetInteger("movement", 1);
				}
			}
			else{
				myAnim.SetBool("isJumping", true);
			}


			if(hMove <= -0.01 && facingRight){
				flip();
			}
			else if(hMove >= 0.01 && !facingRight){
				flip();
			}

			pController.Move(hMove, jump);

			if(Input.GetKeyDown(KeyCode.Mouse0) && 
				shooting == false &&
				manager.marioState == 2){
				myAnim.SetBool("shooting", true);
				shooting = true;
				shootTimer = 1f;

				if(facingRight){
					Instantiate(fireball, new Vector3(transform.position.x + .3f, transform.position.y, -1f), Quaternion.identity);
				}else{
					Instantiate(fireball, new Vector3(transform.position.x - .3f, transform.position.y, -1f), Quaternion.identity);	
				}
			}

			if(shooting){
				shootTimer -= Time.deltaTime;
				if(shootTimer <= 0){
					myAnim.SetBool("shooting", false);
					shooting = false;
				}
			}
		}
	}

	void flip(){
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
