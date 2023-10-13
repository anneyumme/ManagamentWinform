namespace DevExpress.UITemplates.Collection.Editors {
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using UserManagament.Assets.Toolbox; 
    using DevExpress.UITemplates.Collection.Utilities;
    using DevExpress.Utils.Html;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Internal;
    using DevExpress.XtraEditors.Mask;
    using DevExpress.XtraEditors.Repository;

    [ToolboxItem(true), ToolboxBitmap(typeof(ToolboxBitmapRoot), "EditBox.bmp")]
    [Description("A styled text input field.")]
    [DevExpress.Utils.Design.SmartTagFilter(typeof(HtmlTextBoxBaseFilterWithMask))]
    public class EditBox : HtmlTextBoxBase {
        protected override RepositoryItem CreateRepositoryItemCore() {
            return new EditBoxProperties();
        }
        #region Properties
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new EditBoxProperties Properties {
            get { return base.Properties as EditBoxProperties; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [System.ComponentModel.Category("Appearance")]
        [Utils.Design.SmartTagProperty("LeadingIcon", "", 2, Utils.Design.SmartTagActionType.RefreshBoundsAfterExecute | Utils.Design.SmartTagActionType.RefreshContentAfterExecute)]
        public ContextImageOptions LeadingIconOptions {
            get { return Properties.LeadingIconOptions; }
        }
        [DefaultValue(CharacterCasing.Normal), System.ComponentModel.Category("Behavior")]
        public virtual CharacterCasing CharacterCasing {
            get { return Properties.CharacterCasing; }
            set { Properties.CharacterCasing = value; }
        }
        [DefaultValue(0), System.ComponentModel.Category("Text")]
        public virtual int MaxLength {
            get { return Properties.MaxLength; }
            set { Properties.MaxLength = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [RefreshProperties(RefreshProperties.All)]
        [Editor("DevExpress.XtraEditors.Design.MaskSettingsDesigner, " + AssemblyInfo.SRAssemblyEditorsDesignFull, typeof(System.Drawing.Design.UITypeEditor))]
        [System.ComponentModel.Category("Format")]
        public virtual MaskSettings MaskSettings {
            get { return Properties.MaskSettings; }
        }
        #endregion Properties
        public class EditBoxProperties : HtmlTextBoxBaseProperties {
            public override BaseEdit CreateEditor() {
                return new EditBox();
            }
            #region Icons
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public ContextImageOptions LeadingIconOptions {
                get { return ContextImageOptions; }
            }
            protected override ContextImageOptions CreateContextImageOptions() {
                return new EditBoxContextImageOptions();
            }
            sealed class EditBoxContextImageOptions : ContextImageOptions {
                public EditBoxContextImageOptions() {
                    SvgImageSize = new Size(16, 16);
                }
                protected override bool ShouldSerializeSvgImageSize() {
                    return base.ShouldSerializeSvgImageSize() && (SvgImageSize != new Size(16, 16));
                }
            }
            #endregion Icons
            #region Assets
            protected override string LoadDefaultStyles() {
                return EditBoxHtmlCssAsset.Default.Css;
            }
            protected override string LoadDefaultTemplate() {
                return EditBoxHtmlCssAsset.Default.Html;
            }
            sealed class EditBoxHtmlCssAsset : HtmlCssAsset {
                public static readonly HtmlCssAsset Default = new EditBoxHtmlCssAsset();
            }
            #endregion Style
        }
        protected override ICustomDxHtmlPreview CreateHtmlEditorPreview() {
            var previewControl = new EditBox();
            previewControl.Properties.BeginUpdate();
            previewControl.Properties.HeaderLabel = string.IsNullOrEmpty(HeaderLabel) ? "{HeaderLabel}" : HeaderLabel;
            previewControl.Properties.FooterLabel = string.IsNullOrEmpty(FooterLabel) ? "{FooterLabel}" : FooterLabel;
            previewControl.Properties.NullValuePrompt = string.IsNullOrEmpty(Placeholder) ? "{Placeholder}" : Placeholder;
            previewControl.Properties.MaxLength = MaxLength;
            previewControl.Properties.EndUpdate();
            return previewControl;
        }
    }
}
