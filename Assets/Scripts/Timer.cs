using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
	public int time = 0;
	//public Text countdownText;

	bool flag = false;

	// Use this for initialization
	void Start()
	{
		StartCoroutine("start");
	}

	// Update is called once per frame
	void Update()
	{
		if (flag == false) {
			GetComponent<TextMesh>().text = time+"";
		}
	}

	IEnumerator start()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			time++;
		}
	}

	public void stop(){
		Debug.Log ("called stop!");
		StopCoroutine("start");
		this.change_status ();
		GetComponent<TextMesh>().text = "Gotcha!";
	}

	public void change_status(){
		this.flag = true;
	}
}

//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;
//
//public class Timer : MonoBehaviour
//{
//	public int timeLeft = 5;
//	//public Text countdownText;
//
//	// Use this for initialization
//	void Start()
//	{
//		StartCoroutine("LoseTime");
//	}
//
//	// Update is called once per frame
//	void Update()
//	{
//		//countdownText.text = ("Time Left = " + timeLeft);
//		GetComponent<TextMesh>().text = timeLeft+"";
//
//		if (timeLeft <= 0)
//		{
//			StopCoroutine("LoseTime");
//			//countdownText.text = "Times Up!";
//
//			GetComponent<TextMesh>().text = "Times Up!";
//		}
//	}
//
//	IEnumerator LoseTime()
//	{
//		while (true)
//		{
//			yield return new WaitForSeconds(1);
//			timeLeft--;
//		}
//	}
//}