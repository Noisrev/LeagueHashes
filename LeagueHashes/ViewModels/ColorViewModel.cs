
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Windows.UI;

namespace LeagueHashes.ViewModels
{
    public enum ColorType
    {
        R,
        G,
        B,
        A
    }
    public class ColorViewModel : ObservableObject
    {
        public Color AutoColor
        {
            get => __autoColor;
            set
            {
                SetProperty(ref __autoColor, value);
                if (CanSetRGBA)
                {
                    R = value.R / (float)255;
                    G = value.G / (float)255;
                    B = value.B / (float)255;
                    A = value.A / (float)255;
                }
            }
        }
        public float R
        {
            get => __r;
            set => SetProperty(ref __r, value);
        }
        public float G
        {
            get => __g;
            set => SetProperty(ref __g, value);
        }
        public float B
        {
            get => __b;
            set => SetProperty(ref __b, value);
        }
        public float A
        {
            get => __a;
            set => SetProperty(ref __a, value);
        }
        public bool CanSetRGBA = true;
        private float __r;
        private float __g;
        private float __b;
        private float __a;
        private Color __autoColor;
        public ColorViewModel()
        {
            __r = 1;
            __g = 1;
            __b = 1;
            __a = 1;
            __autoColor = new Color() { R = 255, G = 255, B = 255, A = 255 };
        }
    }
}
