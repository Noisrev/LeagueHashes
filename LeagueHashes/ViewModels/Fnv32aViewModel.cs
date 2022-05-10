using LeagueHashes.Services;

namespace LeagueHashes.ViewModels
{
    public class Fnv32aViewModel : HashBaseViewModel<string, uint>
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
                hash ^= s[i];
                hash *= 16777619;
            }
            Submit(s, hash);
        }

        public override void Submit(string key, uint value)
        {
            Hash = value;
            HistoryService.AddHistory(new HistoryEntry(HistoryType.Fnv32a, $"{key} {Output}"));
        }
    }
}
