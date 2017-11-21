using UnityEngine;
using System.Collections;

public class AuxiliarAnimator : MonoBehaviour {

	public GameObject MainManager;
	private int direct;
	private int l, c;
	private float timer = 0.2f;
	// Use this for initialization
	void Start () {
		direct = MainManager.GetComponent<Manager> ().direction;
		l = MainManager.GetComponent<Manager> ().l;
		c = MainManager.GetComponent<Manager> ().c;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer >= 0.0f) {
			if (direct == 1) {
				if (c == 1) {
					transform.position += new Vector3 (0.05f, 0.0f, 0.0f);
				}
			}
		} else {
			Destroy (this);
		}
	}
}
