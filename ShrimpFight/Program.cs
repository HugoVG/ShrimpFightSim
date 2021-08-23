using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;

namespace ShrimpFight
{
    class Program
    {
        static void Main(string[] args)
        {
            Shrimp Rictor = new Shrimp("Rictor", 5, 5, 5, 5);
            Shrimp Samantha = new Shrimp("Samantha", 1, 6, 3, 10);
            Shrimp Donny = new Shrimp("Donny", 6, 10, 2, 2);
            Shrimp Brunt = new Shrimp("Brunt", 10, 4, 5, 1);
            Shrimp Zippy = new Shrimp("Zippy", 6, 1, 8, 5);
            Shrimp ZIP = new Shrimp("Zip", 6, 6, 6, 2);
            Shrimp Rex = new Shrimp("Rex", 1, 1, 1, 17);
            Shrimp Crimson = new Shrimp("Crimson", 8, 2, 8, 2);
            Shrimp Randal = new Shrimp("Randal", 9, 9, 1, 1);
            Shrimp Bumpsy = new Shrimp("Bumpsy", 0.5f, 9.5f, 9.5f, 0.5f);
            Shrimp Johnny = new Shrimp("Johnny", 10, 0, 10, 0);
            Shrimp Bispy = new Shrimp("Bispy", 7, 2, 3, 8);
            Shrimp Jeepers = new Shrimp("Bispy", 0.5f, 0, 5, 20);

            Shrimp[] shrimplist =
                {Rictor, Samantha, Donny, Brunt, Zippy, ZIP, Rex, Crimson, Randal, Bumpsy, Johnny, Bispy, Jeepers};
            
            for (int i = 0; i < shrimplist.Length; i++)
            {
                for (int j = 0; j < shrimplist.Length; j++)
                {
                    foreach (var VARIABLE in shrimplist)
                    {
                        VARIABLE.Heal();
                    }

                    if (BattleSim(shrimplist[i], shrimplist[j]))
                    {
                        shrimplist[i].Win(shrimplist[j]);
                        Console.WriteLine(shrimplist[i].name);
                    }
                    else
                    {
                        shrimplist[i].Lose(shrimplist[j]);
                        Console.WriteLine(shrimplist[j].name);
                    }
                    


                }
            }

            foreach (var VARIABLE in shrimplist)
            {
                Console.WriteLine($"{VARIABLE.name} \t Lost {VARIABLE.GetLost()} \t Won {VARIABLE.GetWins()}");
            }
        }
            
        //returns TRUE if Shrimp1 Wins, FALSE if Shrimp2 Wins
        public static bool BattleSim(Shrimp S1, Shrimp S2)
        {
            if (S1.speedVAL > S2.speedVAL)
            {
                while (S1.Health != 0 || S2.Health != 0)
                {
                    S1.attack(S2);
                    if (S2.Health == 0)
                    {
                        return true;
                    }
                    S2.attack(S1);
                    if (S1.Health == 0)
                    {
                        return false;
                    }
                }
                if (S2.Health == 0)
                {
                    return true;
                }
                if (S1.Health == 0)
                {
                    return false;
                }
            }
            else
            {
                while (S1.Health != 0 || S2.Health != 0)
                {
                    S2.attack(S1);
                    if (S1.Health == 0)
                    {
                        return false;
                    }
                    S1.attack(S2);
                    if (S2.Health == 0)
                    {
                        return true;
                    }
                    
                }
                if (S2.Health == 0)
                {
                    return true;
                }
                if (S1.Health == 0)
                {
                    return false;
                }
            }

            return false;
        }
    }
    
    class Shrimp
    {
        public string name; // not changing stat
        private float attackVAL;
        private float healthVAL;
        private float maxHealth;
        public float speedVAL; // not changing stat
        private float special; // not changing stat
        private List<string> LosesAgainst = new List<string>();
        private List<string> WinsAgainst = new List<string>();

        public void Lose(Shrimp other)
        {
            LosesAgainst.Add(other.name);
        }

        public int GetLost()
        {
            return LosesAgainst.Count;
        }

        public int GetWins()
        {
            return WinsAgainst.Count;
        }

        public void Win(Shrimp other)
        {
            WinsAgainst.Add(other.name);
        }

        public float Attack
        {
            get { return attackVAL; }
            set { attackVAL = value;}
        }

        public float Health
        {
            get { return healthVAL < 0 ? 0 : healthVAL; }
            set { healthVAL = value < 0 ? 0 : value; }
        }

        public void Heal()
        {
            this.Health = maxHealth;
        }
        public Shrimp(string name, float attackVal, float healthVal, float speedVal, float special)
        {
            this.name = name;
            attackVAL = attackVal;
            healthVAL = healthVal + 1;
            speedVAL = speedVal;
            this.special = special;
            maxHealth = healthVal;
        }

        public void attack(Shrimp otherShrimp)
        {
            float attackDMG = this.attackVAL;
            int randInt = new Random().Next(0, 100);
            if (randInt <= special * 5)
            {
                attackDMG *= 2;
            }
            otherShrimp.Health =- attackDMG;
        }
        
    }
}