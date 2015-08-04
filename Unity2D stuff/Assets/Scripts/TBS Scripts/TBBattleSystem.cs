using UnityEngine;
using System.Collections;

public class TBBattleSystem : MonoBehaviour
{
    public static TBBattleSystem battleManager;

    public enum BattleStates {Player, PlayerMove, PlayerAct, Enemy, EnemyMove, EnemyAct, Win, Lose};

    private BattleStates state;

    public Transform[] charStartPos = new Transform[12];//stores starting positions for characters

    public ArrayList enemies = new ArrayList();
    public ArrayList battlers = new ArrayList();
    public CharacterBaseClass curChar;//the character currently acting their turn
    public CharacterBaseClass target;//character currently selected to act on

    public Canvas UI;


    public bool canMove = false;
    private bool hasMoved = false;
    private bool hasActed = false;

    // Use this for initialization
    void Start()
    {

        battleManager = this;

        state = BattleStates.Player;//=============================placeholder code

        //populate turn list
        for (int i = 0; i < 4; i++)
            battlers.Add(GameData.data.Characters[i]);//add player characters

        for (int i = 0; i < 3; i++)
            enemies.Add(new CharacterBaseClass(CharacterBaseClass.Faction.Enemy));//create a list of enemies

        for (int i = 0; i < 3; i++)
            battlers.Add(enemies[i]);//add enemies to battler list

        for (int i = 0; i < battlers.Count; i++)
        {
            GameObject battler;
            battler = (GameObject)Instantiate(Resources.Load("Battler"), charStartPos[i].position, Quaternion.identity);
            battler.GetComponent<RpgCharacterController>().charData = (CharacterBaseClass)battlers[i];
        }

            //go to first character in turn queue
            curChar = (CharacterBaseClass)battlers[0];
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
            case (BattleStates.PlayerMove):
                EndMove();//listen for end of movement phase
                break;
            case (BattleStates.Enemy)://turn on enemy AI to take a turn
                EnemyChoose();
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
                curChar.Wait();//end turn immediately
                break;
            case (1):
                curChar.Attack(target);
                break;
            case (3):
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

    //start movement phase
    public void StartMove()
    {
        if (!hasMoved)//can only move once per turn
        {
            canMove = true;
            UI.enabled = false;//turn off UI until the player finishes moving

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
        else if (state == BattleStates.EnemyMove)
            state = BattleStates.Enemy;
    }

    //end the turn and go to the execution phase
    public void EndTurn()
    {
        if (UI.enabled)//turn off UI if it's not already off
            UI.enabled = false;

        //reset flags for ending turn
        canMove = true;
        hasMoved = false;
        hasActed = false;

        //go to next character in turn queue
        battlers.Add((CharacterBaseClass)battlers[0]);//add current character to end of queue
        battlers.RemoveAt(0);//removes current character from front of queue
        curChar = (CharacterBaseClass)battlers[0];//select new character at front of queue

        //check faction of next character
        if (curChar.fac == CharacterBaseClass.Faction.Player)
            state = BattleStates.Player;
        else
            state = BattleStates.Enemy;
    }

}