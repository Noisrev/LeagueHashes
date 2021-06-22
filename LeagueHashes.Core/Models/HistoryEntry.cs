using Windows.UI.Xaml.Media;

namespace LeagueHashes.Core.Models
{
    public class HistoryEntry
    {
        public HistoryType Type { get; }
        public string Text { get; }
        public bool HasColor { get; } = false;
        public SolidColorBrush SolidColor { get; }
        public HistoryEntry(HistoryType type, string text)
        {
            Type = type;
            Text = text;
        }
        public HistoryEntry(HistoryType type, string text, SolidColorBrush brush) : this(type, text)
        {
            HasColor = true;
            SolidColor = brush;
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
