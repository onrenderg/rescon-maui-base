#if ANDROID
using Android.Text;
using Android.Views;
using Android.Widget;
using Microsoft.Maui.Handlers;
using ResillentConstruction;

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
                    textView.JustificationMode = JustificationMode.InterWord;
                    textView.Gravity = GravityFlags.FillHorizontal | GravityFlags.CenterVertical;
                    textView.SetLineSpacing(5f, 1.2f);
                    textView.Text = label.Text;
                }
            }
        }
    }
}
#endif
