using Extensions.Data;
using LeagueHashes.Services;
using System.Text;

namespace LeagueHashes.ViewModels
{
    public class XXHash32ViewModel : HashBaseViewModel<string, uint>
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
            uint hash = XXHash.XXH32(Encoding.UTF8.GetBytes(s));
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
            HistoryService.AddHistory(new HistoryEntry(HistoryType.XXHash32, $"{key} {(IsHex ? Output_Hex : Output_Dec)}"));
        }
    }
}
