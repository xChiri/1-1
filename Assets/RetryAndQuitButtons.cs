using UnityEngine;
using System.Collections;

public class RetryAndQuitButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Restart()
	{
		Application.LoadLevel ("Main 1+1");
	}
	public void Quit()
	{
		Application.Quit ();
	}
}
