using Xamarin.Forms;

namespace Horus.Controls
{
    public class ExtendedEntry : Entry
    {
        public static readonly BindableProperty HasBorderLinesProperty =
              BindableProperty.Create(nameof(HasBorderLines), typeof(bool), typeof(ExtendedEntry), true);

        public bool HasBorderLines
        {
            get { return (bool)GetValue(HasBorderLinesProperty); }
            set { SetValue(HasBorderLinesProperty, value); }
        }

        public static readonly BindableProperty LeftIconProperty =
            BindableProperty.Create(nameof(LeftIcon), typeof(string), typeof(ExtendedEntry), default(string));

        public string LeftIcon
        {
            get { return (string)GetValue(LeftIconProperty); }
            set { SetValue(LeftIconProperty, value); }
        }

        public static readonly BindableProperty RightIconProperty =
            BindableProperty.Create(nameof(RightIcon), typeof(string), typeof(ExtendedEntry), default(string));

        public string RightIcon
        {
            get { return (string)GetValue(RightIconProperty); }
            set { SetValue(RightIconProperty, value); }
        }

        public static readonly BindableProperty LeftIconWidthProperty =
            BindableProperty.Create(nameof(LeftIconWidth), typeof(int), typeof(ExtendedEntry), 25);

        public int LeftIconWidth
        {
            get { return (int)GetValue(LeftIconWidthProperty); }
            set { SetValue(LeftIconWidthProperty, value); }
        }

        public static readonly BindableProperty LeftIconHeightProperty =
            BindableProperty.Create(nameof(LeftIconHeight), typeof(int), typeof(ExtendedEntry), 25);

        public int LeftIconHeight
        {
            get { return (int)GetValue(LeftIconHeightProperty); }
            set { SetValue(LeftIconHeightProperty, value); }
        }

        public static readonly BindableProperty RightIconWidthProperty =
            BindableProperty.Create(nameof(RightIconWidth), typeof(int), typeof(ExtendedEntry), 25);

        public int RightIconWidth
        {
            get { return (int)GetValue(RightIconWidthProperty); }
            set { SetValue(RightIconWidthProperty, value); }
        }

        public static readonly BindableProperty RightIconHeightProperty =
            BindableProperty.Create(nameof(RightIconHeight), typeof(int), typeof(ExtendedEntry), 25);

        public int RightIconHeight
        {
            get { return (int)GetValue(RightIconHeightProperty); }
            set { SetValue(RightIconHeightProperty, value); }
        }
    }
}

