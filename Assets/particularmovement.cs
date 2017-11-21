using UnityEngine;
using System.Collections;

public class particularmovement : MonoBehaviour {
	
	public GameObject MainManager;
	private int l, c;
	private float timer = 0;
	public Vector3 startposition;
	private bool moveupTrigger = false;
	private bool movedownTrigger = false;
	private bool moveleftTrigger = false;
	private bool moverightTrigger = false;
	private bool col = false;
	private bool abletomove = false;
	public Vector3 gotoposition = Vector3.zero;
	public int gotocolumn;

	public bool moveUpsidetoLeft = false;
	// Use this for initialization
	void Start () {
		l = MainManager.GetComponent<IManager> ().l;
		c = MainManager.GetComponent<IManager> ().c;
		for (int i = 1; i <= 3; i++)
			for (int j = 1; j <= 3; j++)
				if (MainManager.GetComponent<IManager> ().positions [l, c] == transform.position) {
				abletomove = true;
				i = 4;
				}
	}
	
	// Update is called once per frame
	void Update () {
		if (abletomove == true && MainManager.GetComponent<IManager>().GameOver == false) {
			l = MainManager.GetComponent<IManager> ().l;
			c = MainManager.GetComponent<IManager> ().c;
			//up movement activator
			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				if (l >= 1 && l <= 3 && c >= 1 && c <= 3) {
					moveupTrigger = true;
					l = 0;
				}
				startposition = transform.position;
			}
			//down movement activator
			if (Input.GetKeyDown (KeyCode.DownArrow)) {
				if (l >= 1 && l <= 3 && c >= 1 && c <= 3) {
					movedownTrigger = true;
					l = 4;
				}
				startposition = transform.position;
			}
			//left movement activator
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				if (l >= 1 && l <= 3 && c >= 1 && c <= 3) {
					moveleftTrigger = true;
					c = 0;
				}
				//if(l == 0){
				//	moveUpsidetoLeft = true;
				//}
				startposition = transform.position;
			}
			//right movement activator
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				if (l >= 1 && l <= 3 && c >= 1 && c <= 3) {
					moverightTrigger = true;
					c = 4;
				}
				startposition = transform.position;
			}
			// up movement
			if (moveupTrigger == true) {
				MoveUpfromCenter();
			}
			//down movement
			if(movedownTrigger == true){
				MoveDownfromCenter();
			}
			//left movement
			if(moveleftTrigger == true){
				MoveLeftfromCenter();
			}
			//right movement
			if(moverightTrigger == true){
				MoveRightfromCenter();
			}
			if(moveUpsidetoLeft == true){
				MoveUpside();
			}
		}
	}

	void MoveUpside(){
		timer += Time.deltaTime;
		if (transform.position == gotoposition) {
			moveUpsidetoLeft = false;
		} else {
			transform.position = Vector3.Lerp (startposition, gotoposition, timer * 5);
		}
	}

	void MoveUpfromCenter(){
		timer += Time.deltaTime;
		if (MainManager.GetComponent<IManager> ().values [0, c] > 1) {
			if (transform.position != MainManager.GetComponent<IManager> ().positions [1, c]) {
				transform.position = Vector3.Lerp (startposition, MainManager.GetComponent<IManager> ().positions [1, c], timer * 5);
			}
			if (transform.position == MainManager.GetComponent<IManager> ().positions [1, c]) {
				moveupTrigger = false;
				MainManager.GetComponent<IManager> ().GameOver = true;
			}
		} 
		else
		if(MainManager.GetComponent<IManager>().values [0,c] <= 1){
			if (transform.position == MainManager.GetComponent<IManager> ().positions [0, c]) {
				timer = 0;
				l = 0;
				if(MainManager.GetComponent<IManager>().values[0,c] == 1)
					MainManager.GetComponent<IManager>().values[0, c] = 2;
				if(MainManager.GetComponent<IManager>().values[0,c] == 0)
					MainManager.GetComponent<IManager>().values[0, c] = 1;
				MainManager.GetComponent<IManager>().moved = true;
				moveupTrigger = false;
			}
			if (transform.position != MainManager.GetComponent<IManager> ().positions [0, c]) {
				transform.position = Vector3.Lerp (startposition, MainManager.GetComponent<IManager> ().positions [0, c], timer * 5);
			}
		}
	}

	void MoveDownfromCenter(){
		timer += Time.deltaTime;
		if (MainManager.GetComponent<IManager> ().values [4, c] > 1) {
			if (transform.position != MainManager.GetComponent<IManager> ().positions [3, c]) {
				transform.position = Vector3.Lerp (startposition, MainManager.GetComponent<IManager> ().positions [3, c], timer * 5);
			}
			if (transform.position == MainManager.GetComponent<IManager> ().positions [3, c]) {
				movedownTrigger = false;
				MainManager.GetComponent<IManager> ().GameOver = true;
			}
		} 
		else
		if(MainManager.GetComponent<IManager>().values [4,c] <= 1){
			if (transform.position == MainManager.GetComponent<IManager> ().positions [4, c]) {
				timer = 0;
				l = 4;
				if(MainManager.GetComponent<IManager>().values[4,c] == 1)
					MainManager.GetComponent<IManager>().values[4, c] = 2;
				if(MainManager.GetComponent<IManager>().values[4,c] == 0)
					MainManager.GetComponent<IManager>().values[4, c] = 1;
				MainManager.GetComponent<IManager>().moved = true;
				movedownTrigger = false;
			}
			if (transform.position != MainManager.GetComponent<IManager> ().positions [4, c]) {
				transform.position = Vector3.Lerp (startposition, MainManager.GetComponent<IManager> ().positions [4, c], timer * 5);
			}
		}
	}

	void MoveLeftfromCenter(){
		timer += Time.deltaTime;
		if (MainManager.GetComponent<IManager> ().values [l, 0] > 1) {
			if (transform.position != MainManager.GetComponent<IManager> ().positions [l, 1]) {
				transform.position = Vector3.Lerp (startposition, MainManager.GetComponent<IManager> ().positions [l, 1], timer * 5);
			}
			if (transform.position == MainManager.GetComponent<IManager> ().positions [l, 1]) {
				moveleftTrigger = false;
				MainManager.GetComponent<IManager> ().GameOver = true;
			}
		} 
		else
		if(MainManager.GetComponent<IManager>().values [l,0] <= 1){
			if (transform.position == MainManager.GetComponent<IManager> ().positions [l, 0]) {
				timer = 0;
				c = 0;
				if(MainManager.GetComponent<IManager>().values[l,0] == 1)
					MainManager.GetComponent<IManager>().values[l, 0] = 2;
				if(MainManager.GetComponent<IManager>().values[l,0] == 0)
					MainManager.GetComponent<IManager>().values[l, 0] = 1;
				MainManager.GetComponent<IManager>().moved = true;
				moveleftTrigger = false;
			}
			if (transform.position != MainManager.GetComponent<IManager> ().positions [l, 0]) {
				transform.position = Vector3.Lerp (startposition, MainManager.GetComponent<IManager> ().positions [l, 0], timer * 5);
			}
		}
	}

	void MoveRightfromCenter(){
		timer += Time.deltaTime;
		if (MainManager.GetComponent<IManager> ().values [l, 4] > 1) {
			if (transform.position != MainManager.GetComponent<IManager> ().positions [l, 3]) {
				transform.position = Vector3.Lerp (startposition, MainManager.GetComponent<IManager> ().positions [l, 3], timer * 5);
			}
			if (transform.position == MainManager.GetComponent<IManager> ().positions [l, 3]) {
				moverightTrigger = false;
				MainManager.GetComponent<IManager> ().GameOver = true;
			}
		} 
		else
		if(MainManager.GetComponent<IManager>().values [l,4] <= 1){
			if (transform.position == MainManager.GetComponent<IManager> ().positions [l, 4]) {
				timer = 0;
				c = 4;
				if(MainManager.GetComponent<IManager>().values[l,4] == 1)
					MainManager.GetComponent<IManager>().values[l, 4] = 2;
				if(MainManager.GetComponent<IManager>().values[l,4] == 0)
					MainManager.GetComponent<IManager>().values[l, 4] = 1;
				MainManager.GetComponent<IManager>().moved = true;
				moverightTrigger = false;
			}
			if (transform.position != MainManager.GetComponent<IManager> ().positions [l, 4]) {
				transform.position = Vector3.Lerp (startposition, MainManager.GetComponent<IManager> ().positions [l, 4], timer * 5);
			}
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "1" && transform.gameObject.tag == "1") {
			MainManager.GetComponent<IManager> ().moved = true;
			MainManager.GetComponent<IManager>().values[l, c] = 2;
			Instantiate(MainManager.GetComponent<IManager>().Circle[1], MainManager.GetComponent<IManager>().positions[l, c], Quaternion.identity);
			Destroy(gameObject);
			Destroy(other.gameObject);
		}
	}
}
