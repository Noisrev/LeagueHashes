using LeagueHashes.Services;

namespace LeagueHashes.ViewModels
{
    public class SDBMViewModel : HashBaseViewModel<string, long>
    {
        public override void ComputedHash()
        {
            if (Input == null)
            {
                return;
            }
            string toHash = Input;

            long hash = 0;
            for (int i = 0; i < toHash.Length; i++)
            {
                int c = toHash[i];
                hash = c + (hash << 6) + (hash << 16) - hash;
            }

            Submit(toHash, hash);
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

        public override void Submit(string key, long value)
        {
            Hash = value;
            Output_Dec = Hash.ToString();
            Output_Hex = Hash.ToString("X");
            OnChangedText();
            HistoryService.AddHistory(new HistoryEntry(HistoryType.SDBM, $"{key} {(IsHex ? Output_Hex : Output_Dec)}"));
        }
    }
}
