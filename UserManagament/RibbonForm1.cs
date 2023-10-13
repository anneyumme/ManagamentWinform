using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using UserManagament.Model;

namespace UserManagament
{
    public partial class RibbonForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        UserManagament.Model.MidTermEntities dbContext = new Model.MidTermEntities();
        public RibbonForm1()
        {
            InitializeComponent();
        }

        public RibbonForm1(User_List user)
        {
            InitializeComponent();

        }


        void tabbedView1_QueryControl(object sender, DevExpress.XtraBars.Docking2010.Views.QueryControlEventArgs e)
        {
           
        }

        private void RibbonForm1_Load(object sender, EventArgs e)
        {
            dbContext.User_List.Load();
            user_ListBindingSource.DataSource = dbContext.User_List.Local.ToBindingList();
        }

        private void btn_edit_ItemClick(object sender, ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            XtraMessageBoxArgs args_error = new XtraMessageBoxArgs()
            {
                Caption = "Error",
                Text = "There is no row to edit!",
                ImageOptions = new MessageBoxImageOptions()
                {
                    SvgImage = svgImageCollection1[1],
                    SvgImageSize = new Size(24, 24),
                }

            };
            if(this.user_ListBindingSource.Count == 0)
            {
                splashScreenManager1.CloseWaitForm();
                XtraMessageBox.Show(args_error);
                return;
            }
            else
            {
                Edit_form edit_Form = new Edit_form(user_ListBindingSource, dbContext, "Edit user");
                splashScreenManager1.CloseWaitForm();
                edit_Form.ShowDialog();
            }
            
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void btn_addNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            

            splashScreenManager1.ShowWaitForm();
         
            this.user_ListBindingSource.AddNew();
            Edit_form edit_Form = new Edit_form(user_ListBindingSource, dbContext, "Add new user", true, true);
            
            splashScreenManager1.CloseWaitForm();
            edit_Form.ShowDialog(this);
        }



        private void btn_SignOut_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
            UserLogin userLogin = new UserLogin();
            userLogin.Show();
        }

        private void btn_delete_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            XtraMessageBoxArgs args_conform = new XtraMessageBoxArgs()
            {
                Caption = "Warning",
                Text = "Do you want to delete this row ?",
                Buttons = new DialogResult[] { DialogResult.Yes, DialogResult.No },
                ImageOptions = new MessageBoxImageOptions()
                {
                    SvgImage = svgImageCollection1[0],
                    SvgImageSize = new Size(24, 24),
                }

            };
            if (this.user_ListBindingSource.Count == 0)
            {
                XtraMessageBox.Show(NotificationArgs.Args(svgImageCollection1, 1, "Error", "No record to delete!"));
            }
            else
            {
                if(XtraMessageBox.Show(args_conform)  == DialogResult.No)
                {
                    return;
                }

                // Perform action delete with relate row 
                // Find login history to delete of current user to delete
                var UserCurrent = user_ListBindingSource.Current as User_List;
                var LoginHistoryCurrent =
                    dbContext.Login_History.Where(lh => lh.ID_user == UserCurrent.ID);
                if (LoginHistoryCurrent != null)
                {
                    dbContext.Login_History.RemoveRange(LoginHistoryCurrent);
                }
                // Delete current user actually 

                user_ListBindingSource.RemoveCurrent();
                user_ListBindingSource.EndEdit();
                dbContext.SaveChanges();
            }
        }

        private void user_ListBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

       
    }
}