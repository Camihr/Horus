using Horus.Controls;
using Horus.iOS.Renderers;
using System.Drawing;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace Horus.iOS.Renderers
{
    public class ExtendedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            SetBorderLines();

            var element = (ExtendedEntry)Element;
            if (element != null)
            {
                if (!string.IsNullOrEmpty(element.LeftIcon))
                {
                    Control.LeftViewMode = UITextFieldViewMode.Always;
                    Control.LeftView = GetLeftImageView(element.LeftIcon, element.LeftIconHeight, element.LeftIconWidth);

                    Control.LeftView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                    {
                        Element.Focus();
                    }));
                }

                if (!string.IsNullOrEmpty(element.RightIcon))
                {
                    Control.RightViewMode = UITextFieldViewMode.Always;
                    Control.RightView = GetRightImageView(element.RightIcon, element.RightIconHeight, element.RightIconWidth);

                    Control.RightView.AddGestureRecognizer(new UITapGestureRecognizer(() =>
                    {
                        Element.Focus();
                    }));
                }
            }
        }
        private void SetBorderLines()
        {
            var element = (ExtendedEntry)Element;

            if (element != null & Control != null)
            {
                if (!element.HasBorderLines)
                {
                    Control.BorderStyle = UITextBorderStyle.None;
                    Control.Layer.BorderWidth = 0;
                }
            }
        }

        private UIView GetLeftImageView(string imagePath, int height, int width)
        {
            var uiImageView = new UIImageView(UIImage.FromBundle(imagePath))
            {
                Frame = new RectangleF(5, 0, width, height)
            };

            UIView view = new UIView(new System.Drawing.Rectangle(5, 0, width + 3, height));

            view.AddSubview(uiImageView);

            return view;
        }

        private UIView GetRightImageView(string imagePath, int height, int width)
        {
            var uiImageView = new UIImageView(UIImage.FromBundle(imagePath))
            {
                Frame = new RectangleF(0, 0, width, height)
            };

            UIView view = new UIView(new System.Drawing.Rectangle(0, 0, width + 5, height));

            view.AddSubview(uiImageView);

            return view;
        }
    }
}