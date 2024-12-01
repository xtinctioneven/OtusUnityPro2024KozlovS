namespace Game.Gameplay
{
    public class StatsComponent
    {
        private Stat[] _stats;

        public StatsComponent(Stat.StatData[] statsData)
        {
            _stats = new Stat[statsData.Length];
            for (int i = 0; i < statsData.Length; i++)
            {
                var statData = statsData[i];
                _stats[i] = new Stat(statData.StatType, statData.Value);
            }
        }

        public Stat[] GetAllStats()
        {
            return _stats;
        }

        public bool TryGetStat(StatType statType, out Stat stat)
        {
            for (int i = 0; i < _stats.Length; i++)
            {
                stat = _stats[i];
                if (stat.StatType == statType)
                {
                    return true;
                }
            }

            stat = null;
            return false;
        }
    }
}