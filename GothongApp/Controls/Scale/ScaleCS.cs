using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GothongApp.Controls.Scale
{
    [Preserve(AllMembers = true)]
    public static class ScaleCS
    {
        public static float ScaleHeight(this int number, int? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.ScreenHeight / 568.0));
        }

        public static float ScaleHeight(this double number, double? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;
            return (float)(number * (App.ScreenHeight / 568.0));
        }

        public static float ScaleHeight(this float number, float? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.ScreenHeight / 568.0));
        }

        public static float ScaleWidth(this int number, int? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.ScreenWidth / 320.0));
        }

        public static float ScaleWidth(this double number, double? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.ScreenWidth / 320.0));
        }

        public static float ScaleWidth(this float number, float? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.ScreenWidth / 320.0));
        }

        public static double ScaleFont(this int number, int? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (number * GetMinSize() - (Device.RuntimePlatform == Device.iOS ? 0.5 : 0));
        }

        public static double ScaleFont(this double number, double? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (number * GetMinSize() - (Device.RuntimePlatform == Device.iOS ? 0.5 : 0));
        }

        public static Thickness ScaleThickness(this Thickness number, Thickness? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;
            number.Left = number.Left.ScaleWidth();
            number.Top = number.Top.ScaleHeight();
            number.Right = number.Right.ScaleWidth();
            number.Bottom = number.Bottom.ScaleHeight();
            return number;
        }

        public static CornerRadius ScaleCornerRadius(this CornerRadius number, CornerRadius? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return new CornerRadius(number.TopLeft.ScaleHeight(), number.TopRight.ScaleHeight(), number.BottomLeft.ScaleHeight(), number.BottomRight.ScaleHeight());
        }

        public static double GetMinSize()
        {
            return Math.Min((App.ScreenHeight / 568.0), (App.ScreenWidth / 320.0));
        }

        public static T Clone<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
