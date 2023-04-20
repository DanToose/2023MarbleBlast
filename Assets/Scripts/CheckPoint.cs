using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		if (other.transform.parent.tag == "Player")
			other.transform.parent.GetComponent<Player> ().ResetPosition = transform.position;
	}
}
