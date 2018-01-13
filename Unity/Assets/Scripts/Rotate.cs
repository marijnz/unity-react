using UnityEngine;

public class Rotate : MonoBehaviour
{
	void Update ()
	{
		transform.rotation *= Quaternion.Euler(0,3,0);
	}
}
