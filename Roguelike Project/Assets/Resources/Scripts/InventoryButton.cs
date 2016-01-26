using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryButton : MonoBehaviour {

    Text text;
    Button button;


    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        button = GetComponent<Button>();
    }

    public Text Text
    {
        get { return text; }
        set { text = value; }
    }

}
