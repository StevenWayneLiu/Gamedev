using UnityEngine;
using System.Collections;

public class TBBattleSystem : MonoBehaviour
{
    public static TBBattleSystem battleManager;

    public enum BattleStates {Choose, Action, Win, Lose };
    public enum Turn {Player, Enemy};

    private BattleStates state;
    private Turn turn;
    public ArrayList battlers = new ArrayList();
    public CharacterBaseClass selected;//the character currently selected by the player

    public Canvas UI;

    // Use this for initialization
    void Start()
    {
        battleManager = this;
        state = BattleStates.Choose;
        turn = Turn.Player;
        //populate turn list
        for (int i = 0; i < 4; i++)
            battlers.Add(GameData.data.Characters[i]);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        switch (state)
        {
            case (BattleStates.Choose):
                //take input from player or AI. If input recieved, run Act()
                if(turn == Turn.Player)//if it's the player's turn
                {
                    
                }
                state = BattleStates.Action;
                break;
            case (BattleStates.Action):
                Action();
                break;
            case (BattleStates.Win):
                break;
            case (BattleStates.Lose):
                break;
            default:
                break;
        }
    }

    //play out phase
    private void Action()
    {
        if (turn == Turn.Player)//swap whether it's enemy or ally turn
            turn = Turn.Enemy;
        else
            turn = Turn.Player;

        //check if winning or losing conditions are met


        //add another turn to the turn queue

        //transition back to waiting for input
        state = BattleStates.Choose;
    }

    //accept player or AI input
    private void Choose()
    {
        bool isFinished = false;
        if (turn == Turn.Player)//player turn
        {
            UI.enabled = true;//turn on player UI



        }
        else//enemy turn
        {
            //enemy AI makes decision here
        }

        if (isFinished)
        {
            if (UI.enabled)//turn off UI if it's not already off
                UI.enabled = false;
            state = BattleStates.Action;//advance to action stage
        }
    }
}