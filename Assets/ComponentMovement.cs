using UnityEngine;
using System.Collections;

public class ComponentMovement : MonoBehaviour {
	
	public GameObject manager;
	public int l, c;
	float timer = 0.0f;
	public Vector3 startposition;
	public Vector3 positiontoreach;
	public int ownvalue;
	public bool IsOnMovement;
	// Use this for initialization
	void Start () {
		l = -1;
		c = -1;
		for (int i = 0; i <= 4; i ++)
			for (int j = 0; j <= 4; j ++) 
				if (transform.position == manager.GetComponent<MainManager> ().positions [i, j])
			{
				l = i;
				c = j;
				i = 5;
				break;
			}
		int.TryParse (gameObject.tag, out ownvalue);
		IsOnMovement = false;
		if(l != -1 && c != -1)
		if (manager.GetComponent<MainManager> ().adiac [l, c] == true && manager.GetComponent<MainManager> ().values [l, c] != ownvalue) {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position == Vector3.zero) {
			for (int i = 0; i <= 4; i ++)
				for (int j = 0; j <= 4; j ++) 
				if (manager.GetComponent<MainManager>().adiac[i,j] == false && manager.GetComponent<MainManager>().values[i,j] == ownvalue){
					if(manager.GetComponent<MainManager>().values[i,j] == ownvalue){
						transform.position = manager.GetComponent<MainManager>().positions[i, j];
						l = i;
						c = j;
						i = 5;
						break;
					}
				}
		}
		
		
		if (l != -1 && c != -1) {
			if(manager.GetComponent<MainManager>().didntacceptyet == true)
			{
				Debug.Log("enter");
				for (int i = 0; i <= 4; i ++)
					for (int j = 0; j <= 4; j ++) 
					if (transform.position == manager.GetComponent<MainManager> ().positions [i, j]){
						l = i;
						c = j;
						i = 5;
						break;
					}
				if(l < 4 && l > 0 && c > 0 && c < 4){
					Destroy(gameObject);
				}
				//manager.GetComponent<MainManager>().finishedad = false;
			}
			
			if(manager.GetComponent<MainManager>().CheckIfMovementHadFinished() == true)
			if (manager.GetComponent<MainManager> ().adiac [l, c] == true && manager.GetComponent<MainManager> ().values [l, c] != ownvalue) {
				Destroy(gameObject);
			}
			if (keypressed() == true) {
				Debug.Log("Bine asa!");
				for (int i = 0; i <= 4; i ++)
					for (int j = 0; j <= 4; j ++) 
					if (transform.position == manager.GetComponent<MainManager> ().positions [i, j]){
						l = i;
						c = j;
						i = 5;
						break;
					}
				for (int i = 0; i <= 4; i ++)
				for (int j = 0; j <= 4; j ++) {
					manager.GetComponent<MainManager>().alreadyspawned[i, j] = false;
				}
				startposition = transform.position;
				Debug.Log(startposition);
				timer = 0.0f;
				manager.GetComponent<MainManager>().touchedmovement = false;
			}
			startposition = manager.GetComponent<MainManager>().positions[l, c];
			if (manager.GetComponent<MainManager> ().movementselected [l, c] == true) {
				positiontoreach = manager.GetComponent<MainManager> ().positions [manager.GetComponent<MainManager> ().movementposition [l, c].ltogo, manager.GetComponent<MainManager> ().movementposition [l, c].ctogo];
				//for(int i = 0; i <= 4; i++)
				//	for(int j = 0; j <= 4; j++)
				//		if(positiontoreach == manager.GetComponent<MainManager>().positions[i, j])
				//			manager.GetComponent<MainManager>().adiac[i, j] = true;
				timer += Time.deltaTime;
				if (transform.position != positiontoreach) {
					transform.position = Vector3.Lerp (startposition, positiontoreach, timer * 5);
				} else {
					//manager.GetComponent<MainManager>().Score += ownvalue;
					timer = 0.0f;
					manager.GetComponent<MainManager> ().movementselected [l, c] = false;
					for (int i = 0; i <= 4; i ++)
						for (int j = 0; j <= 4; j ++) 
						if (transform.position == manager.GetComponent<MainManager> ().positions [i, j]){
							l = i;
							c = j;
							i = 5;
							break;
						}
				}
			}
			else transform.position = manager.GetComponent<MainManager>().positions[l, c];
		}
	}
	
