using System;
using UI;

[Serializable]
public class ViewComponent
{
    public HeroView Value { get; }

    public ViewComponent(HeroView viewController)
    {
        Value = viewController;
    }
}