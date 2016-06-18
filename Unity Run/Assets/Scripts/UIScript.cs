using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
	
	public Text gameovertext;
	Canvas canvas;

	void Start () {
		canvas = GetComponent<Canvas> ();
		canvas.enabled = false;
	}

	void Update () {
	
	}

	public void Gameover(){
		gameovertext.text = "GAME\nOVER";
		canvas.enabled = true;
	}

	public void Goal(){
		gameovertext.text = "GOAL";
		canvas.enabled = true;
	}
		
	public void Retry(){
		Application.LoadLevel(Application.loadedLevelName);
	}
	
}