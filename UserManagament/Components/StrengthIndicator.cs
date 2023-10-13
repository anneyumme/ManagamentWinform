namespace DevExpress.UITemplates.Collection.Components {
    using System.Drawing;
    using DevExpress.Utils.Html;

    public sealed class StrengthIndicator : ProgressIndicatorBase {
        public static void Register() {
            DxHtmlDocument.Define<StrengthIndicator>("strength-indicator");
        }
        //
        public string WeakColor {
            get { return GetAttribute(nameof(WeakColor)) as string ?? "@Red"; }
        }
        public string MediumColor {
            get { return GetAttribute(nameof(MediumColor)) as string ?? "@Orange"; }
        }
        public string StrongColor {
            get { return GetAttribute(nameof(StrongColor)) as string ?? "@Green"; }
        }
        public string BackgroundColor {
            get { return GetAttribute(nameof(BackgroundColor)) as string ?? "@Gray"; }
        }
        public override void AttributeChangedCallback(string attributeName) {
            base.AttributeChangedCallback(attributeName);
            if(attributeName == "value")
                UpdateIndicator(MaxValue - MinValue, Value - MinValue);
        }
        protected override void UpdateBackground(double range, double value, DxHtmlElement element) {
            if(value > 0) 
                element.Style.SetBackgroundColor(BackgroundColor);
            else
                element.Style.SetBackgroundColor(Color.Empty);
        }
        protected override void UpdateProgress(double range, double value, DxHtmlElement element) {
            if(value > 0) {
                if(value < range * 0.333)
                    element.Style.SetBackgroundColor(WeakColor);
                else {
                    if(value < range * 0.666)
                        element.Style.SetBackgroundColor(MediumColor);
                    else
                        element.Style.SetBackgroundColor(StrongColor);
                }
                double progress = System.Math.Round(100 * (value / range));
                element.Style.SetProperty("width", progress.ToString() + "%");
            }
            else element.Style.SetBackgroundColor(Color.Empty);
        }
    }
}
