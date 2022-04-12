using LeagueHashes.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;

namespace LeagueHashes.Services
{
    public static class HistoryService
    {
        private const string HistoryFileName = ".history";
        private const string HistoryEntriesKey = "AppHistoryEntries";
        private static ObservableCollection<HistoryEntry> history;

        public static async Task<IEnumerable<HistoryEntry>> ReadAsync()
        {
            return await ApplicationData.Current.LocalCacheFolder.ReadAsync<IEnumerable<HistoryEntry>>(HistoryFileName);
        }
        public static async Task SaveAsync()
        {
            await ApplicationData.Current.LocalCacheFolder.SaveAsync(HistoryFileName, history);
        }

        public static void AddHistory(HistoryEntry historyEntry)
        {
            history.Add(historyEntry);
        }
        public static async Task<ObservableCollection<HistoryEntry>> InitializedAsync()
        {
            var history = await ReadAsync();
            if (history != null)
                HistoryService.history = new ObservableCollection<HistoryEntry>(history);
            else
                HistoryService.history = new ObservableCollection<HistoryEntry>();

            return HistoryService.history;
        }
    }
    public class HistoryEntry
    {
        public HistoryType Type { get; set; }
        public string Text { get; set; }
        public bool HasColor { get; set; }
        public Color Color { get; set; }

        public HistoryEntry()
        {
        }

        public HistoryEntry(HistoryType type, string text)
        {
            Type = type;
            Text = text;
        }
        public HistoryEntry(HistoryType type, string text, Color color) : this(type, text)
        {
            HasColor = true;
            Color = color;
        }
    }
    public enum HistoryType
    {
        CCITT32,
        Color,
        Fnv32,
        Fnv32a,
        ELF,
        RST,
        SDBM,
        Troy,
        XXHash32,
        XXHash64
    }
}
