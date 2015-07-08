using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TBBattleSystem : MonoBehaviour {

    private List<GameObject> PlayerList;//stores player characters
    private List<GameObject> EnemyList;//stores enemy
    private List<GameObject> TurnOrder = new List<GameObject>();//stores the turn order of list
    private int OrderSize;//number of turns ahead that are shown
    private bool NextTurn = false;//true if player has taken their turn

	// Use this for initialization
	void Start () {
	    //add all entities to turn order
        //go to first character's turn
	}
	
	// Update is called once per frame
	void Update () {
        //check to see if character has taken their turn
        if (NextTurn)//If character has taken their turn,
        {
            //check to see if battle is over
            if (BattleWon() || BattleLost())
            {
                //if so, end battle
                BattleEnd();
            }
            else//else continue with battle
            {
                //recalculate turn order based on speed
                //check turn list and go to next character's turn
            }
            
        }
        
	}

    //ends battle, should only be called if either BattleWon() or BattleLost() is true
    void BattleEnd()
    {
        if(BattleWon())
        {
            //award end-of-battle stuff to player
        }
        else if(BattleLost())
        {
            //game over stuff
        }
    }
    //returns ture if all the enemies are defeated
    bool BattleWon()
    {
        if (EnemyList.Count == 0)//
            return true;//if all enemies are dead battle is won
        else
            return false;//else return false
    }
    //returns true if all the player's characters are dead
    bool BattleLost()
    {
        bool allDead = true;
        for(int i = 0; i < PlayerList.Count; i++)
        {
            //if character's hp is greater than zero
            //allDead becomes false
        }
        return allDead;
    }
}
