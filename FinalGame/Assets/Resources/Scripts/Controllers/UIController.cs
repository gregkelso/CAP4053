using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	public GameObject PauseUI;
	public Text scoreText;
    public Text bestScoreText;
	public float timeElapsed = 0.0f;
	public float bestTime = 0.0f;

	private bool paused = false;

	void Start() {
		PauseUI.SetActive (false);
		timeElapsed = 0.0f;
	}

	void Update() {
		if (Input.GetButtonDown("Pause")) {
			paused = !paused;
		}
		if (paused) {
			PauseUI.SetActive (true);
			Time.timeScale = 0;
		}
		if (!paused) {
			PauseUI.SetActive (false);
			Time.timeScale = 1;
		}

		scoreText.text = "Time: " + FormatTime (timeElapsed);
		timeElapsed += Time.deltaTime;
	}

	public void ResumeGame() {
		paused = false;
	}

	// Modify the button object's OnClick() properties in the inspector window to set button's destinations
	public void ChangeScene(string sceneName) {
		//SceneManager.LoadScene (sceneName);
	}

	public void QuitGame() {
		Application.Quit ();
	}

	public string FormatTime(float value) {
		int minutes = (int)(Mathf.Floor (value / 60));
		int seconds = (int)(Mathf.Floor (value % 60));
		int milli = (int)(Time.time * 1000);
		milli %= 1000;

		return string.Format ("{0:D2}:{1:D2}:{2:D3}", minutes, seconds, milli);
	}

    public void saveBest() {
        bestScoreText.text = "Best: " + FormatTime(timeElapsed);
    }

    public void resetTimer() {
        timeElapsed = 0.0f;
    }
}   