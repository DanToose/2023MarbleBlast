using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		if (other.transform.parent.tag == "Player")
			other.transform.parent.GetComponent<Player> ().ResetPlayer ();
	}
}
