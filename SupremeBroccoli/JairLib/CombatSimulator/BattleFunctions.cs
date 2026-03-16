using System.Collections;
using System.Collections.Generic;

//A GOOD START TO BE ABLE TO WORK WITH BUT NEED TO CLEAN UP AND DECOUPLE
public class BattleFunctions{
    //decides the turn order
    public static int TurnDecider(Monster mon1, Monster mon2){
        
        if (mon1.Speed > mon2.Speed){
            //System.Console.WriteLine($"{mon1.Name} will go first");
            Console.WriteLine($"{mon1.Name} will go first");
            return 0;
        }
        else{    
            Console.WriteLine($"{mon2.Name} will go first");
            return 1; 
        }
    }
    public static int TurnDeciderOpp(Monster mon1, Monster mon2){
        
        if (mon1.Speed < mon2.Speed){
            Console.WriteLine($"{mon1.Name} will go second");
            return 0;
        }
        else{    
            Console.WriteLine($"{mon2.Name} will go second");
            return 1; 
        }
    }

    
    //gets the move that the player will act with
    public static int ActionDecision(Monster currTurnMon){
        Console.WriteLine($"{currTurnMon.Name}'s turn");
        Console.WriteLine("what would you like to do?");
        Console.WriteLine($"1. {currTurnMon.MoveOne.Name}\n" +
                                $"2. {currTurnMon.MoveTwo.Name}\n" +
                                $"3. {currTurnMon.MoveThree.Name}\n" +
                                $"4. {currTurnMon.MoveFour.Name}\n");
        return int.Parse(Console.ReadLine());
    }

    //checks if either of the participating monsters have fainted 
    public static bool FaintCheck(Monster currTurnMon, Monster currOppMon){
        if (currTurnMon.Health <= 0){
            return false;
        }
        if (currOppMon.Health <= 0){
            return false;
        }
        return true;
    }

    //sees if the attack hits and calculates the damage
    public static int AttackAction(Monster monster, int actionDecision){
        
        Random random = new Random();
        int rndAcc = random.Next(0,100);

        switch (actionDecision){
            case 1:
                if (rndAcc < monster.MoveOne.Accuracy){
                    int damage = Convert.ToInt32(monster.Attack * monster.MoveOne.Power);
                    if(monster.TypeOne.Equals(monster.MoveOne.Type)){
                        System.Console.WriteLine("STAB Applied!");
                        damage = Convert.ToInt32(damage*1.5);
                    }
                    return damage;
                }
                else{
                    System.Console.WriteLine($"{monster.Name}'s {monster.MoveOne.Name} missed!");
                    return 0;
                }
            case 2:
                if (rndAcc < monster.MoveTwo.Accuracy){
                    int damage = Convert.ToInt32(monster.Attack * monster.MoveTwo.Power);
                    if(monster.TypeOne.Equals(monster.MoveTwo.Type)){
                        System.Console.WriteLine("STAB Applied!");
                        damage = Convert.ToInt32(damage*1.5);
                    }
                    return damage;
                }
                else{
                    System.Console.WriteLine($"{monster.Name}'s {monster.MoveTwo.Name} missed!");
                    return 0;
                }              
            case 3:
                if (rndAcc < monster.MoveThree.Accuracy){
                    int damage = Convert.ToInt32(monster.Attack * monster.MoveThree.Power);
                    if(monster.TypeOne.Equals(monster.MoveThree.Type)){
                        System.Console.WriteLine("STAB Applied!");
                        damage = Convert.ToInt32(damage*1.5);
                    }
                    return damage;
                }
                else{
                    System.Console.WriteLine($"{monster.Name}'s {monster.MoveThree.Name} missed!");
                    return 0;
                }              
            case 4:
                if (rndAcc < monster.MoveFour.Accuracy){
                    int damage = Convert.ToInt32(monster.Attack * monster.MoveFour.Power);
                    if(monster.TypeOne.Equals(monster.MoveFour.Type)){
                        System.Console.WriteLine("STAB Applied!");
                        damage = Convert.ToInt32(damage*1.5);
                    }
                    return damage;
                }
                else{
                    System.Console.WriteLine($"{monster.Name}'s {monster.MoveFour.Name} missed!");
                    return 0;
                }               
            default:
                if (rndAcc < monster.MoveFour.Accuracy)
                    return Convert.ToInt32(monster.Attack * monster.MoveFour.Power);
                else{
                    System.Console.WriteLine($"{monster.Name}'s {monster.MoveFour.Name} missed!");
                    return 0;
                }                
        }
    }
}