using UnityEngine;
using System.Collections;

public class testanimation : MonoBehaviour {
	
	int t = 0;
	int p = 0;
	float timer = 0.05f;
	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (0.07f, 0.07f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (t <= 10) {
			t++;
			transform.localScale += new Vector3 (0.0002f, 0.0002f, 0.0f);
		} else {
			if (p <= 10) {
				p++;
				transform.localScale -= new Vector3 (0.0002f, 0.0002f, 0.0f);
			}
		}
		if(t >= 10 && p >= 10)
			transform.localScale = new Vector3 (0.07f, 0.07f, 1.0f);
	}
}
