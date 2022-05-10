using LeagueHashes.Services;

namespace LeagueHashes.ViewModels
{
    public class Fnv32ViewModel : HashBaseViewModel<string, uint>
    {
        public override void ComputedHash()
        {
            if (Input == null)
            {
                return;
            }
            string s = Input;
            uint hash = 2166136261;
            if (ToLower)
            {
                s = s.ToLower();
            }
            for (int i = 0; i < s.Length; i++)
            {
                hash *= 16777619;
                hash ^= s[i];
            }
            Submit(s, hash);
        }

        public override void Submit(string key, uint value)
        {
            Hash = value;
            HistoryService.AddHistory(new HistoryEntry(HistoryType.Fnv32, $"{key} {Output}"));
        }
    }
}
