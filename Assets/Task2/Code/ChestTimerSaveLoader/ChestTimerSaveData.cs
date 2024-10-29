using System;
using System.Collections.Generic;

namespace SaveSystem
{
    public class ChestTimerServiceSaveData
    {
        public Dictionary<string, ChestTimerSaveData> ChestTimerSaveDataCollection = new();

        public struct ChestTimerSaveData
        {
            public DateTime StartTime;
        }
    }
}