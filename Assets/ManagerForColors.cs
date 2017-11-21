using UnityEngine;
using System.Collections;

public class ManagerForColors : MonoBehaviour {

	public Material[] material = new Material[5];
	public GameObject Table;
	public GameObject Background;
	public int currentSelectedColorForTable = 0;
	public int currentSelectedColorForBackground = 10;
	//0 - blue
	//1- green
	//2 - purple
	//3 - red
	//4 - yellow
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PressedColorButtonForTable(string x)
	{
		if (x == "Blue")
			currentSelectedColorForTable = 0;
		if (x == "Green")
			currentSelectedColorForTable = 1;
		if (x == "Purple")
			currentSelectedColorForTable = 2;
		if (x == "Red")
			currentSelectedColorForTable = 3;
		if (x == "Black")
			currentSelectedColorForTable = 4;
	}

	public void PressedColorButtonForBackground(string x)
	{
		if (x == "Blue")
			currentSelectedColorForBackground = 0;
		if (x == "Green")
			currentSelectedColorForBackground = 1;
		if (x == "Purple")
			currentSelectedColorForBackground = 2;
		if (x == "Pink")
			currentSelectedColorForBackground = 5;
		if (x == "White")
			currentSelectedColorForBackground = 10;
	}
}
