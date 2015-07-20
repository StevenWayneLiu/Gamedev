using UnityEngine;
using System.Collections;

public class TBBattleSystem : MonoBehaviour
{
    public static TBBattleSystem battleManager;

    public enum BattleStates {Choose, Win, Lose };

    private BattleStates turn;
    public ArrayList battlers = new ArrayList();
    public CharacterBaseClass selected;//the character currently selected by the player

    // Use this for initialization
    void Start()
    {
        battleManager = this;
        turn = BattleStates.Choose;
        //populate turn list
        for (int i = 0; i < 4; i++)
            battlers.Add(GameData.data.Characters[i]);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(turn);
        switch (turn)
        {
            case (BattleStates.Choose):
                //take input from player or AI. If input recieved, run Act()
                break;
            case (BattleStates.Win):
                break;
            case (BattleStates.Lose):
                break;
            default:
                break;
        }
    }

    //take an action as a character, advancing the turn order
    private void Act()
    {
        
    }

}