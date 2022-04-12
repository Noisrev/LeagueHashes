using LeagueHashes.Services;
using LeagueHashes.ViewModels;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace LeagueHashes.Views
{
    public sealed partial class ColorPage : Page
    {
        public ColorViewModel ViewModel { get; } = new ColorViewModel();

        public ColorPage()
        {
            InitializeComponent();
        }

        private void Grid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void Color_TextBox_KeyDown(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = ((TextBox)sender);
            if (textBox.DataContext is ColorType type)
            {
                string text = textBox.Text;
                byte val = 0;
                if (string.IsNullOrEmpty(text))
                {
                    ((TextBox)sender).Text = "0";
                    return;
                }
                else
                {
                    if (!(text == "0"))
                    {
                        if (!TryParseValue(text, out val))
                        {
                            return;
                        }
                    }
                }


                Color color = new Color() { R = ViewModel.AutoColor.R, G = ViewModel.AutoColor.G, B = ViewModel.AutoColor.B, A = ViewModel.AutoColor.A };
                if (type == ColorType.R)
                {
                    color.R = val;
                }
                if (type == ColorType.G)
                {
                    color.G = val;
                }
                if (type == ColorType.B)
                {
                    color.B = val;
                }
                if (type == ColorType.A)
                {
                    color.A = val;
                }

                ViewModel.CanSetRGBA = false;
                ViewModel.AutoColor = color;
                ViewModel.CanSetRGBA = true;
            }
        }
        private bool TryParseValue(string text, out byte result)
        {
            if (float.TryParse(text, out float f))
            {
                return byte.TryParse(((int)(f * 255)).ToString(), out result);
            }
            else
            {
                result = 0;
                return false;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            HistoryService.AddHistory(new HistoryEntry(HistoryType.Color, $"{ViewModel.AutoColor.R / (float)255}, {ViewModel.AutoColor.G / (float)255}, {ViewModel.AutoColor.B / (float)255}, {ViewModel.AutoColor.A / (float)255}", ViewModel.AutoColor));
        }
    }
}
