using UnityEngine;
using System.Collections;

public class CollisionChecker : MonoBehaviour {

	public int l;
	public int c;
	public GameObject manager;
	public bool checker = false;
	private int contor;
	private int lastcontor;
	public int value;
	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Manager");
		contor = 0;
		lastcontor = -1;
	}
	
	// Update is called once per frame
	void Update () {
		Verify ();
		if (checker == true) {
			manager.GetComponent<MainManager> ().adiac [l, c] = true;
		} else {
			manager.GetComponent<MainManager> ().adiac [l, c] = false;
			manager.GetComponent<MainManager>().ComponentValues[l,c] = 0;
		}
	}

	void Verify()
	{
		lastcontor++;
		if (lastcontor >= contor) {
			checker = false;
			contor = 0;
			lastcontor = -1;
		}
	}

	void OnCollisionStay(Collision other)
	{
		checker = true;
		contor++;
		manager.GetComponent<MainManager>().ComponentValues[l,c] = other.gameObject.GetComponent<ComponentMovement> ().ownvalue;
	}

	void OnCollisionExit(Collision other)
	{
		checker = false;
		contor = 0;
		lastcontor = -1;
		manager.GetComponent<MainManager> ().ComponentValues [l, c] = 0;
	}
}
