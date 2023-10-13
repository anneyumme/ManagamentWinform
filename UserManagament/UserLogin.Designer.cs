namespace UserManagament
{
    partial class UserLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(this.components);
            this.userNameBox = new DevExpress.UITemplates.Collection.Editors.EditBox();
            this.passwordBox = new DevExpress.UITemplates.Collection.Editors.PasswordBox();
            this.btn_Login = new DevExpress.UITemplates.Collection.Editors.Button();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::UserManagament.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userNameBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.passwordBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Brush Script MT", 35F);
            this.label1.Location = new System.Drawing.Point(273, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 72);
            this.label1.TabIndex = 3;
            this.label1.Text = "Login form";
            // 
            // svgImageCollection1
            // 
            this.svgImageCollection1.Add("actions_checkcircled", "image://svgimages/icon builder/actions_checkcircled.svg");
            this.svgImageCollection1.Add("security_warningcircled2", "image://svgimages/icon builder/security_warningcircled2.svg");
            // 
            // userNameBox
            // 
            this.userNameBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.userNameBox.HeaderLabel = "Your username";
            this.userNameBox.HtmlTemplate.Styles = resources.GetString("userNameBox.HtmlTemplate.Styles");
            this.userNameBox.LeadingIconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("userNameBox.LeadingIconOptions.SvgImage")));
            this.userNameBox.Location = new System.Drawing.Point(225, 165);
            this.userNameBox.Margin = new System.Windows.Forms.Padding(6);
            this.userNameBox.Name = "userNameBox";
            this.userNameBox.Placeholder = "Enter your username";
            this.userNameBox.Size = new System.Drawing.Size(336, 42);
            this.userNameBox.TabIndex = 0;
            // 
            // passwordBox
            // 
            this.passwordBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.passwordBox.HeaderLabel = "Your password";
            this.passwordBox.HtmlTemplate.Styles = resources.GetString("passwordBox.HtmlTemplate.Styles");
            this.passwordBox.Location = new System.Drawing.Point(225, 247);
            this.passwordBox.Margin = new System.Windows.Forms.Padding(6);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Placeholder = "Enter your password";
            this.passwordBox.Size = new System.Drawing.Size(341, 42);
            this.passwordBox.TabIndex = 4;
            // 
            // btn_Login
            // 
            this.btn_Login.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Login.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.btn_Login.Appearance.Options.UseFont = true;
            this.btn_Login.HtmlTemplate.Styles = resources.GetString("btn_Login.HtmlTemplate.Styles");
            this.btn_Login.Location = new System.Drawing.Point(225, 329);
            this.btn_Login.Margin = new System.Windows.Forms.Padding(6);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(341, 55);
            this.btn_Login.TabIndex = 1;
            this.btn_Login.Text = "Log in";
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // UserLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 518);
            this.Controls.Add(this.userNameBox);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "UserLogin";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.UserLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userNameBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.passwordBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.UITemplates.Collection.Editors.EditBox userNameBox;
        private DevExpress.UITemplates.Collection.Editors.Button btn_Login;
        private System.Windows.Forms.Label label1;
        private DevExpress.UITemplates.Collection.Editors.PasswordBox passwordBox;
        private DevExpress.Utils.SvgImageCollection svgImageCollection1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}

