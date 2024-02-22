using System;
using System.Collections.Generic;

namespace RpgCore.Interface
{

    public interface IFighter : ICharacter
    {
        void Attack(IFighter target);
        void Hit(List<IEffect> dmg);
    }
}
