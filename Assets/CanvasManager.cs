using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	public Button PauseButton;
	public Button RunButton;
	public bool paused;
	public Canvas SettingsCanvas;
	// Use this for initialization
	void Start () {
		RunButton.gameObject.SetActive (false);
		paused = false;
		SettingsCanvas.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (paused == true) {
				paused = false;
				PauseButton.gameObject.SetActive(true);
				RunButton.gameObject.SetActive(false);
				if(SettingsCanvas.gameObject.activeSelf == true)
					SettingsCanvas.gameObject.SetActive(false);
			} else if (paused == false){
				paused = true;
				PauseButton.gameObject.SetActive(false);
				RunButton.gameObject.SetActive(true);
				if(SettingsCanvas.gameObject.activeSelf == false)
					SettingsCanvas.gameObject.SetActive(true);
			}
		}
	}

	public void HitPauseButton(){
			paused = true;
			PauseButton.gameObject.SetActive(false);
			RunButton.gameObject.SetActive(true);
			SettingsCanvas.gameObject.SetActive(true);
	}

	public void HitRunButton(){
			paused = false;
			PauseButton.gameObject.SetActive(true);
			RunButton.gameObject.SetActive(false);
			SettingsCanvas.gameObject.SetActive(false);
	}

	public void HitSettingsButton(){
		if (SettingsCanvas.gameObject.activeSelf == false) {
			paused = true;
			PauseButton.gameObject.SetActive (false);
			RunButton.gameObject.SetActive (true);
			SettingsCanvas.gameObject.SetActive(true);
		} else {
			paused = false;
			PauseButton.gameObject.SetActive(true);
			RunButton.gameObject.SetActive(false);
			SettingsCanvas.gameObject.SetActive(false);
		}
	}
}
