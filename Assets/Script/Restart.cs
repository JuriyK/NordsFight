using UnityEngine;
using System.Collections;


public class Restart : MonoBehaviour {

	public GameObject Controll;
	public Transform point;

    void OnGUI()
		{
			if(GUI.Button(new Rect(0, 80, 90, 40), "Restart")){
			Destroy(GameObject.FindWithTag("Controller"));
			Instantiate (Controll, point.position, point.rotation);

				
			}
		}
	
	}

