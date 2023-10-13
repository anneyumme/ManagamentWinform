using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Mvvm.Native;

namespace UserManagament
{
    public partial class UserLogin : DevExpress.XtraEditors.XtraForm
    {
        UserManagament.Model.MidTermEntities dbContext = new Model.MidTermEntities();
        public UserLogin()
        {
            InitializeComponent();
        }


        private void btn_Login_Click(object sender, EventArgs e)
        {
            var user = dbContext.User_List.FirstOrDefault(id => userNameBox.Text == id.First_name);
            if (user != null)
            {
                if (SHA256Password.HashPassword(passwordBox.Text) == user.User_pass)
                {
                    XtraMessageBox.Show(NotificationArgs.Args(svgImageCollection1,0, 
                        "Login successful!", "Successful"));
                    splashScreenManager1.ShowWaitForm();
                    LoginInformation loginInformation= new LoginInformation();
                    loginInformation.Record(user);
                    this.Hide();
                    RibbonForm1 ribbonForm1 = new RibbonForm1(user);
                    splashScreenManager1.CloseWaitForm();
                    ribbonForm1.Show();
                }
                else
                {
                    XtraMessageBox.Show(NotificationArgs.Args(svgImageCollection1,
                        1, "Something went wrong with your credential!", "Error"));
                }
            }
            else
            {
                XtraMessageBox.Show(NotificationArgs.Args(svgImageCollection1,
                    1, "Something went wrong with your credential!", "Error"));
            }
        }
        private void UserLogin_Load(object sender, EventArgs e)
        {
        }
    }
}
