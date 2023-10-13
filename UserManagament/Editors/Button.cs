namespace DevExpress.UITemplates.Collection.Editors {
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using UserManagament.Assets.Toolbox; 
    using DevExpress.UITemplates.Collection.Utilities;
    using DevExpress.Utils;
    using DevExpress.Utils.Html;

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ToolboxBitmapRoot), "Button.bmp")]
    [Description("A styled, simple button.")]
    public class Button : ButtonBase, IButtonControl {
        DialogResult dialogResultCore;
        [System.ComponentModel.Category(XtraEditors.CategoryName.Behavior), DefaultValue(DialogResult.None)]
        public DialogResult DialogResult {
            get { return dialogResultCore; }
            set {
                if(dialogResultCore == value) return;
                dialogResultCore = value;
                OnPropertiesChanged();
            }
        }
        protected override void OnIconImageOptionsChanged(object sender, ImageOptionsChangedEventArgs args) {
            base.OnIconImageOptionsChanged(sender, args);
            if(!isSimpleButton)
                UpdatePartVisibility(icon);
            if(AutoSize) AdjustSize();
        }
        protected override void OnButtonSizeChanged(SizeType value) {
            base.OnButtonSizeChanged(value);
            if(!isSimpleButton)
                InvalidatePart(button);
            if(AutoSize) AdjustSize();
        }
        protected override void SetButtonElementSize(SizeType value) {
            if(!isSimpleButton && buttonElement != null)
                buttonElement.ClassName = buttonClassNameOriginal + " " + value.ToString().ToLowerInvariant();
        }
        bool isSimpleButton;
        protected readonly static object button = new object();
        string buttonClassNameOriginal;
        DxHtmlElement buttonElement;
        protected override void FindParts(Dictionary<object, DxHtmlElement> parts, DxHtmlRootElement root) {
            base.FindParts(parts, root);
            parts[button] = root.FindElementById(nameof(button));
            var simpleButtonElement = root.FindElementsByTag("dx-simple-button").FirstOrDefault();
            isSimpleButton = (simpleButtonElement != null);
            buttonElement = (parts[button] ?? simpleButtonElement);
            if(buttonElement != null)
                buttonClassNameOriginal = buttonElement.ClassName;
            SetButtonElementSize(ButtonSize);
        }
        protected override void CalcPartsVisibility(Dictionary<object, DxHtmlElement> parts) {
            if(!isSimpleButton) 
                base.CalcPartsVisibility(parts);
        }
        protected override bool OnGotFocus(Dictionary<object, DxHtmlElement> parts, DxHtmlRootElement root) {
            if(buttonElement != null)
                buttonElement.Focus(true);
            return (buttonElement != null);
        }
        protected override bool OnLostFocus(Dictionary<object, DxHtmlElement> parts, DxHtmlRootElement root) {
            if(buttonElement != null)
                buttonElement.Focus(false);
            return (buttonElement != null);
        }
        protected override bool IsIconVisible() {
            return !isSimpleButton && IconImageOptions.HasImage;
        }
        public void NotifyDefault(bool value) {
            // TODO
        }
        public void PerformClick() {
            PerformClickCore();
        }
        protected override void OnClick(System.EventArgs e) {
            Form form = FindForm();
            if(form != null)
                form.DialogResult = DialogResult;
            if(ShouldNotifyAccessibilityClients) {
                NotifyAccessibilityClients(AccessibleEvents.StateChange, -1);
                NotifyAccessibilityClients(AccessibleEvents.NameChange, -1);
            }
            base.OnClick(e);
        }
        #region Theme
        protected override string LoadDefaultTemplate() {
            return ButtonHtmlCssAsset.Default.Html;
        }
        protected override string LoadDefaultStyles() {
            return ButtonHtmlCssAsset.Default.Css;
        }
        sealed class ButtonHtmlCssAsset : HtmlCssAsset {
            public static readonly HtmlCssAsset Default = new ButtonHtmlCssAsset();
        }
        #endregion Theme
        protected override ICustomDxHtmlPreview CreateHtmlEditorPreview() {
            var previewControl = new Button();
            previewControl.Text = string.IsNullOrEmpty(Text) ? "{Text}" : Text;
            previewControl.IconImageOptions.Assign(IconImageOptions);
            return previewControl;
        }
    }
}
