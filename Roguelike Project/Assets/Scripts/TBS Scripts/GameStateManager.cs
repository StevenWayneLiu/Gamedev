﻿using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager stateManager;
    //states
    public enum GameStates {PlayerTurn, PlayerAct, EnemyTurn, EnemyAct, Lose, Peace};
    public GameStates state;

    //variables
    public float timePoints = 100f;
    public float bankedPoints = 0f;
    public Transform[] charStartPos = new Transform[12];//stores starting positions for characters
    public int actionIndex = 0;//the index of the action currently selected

    public ArrayList aggro = new ArrayList();//holds all the enemies that the player is currently aggroing
    public ArrayList enemies = new ArrayList();
    public ArrayList turnList = new ArrayList();
    public ArrayList battlers = new ArrayList();//list containing characters on field
    public CharacterBaseClass curChar;//the character currently acting their turn
    public CharacterBaseClass target;//character currently selected to act on
    private Transform curPos;//current position of character on map

    public Canvas UI;

    public GameObject targetObject;//the gameobject on the battlefield linked to the target

    // Use this for initialization
    void Start()
    {

        stateManager = this;

        //start in peace
        state = GameStates.Peace;

        //populate turn list
        for (int i = 0; i < GameData.data.Characters.Count; i++)
            turnList.Add(GameData.data.Characters[i]);//add player characters

        for (int i = 0; i < 3; i++)
            enemies.Add(new CharacterBaseClass(CharacterBaseClass.Faction.Enemy));//create a list of enemies

        for (int i = 0; i < 3; i++)
            turnList.Add(enemies[i]);//add enemies to battler list

        //create battlers
        for (int i = 0; i < turnList.Count; i++)
        {
            battlers.Add((GameObject)Instantiate(Resources.Load("Enemy"), new Vector3(i,1,i), Quaternion.identity));
            ((GameObject)battlers[i]).GetComponent<BattlerController>().charData = (CharacterBaseClass)turnList[i];
            ((CharacterBaseClass)turnList[i]).Battler = (GameObject)battlers[i];
        }


            //go to first character in turn queue
            curChar = (CharacterBaseClass)turnList[0];
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)//state machine
        {
            case (GameStates.PlayerTurn)://waits for player to take an action
                if (curChar.TimePoints == 0)//check to see if turn should end
                    EndTurn();
                else if(!UI.enabled)//else turn UI back on
                    UI.enabled = true;
                break;
            case(GameStates.PlayerAct):
                AimAction();
                break;
            case (GameStates.EnemyTurn)://turn on enemy AI to take a turn
                EnemyChoose();
                break;
            case (GameStates.EnemyAct):
                break;
            case (GameStates.Lose):
                break;
            default:
                break;
        }
    }

    //Called by input from player via UI
    public void ChooseAction(int actIndex)
    {
        //Set up choosing UI for respective actions
        switch (actIndex)
        {
            default:
                break;
        }


        state = GameStates.PlayerAct;//transition states to confirmation stage
    }

    //Listens for player input to confirm current action
    public void AimAction()
    {
        UI.enabled = false;
        if (Input.GetButtonDown("Fire1"))//left mouse click to confirm action
        {
            switch (actionIndex)
            {
                case(0)://wait is selected
                    Wait();
                    break;
                case(1)://move is selected
                    Move(curChar.Battler);
                    break;
                case(2)://attack is selected
                    Attack(target);
                    break;
                default:
                    break;
            }
        }
        
    }

    public void Move(GameObject character)
    {
        //move character to location
        (character.GetComponent<NavMeshController>()).Move();
    }

    //AI logic
    private void EnemyChoose()
    {
        //AI code here
        Debug.Log("Enemy waits");
        EndTurn();
    }

    //start a fight
    public void StartBattle()
    {
        state = GameStates.PlayerTurn;

        for (int i = 0; i < turnList.Count; i++)//go through list and kill momentum
        {
            NavMeshAgent agent = ((CharacterBaseClass)turnList[i]).Battler.GetComponent<NavMeshAgent>();
            //set character's destination to their current location
            agent.SetDestination(((CharacterBaseClass)turnList[i]).Battler.transform.position);

            agent.velocity = Vector3.zero;
        }

        
    }



    //execute attack on target
    public void Attack(CharacterBaseClass target)
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (target == null)
            {
                Debug.Log("Select a target first!");
            }
            else
            {
                curChar.Attack(target);
                if (target.CurHealth <= 0)//if target is dead
                {
                    turnList.Remove(target);
                    battlers.Remove(targetObject);
                    Destroy(targetObject);
                    if (target.fac == CharacterBaseClass.Faction.Player)
                    {
                        GameData.data.Characters.Remove(target);
                    }
                    else if (target.fac == CharacterBaseClass.Faction.Enemy)
                    {
                        enemies.Remove(target);
                    }
                    target = null;
                }

                EndBattle();

                UI.enabled = true;
                state = GameStates.PlayerTurn;
            }
        }
    }

    //end the turn and go to the execution phase
    public void EndTurn()
    {
        if (UI.enabled)//turn off UI if it's not already off
            UI.enabled = false;

        //reset character's TP and add banked TP from waiting
        curChar.BankedTP = curChar.TimePoints;//any remaining TP is banked
        curChar.TimePoints = 100f;

        EndBattle();//check if battle should end
        
        //go to next character in turn queue
        turnList.Add((CharacterBaseClass)turnList[0]);//add current character to end of queue
        turnList.RemoveAt(0);//removes current character from front of queue
        curChar = (CharacterBaseClass)turnList[0];//select new character at front of queue

        //check faction of next character
        if (curChar.fac == CharacterBaseClass.Faction.Player)
            state = GameStates.PlayerTurn;
        else
            state = GameStates.EnemyTurn;
        
    }

    //turn on both flags to end turn
    public void Wait()
    {
        //save up TP
        EndTurn();
    }
    public void EndBattle()
    {
        //check if the player has won or lost
        if (enemies.Count == 0)//win condition
            Win();
        else if (GameData.data.Characters.Count == 0)//lose condition
            Lose();
    }

    public void Win()
    {
       //change back to walking mode
        state = GameStates.Peace;
    }
    public void Lose()
    {
        Application.LoadLevel(3);//go to death screen
    }
}