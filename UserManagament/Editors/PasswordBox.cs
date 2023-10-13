namespace DevExpress.UITemplates.Collection.Editors {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using UserManagament.Assets.Toolbox; 
    using DevExpress.UITemplates.Collection.Images;
    using DevExpress.UITemplates.Collection.Utilities;
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Drawing;
    using DevExpress.Utils.Html;
    using DevExpress.Utils.Svg;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Internal;
    using DevExpress.XtraEditors.Repository;

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ToolboxBitmapRoot), "PasswordBox.bmp")]
    [Description("A masked field used to enter passwords with integrated password strength indication.")]
    public class PasswordBox : HtmlTextBoxBase {
        protected override RepositoryItem CreateRepositoryItemCore() {
            return new PasswordBoxProperties();
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new PasswordBoxProperties Properties {
            get { return base.Properties as PasswordBoxProperties; }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Password {
            get { return EditValue as string; }
            set { EditValue = value; }
        }
        [Browsable(false)]
        public bool IsPasswordWeak {
            get {
                var strength = Utilities.Password.Check(EditValue as string);
                return strength > Utilities.Password.Strength.Blank && strength < Utilities.Password.Strength.Strong;
            }
        }
        #region Properties
        [DefaultValue(false), System.ComponentModel.Category(CategoryName.Appearance)]
        public bool ShowStrengthIndicator {
            get { return Properties.ShowStrengthIndicator; }
            set { Properties.ShowStrengthIndicator = value; }
        }
        [System.ComponentModel.Category(CategoryName.ToolTip)]
        public bool ShowPasswordWeaknessWarningToolTip {
            get { return Properties.ShowPasswordWeaknessWarningToolTip; }
            set { Properties.ShowPasswordWeaknessWarningToolTip = value; }
        }
        bool ShouldSerializeShowPasswordWeaknessWarningToolTip() {
            return Properties.ShouldSerializeShowPasswordWeaknessWarningToolTip();
        }
        void ResetShowPasswordWeaknessWarningToolTip() {
            Properties.ResetShowPasswordWeaknessWarningToolTip();
        }
        [System.ComponentModel.Category(CategoryName.ToolTip)]
        public bool ShowCapsLockWarningToolTip {
            get { return Properties.ShowCapsLockWarningToolTip; }
            set { Properties.ShowCapsLockWarningToolTip = value; }
        }
        bool ShouldSerializeShowCapsLockWarningToolTip() {
            return Properties.ShouldSerializeShowCapsLockWarningToolTip();
        }
        void ResetShowCapsLockWarningToolTip() {
            Properties.ResetShowCapsLockWarningToolTip();
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [System.ComponentModel.Category("Appearance")]
        public ContextImageOptions LeadingIconOptions {
            get { return Properties.ContextImageOptions; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [System.ComponentModel.Category("Appearance")]
        public ContextImageOptions TrailingIconOptions {
            get { return Properties.TrailingIconOptions; }
        }
        #endregion Properties
        protected override void OnGotFocus(EventArgs e) {
            base.OnGotFocus(e);
            if(Properties.ShowCapsLockWarningToolTip && IsCapsLockOn)
                QueueWarningToolTip(new Action(ShowCapsLockWarning));
        }
        protected override void OnEditValueChanging(ChangingEventArgs e) {
            base.OnEditValueChanging(e);
            if(!e.Cancel && !IsPasswordWeak)
                HideLeadingIconTooltip();
        }
        protected override void OnValidatingCore(CancelEventArgs e) {
            base.OnValidatingCore(e);
            if(e.Cancel || !IsModified)
                return;
            if(Properties.ShowPasswordWeaknessWarningToolTip && IsPasswordWeak)
                QueueWarningToolTip(new Action(ShowPasswordWeaknessWarning));
        }
        protected virtual void ShowCapsLockWarning() {
            var controller = GetToolTipController();
            if(controller == null)
                return;
            string title = "Caps Lock is On";
            string text = "Having Caps Lock on may cause you to enter your password incorrectly. You should press Caps Lock to turn it off before entering your password.";
            var showArgs = new ToolTipControllerShowEventArgs(this, this, text, title, WarningToolTipLocation, false, false, 0, ToolTipIconType.Warning, ToolTipIconSize.Small, null, -1, controller.Appearance, controller.AppearanceTitle);
            showArgs.ToolTipType = ToolTipType.SuperTip;
            if(WarningTooltipAnchor == EditToolTipAnchor.LeadingIcon)
                showArgs.ObjectBounds = RectangleToScreen(Properties.GetLeadingIconBounds());
            ShowCapsLockWarning(controller, showArgs);
        }
        protected virtual void ShowCapsLockWarning(ToolTipController controller, ToolTipControllerShowEventArgs showArgs) {
            ShowLeadingIconTooltip(controller, showArgs);
        }
        protected virtual void ShowPasswordWeaknessWarning() {
            var controller = GetToolTipController();
            if(controller == null)
                return;
            string title = "Password is Weak";
            string text = "A strong password should contain at least 8 characters, including upper and lowercase letters, digits and special characters.";
            var showArgs = new ToolTipControllerShowEventArgs(this, this, text, title, WarningToolTipLocation, false, false, 0, ToolTipIconType.Warning, ToolTipIconSize.Small, null, -1, controller.Appearance, controller.AppearanceTitle);
            showArgs.ToolTipType = ToolTipType.SuperTip;
            if(WarningTooltipAnchor == EditToolTipAnchor.LeadingIcon)
                showArgs.ObjectBounds = RectangleToScreen(Properties.GetLeadingIconBounds());
            ShowPasswordWeaknessWarning(controller, showArgs);
        }
        protected virtual void ShowPasswordWeaknessWarning(ToolTipController controller, ToolTipControllerShowEventArgs showArgs) {
            ShowLeadingIconTooltip(controller, showArgs);
        }
        bool QueueWarningToolTip(Action action = null) {
            if(action == null && Properties.ShowPasswordWeaknessWarningToolTip && IsPasswordWeak)
                action = new Action(ShowPasswordWeaknessWarning);
            if(action == null && Properties.ShowCapsLockWarningToolTip && IsCapsLockOn)
                action = new Action(ShowCapsLockWarning);
            if(action != null && IsHandleCreated) 
                BeginInvoke(action);
            return (action != null);
        }
        protected override bool QueryLeadingIconTooltip() {
            return QueueWarningToolTip();
        }
        public class PasswordBoxProperties : HtmlTextBoxBaseProperties {
            ContextImageOptions trailingIconOptionsCore;
            readonly Action<object> passwordShowingActionRef;
            public PasswordBoxProperties() {
                trailingIconOptionsCore = CreateTrailingIconOptions();
                passwordShowingActionRef = new Action<object>(OnPasswordShowing);
            }
            protected override void Dispose(bool disposing) {
                CancelPasswordShowing();
                DevExpress.Utils.DisposeHelper.Dispose(ref trailingIconOptionsCore);
                base.Dispose(disposing);
            }
            public override BaseEdit CreateEditor() {
                return new PasswordBox();
            }
            protected override bool IsPasswordBox {
                get { return true; }
            }
            [DefaultValue('\u25CF')]
            public override char PasswordChar {
                get { return (fPasswordChar == '\0') ? '\u25CF' : fPasswordChar; }
                set { base.PasswordChar = value; }
            }
            bool showStrengthIndicatorCore = false;
            [DefaultValue(false)]
            public bool ShowStrengthIndicator {
                get { return showStrengthIndicatorCore; }
                set {
                    if(showStrengthIndicatorCore == value) return;
                    showStrengthIndicatorCore = value;
                    OnPropertiesChanged(new PropertyChangedEventArgs(nameof(ShowStrengthIndicator)));
                }
            }
            bool? showPasswordWeaknessWarningToolTipCore;
            public bool ShowPasswordWeaknessWarningToolTip {
                get { return showPasswordWeaknessWarningToolTipCore.GetValueOrDefault(ShowStrengthIndicator); }
                set {
                    if(ShowPasswordWeaknessWarningToolTip == value) return;
                    showPasswordWeaknessWarningToolTipCore = value;
                    OnPropertiesChanged(new PropertyChangedEventArgs(nameof(ShowPasswordWeaknessWarningToolTip)));
                }
            }
            protected internal bool ShouldSerializeShowPasswordWeaknessWarningToolTip() {
                return showPasswordWeaknessWarningToolTipCore.HasValue;
            }
            protected internal void ResetShowPasswordWeaknessWarningToolTip() {
                showPasswordWeaknessWarningToolTipCore = null;
            }
            bool? showCapsLockWarningToolTipCore;
            public bool ShowCapsLockWarningToolTip {
                get { return showCapsLockWarningToolTipCore.GetValueOrDefault(true); }
                set {
                    if(ShowCapsLockWarningToolTip == value) return;
                    showCapsLockWarningToolTipCore = value;
                    OnPropertiesChanged(new PropertyChangedEventArgs(nameof(ShowCapsLockWarningToolTip)));
                }
            }
            protected internal bool ShouldSerializeShowCapsLockWarningToolTip() { 
                return showCapsLockWarningToolTipCore.HasValue; 
            }
            protected internal void ResetShowCapsLockWarningToolTip() {
                showCapsLockWarningToolTipCore = null;
            }
            #region Icons
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public ContextImageOptions TrailingIconOptions {
                get { return trailingIconOptionsCore; }
            }
            protected virtual ContextImageOptions CreateTrailingIconOptions() {
                return new PasswordTrailingIconOptions(this);
            }
            protected override ContextImageOptions CreateContextImageOptions() {
                return new PasswordLeadingIconOptions();
            }
            abstract class PasswordBoxIconOptions : ContextImageOptions {
                protected PasswordBoxIconOptions() {
                    base.SvgImage = GetDefaultIcon();
                    SvgImageSize = GetDefaultIconSize();
                }
                protected virtual Size GetDefaultIconSize() {
                    return new Size(16, 16);
                }
                protected override bool ShouldSerializeSvgImage() {
                    return base.ShouldSerializeSvgImage() && !ReferenceEquals(SvgImage, GetDefaultIcon());
                }
                protected override bool ShouldSerializeSvgImageSize() {
                    return base.ShouldSerializeSvgImageSize() && (SvgImageSize != GetDefaultIconSize());
                }
                protected abstract SvgImage GetDefaultIcon();
            }
            sealed class PasswordLeadingIconOptions : PasswordBoxIconOptions {
                protected override SvgImage GetDefaultIcon() {
                    return DataEntry.Key;
                }
            }
            sealed class PasswordTrailingIconOptions : PasswordBoxIconOptions {
                readonly PasswordBoxProperties owner;
                readonly SvgBitmap SvgBitmapInverted;
                public PasswordTrailingIconOptions(PasswordBoxProperties owner) {
                    this.owner = owner;
                    this.SvgBitmapInverted = SvgBitmap.Create(UIElements.Hide);
                }
                protected override SvgImage GetDefaultIcon() {
                    return UIElements.Show;
                }
                protected override Image GetImageFromSvg(Size userSvgImageSize, bool scaleSvgImageSize, ISvgPaletteProvider palette) {
                    palette = CheckSvgPalette(palette);
                    if(!owner.IsPasswordVisible)
                        return SvgBitmap.Render(GetSvgImageSize(SvgImage, SvgImageSize, userSvgImageSize, scaleSvgImageSize, ForcedScaleFactor), palette);
                    return SvgBitmapInverted.Render(GetSvgImageSize(UIElements.Hide, SvgImageSize, userSvgImageSize, scaleSvgImageSize, ForcedScaleFactor), palette);
                }
            }
            #endregion Icons
            #region Parts
            protected readonly static object strengthIndicator = new object();
            //
            protected override void FindParts(Dictionary<object, DxHtmlElement> parts, DxHtmlRootElement root) {
                trailingIconElement = parts[trailingIcon] = root.FindElementById(nameof(trailingIcon));
                parts[strengthIndicator] = root.FindElementById(nameof(strengthIndicator));
                base.FindParts(parts, root);
            }
            string trailingIconClassNameOriginal;
            protected override void InitializePartsOriginalClassNames() {
                if(trailingIconElement != null)
                    trailingIconClassNameOriginal = trailingIconElement.ClassName;
                base.InitializePartsOriginalClassNames();
            }
            DxHtmlElement trailingIconElement;
            protected override void SetEditorSizeForParts(EditSize value) {
                var sizeClassModifier = value.ToString().ToLowerInvariant();
                if(trailingIconElement != null)
                    trailingIconElement.ClassName = trailingIconClassNameOriginal + " trailing-icon-" + sizeClassModifier;
                base.SetEditorSizeForParts(value);
            }
            protected override void CalcPartsVisibility(Dictionary<object, DxHtmlElement> parts, object editValue) {
                base.CalcPartsVisibility(parts, editValue);
                parts[trailingIcon].Hidden = !IsTrailingIconVisible(editValue);
                parts[strengthIndicator].Hidden = !IsStrengthIndicatorVisible(editValue);
            }
            protected override Rectangle CalcPartBounds(Dictionary<object, DxHtmlElement> parts, object partKey) {
                if(Equals(partKey, trailingIcon))
                    return parts[trailingIcon].AbsoluteBounds;
                return base.CalcPartBounds(parts, partKey);
            }
            protected override object GetPartImage(string partName, ObjectState state) {
                if(partName == "TrailingIcon")
                    return TrailingIconOptions.GetImage(LookAndFeel, state);
                return base.GetPartImage(partName, state);
            }
            protected virtual bool IsTrailingIconVisible(object editValue) {
                return TrailingIconOptions.HasImage && !IsEmptyValue(editValue);
            }
            protected virtual bool IsStrengthIndicatorVisible(object editValue) {
                return ShowStrengthIndicator;
            }
            protected override bool OnPartClick(DxHtmlElementMouseEventArgs args) {
                if(IsPasswordVisibilityIcon(args))
                    return TogglePasswordVisibility();
                return base.OnPartClick(args);
            }
            bool IsPasswordVisibilityIcon(DxHtmlElementEventArgs args) {
                return IsTrailingIcon(args);
            }
            int isPasswordVisibilityIconPressed;
            protected override bool OnPartMouseDown(DxHtmlElementMouseEventArgs args) {
                if(IsPasswordVisibilityIcon(args)) {
                    if(0 == isPasswordVisibilityIconPressed++) {
                        if(!isPasswordVisibleByVisibilityMode) StartPasswordShowing();
                    }
                }
                return base.OnPartMouseDown(args);
            }
            protected override bool OnPartMouseUp(DxHtmlElementMouseEventArgs args) {
                if(IsPasswordVisibilityIcon(args)) {
                    if(--isPasswordVisibilityIconPressed == 0) {
                        CancelPasswordShowing();
                        UpdatePasswordVisibility();
                    }
                }
                return base.OnPartMouseUp(args);
            }
            protected bool IsPasswordVisible {
                get { return isPasswordVisibleByTimer || isPasswordVisibleByVisibilityMode; }
            }
            void CancelPasswordShowing() {
                DevExpress.Utils.DTimer.Current.RemoveTimer(passwordShowingActionRef);
                isPasswordVisibleByTimer = false;
            }
            bool isPasswordVisibleByVisibilityMode, isPasswordVisibleByTimer;
            protected virtual void StartPasswordShowing() {
                DevExpress.Utils.DTimer.Current.AddTimer(passwordShowingActionRef, 500);
            }
            void OnPasswordShowing(object sender) {
                DevExpress.Utils.DTimer.Current.RemoveTimer(passwordShowingActionRef);
                isPasswordVisibleByTimer = (isPasswordVisibilityIconPressed > 0);
                UpdatePasswordVisibility();
            }
            protected virtual bool TogglePasswordVisibility() {
                if(!isPasswordVisibleByTimer) {
                    CancelPasswordShowing();
                    isPasswordVisibleByVisibilityMode = !isPasswordVisibleByVisibilityMode;
                    UpdatePasswordVisibility();
                    return true;
                }
                return false;
            }
            void UpdatePasswordVisibility() {
                InvokeControllerAction(TogglePasswordVisibility);
            }
            void TogglePasswordVisibility(XtraEditors.TextEditController.TextEditController controller) {
                if(controller.ShowPassword == IsPasswordVisible)
                    return;
                controller.TogglePasswordVisibility();
                InvalidatePart(trailingIcon);
            }
            int prevStrength;
            protected override void RaiseEditValueChanged(EventArgs e) {
                base.RaiseEditValueChanged(e);
                var args = e as XtraEditors.Controls.ChangingEventArgs;
                if(args != null) {
                    int strength = (int)Utilities.Password.Check(args.NewValue as string);
                    if(strength != prevStrength)
                        UpdatePart(strengthIndicator, x => x.SetAttribute("value", strength.ToString()), true, true);
                    prevStrength = strength;
                }
            }
            protected internal Rectangle GetLeadingIconBounds() {
                return GetPartBounds(leadingIcon);
            }
            #endregion Parts
            #region Assets
            protected override string LoadDefaultStyles() {
                return PasswordBoxHtmlCssAsset.Default.Css;
            }
            protected override string LoadDefaultTemplate() {
                return PasswordBoxHtmlCssAsset.Default.Html;
            }
            sealed class PasswordBoxHtmlCssAsset : HtmlCssAsset {
                [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
                static PasswordBoxHtmlCssAsset() {
                    Components.StrengthIndicator.Register();
                }
                public static readonly HtmlCssAsset Default = new PasswordBoxHtmlCssAsset();
            }
            #endregion Style
        }
        protected override ICustomDxHtmlPreview CreateHtmlEditorPreview() {
            var previewControl = new PasswordBox();
            previewControl.Properties.BeginUpdate();
            previewControl.Properties.HeaderLabel = string.IsNullOrEmpty(HeaderLabel) ? "{HeaderLabel}" : HeaderLabel;
            previewControl.Properties.FooterLabel = string.IsNullOrEmpty(FooterLabel) ? "{FooterLabel}" : FooterLabel;
            previewControl.Properties.NullValuePrompt = string.IsNullOrEmpty(Placeholder) ? "{Placeholder}" : Placeholder;
            previewControl.Properties.EndUpdate();
            return previewControl;
        }
    }
}
