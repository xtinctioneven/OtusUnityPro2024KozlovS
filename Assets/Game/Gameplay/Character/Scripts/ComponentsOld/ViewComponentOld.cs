using System;
using UI;

[Serializable]
public class ViewComponentOld
{
    public HeroView Value { get; }

    public ViewComponentOld(HeroView viewController)
    {
        Value = viewController;
    }
}