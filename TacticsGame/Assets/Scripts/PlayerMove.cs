using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove {

	// Use this for initialization
	void Start () {
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!moving) {
			FindSelectableTiles ();
			CheckMouse ();
		} 

		else {
			Move ();
		}
	}

	void CheckMouse(){
		if (Input.GetMouseButtonUp (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {


				//If the player clicks on a tile
				if (hit.collider.tag == "Tile") {
					Tile t = hit.collider.GetComponent<Tile> ();
					print ("Test1");
					if (t.selectable) {
						//Found selectable tile to move to
						MoveToTile(t);
						print ("Test2");
					}
				
				}
			}
		}
	}
}
