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
    /// Interaction logic for frmUser.xaml
    /// </summary>
    public partial class frmUser : Window        
    {
        private User _user = null;
        private IUserManager _userManager = null;
        private bool _addMode = true;
        public frmUser(IUserManager userManager)
        {
            InitializeComponent();
            _userManager = userManager;            
        }
        public frmUser(User user)
        {
            _user = user;
            InitializeComponent();
            
        }
        public frmUser(User user, IUserManager userManager)
        {
            _user = user;            
            _userManager = userManager;
            _addMode = false;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserID.IsReadOnly = true;
            if (_addMode == false)
            {
                txtUserID.Text = _user.UserID.ToString();
                txtFirstName.Text = _user.FirstName;
                txtLastName.Text = _user.LastName;
                txtEmail.Text = _user.Email;
                txtPhoneNumber.Text = _user.PhoneNumber;
                chkActive.IsChecked = _user.Active;

                txtUserID.IsReadOnly = true;
                txtFirstName.IsReadOnly = true;
                txtLastName.IsReadOnly = true;
                txtPhoneNumber.IsReadOnly = true;
                txtEmail.IsReadOnly = true;
                chkActive.IsEnabled = false;


                populateRoles();
            }
            else
            {
                chkActive.IsChecked = true;
                chkActive.IsEnabled = false;
                txtFirstName.Focus();
                btnEdit.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Visible;
            }
        }

        private void populateRoles()
        {
            try
            {
                var UserRoles = _userManager.RetreiveUserRoles(_user.UserID);
                lstUserRoles.ItemsSource = UserRoles;
                var UARoles = _userManager.RetreiveUserRoles();
                foreach (string r in UserRoles)
                {
                    UARoles.Remove(r);
                }
                lstUnassignedRoles.ItemsSource = UARoles;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            
            txtFirstName.IsReadOnly = false;
            txtLastName.IsReadOnly = false;
            txtPhoneNumber.IsReadOnly = false;
            txtEmail.IsReadOnly = false;
            chkActive.IsEnabled = true;

            lstUserRoles.IsEnabled = true;
            lstUnassignedRoles.IsEnabled = true;

            txtFirstName.Focus();
            btnEdit.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtFirstName.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid first name!");
                txtFirstName.Focus();
                return;
            }
            if (txtLastName.Text.ToString() == "")
            {
                MessageBox.Show("you must enter a valid Last name");
                txtLastName.Focus();
                return;
            }
            if (!(txtEmail.Text.ToString().Length > 6 && txtEmail.Text.ToString().Contains("@") && txtEmail.Text.ToString().Contains("@")))
            {
                MessageBox.Show("you must enter a valid email address");
                txtEmail.Focus();
                return;
            }
            if (txtPhoneNumber.Text.ToString().Length < 10 || txtPhoneNumber.Text.ToString().Contains(" "))
            {
                MessageBox.Show("you must enter a valid Phone number");
                txtPhoneNumber.Focus();
                return;
            }
            User user = new User() 
            {
                FirstName = txtFirstName.Text.ToString(),
                LastName = txtLastName.Text.ToString(),
                PhoneNumber = txtPhoneNumber.Text.ToString(),
                Email = txtEmail.Text.ToString()
            };
            if (_addMode)
            {
                try
                {
                    if (_userManager.AddUser(user))
                    {
                        this.DialogResult = true;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
                }
            }
            else
            {
                try
                {
                    if(_userManager.EditUser(_user, user))
                    {
                        this.DialogResult = true;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n" + ex.InnerException.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void chkActive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string caption = (bool)chkActive.IsChecked ?
                    "Reactivate this User" : "Deactivate this User";
                if (MessageBox.Show("Are You Sure?", caption, MessageBoxButton.YesNo, MessageBoxImage.Warning)== MessageBoxResult.No)
                {
                    chkActive.IsChecked = (bool)chkActive.IsChecked;
                    return;
                }
                _userManager.SetUserActiveState((bool)chkActive.IsChecked, _user.UserID);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }


        }

        private void lstUnassignedRoles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_addMode || lstUnassignedRoles.SelectedItems.Count == 0) 
            {
                return;
            }
            if (MessageBox.Show("Are You Sure?", "Change Role Assignment", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            try
            {
                if (_userManager.addUserRole(_user.UserID, (string)lstUnassignedRoles.SelectedItem))
                {
                    populateRoles();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void lstUserRoles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_addMode|| lstUserRoles.SelectedItems.Count == 0)
            {
                return;
            }
            if ((MessageBox.Show("Are You Sure?", "Change Role Assignment", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No))
            {
                return;
            }
            try
            {
                if (_userManager.deleteUserRole(_user.UserID, (string)lstUserRoles.SelectedItem))
                {
                    populateRoles();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message); 
            }
        }
    }
}
