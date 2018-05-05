using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public bool walkable = true;
	public bool current = false; //Tile player is standing on
	public bool target = false; //Where player is selecting to move to.
	public bool selectable = false; // Check if tile is selectable

	public List<Tile> adjacencyList = new List<Tile> ();

	//Needed for BFSearch
	public bool visted = false; //Only process tile once with bfs
	public Tile Parent = null; // Once target is reached 
	public int distance = 0; // Used for A* how far each tile is from the start tile.


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (current) {
			GetComponent<Renderer> ().material.color = Color.magenta;
		} else if (target) {
			GetComponent<Renderer> ().material.color = Color.green;
		} else if (selectable) {
			GetComponent<Renderer> ().material.color = Color.red;
		} else {
			GetComponent<Renderer> ().material.color = Color.white;
		}
		
	}

	public void Reset(){
		
		adjacencyList.Clear ();
		//Reset all variables to original
		walkable = true;
		current = false; 
		target = false; 
		selectable = false; 
		visted = false; 
		Parent = null; 
		distance = 0; 
	}


	public void findNeighbors(){
		
		Reset ();
		CheckTile (Vector3.forward);
		CheckTile (-Vector3.forward); 	// Negative foward
		CheckTile (Vector3.right); 		// Z axis for grid
		CheckTile (-Vector3.right); 	// Negative right
	
	}


	public void CheckTile (Vector3 direction){

		Vector3 halfExtends = new Vector3 (0.25f,0.25f,0.25f);
		Collider[] colliders = Physics.OverlapBox (transform.position + direction, halfExtends);

		foreach (Collider item in colliders) {
			
			Tile tile = item.GetComponent<Tile> ();
			if (tile != null && tile.walkable) {

				RaycastHit hit;

				//Returns true if raycast hits something therefore we want not ray cast hit
				// Now if it hits something if it will return true and will only add to adj list if its false.
				if (!Physics.Raycast (tile.transform.position, Vector3.up, out hit, 1)) {
					adjacencyList.Add (tile);
				}

			}
		}
	}






}
