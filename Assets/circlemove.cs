using UnityEngine;
using System.Collections;

public class circlemove : MonoBehaviour {
	
	public Vector3 circlepositiontomove;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionStay(Collision other){
		if (circlepositiontomove != Vector3.zero) {
			Debug.Log("da");
			other.gameObject.GetComponent<particularmovement>().gotoposition = circlepositiontomove;
			other.gameObject.GetComponent<particularmovement>().moveUpsidetoLeft = true;
			circlepositiontomove = Vector3.zero;
		}
	}
}
