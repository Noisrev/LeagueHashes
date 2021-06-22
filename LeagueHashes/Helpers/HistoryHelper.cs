using LeagueHashes.Core.Models;
using System;

namespace LeagueHashes.Helpers
{
    public static class HistoryHelper
    {
        public static Action<HistoryEntry> AddHistory { get; set; }
    }
}