	bool keypressed(){
		/*if (Input.GetKeyDown (KeyCode.UpArrow))
			return true;
		if (Input.GetKeyDown (KeyCode.DownArrow))
			return true;
		if (Input.GetKeyDown (KeyCode.LeftArrow))
			return true;
		if (Input.GetKeyDown (KeyCode.RightArrow))
			return true;*/
		if (manager.GetComponent<MainManager> ().touchedmovement == true) {
			manager.GetComponent<MainManager> ().touchedmovement = false;
			return true;
		} else {
			return false;
		}
	}
	
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == transform.gameObject.tag) {
			//Debug.Log((int)(Mathf.Log (manager.GetComponent<MainManager>().values [l, c], 2)));
			timer = 0.0f;
			manager.GetComponent<MainManager> ().movementselected [l, c] = false;
			for (int i = 0; i <= 4; i ++)
				for (int j = 0; j <= 4; j ++) 
				if (positiontoreach == manager.GetComponent<MainManager> ().positions [i, j]){
					l = i;
					c = j;
					i = 5;
					break;
				}
			//manager.GetComponent<MainManager>().adiac[l, c] = true;
			//Instantiate (manager.GetComponent<MainManager>().Circle [(int)(Mathf.Log (manager.GetComponent<MainManager>().values [l, c], 2))], positiontoreach, Quaternion.identity);
			if(other.gameObject.CompareTag("1") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				//Debug.Log(positiontoreach);
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[1], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("2") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[2], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("4") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[3], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("8") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[4], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("16") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[5], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("32") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[6], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("64") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[7], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("128") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[8], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("256") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[9], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("512") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[10], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
		}
	}
}

/*if (l != -1 && c != -1){
				if(IsOnMovement == false)
				{
					for (int i = 0; i <= 4; i ++)
						for (int j = 0; j <= 4; j ++) 
							if (transform.position == manager.GetComponent<MainManager> ().positions [i, j])
							{
								l = i;
								c = j;
								i = 5;
								break;
							}
				}
				if(keypressed() == true)
				{
					startposition = transform.position;
					IsOnMovement = true;
					timer = 0.0f;
					positiontoreach = manager.GetComponent<MainManager>().positions[manager.GetComponent<MainManager>().movementposition[l,c].ltogo, manager.GetComponent<MainManager>().movementposition[l,c].ctogo];
				}
				if(IsOnMovement == true && manager.GetComponent<MainManager>().movementselected[l, c] == true)
				{
					timer += Time.deltaTime;
					if(transform.position == positiontoreach)
					{
						IsOnMovement = false;
						manager.GetComponent<MainManager>().movementselected[l, c] = false;
					for (int i = 0; i <= 4; i ++)
						for (int j = 0; j <= 4; j ++) 
						if (transform.position == manager.GetComponent<MainManager> ().positions [i, j]){
							l = i;
							c = j;
							i = 5;
							break;
						}
					}
					else{
						transform.position = Vector3.Lerp(startposition, positiontoreach, timer*5);
						
					}
				}
			}
				   }

					   bool keypressed(){
						if (Input.GetKeyDown (KeyCode.UpArrow))
							return true;
						if (Input.GetKeyDown (KeyCode.DownArrow))
							return true;
						if (Input.GetKeyDown (KeyCode.LeftArrow))
							return true;
						if (Input.GetKeyDown (KeyCode.RightArrow))
							return true;
						return false;
					}
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == transform.gameObject.tag) {
			//Debug.Log((int)(Mathf.Log (manager.GetComponent<MainManager>().values [l, c], 2)));
			timer = 0.0f;
			manager.GetComponent<MainManager> ().movementselected [l, c] = false;
			for (int i = 0; i <= 4; i ++)
				for (int j = 0; j <= 4; j ++) 
				if (positiontoreach == manager.GetComponent<MainManager> ().positions [i, j]){
					l = i;
					c = j;
					i = 5;
					break;
				}
			//Instantiate (manager.GetComponent<MainManager>().Circle [(int)(Mathf.Log (manager.GetComponent<MainManager>().values [l, c], 2))], positiontoreach, Quaternion.identity);
			if(other.gameObject.CompareTag("1") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				//Debug.Log(positiontoreach);
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[1], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("2") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[2], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("4") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[3], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("8") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[4], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("16") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[5], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("32") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[6], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("64") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[7], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("128") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[8], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("256") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[9], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
			if(other.gameObject.CompareTag("512") && manager.GetComponent<MainManager>().alreadyspawned[l, c] == false){
				manager.GetComponent<MainManager>().alreadyspawned[l, c] = true;
				Instantiate(manager.GetComponent<MainManager>().Circle[10], positiontoreach, Quaternion.identity);
				Destroy(other.gameObject);
				Destroy(gameObject);
			}
		}
	}


	}


*/





