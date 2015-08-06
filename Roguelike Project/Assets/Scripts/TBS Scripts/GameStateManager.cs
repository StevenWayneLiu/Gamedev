using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager stateManager;
    //states
    public enum PlayStates {PlayerTurn, PlayerMove, PlayerAct, EnemyTurn, EnemyMove, EnemyAct, Lose, Peace};
    private PlayStates state;

    //variables
    public float timePoints = 100f;
    public float bankedPoints = 0f;
    public Transform[] charStartPos = new Transform[12];//stores starting positions for characters

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

        state = PlayStates.PlayerTurn;//=============================placeholder code

        //populate turn list
        for (int i = 0; i < GameData.data.Characters.Count; i++)
            turnList.Add(GameData.data.Characters[i]);//add player characters

        for (int i = 0; i < 3; i++)
            enemies.Add(new CharacterBaseClass(CharacterBaseClass.Faction.Enemy));//create a list of enemies

        for (int i = 0; i < 3; i++)
            turnList.Add(enemies[i]);//add enemies to battler list

        for (int i = 0; i < turnList.Count; i++)
        {
            battlers.Add((GameObject)Instantiate(Resources.Load("Battler"), charStartPos[i].position, Quaternion.identity));
            ((GameObject)battlers[i]).GetComponent<BattlerController>().charData = (CharacterBaseClass)turnList[i];
        }

            //go to first character in turn queue
            curChar = (CharacterBaseClass)turnList[0];
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case (PlayStates.PlayerTurn)://waits for player to take an action
                if (curChar.TimePoints == 0)//check to see if turn should end
                    EndTurn();
                else if(!UI.enabled)//else turn UI back on
                    UI.enabled = true;
                break;
            case(PlayStates.PlayerAct):
                EndAct();
                break;
            case (PlayStates.PlayerMove):
                EndMove();//listen for end of movement phase
                break;
            case (PlayStates.EnemyTurn)://turn on enemy AI to take a turn
                EnemyChoose();
                break;
            case (PlayStates.EnemyAct):
                EndAct();
                break;
            case (PlayStates.EnemyMove):
                EndMove();
                break;
            case (PlayStates.Lose):
                break;
            default:
                break;
        }
    }

    //Called by input for player to take action
    public void PlayerChoose(int actionIndex)
    {
        switch (actionIndex)
        {
            case (0):
                Wait();//end turn immediately
                break;
            case (1):
                StartAct();
                break;
            case (2):
                StartMove();
                break;
            default:
                break;
        }
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
        state = PlayStates.PlayerTurn;
    }

    //start action phase
    public void StartAct()
    {
        UI.enabled = false;
        Debug.Log("Choose an attack target");
        //change state to movement state
        if (state == PlayStates.PlayerTurn)
            state = PlayStates.PlayerAct;
        else if (state == PlayStates.EnemyTurn)
            state = PlayStates.EnemyAct;  
    }

    //end acting phase
    public void EndAct()
    {
        if (state == PlayStates.PlayerAct && Input.GetButton("Submit"))
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
                    if(target.fac == CharacterBaseClass.Faction.Player)
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
                state = PlayStates.PlayerTurn;
            }
        }
        else if (state == PlayStates.PlayerAct && Input.GetButton("Cancel"))
        {
            UI.enabled = true;
            state = PlayStates.PlayerTurn;
        }
        else if (state == PlayStates.EnemyMove)
        {
            
            state = PlayStates.EnemyTurn;
        }
    }

    //start movement phase
    public void StartMove()
    {
        UI.enabled = false;//turn off UI until the player finishes moving

        //set curPos to position of character
        curPos = ((GameObject)battlers[0]).transform;

        //change state to movement state
        if (state == PlayStates.PlayerTurn)
            state = PlayStates.PlayerMove;
        else if (state == PlayStates.EnemyTurn)
            state = PlayStates.EnemyMove;
        
    }

    //end movement phase
    public void EndMove()
    {
        if (state == PlayStates.PlayerMove && Input.GetButton("Submit"))
        {
            
            state = PlayStates.PlayerTurn;
        }
        else if (state == PlayStates.PlayerMove && Input.GetButton("Cancel"))
        {
            
            ((GameObject)battlers[0]).transform.position = curPos.position;//reset of character if cancel movement
            state = PlayStates.PlayerTurn;
        }
        else if (state == PlayStates.EnemyMove)
        {
            
            state = PlayStates.EnemyTurn;
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
            state = PlayStates.PlayerTurn;
        else
            state = PlayStates.EnemyTurn;
        
    }

    //turn on both flags to end turn
    public void Wait()
    {
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
        state = PlayStates.Peace;
    }
    public void Lose()
    {
        Application.LoadLevel(3);//go to death screen
    }
}