using UnityEngine;
using System.Collections;

public class spherecommand : MonoBehaviour {
	
	public GameObject Manager;
	int l = -1, c = -1;
	public int ownvalue;
	public Vector3 InitialPosition;
	public Vector3 FinalPosition;
	public bool SetFinalPosition = false;
	public float timer = 0.0f;
	// Use this for initialization
	void Start () {
		int.TryParse (gameObject.tag, out ownvalue);
		SearchForPosition ();
	}
	
	// Update is called once per frame
	void Update () {
		if (l != -1 && c != -1) {
			if(Manager.GetComponent<gamecontroller>().values[l, c] != ownvalue && (l == 0 ||c == 0 || l == 4 || c == 4))
			{
				Destroy(gameObject);
			}
			if(Manager.GetComponent<gamecontroller>().movementselected[l, c] == true)
			{
				timer += Time.deltaTime;
				if(SetFinalPosition == false){
					FinalPosition = Manager.GetComponent<gamecontroller>().positions[Manager.GetComponent<gamecontroller>().movementposition[l, c].ltogo, Manager.GetComponent<gamecontroller>().movementposition[l, c].ctogo];
					SetFinalPosition = true;
					InitialPosition = transform.position;
					timer = 0.0f;
				}
				if(transform.position != FinalPosition)
				{
					transform.position = Vector3.Lerp(InitialPosition, FinalPosition, 5 * timer);
				}
				else
				{
					Manager.GetComponent<gamecontroller>().movementselected[l, c] = false;
					Manager.GetComponent<gamecontroller>().privatematrix[l, c] = 0;
					SearchForPosition();
					Manager.GetComponent<gamecontroller>().privatematrix[l, c] = ownvalue;
					if(Manager.GetComponent<gamecontroller>().values[l, c] != ownvalue)
					{
						Debug.Log("distrus");
						Destroy(gameObject);
					}
					SetFinalPosition = false;
				}

			}
		}
	}

	void SearchForPosition()
	{
		for (int i = 0; i <= 4; i++)
			for (int j = 0; j <= 4; j++)
				if (Manager.GetComponent<gamecontroller> ().positions [i, j] == transform.position) {
					l = i;
					c = j;
					i = 5;
					break;
				}
	}
}
