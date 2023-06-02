using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickBlockActions : MonoBehaviour {
	ContactPoint2D[] list = new ContactPoint2D[100];
	void OnCollisionEnter2D(Collision2D col)
	{
		col.GetContacts(list);
		foreach(ContactPoint2D hitPos in list)
		{
			if(col.gameObject.name == "player" && hitPos.normal.y == 1){
				Destroy(gameObject);
					
			}
		}
	}
}
