using System.Collections;
using System.Collections.Generic;

namespace JairLib.CombatSimulator;

public class AssembleRascal
{
    private string _name;
    private int _speed;
    private int _attack;
    private string _moveTwo;
    private string _typeOne;
    private int _specialAttack;
    private int _defense;
    // Start is called before the first frame update
    void Start()
    { }
    private Monster mon1 = new()
    {
        //Name = _name,
        //Speed = _speed,
        //Attack = _attack,
        //MoveTwo = IndividualMove.GetMove(_moveTwo),
        //TypeOne = _typeOne,
        //SpecialAttack = _specialAttack,
        //Defense = _defense
    };
}
