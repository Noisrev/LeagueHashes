﻿using LeagueHashes.Helpers;

namespace LeagueHashes.ViewModels
{
    public class ELFViewModel : HashBaseViewModel<string, uint>
    {
        public override void ComputedHash()
        {
            if (Input == null)
            {
                return;
            }
            string toHash = Input;
            if (ToLower)
            {
                toHash = toHash.ToLower();
            }

            uint hash = 0;
            uint high = 0;
            for (int i = 0; i < toHash.Length; i++)
            {
                hash = (hash << 4) + ((byte)toHash[i]);

                if ((high = hash & 0xF0000000) != 0)
                {
                    hash ^= high >> 24;
                }

                hash &= ~high;
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

        public override void Submit(string key, uint value)
        {
            Hash = value;
            Output_Dec = Hash.ToString();
            Output_Hex = Hash.ToString("X");
            OnChangedText();
            HistoryHelper.AddHistory(new Core.Models.HistoryEntry(Core.Models.HistoryType.ELF, $"{key} {(IsHex ? Output_Hex : Output_Dec)}"));
        }
    }
}
