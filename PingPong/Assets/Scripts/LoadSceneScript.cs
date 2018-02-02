using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour {
	public void LoadScene (string name) {
		if (name == "quit") {
			Application.Quit ();
		} else {
			SceneManager.LoadScene (name);
		}
	}
}