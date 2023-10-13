using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.Utils.CommonDialogs;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.UITemplates.Collection.Utilities;
using UserManagament.Model;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace UserManagament
{
    public partial class Edit_form : DevExpress.XtraEditors.XtraForm
    {

        BindingSource bindingSource_editForm;
        UserManagament.Model.MidTermEntities dbContext_editForm;
        string textEditFirstName_default;
        string textEditAge_default;
        string textEditLastName_default;
        string textEditAddress_default;
        string textEditPhoneNumber_default;
        string textEditEmail_default;
        bool buttonSaveWasClicked = false;
        bool buttonCancelWasClicked = false;
        bool buttonAddWasClicked = false;
        bool passwordBoxIsEnable = false;

        public Edit_form()
        {
            InitializeComponent();
          
        }
        public Edit_form(BindingSource bindingSource, UserManagament.Model.MidTermEntities dbContext, String caption,
            Boolean isVisible = false, Boolean isAddButtonClicked =  false)      
        {
            InitializeComponent();
            dbContext_editForm = dbContext;
            this.Text = caption;
            bindingSource_editForm = bindingSource;
            btn_cancel.Enabled = isVisible;
            passwordBox_EditForm.Enabled = isVisible;
            passwordBoxIsEnable = isVisible;
            buttonAddWasClicked = isAddButtonClicked;

            //==================== Tiến hành binding data giữa Main form (Ribbon Form) và Edit Form sử dụng kỹ thuật Binding Source.
            textEditFirstName.DataBindings.Add("Text", bindingSource_editForm, "First_name");
            textEditAge.DataBindings.Add("Text", bindingSource_editForm, "Age");
            textEditLastName.DataBindings.Add("Text", bindingSource_editForm, "Last_name");
            textEditAddress.DataBindings.Add("Text", bindingSource_editForm, "Address");
            textEditEmail.DataBindings.Add("Text", bindingSource_editForm, "Email");
            textEditPhoneNumber.DataBindings.Add("Text", bindingSource_editForm, "Phone_number");

            //==================== Do trong table Log History có nhiều dòng cần xuât ra, nên không thể sử dụng Binding Source
            // Nên phải sử dụng db set

            var user = bindingSource_editForm.Current as User_List;
            var loginHistoryText = string.Join(Environment.NewLine, user.Login_History.Select(lh => lh.Info_login));
            memoEdit1.Text = loginHistoryText;

            //==================== Hiện thị hình ảnh lên Edit Picture thông qua Binary type từ Database

            if (user.User_image != null)
            {
                using (var ms = new MemoryStream(user.User_image))
                {
                    pictureEdit1.Image = Image.FromStream(ms);
                }
            }
           
        }

        private void employeeDetailsSimple1_Load(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            // Save sẽ lưu mọi thứ kể cả hình ảnh

            buttonSaveWasClicked = true;
            try
            {
                var user = bindingSource_editForm.Current as User_List;
                string fn;
                fn = xtraOpenFileDialog1.FileName;
                // Convert img to binary
                if (!String.IsNullOrEmpty(fn))  // Kiểm tra hình ảnh có được người dùng chọn hay không
                {
                    byte[] img;
                    img = System.IO.File.ReadAllBytes(fn); // Convert image sang binary
                    // Save to database
                    user.User_image = img;
                }
                //Save password hash

                if (passwordBoxIsEnable)
                {
                    String password = passwordBox_EditForm.Text;
                    String passwordHash = SHA256Password.HashPassword(password);

                    user.User_pass = passwordHash;
                }

                this.bindingSource_editForm.EndEdit(); // Thực hiện lưu "mọi thứ" vào database (tìm hiểu Binding source sẽ rõ)
                dbContext_editForm.SaveChanges();
                XtraMessageBoxArgs args = new XtraMessageBoxArgs()
                {

                    Caption = "Notification",
                    Text = "Save Successfully",
                    ImageOptions = new MessageBoxImageOptions()
                    {
                        SvgImage = svgImageCollection1[0],
                        SvgImageSize = new Size(24, 24),
                    }
                };
                XtraMessageBox.Show(args);
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBoxArgs args_error = new XtraMessageBoxArgs()
                {

                    Caption = "Error",
                    Text = "An error occured while saving, please try again.",
                    ImageOptions = new MessageBoxImageOptions()
                    {
                        SvgImage = svgImageCollection1[1],
                        SvgImageSize = new Size(24, 24),
                    }
                };
                XtraMessageBox.Show(args_error);
            }

         
        }
        private void btn_resetChanges_Click(object sender, EventArgs e)
        {
            resetChange();
        }

        private void btn_clearAll_Click(object sender, EventArgs e)
        {
            textEditFirstName.Text = String.Empty;
            textEditLastName.Text = String.Empty;
            textEditEmail.Text = String.Empty;
            textEditPhoneNumber.Text = String.Empty;
            textEditAge.Text = String.Empty;  
            textEditAddress.Text = String.Empty;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            bindingSource_editForm.RemoveCurrent();
            buttonCancelWasClicked = true;
            this.Close();
        }


        private ManualResetEvent splashClosedEvent = new ManualResetEvent(false);

        private void btn_uploadImage_Click(object sender, EventArgs e)
        {

            // Sử dụng thread để dùng wait form để đợi process

            Thread splashThread = new Thread(() =>
            {
                splashScreenManager2.ShowWaitForm();

                splashClosedEvent.WaitOne();

                this.Invoke(new MethodInvoker(() =>
                {
                    splashScreenManager2.CloseWaitForm();
                }));
            });

            splashThread.Start();

            this.Invoke(new MethodInvoker(() =>
            {
                string fn;
                xtraOpenFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
                splashClosedEvent.Set();
                this.xtraOpenFileDialog1.ShowDialog();
                fn = xtraOpenFileDialog1.FileName;
                if (fn != String.Empty)
                {
                    this.pictureEdit1.Image = Image.FromFile(fn);
                }
            }));
            splashThread.Abort();
        }



        public void resetChange()
        {
            textEditFirstName.Text = textEditFirstName_default;
            textEditAge.Text = textEditAge_default;
            textEditLastName.Text = textEditLastName_default;
            textEditAddress.Text = textEditAddress_default;
            textEditPhoneNumber.Text = textEditPhoneNumber_default;
            textEditEmail.Text = textEditEmail_default;
        }

       

        private void Edit_form_Load(object sender, EventArgs e)
        {
            //==================== Load default text
            textEditFirstName_default = textEditFirstName.Text;
            textEditAge_default = textEditAge.Text;
            textEditLastName_default = textEditLastName.Text;
            textEditAddress_default = textEditAddress.Text;
            textEditPhoneNumber_default = textEditPhoneNumber.Text;
            textEditEmail_default = textEditEmail.Text;
        }

      

        private void Edit_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (buttonSaveWasClicked || buttonCancelWasClicked)
            {
                return;
            }
            else if (buttonAddWasClicked)
            {
                bindingSource_editForm.RemoveCurrent();
                return;
            }
            else
            {
                resetChange();
            }
        }

        private void passwordBox_EditForm_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}