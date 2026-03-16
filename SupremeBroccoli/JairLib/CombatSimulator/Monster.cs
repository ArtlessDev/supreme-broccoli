public class Monster{
    int speed, attack, health, specialAttack, defense;
    string name, typeOne;
    IndividualMove moveOne, moveTwo, moveThree, moveFour;

    public Monster(){
        this.Name = "Eevee";
        this.Health = 20;
        this.Speed = 10;
        this.Attack = 15;
        this.moveOne = IndividualMove.GetMove("Cut");
        this.moveTwo = IndividualMove.GetMove("");
        this.moveThree = IndividualMove.GetMove("");
        this.moveFour = IndividualMove.GetMove("");
        this.TypeOne = "Normal";
        this.defense = 12;
        this.specialAttack = 10;
    }

    //properties
    public int Health {
        get { return health; }
        set { this.health = value; }
    }
    public string Name {
        get { return name; }
        set { this.name = value; }
    }
    public int Speed {
        get { return speed; }
        set { this.speed = value; }
    }
    public int Attack {
        get { return attack; }
        set { this.attack = value; }
    }
    public IndividualMove MoveOne{
        get { return moveOne; }
        set { moveOne = value; }
    }
    public IndividualMove MoveTwo{
        get { return moveTwo; }
        set { moveTwo = value; }
    }
    public IndividualMove MoveThree{
        get { return moveThree; }
        set { moveThree = value; }
    }
    public IndividualMove MoveFour{
        get { return moveFour; }
        set { moveFour = value; }
    }
    public string TypeOne {
        get { return typeOne; }
        set { typeOne = value; }
    }
    public int SpecialAttack{
        get { return specialAttack; }
        set { specialAttack = value; }
    }
    public int Defense {
        get { return defense; }
        set { defense = value; }
    }
}