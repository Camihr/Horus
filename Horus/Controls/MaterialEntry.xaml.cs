using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Horus.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialEntry : ContentView
    {
        public MaterialEntry()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialEntry), default(string), BindingMode.TwoWay, propertyChanged: OnTextPropertyChanged);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialEntry), default(string));

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly BindableProperty IsEntryPasswordProperty =
           BindableProperty.Create(nameof(IsEntryPassword), typeof(bool), typeof(MaterialEntry), default(bool), propertyChanged: OnIsEntryPasswordChanged);

        public bool IsEntryPassword
        {
            get { return (bool)GetValue(IsEntryPasswordProperty); }
            set { SetValue(IsEntryPasswordProperty, value); }
        }

        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialEntry), default(Keyboard));

        public Keyboard Keyboard
        {
            get { return (Keyboard)GetValue(KeyboardProperty); }
            set { SetValue(KeyboardProperty, value); }
        }

        public static readonly BindableProperty FocusedBorderColorProperty =
            BindableProperty.Create(nameof(FocusedBorderColor), typeof(Color), typeof(MaterialEntry), default(Color));

        public Color FocusedBorderColor
        {
            get { return (Color)GetValue(FocusedBorderColorProperty); }
            set { SetValue(FocusedBorderColorProperty, value); }
        }

        private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null && !string.IsNullOrEmpty(newValue.ToString()))
            {
                var element = (MaterialEntry)bindable;
                element.stack.Spacing = 5;
            }
        }

        private static void OnIsEntryPasswordChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (Convert.ToBoolean(newValue))
            {
                var element = (MaterialEntry)bindable;
                element.entry.IsPassword = true;
                element.imageShowPassword.IsVisible = true;
                element.imageHidePassword.IsVisible = false;
            }
        }

        private void ExtendedEntry_Focused(object sender, FocusEventArgs e)
        {
            frame.BorderColor = FocusedBorderColor;

            if (string.IsNullOrEmpty(Text))
            {
                stack.Animate("animation", value =>
                {
                    stack.Spacing = value;
                }, -15d, 5d, length: 150);
            }
        }

        private void ExtendedEntry_Unfocused(object sender, FocusEventArgs e)
        {
            frame.BorderColor = Color.Default;

            if (string.IsNullOrEmpty(Text))
            {
                stack.Animate("animation", value =>
                {
                    stack.Spacing = value;
                }, 5d, -15d, length: 150);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            entry.IsPassword = !entry.IsPassword;
            imageShowPassword.IsVisible = entry.IsPassword;
            imageHidePassword.IsVisible = !entry.IsPassword;
        }
    }
}