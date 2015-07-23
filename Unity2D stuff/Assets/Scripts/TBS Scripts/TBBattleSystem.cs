using UnityEngine;
using System.Collections;

public class TBBattleSystem : MonoBehaviour
{
    public static TBBattleSystem battleManager;

    public enum BattleStates {Player, Enemy, Win, Lose };

    private BattleStates state;
    public ArrayList enemies = new ArrayList();
    public ArrayList battlers = new ArrayList();
    public CharacterBaseClass curChar;//the character currently acting their turn
    public CharacterBaseClass target;//character currently selected to act on

    public Canvas UI;

    // Use this for initialization
    void Start()
    {
        battleManager = this;
        state = BattleStates.Player;//placeholder code
        //populate turn list
        for (int i = 0; i < 4; i++)
            battlers.Add(GameData.data.Characters[i]);//add player characters

        for (int i = 0; i < 3; i++)
            enemies.Add(new CharacterBaseClass(CharacterBaseClass.Faction.Enemy));//create a list of enemies

        for (int i = 0; i < 3; i++)
            battlers.Add(enemies[i]);//add enemies to battler list

        //go to first character in turn queue
        curChar = (CharacterBaseClass)battlers[0];
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case (BattleStates.Player)://waits for player to take an action
                UI.enabled = true;
                break;
            case (BattleStates.Enemy)://turn on enemy AI to take a turn
                EnemyChoose();
                break;
            case (BattleStates.Win):
                break;
            case (BattleStates.Lose):
                break;
            default:
                break;
        }
    }

    //Wait for player or AI to make decision
    public void PlayerChoose(int actionIndex)
    {

        curChar.Act(actionIndex, target);
        Debug.Log(target.HealthFract);
    }

    private void EnemyChoose()
    {
        //AI code here
        Debug.Log("Enemy waits");
        EndTurn();
    }

    //end the turn and go to the execution phase
    public void EndTurn()
    {
        if (UI.enabled)//turn off UI if it's not already off
            UI.enabled = false;

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