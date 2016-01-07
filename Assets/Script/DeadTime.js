var deadTime : float;
 transform.Rotate(0,0,0);
function Awake () {
	Destroy (gameObject, deadTime);
}