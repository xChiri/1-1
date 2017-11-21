using UnityEngine;
using System.Collections;

public class CornerMovement : MonoBehaviour {

	public Collider[] topside; 
	bool topsidemovementtoleft = false;
	int lastfreeposition;
	int lastoccupiedposition;
	int lastoccupiedpositionvalue;
	int i;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//activate movement on top side to left
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			topsidemovementtoleft = true;
			lastfreeposition = 0;
			i = 0;
		}
		//move top side to left
		if (topsidemovementtoleft == true) {
		}
	}

//	void OnCollisionEnter(Collision other){
//		if(other.gameObject)//DE ATASAT LA COLLIDER!!! (SCRIPT-UL)
//	}
}
