using Extensions.Data;
using LeagueHashes.Helpers;
using System.Collections.Generic;
using System.Text;

namespace LeagueHashes.ViewModels
{
    public class RSTViewModel : HashBaseViewModel<string, ulong>
    {
        public List<int> Bits
        {
            get => __bits;
            set => SetProperty(ref __bits, value);
        }
        private List<int> __bits = new List<int>() { 39, 40 };
        public int Bit
        {
            get => __bit;
            set => SetProperty(ref __bit, value);
        }
        private int __bit = 39;
        public string Offset
        {
            get => __offset;
            set => SetProperty(ref __offset, value);
        }
        private string __offset;
        public static ulong Compute(int bits)
        {
            return (1UL << bits) - 1;
        }
        public override void ComputedHash()
        {
            if (Input == null)
            {
                return;
            }
            ulong offset = 0;
            if (ulong.TryParse(Offset, out ulong result))
            {
                offset = result << Bit;
            }
            string s = Input.ToLower();
            ulong hash = XXHash.XXH64(Encoding.UTF8.GetBytes(s)) & Compute(Bit);
            hash += offset;
            Submit(Input, hash);
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
            HistoryHelper.AddHistory(new Core.Models.HistoryEntry(Core.Models.HistoryType.RST, $"{key} {(IsHex ? Output_Hex : Output_Dec)}"));
        }
    }
}
