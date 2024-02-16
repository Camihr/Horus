using Android.Content;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Views;
using AndroidX.Core.Content;
using Xamarin.Forms.Platform.Android;
using Horus.Controls;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace Horus.Droid.Renderers
{
    public class ExtendedEntryRenderer : EntryRenderer
    {
        public ExtendedEntryRenderer(Context context) : base(context){}

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            SetBorderLines();
            SetIcons();
        }

        private void SetBorderLines()
        {
            var element = (ExtendedEntry)Element;

            if (element != null && Control != null)
            {
                if (!element.HasBorderLines)
                {
                    Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                    Control.SetPadding(0, 0, 0, 0);
                }
            }
        }

        private void SetIcons()
        {
            var element = (ExtendedEntry)Element;

            if (element != null && Control != null &&
                (!string.IsNullOrEmpty(element.LeftIcon) || !string.IsNullOrEmpty(element.RightIcon)))
            {
                if (element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Center)
                    Control.Gravity = GravityFlags.Center;
                else
                    Control.Gravity = GravityFlags.CenterVertical;

                Drawable leftIcon = null;
                Drawable rightIcon = null;

                if (!string.IsNullOrEmpty(element.LeftIcon))
                    leftIcon = GetDrawable(element.LeftIcon, element.LeftIconWidth, element.LeftIconHeight);

                if (!string.IsNullOrEmpty(element.RightIcon))
                    rightIcon = GetDrawable(element.RightIcon, element.RightIconWidth, element.RightIconHeight);

                Control.SetCompoundDrawablesWithIntrinsicBounds(leftIcon, null, rightIcon, null);
            }
        }

        private Drawable GetDrawable(string iconFileName, int iconWidth, int iconHeight)
        {
            iconFileName = iconFileName.Replace(".jpg", "").Replace(".png", "");

            int id = Resources.GetIdentifier(iconFileName, "drawable", this.Context.PackageName);

            var drawable = ContextCompat.GetDrawable(this.Context, id);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            var scaledBitmap = Bitmap.CreateScaledBitmap(bitmap, iconWidth * 2, iconHeight * 2, true);

            var borderSize = 10;
            Bitmap bmpWithBorder = Bitmap.CreateBitmap(scaledBitmap.Width + borderSize * 2,
                scaledBitmap.Height, scaledBitmap.GetConfig());

            Canvas canvas = new Canvas(bmpWithBorder);
            canvas.DrawColor(Xamarin.Forms.Color.Transparent.ToAndroid());
            canvas.DrawBitmap(scaledBitmap, borderSize, 0, null);

            return new BitmapDrawable(Resources, bmpWithBorder);
        }
    }
}