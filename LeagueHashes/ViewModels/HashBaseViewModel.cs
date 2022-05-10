using LeagueHashes.Interfaces;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace LeagueHashes.ViewModels
{
    public abstract class HashBaseViewModel<TKey, TValue> : ObservableObject, IHashBase where TValue : IComparable, IComparable<TValue>, IConvertible, IEquatable<TValue>, IFormattable
    {
        public bool IsHex
        {
            get => __isHex;
            set
            {
                if (value)
                {
                    Output = Output_Hex;
                }
                else
                {
                    Output = Output_Dec;
                }
                SetProperty(ref __isHex, value, nameof(IsHex));
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
            set
            {
                if (__hash.CompareTo(value) == 0) return;

                Output_Dec = value.ToString();

                string formatString;
                switch (Type.GetTypeCode(typeof(TValue)))
                {
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                        formatString = "0x{0:X2}";
                        break;
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                        formatString = "0x{0:X4}";
                        break;
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                        formatString = "0x{0:X8}";
                        break;
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                        formatString = "0x{0:X16}";
                        break;
                    default: throw new InvalidOperationException();
                }
                Output_Hex = string.Format(formatString, value);

                Output = IsHex ? Output_Hex : Output_Dec;
                SetProperty(ref __hash, value);
            }
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
        public ICommand OnComputedHash { get; }

        private bool __isHex;
        private bool __toLower;
        private TKey __input;
        private TValue __hash;
        private string __output;
        private string __output_Dec;
        private string __output_Hex;

        public HashBaseViewModel()
        {
            OnComputedHash = new RelayCommand(ComputedHash);
        }

        public abstract void Submit(TKey key, TValue value);

        public abstract void ComputedHash();
    }
}
