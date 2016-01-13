using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryButton : MonoBehaviour {

    public Text text;
    public Button button;


    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        button = GetComponent<Button>();
    }
}
