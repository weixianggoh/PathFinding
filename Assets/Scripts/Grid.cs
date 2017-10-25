using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//	ONLY FOR CHUNKING. No other path finding process in here,
//	Have GetNeighbour(). That is just about it. Attach in EMPTY_GameObject; ; with PATHREQUESTMANAGER & PATHFINDER
public class Grid : MonoBehaviour {
	
	public bool displayGridGizmos;			//	to display CHUNKING MATRIX
	public LayerMask unwalkableMask;		//	to disallow walking into WALLS. Collision prevention
	public Vector2 gridWorldSize;			//	Request for size from USER. In the script GUI (component attatched)
	public float nodeRadius;				//	size of CHUNKING radius
	Node[,] grid;							//	will create to plane-to-Node area scape

	float nodeDiameter;						//	NODE diameter reference
	int gridSizeX, gridSizeY;				//	GRID size reference

	//	Faster than start. Does not loop like Update();
	void Awake() {
		// Get NODE size based on USER Settings.
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
		CreateGrid();
	}

	void Update(){
		CreateGrid ();
	}

	//	Path reference on how far should the calculation go. Not used here.
	public int MaxSize {
		get {
			return gridSizeX * gridSizeY;
		}
	}

	//	Creating CHUNKING PLANE.
	void CreateGrid() {
		grid = new Node[gridSizeX,gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;

		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius,unwalkableMask));
				grid[x,y] = new Node(walkable,worldPoint, x,y);
			}
		}
	}

	//	Just getting the SURROUNDING CHUNK from a SELECTED NODE. Processing in PATHFINDER
	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}
	
	//	Getting CHUNK position. Change from a CHUNK value to location value for Seeker reference.
	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y/2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		return grid[x,y];
	}


	//	Function to view the CHUNKS on SCENE. Can toggle on component GUI (attatched)
	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,1,gridWorldSize.y));
		if (grid != null && displayGridGizmos) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable)?Color.white:Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
			}
		}
	}
}