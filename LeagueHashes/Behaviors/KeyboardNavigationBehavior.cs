using Microsoft.Xaml.Interactivity;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LeagueHashes.Behaviors
{
    internal class KeyboardNavigationBehavior : DependencyObject, IBehavior
    {
        public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register("TargetObject", typeof(object),
            typeof(KeyboardNavigationBehavior), new PropertyMetadata(null));

        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(VirtualKey),
            typeof(KeyboardNavigationBehavior), new PropertyMetadata(null));

        public object TargetObject
        {
            get => GetValue(TargetObjectProperty);
            set => SetValue(TargetObjectProperty, value);
        }
        public VirtualKey Key
        {
            get => (VirtualKey)GetValue(KeyProperty);
            set => SetValue(KeyProperty, value);
        }


        public DependencyObject AssociatedObject
        {
            get;
            private set;
        }

        public void Attach(DependencyObject associatedObject)
        {
            if (associatedObject is FrameworkElement frameworkElement)
            {
                frameworkElement.KeyDown += FrameworkElement_KeyDown;
            }
            AssociatedObject = associatedObject;
        }

        private void FrameworkElement_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Key && TargetObject is Control targetObject)
            {
                targetObject.Focus(FocusState.Pointer);
            }
        }

        public void Detach()
        {
            if (AssociatedObject is FrameworkElement frameworkElement)
            {
                frameworkElement.KeyDown -= FrameworkElement_KeyDown;
            }
            AssociatedObject = null;
        }
    }
}
