using System;
using System.Collections.Generic;

namespace MythicPuzzle.Runtime
{
    [Serializable]
    public sealed class PlayerProgressData
    {
        public int schemaVersion = 1;
        public int highestUnlockedLevel = 1;
        public List<string> completedLevelIds = new();
        public bool musicEnabled = true;
        public bool soundEnabled = true;
    }
}
