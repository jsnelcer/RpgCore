using System;
using System.Collections.Generic;

namespace RpgCore.Interface
{

    public interface IFighter
    {
        void Attack(IEnemy target);
        void Hit(List<IEffect> dmg);
        bool Alive();
    }
}
