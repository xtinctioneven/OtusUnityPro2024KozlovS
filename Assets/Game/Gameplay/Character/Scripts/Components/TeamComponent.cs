using System;

namespace Game.Gameplay
{
    [Serializable]
    public class TeamComponent
    {
        public Team Value { get; private set; }

        public TeamComponent(Team team = Team.Undefined)
        {
            Value = team;
        }

        public void SetTeam(Team team)
        {
            Value = team;
        }
    }
}