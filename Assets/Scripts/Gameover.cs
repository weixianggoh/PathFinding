using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameover : MonoBehaviour {

	// Use this for initialization
	public Button myButton;

	void Start () {
		Button restart = myButton.GetComponent<Button>();
		restart.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TaskOnClick() {
		Debug.Log("haha");
		//Application.LoadLevel(Application.loadedLevel);
		SceneManager.LoadScene("Game", LoadSceneMode.Additive);
		//Application.LoadLevel(Application.loadedLevel);
	}
}
