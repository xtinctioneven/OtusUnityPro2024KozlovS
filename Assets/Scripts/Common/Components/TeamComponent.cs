using System;

[Serializable]
public class TeamComponent
{
    public Team Value { get; }

    public TeamComponent(Team value)
    {
        Value = value;
    }
}