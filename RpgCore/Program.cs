using System;
using RpgCore.Enum;
using RpgCore.Storaged;
using RpgCore.Items;
using RpgCore.Stats;
using System.Collections.Generic;
using RpgCore.Inteface;

namespace RpgCore
{
    class Program
    {
        static void Main(string[] args)
        {

            List<IStat> stats = new List<IStat>
            {
                new RegenerationStat(100f, StatType.Health),
                new RegenerationStat(100f, StatType.Energy),
                new RegenerationStat(100f, StatType.Stamina),
                new Stat(30f, StatType.Intelligence),
                new Stat(50f, StatType.Luck)
            };

            Player hero = new Player("Kazisvet III.", "z Bozi vule král", stats, new Storage<IItem>(), new Storage<ConsumableItem>(), new Storage<IEquiped>());

            Console.WriteLine(hero.ToString());
            IEffect dmg = new TimeEffect(EffectTarget.Character, StatType.Health, -5f, 5);
            IEffect effect = new InstantEffect(EffectTarget.Character, StatType.Health, +40f);
            IEffect InstaDmg = new InstantEffect(EffectTarget.Character, StatType.Health, -85f);

            IEffect time = new TimeEffect(EffectTarget.Character, StatType.Health, -10, 3, 1);
            Console.WriteLine("100f => " + hero.GetStat(StatType.Health).Value);
            hero.AddEffect(time);

            Console.WriteLine("100f => " + hero.GetStat(StatType.Health).Value);
            //Assert.AreEqual(3, time.Stack);
            hero.Update();

            Console.WriteLine("90f => " + hero.GetStat(StatType.Health).Value);
            //Assert.AreEqual(2, time.Stack);
            hero.Update();

            Console.WriteLine("80f => " + hero.GetStat(StatType.Health).Value);
            //Assert.AreEqual(1, time.Stack);
            hero.Update();

            Console.WriteLine("70f => " + hero.GetStat(StatType.Health).Value);
            //Assert.AreEqual(0, time.Stack);
            hero.Update();

            Console.WriteLine("70f => " + hero.GetStat(StatType.Health).Value);


            Console.ReadLine();

        }
    }
}
