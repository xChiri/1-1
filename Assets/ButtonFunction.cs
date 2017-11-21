using UnityEngine;
using System.Collections;

public class ButtonFunction : MonoBehaviour {

	//public string x;

	public void PressButton(string x)
	{
		Application.LoadLevel (x);
	}

	public void Quit(){
		Application.Quit ();
	}
}
