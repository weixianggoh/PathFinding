using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Seeker : MonoBehaviour {
	
	public Transform target;
	public float speed = 10;
	public bool showpath = true;
	Vector3[] path;
	int targetIndex;
	public float nodeRadius;


	public Timer timer;

	void Start () {
		//SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
		timer = FindObjectOfType (typeof(Timer)) as Timer;
	}

	// NO NEED START() It's gonna always be chasing
	void Update() {
		this.chase ();
	}

	void change(){
		target.position = new Vector3(0,0,0);
	}

	void chase(){
		PathRequestManager.RequestPath(transform.position,target.position, OnFoundPath);
	}


	// Once found, TRIGGER the follow path
	public void OnFoundPath(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	// Console command to followPath();
	IEnumerator FollowPath() {
		if (path.Length > 0) {
			Vector3 currentWaypoint = path[0];
			while (true) {
				if (transform.position == currentWaypoint) {
					targetIndex ++;
					currentWaypoint = path[targetIndex];
				}
				transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed/15 * Time.deltaTime);
				yield return null;
			}
		} else {
			timer.stop ();
			Application.LoadLevel(Application.loadedLevel);
			//SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
		}
	}

	// to view the path taken in SCENE
	public void OnDrawGizmos() {
		if (path != null && showpath) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], new Vector3(1,1,1) * nodeRadius);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}
