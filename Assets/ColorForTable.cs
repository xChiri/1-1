using UnityEngine;
using System.Collections;

public class ColorForTable : MonoBehaviour {

	public Renderer rend;
	public GameObject ColorManager;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		rend.material = ColorManager.GetComponent<ManagerForColors> ().material [ColorManager.GetComponent<ManagerForColors> ().currentSelectedColorForTable];
	}
}
