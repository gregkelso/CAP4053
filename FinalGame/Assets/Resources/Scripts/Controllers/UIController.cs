using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	public GameObject PauseUI;

	private bool paused = false;

	void Start() {
		PauseUI.SetActive (false);
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
}   