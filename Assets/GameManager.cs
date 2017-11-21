using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int[,] values = new int[,]{{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0}};
	public Vector3[,] positions = new Vector3[,]{ {new Vector3(-2.12f, 2.106f, -4.0f), new Vector3(-1.03f, 2.106f, -4.0f), new Vector3(0.026f, 2.106f, -4.0f), new Vector3(1.06f, 2.106f, -4.0f), new Vector3(2.16f, 2.106f, -4.0f)}, 
		{new Vector3(-2.12f, 1.05f, -4.0f), new Vector3(-1.03f, 1.05f, -4.0f), new Vector3(0.026f, 1.05f, -4.0f), new Vector3(1.06f, 1.05f, -4.0f), new Vector3(2.16f, 1.05f, -4.0f)}, 
		{new Vector3(-2.12f, 0f, -4.0f), new Vector3(-1.03f, 0f, -4.0f), new Vector3(0.026f, 0f, -4.0f), new Vector3(1.06f, 0f, -4.0f), new Vector3(2.16f, 0f, -4.0f)},
		{new Vector3(-2.12f, -1.05f, -4.0f), new Vector3(-1.03f, -1.05f, -4.0f), new Vector3(0.026f, -1.05f, -4.0f), new Vector3(1.06f, -1.05f, -4.0f), new Vector3(2.16f, -1.05f, -4.0f)}, 
		{new Vector3(-2.12f, -2.14f, -4.0f), new Vector3(-1.03f, -2.14f, -4.0f), new Vector3(0.026f, -2.14f, -4.0f), new Vector3(1.06f, -2.14f, -4.0f), new Vector3(2.16f, -2.14f, -4.0f)} };
	public GameObject[] Circle = new GameObject[10];
	public Transform[] sketch = new Transform[16];
	bool GameLost = false, moved = true, spawned = false;
	int direction; // 1 right 2 up 3 left 4 down
	int l, c;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (GameLost == false){
				if (spawned == false) {
					l = (int)(Random.Range (1, 4));
					c = (int)(Random.Range (1, 4));
					Spawn ();
					}
				if(Input.GetKeyDown(KeyCode.UpArrow)){
					MoveUp();
					spawned = false;
				}
		}
	}

	void Spawn(){
		values [l, c] = 1;
		Instantiate(Circle[0], positions[l, c], Quaternion.identity);
		spawned = true;
	}

	void MoveUp(){
		values [l, c] = 0;
		if (c == 1) {
			if (values [0, 1] == 0 || values[0,1] == 1){
				values [0, 1] += 1;
			}
			else{
				values[1, 1] = 1;
				LoseScreen();
			}
		}
		if (c == 2) {
			if (values [0, 2] == 0 || values[0,2] == 1)
				values [0, 2] += 1;
			else{
				values[1, 2] = 1;
				LoseScreen();
			}
		}
		if (c == 3) {
			if (values [0, 3] == 0 || values[0,3] == 1)
				values [0, 3] += 1;
			else{
				values[1,3] = 1;
				LoseScreen();
			}
		}

	}

	void LoseScreen(){
	}
}
