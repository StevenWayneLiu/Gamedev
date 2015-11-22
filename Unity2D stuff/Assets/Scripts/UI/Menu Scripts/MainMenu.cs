using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour {
    Canvas menuCanvas;
    void Start()
    {
        menuCanvas = GetComponent<Canvas>();
    }
	// Send you to game scene
	public void LoadLevel()
    {
        Application.LoadLevel(1);
    }
    public void QuitGame()
    {
        #if UNITYEDITOR
                EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
