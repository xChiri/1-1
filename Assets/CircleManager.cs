using UnityEngine;
using System.Collections;

public class CircleManager : MonoBehaviour {

	public GameObject manager;
	private bool justSpawned = true;
	// Use this for initialization
	void Awake () {
	}

	/*void SpawnAnimation(){
		if (transform.localScale != new Vector3 (0.124f, 0.124f, 1.0f)) {
			transform.localScale -= new Vector3(0.001f, 0.001f, 0.0f);
		}
	}*/

	// Update is called once per frame
	void Update () {
		/*if (justSpawned == true) {
			transform.localScale = new Vector3(0.135f, 0.135f, 1.0f);
			justSpawned = false;
		}
		SpawnAnimation ();*/
		if (gameObject.CompareTag ("1")) {
			int l = -1, c = -1;
			for(int i = 0; i <= 4; i ++)
				for(int j = 0; j <= 4; j++)
					if(transform.position == manager.GetComponent<Manager>().positions[i, j]){
					l = i;
					c = j;
					break;
					}
			if(l != -1 && c != -1)
			if(manager.GetComponent<Manager>().values[l, c] != 1)
				gameObject.SetActive(false);
		}
		if (gameObject.CompareTag ("2")) {
			int l = -1, c = -1;
			for(int i = 0; i <= 4; i ++)
				for(int j = 0; j <= 4; j++)
				if(transform.position == manager.GetComponent<Manager>().positions[i, j]){
					l = i;
					c = j;
					break;
				}
			if(l != -1 && c != -1)
				if(manager.GetComponent<Manager>().values[l, c] != 2)
					gameObject.SetActive(false);
		}
		if (gameObject.CompareTag ("4")) {
			int l = -1, c = -1;
			for(int i = 0; i <= 4; i ++)
				for(int j = 0; j <= 4; j++)
				if(transform.position == manager.GetComponent<Manager>().positions[i, j]){
					l = i;
					c = j;
					break;
				}
			if(l != -1 && c != -1)
				if(manager.GetComponent<Manager>().values[l, c] != 4)
					gameObject.SetActive(false);
		}
		if (gameObject.CompareTag ("8")) {
			int l = -1, c = -1;
			for(int i = 0; i <= 4; i ++)
				for(int j = 0; j <= 4; j++)
				if(transform.position == manager.GetComponent<Manager>().positions[i, j]){
					l = i;
					c = j;
					break;
				}
			if(l != -1 && c != -1)
				if(manager.GetComponent<Manager>().values[l, c] != 8)
					gameObject.SetActive(false);
		}
		if (gameObject.CompareTag ("16")) {
			int l = -1, c = -1;
			for(int i = 0; i <= 4; i ++)
				for(int j = 0; j <= 4; j++)
				if(transform.position == manager.GetComponent<Manager>().positions[i, j]){
					l = i;
					c = j;
					break;
				}
			if(l != -1 && c != -1)
				if(manager.GetComponent<Manager>().values[l, c] != 16)
					gameObject.SetActive(false);
		}
		if (gameObject.CompareTag ("32")) {
			int l = -1, c = -1;
			for(int i = 0; i <= 4; i ++)
				for(int j = 0; j <= 4; j++)
				if(transform.position == manager.GetComponent<Manager>().positions[i, j]){
					l = i;
					c = j;
					break;
				}
			if(l != -1 && c != -1)
				if(manager.GetComponent<Manager>().values[l, c] != 32)
					gameObject.SetActive(false);
		}
		if (gameObject.CompareTag ("64")) {
			int l = -1, c = -1;
			for(int i = 0; i <= 4; i ++)
				for(int j = 0; j <= 4; j++)
				if(transform.position == manager.GetComponent<Manager>().positions[i, j]){
					l = i;
					c = j;
					break;
				}
			if(l != -1 && c != -1)
				if(manager.GetComponent<Manager>().values[l, c] != 64)
					gameObject.SetActive(false);
		}
		if (gameObject.CompareTag ("128")) {
			int l = -1, c = -1;
			for(int i = 0; i <= 4; i ++)
				for(int j = 0; j <= 4; j++)
				if(transform.position == manager.GetComponent<Manager>().positions[i, j]){
					l = i;
					c = j;
					break;
				}
			if(l != -1 && c != -1)
				if(manager.GetComponent<Manager>().values[l, c] != 128)
					gameObject.SetActive(false);
		}
		if (gameObject.CompareTag ("256")) {
			int l = -1, c = -1;
			for(int i = 0; i <= 4; i ++)
				for(int j = 0; j <= 4; j++)
				if(transform.position == manager.GetComponent<Manager>().positions[i, j]){
					l = i;
					c = j;
					break;
				}
			if(l != -1 && c != -1)
				if(manager.GetComponent<Manager>().values[l, c] != 256)
					gameObject.SetActive(false);
		}
		if (gameObject.CompareTag ("512")) {
			int l = -1, c = -1;
			for(int i = 0; i <= 4; i ++)
				for(int j = 0; j <= 4; j++)
				if(transform.position == manager.GetComponent<Manager>().positions[i, j]){
					l = i;
					c = j;
					break;
				}
			if(l != -1 && c != -1)
				if(manager.GetComponent<Manager>().values[l, c] != 512)
					gameObject.SetActive(false);
		}
	}
}
