using LeagueHashes.Helpers;

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
                hash ^= s[i];
                hash *= 16777619;
            }
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

        public override void Submit(string key, uint value)
        {
            Hash = value;
            Output_Dec = Hash.ToString();
            Output_Hex = Hash.ToString("X");
            OnChangedText();
            HistoryHelper.AddHistory(new Core.Models.HistoryEntry(Core.Models.HistoryType.Fnv32, $"{key} {(IsHex ? Output_Hex : Output_Dec)}"));
        }
    }
}
