#if IOS
using Foundation;
using Microsoft.Maui.Handlers;
using UIKit;

namespace ResillentConstruction.Platforms.iOS
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
            if (handler.PlatformView is UILabel uiLabel)
            {
                if (!string.IsNullOrWhiteSpace(label.Text))
                {
                    var paragraphStyle = new NSMutableParagraphStyle
                    {
                        Alignment = UITextAlignment.Justified,
                        LineSpacing = 6f,
                        LineHeightMultiple = 1.3f
                    };

                    var attributes = new UIStringAttributes
                    {
                        ParagraphStyle = paragraphStyle,
                        Font = uiLabel.Font ?? UIFont.SystemFontOfSize(16),
                        ForegroundColor = uiLabel.TextColor ?? UIColor.Black
                    };

                    uiLabel.AttributedText = new NSMutableAttributedString(label.Text, attributes);
                }
                else
                {
                    uiLabel.AttributedText = null;
                }
            }
        }
    }
}
#endif
