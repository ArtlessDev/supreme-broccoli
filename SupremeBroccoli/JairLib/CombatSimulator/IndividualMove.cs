public class IndividualMove{
    string name, type, spec_phys;
    double power;
    int accuracy;

    public IndividualMove(){
        this.name = "Cut";
        this.power = .50;
        this.accuracy = 95;
        this.type = "Normal";
        this.spec_phys = "physical";
    }
    public IndividualMove(string _name, double _power, int _accuracy, string _type, string _spec_phys){
        this.name = _name;
        this.power = _power;
        this.accuracy = _accuracy;
        this.type = _type;
        this.spec_phys = _spec_phys;
    }
    //properties
    public string Name{
        get{ return this.name; }
        set{ this.name = value; }
    }
    public double Power{
        get{ return this.power; }
        set{ this.power = value; }
    }
    public int Accuracy{
        get{ return this.accuracy; }
        set{ this.accuracy = value; }
    }
    public string Type{
        get{ return this.type; }
        set{ this.type = value; }
    }
    public string SpecPhys{
        get{ return this.spec_phys;}
        set{ this.spec_phys = value;}
    }


    //when called, this will provide the 'requesting monster' with the move that they ask for via string 
    public static IndividualMove GetMove(string moveName){
        IndividualMove move;
        switch (moveName){
            case "Thundershock":
                move = new IndividualMove(moveName, .40, 100, "Electric", "special");
                break;
            case "Cut":
                move = new IndividualMove(moveName, .50, 85, "Normal", "physical");
                break;
            case "Ember":
                move = new IndividualMove(moveName, .40, 100, "Fire", "special");
                break;
            case "Bubble":
                move = new IndividualMove(moveName, .20, 100, "Water", "special");
                break;
            case "Razor Leaf":
                move = new IndividualMove(moveName, .55, 95, "Grass", "physical");
                break;
            default:
                move = new IndividualMove("", 0, 0, "", "");
                break;
        }

        return move;
    }
}