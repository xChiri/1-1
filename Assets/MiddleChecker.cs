using UnityEngine;
using System.Collections;

public class MiddleChecker : MonoBehaviour {

	public Vector3[,] positions = new Vector3[,]{ {new Vector3(-2.12f, 2.106f, -3.05f), new Vector3(-1.03f, 2.106f, -3.05f), new Vector3(0.026f, 2.106f, -3.05f), new Vector3(1.06f, 2.106f, -3.05f), new Vector3(2.16f, 2.106f, -3.05f)}, 
		{new Vector3(-2.12f, 1.05f, -3.05f), new Vector3(-1.03f, 1.05f, -3.05f), new Vector3(0.026f, 1.05f, -3.05f), new Vector3(1.06f, 1.05f, -3.05f), new Vector3(2.16f, 1.05f, -3.05f)}, 
		{new Vector3(-2.12f, 0f, -3.05f), new Vector3(-1.03f, 0f, -3.05f), new Vector3(0.026f, 0f, -3.05f), new Vector3(1.06f, 0f, -3.05f), new Vector3(2.16f, 0f, -3.05f)},
		{new Vector3(-2.12f, -1.05f, -3.05f), new Vector3(-1.03f, -1.05f, -3.05f), new Vector3(0.026f, -1.05f, -3.05f), new Vector3(1.06f, -1.05f, -3.05f), new Vector3(2.16f, -1.05f, -3.05f)}, 
		{new Vector3(-2.12f, -2.14f, -3.05f), new Vector3(-1.03f, -2.14f, -3.05f), new Vector3(0.026f, -2.14f, -3.05f), new Vector3(1.06f, -2.14f, -3.05f), new Vector3(2.16f, -2.14f, -3.05f)} };
	// Use this for initialization
	void Start () {
		for (int i = 1; i <= 3; i++)
			for (int j = 1; j <= 3; j++)
				if(transform.position == positions[i,j])
				Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
