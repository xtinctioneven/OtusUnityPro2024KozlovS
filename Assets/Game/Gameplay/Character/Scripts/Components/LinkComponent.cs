using System;
using Game.Gameplay;

[Serializable]
public class LinkComponent
{
    public LinkStatusType CurrentLinkStatus { get; private set; } = LinkStatusType.None;

    public void ApplyStatus(LinkStatusType linkStatus)
    {
        CurrentLinkStatus = linkStatus;
    }
}