using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour {
	float x;
	float y;
	float z;
	Vector3 pos;

	// Use this for initialization
	void Start () {
		x = Random.Range(-5, 4);
		y = 0;
		z = Random.Range(-5, 5);
		//z1 = Random.Range(-5, 5);
		pos = new Vector3(x, y, z);

		//Debug.Log ("testing");
		//Debug.Log (pos);
		transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}