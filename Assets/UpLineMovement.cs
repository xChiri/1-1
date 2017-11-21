using UnityEngine;
using System.Collections;

public class UpLineMovement : MonoBehaviour {

	public GameObject manager;

	public Collider[] col;
	int i = 1; // i va retine pozitia actuala
	public bool gottomovetoleft = false;
	public int lastfreeposition;
	public int lastvalue;
	public int lastvaluepos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			for(i = 0; i <= 4; i++)
				if(manager.GetComponent<IManager>().values[0, i] == 0){
					lastfreeposition = i;
					break;
				}
			lastvalue = manager.GetComponent<IManager>().values[0, 0];
			lastvaluepos = 0;
			for(i = 1; i <= 4; i++){
				for(int j = 0; j <= 4; j++)
				if(manager.GetComponent<IManager>().values[0, j] == 0){
					lastfreeposition = j;
					break;
				}
				if(manager.GetComponent<IManager>().values[0, i] != 0){
					if(manager.GetComponent<IManager>().values[0, i] == lastvalue){
						col[i].gameObject.GetComponent<circlemove>().circlepositiontomove = manager.GetComponent<IManager>().positions[0, lastvaluepos];
						manager.GetComponent<IManager>().values[0, i] *= 2;
						lastvalue = 0;
						lastvaluepos++;
					}
					else{
						if(lastfreeposition < i){
							col[i].gameObject.GetComponent<circlemove>().circlepositiontomove = manager.GetComponent<IManager>().positions[0, lastfreeposition];
							lastvalue = manager.GetComponent<IManager>().values[0, i];
							manager.GetComponent<IManager>().values[0, lastfreeposition] = manager.GetComponent<IManager>().values[0, i];
							manager.GetComponent<IManager>().values[0, i] = 0;
							lastfreeposition++;
							}
					}
			}
			}
		}
	}
}
