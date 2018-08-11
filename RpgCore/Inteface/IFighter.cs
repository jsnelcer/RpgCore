using System.Collections.Generic;

namespace RpgCore.Inteface
{
    public interface IFighter
    {
        void Attack(IFighter target);
        void Hit(List<IEffect> dmg);
        bool Alive();
    }
}
