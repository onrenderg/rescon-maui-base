#if ANDROID
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using Microsoft.Maui.Handlers;
using ResillentConstruction;
using System.Runtime.Versioning;

namespace ResillentConstruction.Platforms.Android
{
    public class JustifiedLabelHandler : LabelHandler
    {
        public static new IPropertyMapper<JustifiedLabel, JustifiedLabelHandler> Mapper =
            new PropertyMapper<JustifiedLabel, JustifiedLabelHandler>(LabelHandler.Mapper)
            {
                [nameof(Label.Text)] = MapText
            };

        public JustifiedLabelHandler() : base(Mapper) { }

        public static void MapText(JustifiedLabelHandler handler, JustifiedLabel label)
        {
            if (handler.PlatformView is TextView textView)
            {
                if (!string.IsNullOrWhiteSpace(label.Text))
                {
                    // Only use JustificationMode on Android API 26 (Android 8.0) and above
                    if (global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O)
                    {
#pragma warning disable CA1416
                        textView.JustificationMode = JustificationMode.InterWord;
#pragma warning restore CA1416
                    }
                    textView.Gravity = GravityFlags.FillHorizontal | GravityFlags.CenterVertical;
                    textView.SetLineSpacing(5f, 1.2f);
                    textView.Text = label.Text;
                }
            }
        }

    }
}
#endif
