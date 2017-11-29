using UnityEngine;
using System.Collections;


public class LevelManager : MonoBehaviour {


	public void LoadMenu(string name)
    {
		UnityEngine.SceneManagement.SceneManager.LoadScene(name) ;
		UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

	public void HowToPlay(string name)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("HowToPlay");
	}

	public void ExitBacktoMenu (string name)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("TitleScreen");
	}

	public void QuitGame()
	{
		Application.Quit ();
	}
}
