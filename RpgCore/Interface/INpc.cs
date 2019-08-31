using System;
using System.Collections.Generic;
using System.Text;

namespace RpgCore.Interface
{
    public interface INpc : ICharacter
    {
        void Move();
    }
}
