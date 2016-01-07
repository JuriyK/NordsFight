using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	private Animation animN;
	private Animation animO;
	public Transform targetOrc;
	public Transform targetNord;
	public float speed;
	public GameObject Nord;
	public GameObject Orc;
	public GameObject Control;

	void Start(){


		animN = Nord.GetComponent<Animation>();
		animO = Orc.GetComponent<Animation>();
	
	}

	public void FixedUpdate()
	{
		Control = GameObject.FindWithTag("Controller");
		Nord = GameObject.FindWithTag("Nord");
		Orc = GameObject.FindWithTag("Orc");
		Control.BroadcastMessage("Set");
		if (Vector3.Distance(Nord.transform.position, targetOrc.position) > 4f)
		{
		float step = speed * Time.deltaTime;
		Nord.transform.position = Vector3.MoveTowards(Nord.transform.position, targetOrc.position, step);
		Orc.transform.position = Vector3.MoveTowards(Orc.transform.position, targetNord.position, step);
		}
		else
		{

			animN.CrossFade ("Idle");
			animO.CrossFade ("Idle");

			Control.BroadcastMessage("AttackN1");
			Destroy(this);	
		}
	}
}