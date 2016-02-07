using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathMenu : MonoBehaviour {
    Canvas deathCanvas;
    void Start()
    {
        deathCanvas = GetComponent<Canvas>();
        deathCanvas.enabled = false;
    }
    //give up and go to start screen
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
	public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//loads the currently loaded level again
    }
}
