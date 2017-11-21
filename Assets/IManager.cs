using UnityEngine;
using System.Collections;

public class IManager : MonoBehaviour {

	public int[,] values = new int[,]{{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0}};
	public Vector3[,] positions = new Vector3[,]{ {new Vector3(-2.12f, 2.106f, -3.05f), new Vector3(-1.03f, 2.106f, -3.05f), new Vector3(0.026f, 2.106f, -3.05f), new Vector3(1.06f, 2.106f, -3.05f), new Vector3(2.16f, 2.106f, -3.05f)}, 
		{new Vector3(-2.12f, 1.05f, -3.05f), new Vector3(-1.03f, 1.05f, -3.05f), new Vector3(0.026f, 1.05f, -3.05f), new Vector3(1.06f, 1.05f, -3.05f), new Vector3(2.16f, 1.05f, -3.05f)}, 
		{new Vector3(-2.12f, 0f, -3.05f), new Vector3(-1.03f, 0f, -3.05f), new Vector3(0.026f, 0f, -3.05f), new Vector3(1.06f, 0f, -3.05f), new Vector3(2.16f, 0f, -3.05f)},
		{new Vector3(-2.12f, -1.05f, -3.05f), new Vector3(-1.03f, -1.05f, -3.05f), new Vector3(0.026f, -1.05f, -3.05f), new Vector3(1.06f, -1.05f, -3.05f), new Vector3(2.16f, -1.05f, -3.05f)}, 
		{new Vector3(-2.12f, -2.14f, -3.05f), new Vector3(-1.03f, -2.14f, -3.05f), new Vector3(0.026f, -2.14f, -3.05f), new Vector3(1.06f, -2.14f, -3.05f), new Vector3(2.16f, -2.14f, -3.05f)} };
	public GameObject[] Circle = new GameObject[10];
	public int l;
	public int c;
	public bool moved = true;
	public bool GameOver = false;
	private float timebetweenspawns= 0.0f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (GameOver == false) {
			if (moved == true) {
				Spawn ();
			}
		}
	}

	void Spawn(){
		//Debug.Log (values [0, 1]);
		//Debug.Log (values [0, 2]);
		//Debug.Log (values [0, 3]);
		l = (int)(Random.Range (1, 4));
		c = (int)(Random.Range (1, 4));
		Instantiate(Circle[0], positions[l, c], Quaternion.identity);
		moved = false;
	}
	
}
