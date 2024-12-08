using System;
using UI;
using UnityEngine;

[Serializable]
public class ViewComponentOld
{
    public HeroView Value { get; }

    public ViewComponentOld(HeroView viewController)
    {
        Value = viewController;
    }
}