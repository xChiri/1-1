using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class appearenceeffect : MonoBehaviour {

	public GameObject manager;
	public CanvasGroup ForAlpha;
	// Use this for initialization
	void Start () {
		ForAlpha.alpha = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (ForAlpha.alpha <= 0.98f) {
			ForAlpha.alpha += 0.015f;
		} else {
			ForAlpha.alpha = 1;
			manager.GetComponent<MainManager>().fullyshowedretrycanvas = true;
		}
	}
}
