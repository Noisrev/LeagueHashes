using Extensions.Data;
using LeagueHashes.Helpers;
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
        public override void OnChangedText()
        {
            if (IsHex)
            {
                Output = Output_Hex;
            }
            else
            {
                Output = Output_Dec;
            }
        }

        public override void Submit(string key, ulong value)
        {
            Hash = value;
            Output_Dec = Hash.ToString();
            Output_Hex = Hash.ToString("X");
            OnChangedText();
            HistoryHelper.AddHistory(new Core.Models.HistoryEntry(Core.Models.HistoryType.XXHash64, $"{key} {(IsHex ? Output_Hex : Output_Dec)}"));
        }
    }
}
