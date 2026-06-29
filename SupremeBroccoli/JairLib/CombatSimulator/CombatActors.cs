using JairLib.QuestCore;
using JairLib.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JairLib.CombatSimulator
{
    public class CombatActors : AnyObject
    {
        private int health;
        private string name;
        private int speed;
        private int attack;
        private int specialAttack;
        private int defense;
        private int specialDefense;
        private int luck;
        private int accuracy;
        private int evasiveness;

        public CombatActors()
        {
            this.health = 0;
            this.name =  default(string);
        }
        //properties
        public int Health
        {
            get { return health; }
            set { this.health = value; }
        }
        public int MaximumHealth
        {
            get { return health; }
            set { this.health = value; }
        }
        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }
        public int Speed
        {
            get { return speed; }
            set { this.speed = value; }
        }
        public int Attack
        {
            get { return attack; }
            set { this.attack = value; }
        }
        public IndividualMove[] Moveset
        {
            get; set;
        }
        public Element[] TypeAffinities
        {
            get; set;
        }
        public Element[] TypeWeaknesses
        {
            get; set;
        }
        public Element[] TypeResistances
        {
            get;
            set;
        }
        public int SpecialAttack
        {
            get { return specialAttack; }
            set { specialAttack = value; }
        }
        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }
        public int SpecialDefense
        {
            get { return  specialDefense; }
            set { specialDefense = value; }
        }
        public int Luck
        {
            get { return luck; }
            set { luck = value; }
        }
        public int Accuracy
        {
            get { return accuracy; }
            set { accuracy = value; }
        }
        public int Evasiveness
        {
            get { return evasiveness; }
            set { evasiveness = value; }
        }
    }
}
