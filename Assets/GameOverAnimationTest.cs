using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverAnimationTest : MonoBehaviour {

	int step = 0;
	float timer = 0.0f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (step == 0) {
			if(transform.position != Vector3.zero)
				transform.position = Vector3.Lerp(new Vector3(0, 900, 0), Vector3.zero, timer);
			else 
			{
				timer = 0.0f;
				step++;
			}
		}
	}
}
