using System.Collections.Generic;

namespace RpgCore.Interface
{
    public interface IFighter
    {
        void Attack(IFighter target);
        void Hit(List<IEffect> dmg);
        bool Alive();
    }
}
