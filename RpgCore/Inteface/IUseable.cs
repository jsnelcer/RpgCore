using RpgCore;
using RpgCore.Inteface;

public interface IUseable
{
    IEffect<StatsManager> Use();
}