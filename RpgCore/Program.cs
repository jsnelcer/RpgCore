using System;
using RpgCore.Enum;
using RpgCore.Storaged;
using RpgCore.Items;
using RpgCore.StateMachine;
using RpgCore.Stats;
using System.Collections.Generic;
using RpgCore.Interface;

namespace RpgCore
{
    class Program
    {
        static void Main(string[] args)
        {

            List<IStat> player_stats = new List<IStat>
            {
                new RegenerationStat(100f, StatType.Health),
                new RegenerationStat(100f, StatType.Energy),
                new RegenerationStat(100f, StatType.Stamina),
                new Stat(30f, StatType.Intelligence),
                new Stat(50f, StatType.Luck)
            };

            List<IStat> orc_stats = new List<IStat>
            {
                new RegenerationStat(100f, StatType.Health),
                new RegenerationStat(100f, StatType.Energy),
                new RegenerationStat(100f, StatType.Stamina),
                new Stat(30f, StatType.Intelligence),
                new Stat(50f, StatType.Luck)
            };

            Player hero = new Player("Kazisvet III.", "z Bozi vule král", player_stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());
            Enemy npc = new Enemy("Orc", "ver. 3.3", orc_stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());


            int iteration = 30;

            while (iteration > 0)
            {
                hero.StateMachine.ExecuteStateUpdate();
                npc.StateMachine.ExecuteStateUpdate();

                if(iteration == 29)
                {
                    hero.StateMachine.ChangeState(new Attack(hero, npc));
                    npc.StateMachine.ChangeState(new Attack(npc, hero));
                }

                iteration--;
            }
            Console.WriteLine("iteration: " + iteration);
            Console.ReadLine();
        }
    }
}
