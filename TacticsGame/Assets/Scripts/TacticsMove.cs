using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour {

	List<Tile> selectableTiles = new List<Tile>();  //List of all selectable tiles
	GameObject[] tiles;								// Array of all the tiles as gameobjects

	Stack<Tile> path = new Stack<Tile>(); 			//Reverse order for path to target
	Tile currentTile;

	public bool moving = false;
	public bool current = false; 					// Tile player is standing on
	public int move = 5; 							// Number of tiles the player can move.
	public float moveSpeed = 2;						// How fast unit will walk across tiles.

	Vector3 velocity = new Vector3();				// How fast the player moves to target location.
	Vector3 heading = new Vector3();   				// Direction player is heading in

	float halfHeight = 0;


	public void Init(){
		tiles = GameObject.FindGameObjectsWithTag ("Tile");

		halfHeight = GetComponent<Collider>().bounds.extents.y; // WAT
	}

	public void GetCurrentTile(){
		currentTile = GetTargetTile (gameObject);
		currentTile.current = true;
	}

	public Tile GetTargetTile(GameObject target){
		RaycastHit hit;
		Tile tile = null;

		//Locating selected tile
		if (Physics.Raycast (target.transform.position, -Vector3.up, out hit, 1)) {
			tile = hit.collider.GetComponent<Tile> ();
		}

		return tile;
	}

	public void ComputeAdjacencyList(){
		foreach (GameObject tile in tiles) {
			Tile t = tile.GetComponent<Tile> ();
			t.findNeighbors ();
		}
	}

	public void FindSelectableTiles(){
		ComputeAdjacencyList ();
		GetCurrentTile ();

		Queue<Tile> process = new Queue<Tile> ();

		process.Enqueue (currentTile);
		currentTile.visted = true;
		//currentTile.Parent = ?? leave as null

		while(process.Count > 0){

			Tile t = process.Dequeue (); //Pop off the front 

			selectableTiles.Add(t);
			t.selectable = true; // Sets the selectable tiles as red.


			if(t.distance < move)
			{
				foreach (Tile tile in t.adjacencyList) {
				
					if(!tile.visted){
						tile.Parent = t;
						tile.visted = true;
						tile.distance = 1 + t.distance;
						process.Enqueue (tile);
					}
				}
			}
		}

	}


	public void MoveToTile(Tile tile){
		path.Clear ();
		tile.target = true;
		moving = true;

		Tile next = tile; // End Location
		while (next != null) // When next == null we've reached the start tiles, every iteration sets it = to parent
		{
			path.Push (next);
			next = next.Parent;
		}
	}

	public void Move(){
		if (path.Count > 0) { //as long as there is something in the path we can move
			Tile t = path.Peek ();
			Vector3 target = t.transform.position;

			//Calculate units position on top of the target tile this is where we'll move character too.
			target.y += halfHeight + t.GetComponent<Collider> ().bounds.extents.y;

			if (Vector3.Distance (transform.position, target) >= 0.2f) {
				CalculateHeading (target);
				setHorizontalVelocity ();

				transform.forward = heading;
				transform.position += velocity * Time.deltaTime;
			} 

			else {
				//Tile center reached.
				transform.position = target;
				path.Pop ();
			}
		}

		else {
			RemoveSelecteableTiles ();
			moving = false;
		}

	}

	protected void RemoveSelecteableTiles(){

		if (currentTile != null) {
			currentTile.current = false;
			currentTile = null;
		}

		foreach (Tile tile in selectableTiles) {
			tile.Reset ();

		}
		selectableTiles.Clear ();
		
	}

	public void CalculateHeading(Vector3 target){
		heading = target - transform.position;
		heading.Normalize ();
	}

	public void setHorizontalVelocity(){
		velocity = heading * moveSpeed;	
	}

}
