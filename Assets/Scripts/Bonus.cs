using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

	public float SpinSpeed;
	public int ScoreValue;

	private GameObject gc;

	void Start ()
	{
		gc = GameObject.FindGameObjectWithTag ("GameController");
	}

	void Update ()
	{
		transform.Rotate (0, Time.deltaTime * SpinSpeed, 0);	
	}

	void OnTriggerEnter ()
	{
		gc.gameObject.GetComponent<GameController> ().Score++;
		Destroy (gameObject.transform.parent.gameObject);
	}
}
