var deadTime : float;
 transform.Rotate(-90,0,0);

function Awake () {
	Destroy (gameObject, deadTime);
}