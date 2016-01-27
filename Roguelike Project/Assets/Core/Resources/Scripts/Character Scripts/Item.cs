using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour, IInteractable {

    public int itemID;//global item list id
    public ItemData data;
    public Collider2D col;

	// Use this for initialization
	void Start () {
        data = new ItemData(GameData.data.AllItems[itemID]);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        //add item to player's inventory
        Debug.Log("touched");
        if (other.tag == "Player")
        {
            other.GetComponent<Character>().Items.Add(data);
            Destroy(gameObject);
        }
    }
    //pick up/activate item
    public void Interact(IInteractable other)
    {
        throw new System.NotImplementedException();
    }
}
