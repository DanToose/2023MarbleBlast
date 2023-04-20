using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject Player;
	public GameObject LevelStart;
	public GameObject LevelGoal;

	public Vector3 StartOffset;

	public Text ScoreText;
	public Text TimeText;

	private int CurrentScene;

	[HideInInspector] public int Score;
	private float TimeElapsed;
	private string FormattedTime;
	private string FormattedMinutes;
	private string FormattedSeconds;

	void Start ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		Player.transform.position = LevelStart.transform.position + StartOffset;

		Score = 0;
		TimeElapsed = 0.0f;
		UpdateUI ();
	}
	
	void Update ()
	{
		TimeElapsed += Time.deltaTime;
		UpdateUI ();
		if (Input.GetKey (KeyCode.KeypadEnter))
			GoToNextScene ();
	}

	public void GoToNextScene()
	{
		Debug.Log (SceneManager.sceneCountInBuildSettings);
		Debug.Log (SceneManager.GetActiveScene ().buildIndex);

		if (SceneManager.GetActiveScene().buildIndex-1 == SceneManager.sceneCountInBuildSettings)
			SceneManager.LoadScene (0);
		else
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex+1);
	}

	private void UpdateUI()
	{
		if ((Mathf.Floor (TimeElapsed / 60)) < 1)
			FormattedMinutes = "00";
		else if ((Mathf.Floor(TimeElapsed / 60)) < 10)
			FormattedMinutes = "0" + Mathf.Floor(TimeElapsed / 60);
		else
			FormattedMinutes = Mathf.Floor(TimeElapsed / 60).ToString();

		if ((Mathf.Floor (TimeElapsed % 60)) < 10)
			FormattedSeconds = "0" + Mathf.Floor (TimeElapsed % 60);
		else
			FormattedSeconds = Mathf.Floor (TimeElapsed % 60).ToString();

		FormattedTime = FormattedMinutes + ":" + FormattedSeconds;

		ScoreText.text = Score.ToString();
		TimeText.text = FormattedTime;
	}
}
