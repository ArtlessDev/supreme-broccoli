using JairLib.QuestCore;
using System.Collections;
using System.Collections.Generic;


public class GameManager 
{
    /*[SerializeField] private Monster mon1 = new()
    {
        Name = "Pikachu",
        Speed = 12,
        Attack = 10,
        MoveTwo = IndividualMove.GetMove("Thundershock"),
        TypeOne = "Electric",
        SpecialAttack = 20,
        Defense = 10
    };*/
    private Monster mon2 = new Monster();
    private Monster mon1;
    private WorldObject txt;

    // Start is called before the first frame update
    void Start()
    {
        BattleFunctions.TurnDecider(mon1, mon2);
        txt.CurrentMessage = mon2.Name;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(mon2.Name);
    }
}
