using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
    private string lvlName = "";
    void LoadScene(int scene)
    {
        Application.LoadLevel(scene);
    }
    void QuitGame()
    {
        Application.Quit();
    }
}
