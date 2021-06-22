using LeagueHashes.Interfaces;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace LeagueHashes.ViewModels
{
    public abstract class HashBaseViewModel<TKey, TValue> : ObservableObject, IHashBase where TValue : struct
    {
        public bool IsHex
        {
            get => __isHex;
            set
            {
                SetProperty(ref __isHex, value, nameof(IsHex));
                OnChangedText();
            }
        }
        public bool ToLower
        {
            get => __toLower;
            set => SetProperty(ref __toLower, value);
        }
        public TKey Input
        {
            get => __input;
            set => SetProperty(ref __input, value);
        }
        public TValue Hash
        {
            get => __hash;
            set => SetProperty(ref __hash, value);
        }
        public string Output
        {
            get => __output;
            set => SetProperty(ref __output, value);
        }
        public string Output_Dec
        {
            get => __output_Dec;
            set => SetProperty(ref __output_Dec, value);
        }
        public string Output_Hex
        {
            get => __output_Hex;
            set => SetProperty(ref __output_Hex, value);
        }
        public ICommand OnComputedHash => __onComputedHash;

        private bool __isHex;
        private bool __toLower;
        private TKey __input;
        private TValue __hash;
        private string __output;
        private string __output_Dec;
        private string __output_Hex;

        private readonly RelayCommand __onComputedHash;
        public HashBaseViewModel()
        {
            __onComputedHash = new RelayCommand(ComputedHash);
        }

        public abstract void Submit(TKey key, TValue value);
        public abstract void ComputedHash();
        public abstract void OnChangedText();

    }
}
