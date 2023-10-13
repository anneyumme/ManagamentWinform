namespace DevExpress.UITemplates.Collection.Components {
    using DevExpress.Utils.Html;

    public abstract class ProgressIndicatorBase : DxHtmlComponent {
        public double MinValue {
            get { return GetDouble(nameof(MinValue), 0); }
        }
        public double MaxValue {
            get { return GetDouble(nameof(MaxValue), 1.0); }
        }
        public double Value {
            get { return GetDouble(nameof(Value), 0); }
        }
        protected double GetDouble(string name, double defaultValue) {
            double value;
            return double.TryParse(GetAttribute(name) as string ?? string.Empty, out value) ? value : defaultValue;
        }
        protected virtual string GetDefaultHeight() {
            return "2px";
        }
        string CalcOwnHeight() {
            var height = Element.GetComputedStyle().Height;
            return height.IsEmpty ? GetDefaultHeight() : height.ToString();
        }
        DxHtmlElement bgElement, progressElement;
        public override void ConnectedCallback() {
            base.ConnectedCallback();
            double range = MaxValue - MinValue, value = Value - MinValue;
            double progress = System.Math.Round(100 * (value / range));
            string ownHeight = CalcOwnHeight();
            bgElement = CreateBackground(ownHeight);
            progressElement = CreateProgress(progress, ownHeight);
            UpdateBackground(range, value, bgElement);
            UpdateProgress(range, value, progressElement);
            AppendChild(bgElement);
            AppendChild(progressElement);
        }
        public override void DisconnectedCallback() {
            base.DisconnectedCallback();
            bgElement = progressElement = null;
        }
        protected virtual DxHtmlElement CreateBackground(string ownHeight) {
            var bgElement = DxHtmlDocument.CreateElement("div");
            bgElement.Style.SetProperty("width", "100%");
            bgElement.Style.SetProperty("height", ownHeight);
            return bgElement;
        }
        protected virtual DxHtmlElement CreateProgress(double progress, string ownHeight) {
            var progressElement = DxHtmlDocument.CreateElement("div");
            progressElement.Style.SetProperty("width", progress.ToString() + "%");
            progressElement.Style.SetProperty("height", ownHeight);
            progressElement.Style.SetProperty("margin-top", "-" + ownHeight);
            return progressElement;
        }
        protected void UpdateIndicator(double range, double value) {
            if(bgElement != null)
                UpdateBackground(range, value, bgElement);
            if(progressElement != null)
                UpdateProgress(range, value, progressElement);
        }
        protected abstract void UpdateBackground(double range, double value, DxHtmlElement element);
        protected abstract void UpdateProgress(double range, double value, DxHtmlElement element);
    }
}
