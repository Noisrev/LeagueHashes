using Extensions.Data;
using LeagueHashes.Services;
using System.Text;

namespace LeagueHashes.ViewModels
{
    public class XXHash64ViewModel : HashBaseViewModel<string, ulong>
    {
        public override void ComputedHash()
        {
            if (Input == null)
            {
                return;
            }
            string s = Input;
            if (ToLower)
            {
                s = Input.ToLower();
            }
            ulong hash = XXHash.XXH64(Encoding.UTF8.GetBytes(s));
            Submit(s, hash);
        }

        public override void Submit(string key, ulong value)
        {
            Hash = value;
            HistoryService.AddHistory(new HistoryEntry(HistoryType.XXHash64, $"{key} {Output}"));
        }
    }
}
