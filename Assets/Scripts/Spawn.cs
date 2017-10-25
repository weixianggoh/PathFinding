using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	Grid map;
	public Transform target;

	// Use this for initialization
	void Start () {
		map = GetComponent<Grid> ();
		//	Update ();
		Spawning();
	}

//	void Update(){
//		if(!map.NodeFromWorldPoint(target.position).walkable)
//			Spawning ();
//	}

	void Spawning (){
		int x1 = Random.Range (-4, 4);
		int y1 = 0;
		int z1 = Random.Range (-4, 4);
		Node site = map.NodeFromWorldPoint (new Vector3 (x1, y1, z1));
		if (site.walkable) {
			target.position = site.worldPosition;
		}
				
	}

}
	
