﻿using System;
using RpgCore.Enum;
using RpgCore.Storaged;
using RpgCore.Items;
using RpgCore.Stats;
using System.Collections.Generic;

namespace RpgCore
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Stat> stats = new List<Stat>
            {
                new RegenerationStat(100f, StatType.Health),
                new RegenerationStat(100f, StatType.Energy),
                new RegenerationStat(100f, StatType.Stamina),
                new Stat(30f, StatType.Intelligence),
                new Stat(50f, StatType.Luck)
            };
            
            Player hero = new Player("Kazisvet III.", "z Bozi vule král", stats);

            Console.WriteLine(hero.ToString());
            Effect dmg = new TimeEffect(EffectTarget.Character, StatType.Health, -5f, 5);
            Effect effect = new InstantEffect(EffectTarget.Character, StatType.Health, +40f);
            Effect InstaDmg = new InstantEffect(EffectTarget.Character, StatType.Health, -85f);
            
            IUseable healthPotion = new ConsumableItem(99, "Health of Potion", "Get 40hp", effect);
            Equipment helm = new Equipment(999, "helm of fire", "fireeee", EquipSlot.Head);
            helm.AddEquipEffect(new EquipEffect(EffectTarget.Character, StatType.Health, +30f));
            Equipment helm_air = new Equipment(997, "helm of air", "air", EquipSlot.Head);
            helm_air.AddEquipEffect(new EquipEffect(EffectTarget.Character, StatType.Health, +80f));

            hero.PickUp(helm);
            hero.PickUp(helm_air);

            Console.WriteLine(hero.GetStat(StatType.Health).GetValue());
            hero.Equip(helm);
            Console.WriteLine(hero.GetStat(StatType.Health).GetValue());
            hero.Equip(helm_air);
            Console.WriteLine(hero.GetStat(StatType.Health).GetValue());

        }
    }
}