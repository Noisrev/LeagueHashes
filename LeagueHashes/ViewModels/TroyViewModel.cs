using LeagueHashes.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace LeagueHashes.ViewModels
{
    public class TroyEntry : ObservableObject
    {
        public string Section
        {
            get => __section;
            set => SetProperty(ref __section, value);
        }
        private string __section;
        public string Property
        {
            get => __property;
            set => SetProperty(ref __property, value);
        }
        private string __property;
    }
    public class TroyViewModel : HashBaseViewModel<TroyEntry, uint>
    {
        public TroyViewModel()
        {
            Input = new TroyEntry();
        }
        public override void ComputedHash()
        {
            if (Input.Section == null || Input.Property == null)
            {
                return;
            }
            uint hash = 0;
            string section = Input.Section.ToLower();
            string property = Input.Property.ToLower();
            for (int i = 0; i < section.Length; i++)
            {
                hash = section[i] + 65599 * hash;
            }
            hash = (65599 * hash + 42);
            for (int i = 0; i < property.Length; i++)
            {
                hash = property[i] + 65599 * hash;
            }
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

        public override void Submit(TroyEntry key, uint value)
        {
            Hash = value;
            Output_Dec = Hash.ToString();
            Output_Hex = Hash.ToString("X");
            OnChangedText();
            HistoryService.AddHistory(new HistoryEntry(HistoryType.Troy, $"[{key.Section}, {key.Property}] {(IsHex ? Output_Hex : Output_Dec)}"));
        }
    }
}
