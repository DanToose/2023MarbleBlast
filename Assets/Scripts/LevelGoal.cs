using UnityEngine;
using System.Collections;

public class LevelGoal : MonoBehaviour {

	private GameObject gc;

	void Start()
	{
		gc = GameObject.FindGameObjectWithTag ("GameController");
	}

	void OnTriggerEnter ()
	{
		gc.GetComponent<GameController> ().GoToNextScene ();
	}

}
