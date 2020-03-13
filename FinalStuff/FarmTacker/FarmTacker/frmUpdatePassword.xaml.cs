using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FarmTacker
{
    /// <summary>
    /// Interaction logic for frmUpdatePassword.xaml
    /// </summary>
    public partial class frmUpdatePassword : Window
    {
        User _user = null;
        IUserManager _userManager = null;
        public frmUpdatePassword(User user, IUserManager manager)
        {
            InitializeComponent();
            _user = user;
            _userManager = manager;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = pwdOldPassword.Password;
            string newPassword = pwdNewPassword.Password;
            string reEnterPassword = pwdRetypePassword.Password;

            if (oldPassword.Length < 7)
            {
                MessageBox.Show("invalid Password entry");
                pwdNewPassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }
            if (newPassword.Length < 7)
            {
                MessageBox.Show("invalid Password entry");
                pwdNewPassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }
            if (newPassword != reEnterPassword)
            {
                MessageBox.Show("retyped password did not match the new password.");
                pwdNewPassword.Password = "";
                pwdRetypePassword.Password = "";
                pwdNewPassword.Focus();
                return;
            }

            try
            {
                if (_userManager.ResetPasword(_user.UserID, pwdOldPassword.Password.ToString(),
                    pwdNewPassword.Password.ToString()))
                {
                    MessageBox.Show("Password was successfully changed.");
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Reset failed.");
                    this.DialogResult = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
                this.DialogResult = false;
            }
        }
    }
}

