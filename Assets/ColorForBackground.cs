using UnityEngine;
using System.Collections;

public class ColorForBackground : MonoBehaviour {

	public Renderer rend;
	public GameObject ColorManager;
	Material defaultMaterial;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		defaultMaterial = rend.material;
	}
	
	// Update is called once per frame
	void Update () {
		if (ColorManager.GetComponent<ManagerForColors> ().currentSelectedColorForBackground != 10)
			rend.material = ColorManager.GetComponent<ManagerForColors> ().material [ColorManager.GetComponent<ManagerForColors> ().currentSelectedColorForBackground];
		else
			rend.material = defaultMaterial;
	}
}
