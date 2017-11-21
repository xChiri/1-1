using UnityEngine;
using System.Collections;

public class lerptest : MonoBehaviour {

	public Transform startMarker;
	public Transform endMarker;
	public float speed = 0.001F;
	private float startTime;
	private float journeyLength;
	private float timer = 0.0f;
	void Start() {
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
	}
	void FixedUpdate() {
		timer += Time.deltaTime;
		/*float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;*/
		if (timer <= 0.2f) {
			transform.position = Vector3.Lerp (startMarker.position, endMarker.position, timer * 500);
		} else {
			Destroy(this);
		}
	}
}
