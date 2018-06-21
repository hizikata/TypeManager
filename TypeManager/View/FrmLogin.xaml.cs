using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using TypeManager.DAL;
using TypeManager.Model;

namespace TypeManager.View
{
    /// <summary>
    /// FrmLogin.xaml 的交互逻辑
    /// </summary>
    public partial class FrmLogin : Window
    {
        #region Fields
        LoginService LoginSer = new LoginService();
        User CurrentUser = new User();
        #endregion
        public FrmLogin()
        {
            InitializeComponent();
            Messenger.Default.Register<object>(this, "Login", obj => Login(obj));
            this.tbUserId.Focus();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }
        void Login(object obj)
        {
            if (string.IsNullOrEmpty(tbUserId.Text.Trim()) || string.IsNullOrEmpty(pdbPwd.Password.Trim()))
            {
                MessageBox.Show("请输入用户名和密码", "系统提示");
                return;
            }
            if (LoginSer.Validation(tbUserId.Text.Trim(), pdbPwd.Password.Trim()))
            {
                CurrentUser = LoginSer.GetUserModel(tbUserId.Text.Trim());
                if((CurrentUser.RoleName=="admin"||CurrentUser.GroupName=="开发部")&&
                    (CurrentUser.UserName=="段飞龙"||CurrentUser.UserName=="束益梅"||CurrentUser.UserName=="徐霄忠"))
                {
                    FrmMain frmMain = new FrmMain(CurrentUser);
                    frmMain.Show();
                    
                }
                else
                {
                    MessageBox.Show("非系统管理员或开发部人员无法登陆系统", "系统提示");
                }
                this.Close();
                
            }
            else
            {
                MessageBox.Show("用户名或密码错误", "系统提示");
                this.pdbPwd.Clear();
                this.pdbPwd.Focus();
            }
        }

        private void tbUserId_KeyUp(object sender, KeyEventArgs e)
       {
            if (e.Key == Key.Enter && this.tbUserId.Text.Trim() != string.Empty)
            {
                this.pdbPwd.Focus();
            }
        }

        private void pdbPwd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && this.pdbPwd.Password.Trim() != string.Empty)
            {
                this.btnLogin.Focus();
            }
        }
    }
}
