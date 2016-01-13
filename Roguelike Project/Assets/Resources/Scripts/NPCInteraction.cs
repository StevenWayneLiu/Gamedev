using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class NPCInteraction : MonoBehaviour {

    GameObject buttonTemplate;
    public Transform contentPanel;
    public Inventory charInv;

	// Use this for initialization
	void Start () {
        charInv = gameObject.GetComponent<CharacterManager>().Items;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //npc behavior when player tries to interact with them
    public void Interact()
    {
        Debug.Log("NPC interact");
        //spawn/activate item list UI
        contentPanel.gameObject.SetActive(true);
        //populate item list UI
        for (int i = 0; i < charInv.Count; i++)
        {
            GameObject newButton = Instantiate(buttonTemplate) as GameObject;
            InventoryButton newItem = newButton.GetComponent<InventoryButton>();
            newItem.text.text = charInv[i].Name;

            //hook up button to content panel
            newButton.transform.SetParent(contentPanel);
        }
    }
}
