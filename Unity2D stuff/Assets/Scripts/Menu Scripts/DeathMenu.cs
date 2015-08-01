using UnityEngine;
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
        Application.LoadLevel(0);
    }
	public void Continue()
    {
        Application.LoadLevel(1);//loads the currently loaded level again
    }
}
