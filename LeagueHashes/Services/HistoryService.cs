using LeagueHashes.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;

namespace LeagueHashes.Services
{
    public static class HistoryService
    {
        private const string HistoryFileName = ".history";
        private static Dictionary<string, HistoryEntry> _history;

        public static async Task<Dictionary<string, HistoryEntry>> InitializedAsync()
        {
            var historyEntries = await ReadAsync();

            _history = new Dictionary<string, HistoryEntry>();
            if (historyEntries != null)
            {
                foreach (var item in historyEntries)
                {
                    if (!_history.ContainsKey(item.Text))
                        _history.Add(item.Text, item);
                }
            }

            return _history;
        }

        public static async Task<IEnumerable<HistoryEntry>> ReadAsync()
        {
            return await ApplicationData.Current.LocalCacheFolder.ReadAsync<IEnumerable<HistoryEntry>>(HistoryFileName);
        }
        public static async Task SaveAsync()
        {
            await ApplicationData.Current.LocalCacheFolder.SaveAsync(HistoryFileName, _history.Values);
        }


        public static async void AddHistory(HistoryEntry historyEntry)
        {
            if (_history.ContainsKey(historyEntry.Text))
                _history.Remove(historyEntry.Text);

            _history.Add(historyEntry.Text, historyEntry);

            await SaveAsync();
            RaisePropertyChanged();
        }

        public static async void RemoveHistory(IEnumerable<KeyValuePair<string, HistoryEntry>> historyEntries)
        {
            foreach (var historyEntry in historyEntries)
            {
                if (_history.ContainsKey(historyEntry.Key))
                    _history.Remove(historyEntry.Key);
            }

            await SaveAsync();
            RaisePropertyChanged();
        }

        public static async void ClearHistory()
        {
            _history.Clear();

            await SaveAsync();
            RaisePropertyChanged();
        }

        internal static void RaisePropertyChanged()
        {
            OnCollectionChanged?.Invoke(null, new EventArgs());
        }

        public static event EventHandler OnCollectionChanged;
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
