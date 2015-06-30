using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour {
    Canvas pauseCanvas;
	// Use this for initialization
	void Start () {
        pauseCanvas = GetComponent<Canvas>();
        pauseCanvas.enabled = false;
	}
	public void Pause()
    {
        //pauses when someone uses the menu/cancel button
        Time.timeScale = (Time.timeScale == 0) ? 1 : 0;//if timescale is zero, then set to one, else set to zero
        pauseCanvas.enabled = !pauseCanvas.enabled;//If not paused, pause game. If already paused, unpause.
    }
    //sends you back to start screen
    public void Quit()
    {
        Application.LoadLevel(0);
    }
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
	}
}
