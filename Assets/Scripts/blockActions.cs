using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockActions : MonoBehaviour {
	GameObject managerOBJ;
	gameManager manager;
	public Sprite hitBlock;
	public GameObject shroom;
	public GameObject flower;

	GameObject[] items = new GameObject[3];
	private bool done;
	private SpriteRenderer spriteRender;
	ContactPoint2D[] list = new ContactPoint2D[100];

	void Start(){
		spriteRender = GetComponent<SpriteRenderer>();
		items[0] = shroom;
		items[1] = flower;
		items[2] = flower;
		done = false;
		managerOBJ = GameObject.FindGameObjectsWithTag("manager")[0];
		manager = managerOBJ.GetComponent<gameManager>();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		
		col.GetContacts(list);
		foreach(ContactPoint2D hitPos in list)
		{
			if(done == false){
				if(col.gameObject.name == "player" && hitPos.normal.y == 1){
					spriteRender.sprite = hitBlock;
					if(gameObject.tag == "item"){
						Instantiate(items[manager.marioState], new Vector3(transform.position.x, transform.position.y+.1f, -1), Quaternion.identity);
					}
					done = true;
				}
			}
		}
	}
}
