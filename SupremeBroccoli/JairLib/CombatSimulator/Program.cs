//Monster mon1 = new()
//{
//    Name = "Pikachu",
//    Speed = 12,
//    Attack = 10,
//    MoveTwo = IndividualMove.GetMove("Thundershock"),
//    TypeOne = "Electric",
//    SpecialAttack = 20,
//    Defense = 10
//};
////eevee
//Monster mon2 = new();

//Monster[] turnOrder = {mon1, mon2};

//int turnTracker = BattleFunctions.TurnDecider(mon1, mon2);
//int currOpp = BattleFunctions.TurnDeciderOpp(mon1, mon2);


//while (BattleFunctions.FaintCheck(mon1, mon2)){
//    //checks for which move
//    int turnAction = BattleFunctions.ActionDecision(turnOrder[turnTracker]);
    
//    //damage calculation
//    int attack = BattleFunctions.AttackAction(turnOrder[turnTracker], turnAction);
//    /*
//    TODO:   just added defense and special attack stats, need to give to pikachu 
//            and then need to account for these factors into the damage calculations
//    */
//    turnOrder[currOpp].Health -= attack ;

//    Console.WriteLine($"{turnOrder[currOpp].Name} took {attack} damage! remaining health: {turnOrder[currOpp].Health}\n");

//    //ie statement checks and reassigns whose going and who is the opponent
//    if (turnTracker == 0){
//        turnTracker = 1;
//        currOpp = 0;
//    }
//    else{
//        turnTracker = 0;
//        currOpp = 1;
//    }
//}

//if(mon1.Health>mon2.Health){
//    System.Console.WriteLine($"{mon1.Name} wins!");
//}
//else{
//    System.Console.WriteLine($"{mon2.Name} wins!");
//}