using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public int[,] values = new int[,]{{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0},{0,0,0,0,0}};
	public Vector3[,] positions = new Vector3[,]{ {new Vector3(-2.12f, 2.106f, -4.0f), new Vector3(-1.03f, 2.106f, -4.0f), new Vector3(0.026f, 2.106f, -4.0f), new Vector3(1.06f, 2.106f, -4.0f), new Vector3(2.16f, 2.106f, -4.0f)}, 
		{new Vector3(-2.12f, 1.05f, -4.0f), new Vector3(-1.03f, 1.05f, -4.0f), new Vector3(0.026f, 1.05f, -4.0f), new Vector3(1.06f, 1.05f, -4.0f), new Vector3(2.16f, 1.05f, -4.0f)}, 
		{new Vector3(-2.12f, 0f, -4.0f), new Vector3(-1.03f, 0f, -4.0f), new Vector3(0.026f, 0f, -4.0f), new Vector3(1.06f, 0f, -4.0f), new Vector3(2.16f, 0f, -4.0f)},
		{new Vector3(-2.12f, -1.05f, -4.0f), new Vector3(-1.03f, -1.05f, -4.0f), new Vector3(0.026f, -1.05f, -4.0f), new Vector3(1.06f, -1.05f, -4.0f), new Vector3(2.16f, -1.05f, -4.0f)}, 
		{new Vector3(-2.12f, -2.14f, -4.0f), new Vector3(-1.03f, -2.14f, -4.0f), new Vector3(0.026f, -2.14f, -4.0f), new Vector3(1.06f, -2.14f, -4.0f), new Vector3(2.16f, -2.14f, -4.0f)} };
	public GameObject[] Circle = new GameObject[10];
	public int direction = 0; // 1 - right 2 - left 3 - top 4 - down
	
	bool spawned = false;
	public int l, c;
	bool starttimer;
	float spawntimer= 0.2f;
	bool GameLost = false, moved = true;
	public RawImage FinishGame;
	public GameObject MainCanvasScript;
	// Use this for initialization
	void Start () {
		FinishGame.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (spawned);
		if (MainCanvasScript.GetComponent<CanvasManager> ().paused == false) {
			Verifier ();
			if (GameLost == false && moved == true) {
				if (spawned == false) {
					l = (int)(Random.Range (1, 4));
					c = (int)(Random.Range (1, 4));
					Spawn ();
				}
				if (Input.GetKeyDown (KeyCode.RightArrow) || HadMovedRight () == 1) {
					MoveRight ();
					starttimer = true;
					spawntimer = 0.2f;
					moved = false;
					spawned = false;
				}
				if (Input.GetKeyDown (KeyCode.LeftArrow) || HadMovedRight () == 2) {
					MoveLeft ();
					starttimer = true;
					spawntimer = 0.2f;
					moved = false;
					spawned = false;
				}
				if (Input.GetKeyDown (KeyCode.UpArrow) || HadMovedRight () == 3) {
					MoveUp ();
					starttimer = true;
					spawntimer = 0.2f;
					moved = false;
					spawned = false;
				}
				if (Input.GetKeyDown (KeyCode.DownArrow) || HadMovedRight () == 4) {
					MoveDown ();
					starttimer = true;
					spawntimer = 0.2f;
					moved = false;
					spawned = false;
				}
			}
			
			if (starttimer == true) {
				spawntimer -= Time.deltaTime;
				if (spawntimer <= 0) {
					moved = true;
					starttimer = false;
				}
			}
			
		}
	}

	int HadMovedRight(){
		return 0;
	}

	void Spawn(){
		values [l, c] = 1;
		Instantiate(Circle[0], positions[l, c], Quaternion.identity);
		spawned = true;
	}

	void MoveRight(){
		direction = 1;
		MoveAnimation ();
		values [l, c] = 0;
		if (l == 1) {
			if (values [1, 4] == 0 || values[1,4] == 1)
				values [1, 4] += 1;
			else{
				values[1,3] = 1;
				LoseScreen();
			}
		}
		if (l == 2) {
			if (values [2, 4] == 0 || values[2,4] == 1)
				values [2, 4] += 1;
			else{
				values[2,3] = 1;
				LoseScreen();
			}
		}
		if (l == 3) {
			if (values [3, 4] == 0 || values[3,4] == 1)
				values [3, 4] += 1;
			else{
				values[3,3] = 1;
				LoseScreen();
			}
		}
		MoveDownSideToRight ();
		MoveUpperSideToRight ();

	}

	void MoveDownSideToRight(){
		int[] aux = new int[5];
		int k = 0;
		for(int i = 0; i < 5; i ++)
			if(values[4, i] != 0)
			   aux[k++] = values[4, i];
		k--;
		for (int i = 0; i < 5; i ++)
			values [4, i] = 0;
		for (int i = k; i > 0; i --)
			if (aux [i] == aux [i - 1]) {
			aux[i] = aux[i] + aux[i-1];
			aux[i-1] = 0;
			}
		int p = 4;
		for(int i = k; i>=0; i--){
			if(aux[i] != 0){
				values[4, p] = aux[i];
				p--;
			}
		}
	}

	void MoveUpperSideToRight (){
		int[] aux = new int[5];
		int k = 0;
		for(int i = 0; i < 5; i ++)
			if(values[0, i] != 0)
				aux[k++] = values[0, i];
		k--;
		for (int i = 0; i < 5; i ++)
			values [0, i] = 0;
		for (int i = k; i > 0; i --)
		if (aux [i] == aux [i - 1]) {
			Debug.Log(aux[i]);
			aux[i] = aux[i] + aux[i-1];
			aux[i-1] = 0;
			Debug.Log(aux[i]);
		}
		int p = 4;
		for(int i = k; i>=0; i--){
			if(aux[i] != 0){
				values[0, p] = aux[i];
				p--;
			}
		}
	}

	void MoveLeft(){
		direction = 2;
		MoveAnimation ();
		values [l, c] = 0;
		if (l == 1) {
			if (values [1, 0] == 0 || values[1,0] == 1)
				values [1, 0] += 1;
			else{
				values[1,1] = 1;
				LoseScreen();
			}
		}
		if (l == 2) {
			if (values [2, 0] == 0 || values[2,0] == 1)
				values [2, 0] += 1;
			else{
				values[2, 1] = 1;
				LoseScreen();
			}
		}
		if (l == 3) {
			if (values [3, 0] == 0 || values[3,0] == 1)
				values [3, 0] += 1;
			else{
				values[3, 1] = 1;
				LoseScreen();
			}
		}
		MoveDownSideToLeft ();
		MoveUpperSideToLeft ();

	}

	void MoveDownSideToLeft(){
		int[] aux = new int[5];
		int k = 0;
		for(int i = 0; i < 5; i ++)
			if(values[4, i] != 0)
				aux[k++] = values[4, i];
		k--;
		for (int i = 0; i < 5; i ++)
			values [4, i] = 0;
		for (int i = 1; i <= k; i ++)
		if (aux [i] == aux [i - 1]) {
			aux[i-1] = aux[i] + aux[i-1];
			aux[i] = 0;
		}
		int p = 0;
		for(int i = 0; i<=k; i++){
			if(aux[i] != 0){
				values[4, p] = aux[i];
				p++;
			}
		}
	}
	void MoveUpperSideToLeft(){
		int[] aux = new int[5];
		int k = 0;
		for(int i = 0; i < 5; i ++)
			if(values[0, i] != 0)
				aux[k++] = values[0, i];
		k--;
		for (int i = 0; i < 5; i ++)
			values [0, i] = 0;
		for (int i = 1; i <= k; i ++)
		if (aux [i] == aux [i - 1]) {
			aux[i-1] = aux[i] + aux[i-1];
			aux[i] = 0;
		}
		int p = 0;
		for(int i = 0; i<=k; i++){
			if(aux[i] != 0){
				values[0, p] = aux[i];
				p++;
			}
		}
	}

	void MoveUp(){
		direction = 3;
		MoveAnimation ();
		values [l, c] = 0;
		if (c == 1) {
			if (values [0, 1] == 0 || values[0,1] == 1)
				values [0, 1] += 1;
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
		MoveLeftSideUpper ();
		MoveRightSideUpper ();
	}

	void MoveLeftSideUpper(){
		int[] aux = new int[5];
		int k = 0;
		for(int i = 0; i < 5; i ++)
			if(values[i, 0] != 0)
				aux[k++] = values[i, 0];
		k--;
		for(int i = 0; i < 5; i ++)
			values[i, 0] = 0;
		for(int i = 0; i < 3; i ++){
			if(aux[i+1] == aux[i]){
				aux[i] *= 2;
				aux[i + 1] = 0;
			}
		}
		int p = 0;
		for(int i = 0; i <= k; i++){
			if(aux[i] != 0){
				values[p, 0] = aux[i];
				p++;
			}
		}
	}

	void MoveRightSideUpper(){
		int[] aux = new int[5];
		int k = 0;
		for(int i = 0; i < 5; i ++)
			if(values[i, 4] != 0)
				aux[k++] = values[i, 4];
		k--;
		for(int i = 0; i < 5; i ++)
			values[i, 4] = 0;
		for(int i = 0; i < 3; i ++){
			if(aux[i+1] == aux[i]){
				aux[i] *= 2;
				aux[i + 1] = 0;
			}
		}
		int p = 0;
		for(int i = 0; i <= k; i++){
			if(aux[i] != 0){
				values[p, 4] = aux[i];
				p++;
			}
		}
	}

	void MoveDown(){
		direction = 4;
		MoveAnimation ();
		values [l, c] = 0;
		if (c == 1) {
			if (values [4, 1] == 0 || values[4,1] == 1)
				values [4, 1] += 1;
			else{
				values[3, 1] = 1;
				LoseScreen();
			}
		}
		if (c == 2) {
			if (values [4, 2] == 0 || values[4,2] == 1)
				values [4, 2] += 1;
			else{
				values[3, 2] = 1;
				LoseScreen();
			}
		}
		if (c == 3) {
			if (values [4, 3] == 0 || values[4,3] == 1)
				values [4, 3] += 1;
			else{
				values[3, 3] = 1;
				LoseScreen();
			}
		}
		MoveLeftSideDowner ();
		MoveRightSideDowner ();
	}

	void MoveLeftSideDowner(){
		int[] aux = new int[5];
		int k = 0;
		for(int i = 4; i >= 0; i --)
			if(values[i, 0] != 0)
				aux[k++] = values[i, 0];
		k--;
		for(int i = 0; i < 5; i ++)
			values[i, 0] = 0;
		for(int i = 0; i < k; i ++){
			if(aux[i+1] == aux[i]){
				aux[i] *= 2;
				aux[i + 1] = 0;
			}
		}
		int p = 4;
		for(int i = 0; i <= k; i++){
			if(aux[i] != 0){
				values[p, 0] = aux[i];
				p--;
			}
		}
	}

	void MoveRightSideDowner(){
		int[] aux = new int[5];
		int k = 0;
		for(int i = 4; i >= 0; i --)
			if(values[i, 4] != 0)
				aux[k++] = values[i, 4];
		k--;
		for(int i = 0; i < 5; i ++)
			values[i, 4] = 0;
		for(int i = 0; i < k; i ++){
			if(aux[i+1] == aux[i]){
				aux[i] *= 2;
				aux[i + 1] = 0;
			}
		}
		int p = 4;
		for(int i = 0; i <= k; i++){
			if(aux[i] != 0){
				values[p, 4] = aux[i];
				p--;
			}
		}
	}


	void LoseScreen(){
		GameLost = true;
		FinishGame.gameObject.SetActive (true);
	}

	void MoveAnimation(){
	}

	void Verifier(){
		if (moved == true) {
			for (int i = 0; i < 5; i ++) 
				for (int j = 0; j < 5; j ++)
					if (values [i, j] > 0)
						Instantiate (Circle [(int)(Mathf.Log (values [i, j], 2))], positions [i, j], Quaternion.identity);
		}
	}

}
