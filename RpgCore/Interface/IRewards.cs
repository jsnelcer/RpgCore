using System.Collections.Generic;

namespace RpgCore.Interface
{
    public interface IRewards
    {
        List<IItem> Items { get; }
        List<IStat> Stats { get; }
    }
}
