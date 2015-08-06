using UnityEngine;
using System.Collections;

public class TBBattleSystem : MonoBehaviour
{
    public static TBBattleSystem battleManager;

    public enum BattleStates {Player, PlayerMove, PlayerAct, Enemy, EnemyMove, EnemyAct, Win, Lose};

    private BattleStates state;

    public Transform[] charStartPos = new Transform[12];//stores starting positions for characters

    public ArrayList enemies = new ArrayList();
    public ArrayList turnList = new ArrayList();
    public ArrayList battlers = new ArrayList();//list containing characters on field
    public CharacterBaseClass curChar;//the character currently acting their turn
    public CharacterBaseClass target;//character currently selected to act on
    private Transform curPos;//current position of character on map

    public Canvas UI;

    public GameObject targetObject;//the gameobject on the battlefield linked to the target

    public bool canMove = false;
    private bool hasMoved = false;
    private bool hasActed = false;

    // Use this for initialization
    void Start()
    {

        battleManager = this;

        state = BattleStates.Player;//=============================placeholder code

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
            case (BattleStates.Player)://waits for player to take an action
                if (hasMoved && hasActed)//check to see if turn should end
                    EndTurn();
                else if(!UI.enabled)//else turn UI back on
                    UI.enabled = true;
                break;
            case(BattleStates.PlayerAct):
                EndAct();
                break;
            case (BattleStates.PlayerMove):
                EndMove();//listen for end of movement phase
                break;
            case (BattleStates.Enemy)://turn on enemy AI to take a turn
                EnemyChoose();
                break;
            case (BattleStates.EnemyAct):
                EndAct();
                break;
            case (BattleStates.EnemyMove):
                EndMove();
                break;
            case (BattleStates.Win):
                break;
            case (BattleStates.Lose):
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

        //after taking an action, if both hasMoved and hasActed are true, end turn
        if (hasMoved && hasActed)
            EndTurn();
    }

    //AI logic
    private void EnemyChoose()
    {
        //AI code here
        Debug.Log("Enemy waits");
        EndTurn();
    }

    //start action phase
    public void StartAct()
    {
        if (!hasActed)
        {
            UI.enabled = false;
            Debug.Log("Choose an attack target");
            //change state to movement state
            if (state == BattleStates.Player)
                state = BattleStates.PlayerAct;
            else if (state == BattleStates.Enemy)
                state = BattleStates.EnemyAct;
        }
        
    }

    //end acting phase
    public void EndAct()
    {
        if (state == BattleStates.PlayerAct && Input.GetButton("Submit"))
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
                hasActed = true;
                state = BattleStates.Player;
            }
        }
        else if (state == BattleStates.PlayerAct && Input.GetButton("Cancel"))
        {
            UI.enabled = true;
            hasActed = false;
            state = BattleStates.Player;
        }
        else if (state == BattleStates.EnemyMove)
        {
            hasActed = true;
            state = BattleStates.Enemy;
        }
    }

    //start movement phase
    public void StartMove()
    {
        if (!hasMoved)//can only move once per turn
        {
            canMove = true;
            UI.enabled = false;//turn off UI until the player finishes moving

            //set curPos to position of character
            curPos = ((GameObject)battlers[0]).transform;

            //change state to movement state
            if (state == BattleStates.Player)
                state = BattleStates.PlayerMove;
            else if (state == BattleStates.Enemy)
                state = BattleStates.EnemyMove;
        }
    }

    //end movement phase
    public void EndMove()
    {
        if (state == BattleStates.PlayerMove && Input.GetButton("Submit"))
        {
            canMove = false;
            hasMoved = true;
            state = BattleStates.Player;
        }
        else if (state == BattleStates.PlayerMove && Input.GetButton("Cancel"))
        {
            canMove = false;
            hasMoved = false;
            ((GameObject)battlers[0]).transform.position = curPos.position;//reset of character if cancel movement
            state = BattleStates.Player;
        }
        else if (state == BattleStates.EnemyMove)
        {
            canMove = false;
            hasMoved = true;
            state = BattleStates.Enemy;
        }
            
    }

    //end the turn and go to the execution phase
    public void EndTurn()
    {
        if (UI.enabled)//turn off UI if it's not already off
            UI.enabled = false;

        //reset flags for ending turn
        canMove = false;
        hasMoved = false;
        hasActed = false;

        EndBattle();
        
        //go to next character in turn queue
        turnList.Add((CharacterBaseClass)turnList[0]);//add current character to end of queue
        turnList.RemoveAt(0);//removes current character from front of queue
        curChar = (CharacterBaseClass)turnList[0];//select new character at front of queue

        //check faction of next character
        if (curChar.fac == CharacterBaseClass.Faction.Player)
            state = BattleStates.Player;
        else
            state = BattleStates.Enemy;
        
    }

    //turn on both flags to end turn
    public void Wait()
    {
        hasMoved = true;
        hasActed = true;
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
        Application.LoadLevel(1);//go back to map screen
    }
    public void Lose()
    {
        Application.LoadLevel(3);//go to death screen
    }
}