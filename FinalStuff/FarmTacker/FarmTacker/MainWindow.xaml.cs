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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FarmTacker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUserManager _userManager;
        private IFarmManager _farmManager;
        private IFieldManager _fieldManager;
        private ICropManager _cropManager;
        private IMachineManager _machineManager;
        private IAssignmentManager _assignmentManager;
        private User _user = null;

        public MainWindow()
        {
            InitializeComponent();
            _userManager = new UserManager();
            _farmManager = new FarmManager();
            _fieldManager = new FieldManager();
            _cropManager = new CropManager();
            _machineManager = new MachineManager();
            _assignmentManager = new AssignmentManager();
        }

        private void hideAllUserTabs()
        {
            foreach (TabItem item in TabSetMain.Items)
            {
                item.Visibility = Visibility.Collapsed;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text;
            var password = pwdPassword.Password;

            if (btnLogin.Content.ToString() == "logout")
            {
                _user = null;
                hideAllUserTabs();
                txtEmail.Text = "";
                pwdPassword.Password = "";
                pwdPassword.IsEnabled = true;
                txtEmail.IsEnabled = true;
                btnLogin.Content = "login";
                lblPassword.Visibility = Visibility.Visible;
                lblEmail.Visibility = Visibility.Visible;
                txtEmail.Focus();
                lblStatusMessage.Content = "You are not logged in, Please log in";
                return;
            }
            if (email.Length<7||password.Length<7)
            {
                MessageBox.Show("Invalid email or password!", "invalid login", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                txtEmail.Text = "";
                pwdPassword.Password = "";
                txtEmail.Focus();
                return;
            }
            try
            {
                _user = _userManager.AuthenticateUser(email, password);

                string roles = "";
                
                for (int i = 0; i < _user.Roles.Count; i++)
                {
                    roles += _user.Roles[i];
                    if (i<_user.Roles.Count-1)
                    {
                        roles += ", ";
                    }
                }
                lblStatusMessage.Content = "Hello, " + _user.FirstName +
                    "You are logged in as: " + roles;
                if (pwdPassword.Password.ToString() == "newuser")
                {
                    var resetPassword = new frmUpdatePassword(_user, _userManager);
                    
                }
                txtEmail.Text = "";
                pwdPassword.Password = "";
                pwdPassword.IsEnabled = false;
                txtEmail.IsEnabled = false;
                btnLogin.Content = "logout";
                lblPassword.Visibility = Visibility.Hidden;
                lblEmail.Visibility = Visibility.Hidden;
                showUserTabs();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message, "login failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error); 
            }
        }

        private void showUserTabs()
        {
            foreach (var role in _user.Roles)
            {
                //check for each role
                switch (role)
                {
                    case "Admin":
                        tabAdmin.Visibility = Visibility.Visible;
                        tabFarms.Visibility = Visibility.Visible;
                        tabMachines.Visibility = Visibility.Visible;
                        tabCrops.Visibility = Visibility.Visible;
                        tabAssignments.Visibility = Visibility.Visible;
                        tabColor.Visibility = Visibility.Collapsed;
                        tabAdmin.IsSelected = true;
                        break;
                    case "Employee":
                        tabFarms.Visibility = Visibility.Visible;
                        tabMachines.Visibility = Visibility.Visible;
                        tabCrops.Visibility = Visibility.Visible;
                        tabAssignments.Visibility = Visibility.Visible;
                        tabColor.Visibility = Visibility.Collapsed;
                        tabFarms.IsSelected = true;
                        break;                    
                    case "LandOwner":
                        tabFarms.Visibility = Visibility.Visible;
                        tabCrops.Visibility = Visibility.Visible;
                        tabAssignments.Visibility = Visibility.Visible;
                        tabColor.Visibility = Visibility.Collapsed;
                        tabFarms.IsSelected = true;
                        break;
                    case "Manager":
                        tabFarms.Visibility = Visibility.Visible;
                        tabMachines.Visibility = Visibility.Visible;
                        tabCrops.Visibility = Visibility.Visible;
                        tabAssignments.Visibility = Visibility.Visible;
                        tabColor.Visibility = Visibility.Collapsed;
                        tabFarms.IsSelected = true;
                        break;                    
                    case "Mechanic":
                        tabMachines.Visibility = Visibility.Visible;
                        tabAssignments.Visibility = Visibility.Visible;
                        tabColor.Visibility = Visibility.Collapsed;
                        tabMachines.IsSelected = true;
                        break;
                    default:
                        break;
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hideAllUserTabs();
            DataObjects.AppDetails.AppPath = AppContext.BaseDirectory;
            imgLogo.Source = new BitmapImage(new Uri(AppDetails.ImagePath + "logo.jpg"));


        }

        private void tabAdmin_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DGUserList.ItemsSource == null)
                {
                    PopulateUserList();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, ex.InnerException.Message);
            }
        }
        private void PopulateUserList(bool active = true)
        { 
            DGUserList.ItemsSource = _userManager.GetUserListByActive(active);            
            DGUserList.Columns[0].Header = "UserID";
            DGUserList.Columns[1].Header = "First Name";
            DGUserList.Columns[2].Header = "Last Name";
            DGUserList.Columns[3].Header = "Email Address";
            DGUserList.Columns[4].Header = "Phone Number";

        }

        private void PopulateFarmList(bool active = true)
        { 
            DGFarmList.ItemsSource = _farmManager.GetFarmListByActive(active);
            DGFarmList.Columns[0].Header = "FarmID";
            DGFarmList.Columns[1].Header = "LandOwner";
            DGFarmList.Columns[2].Header = "Address";
            DGFarmList.Columns[3].Header = "City";
            DGFarmList.Columns[4].Header = "State";
            DGFarmList.Columns[5].Header = "ZipCode";
            DGFarmList.Columns[6].Header = "Active";


        }

        private void PopulateCropList()
        { 
            DGCropList.ItemsSource = _cropManager.GetCropList();
            DGCropList.Columns[0].Header = "CropID";
            DGCropList.Columns[1].Header = "SeedNumber";
            DGCropList.Columns[2].Header = "Description";
            DGCropList.Columns[3].Header = "$/Bag";


        }

        private void PopulateMachineList(bool active = true)
        { 
            DGMachineList.ItemsSource = _machineManager.GetMachineListByActive(active);
            DGMachineList.Columns[0].Header = "MachineID";
            DGMachineList.Columns[1].Header = "Make";
            DGMachineList.Columns[2].Header = "Model";
            DGMachineList.Columns[3].Header = "MachineTypeID";
            DGMachineList.Columns[4].Header = "MachineStatusID";
            DGMachineList.Columns[5].Header = "Hours";
            DGMachineList.Columns[6].Header = "Active";

        }

        private void PopulateAssignmentList(bool completed = false)
        {
            DGAssignmentList.ItemsSource = _assignmentManager.GetAssignmentByCompleted(completed);
            //DGAssignmentList.Columns[0].Header = "MachineFieldUseID";
            //DGAssignmentList.Columns[1].Header = "FarmFieldID";
            //DGAssignmentList.Columns[2].Header = "UsageTypeID";
            //DGAssignmentList.Columns[3].Header = "MachineID";
            //DGAssignmentList.Columns[4].Header = "UserID";
            //DGAssignmentList.Columns[5].Header = "Description";
            //DGAssignmentList.Columns[6].Header = "Completed";

        }

        private void DGUserList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            User user = (User)DGUserList.SelectedItem;
           
            var userForm = new frmUser(user, _userManager);
            if (userForm.ShowDialog() == true)
            {
                PopulateUserList((bool)chkActive.IsChecked);

            }
        }

    

       

        private void DGUserList_AutoGeneratedColumns(object sender, EventArgs e)
        {
            
            DGUserList.Columns.RemoveAt(5);
            



        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            var addUserForm = new frmUser(_userManager);
            if (addUserForm.ShowDialog() == true)
            {
                PopulateUserList((bool)chkActive.IsChecked);

            }
        }

        private void chkActive_Click(object sender, RoutedEventArgs e)
        {
            if (chkActive.IsChecked == true)
            {
                PopulateUserList();
            }
            else
            {
                PopulateUserList(false);
            }
        }

        private void tabFarms_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DGFarmList.ItemsSource == null)
                {
                    PopulateFarmList();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, ex.InnerException.Message);
            }

        }

        private void btnAddFarm_Click(object sender, RoutedEventArgs e)
        {
            var addFarmForm = new frmFarm(_farmManager,_userManager);
            if (addFarmForm.ShowDialog()==true)
            {
                PopulateFarmList((bool)chkActive.IsChecked);
            }
            else
            {
                PopulateFarmList(false);
            }
        }

        private void DGFarmList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Farm farm = (Farm)DGFarmList.SelectedItem;

            var FarmForm = new frmFarm(farm, _farmManager,_fieldManager,_userManager,_cropManager);
            if (FarmForm.ShowDialog() == true)
            {
                PopulateFarmList((bool)chkActive.IsChecked);

            }
        }

        private void chkFarmActive_Click(object sender, RoutedEventArgs e)
        {
            if (chkFarmActive.IsChecked == true)
            {
                PopulateFarmList();
            }
            else
            {
                PopulateFarmList(false);
            }
        }

        private void tabCrops_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DGCropList.ItemsSource == null)
                {
                    PopulateCropList();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, ex.InnerException.Message);
            }
        }

        private void DGCropList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            Crop crop = (Crop)DGCropList.SelectedItem;

            var CropForm = new frmCrop(crop, _cropManager);
            if (CropForm.ShowDialog() == true)
            {
                PopulateCropList();

            }
        }

        private void btnAddCrop_Click(object sender, RoutedEventArgs e)
        {
            var addCropForm = new frmCrop(_cropManager);
            if (addCropForm.ShowDialog() == true)
            {
                PopulateCropList();
            }
            else
            {
                PopulateCropList();
            }
        }

        private void tabMachines_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DGMachineList.ItemsSource == null)
                {
                    PopulateMachineList();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, ex.InnerException.Message);
            }

        }

        private void DGMachineList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Machine machine = (Machine)DGMachineList.SelectedItem;

            var MachineForm = new frmMachine(machine, _machineManager);
            if (MachineForm.ShowDialog() == true)
            {
                PopulateMachineList((bool)chkActive.IsChecked);

            }
        }

        private void btnAddMachine_Click(object sender, RoutedEventArgs e)
        {
            var addMachineForm = new frmMachine(_machineManager);
            if (addMachineForm.ShowDialog() == true)
            {
                PopulateMachineList((bool)chkActive.IsChecked);
            }
            else
            {
                PopulateMachineList(false);
            }
        }

        private void chkMachineActive_Click(object sender, RoutedEventArgs e)
        {
            if (chkMachineActive.IsChecked == true)
            {
                PopulateMachineList();
            }
            else
            {
                PopulateMachineList(false);
            }

        }

        private void tabAssignments_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DGAssignmentList.ItemsSource == null)
                {
                    PopulateAssignmentList();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, ex.InnerException.Message);
            }

        }

        private void chkCompleted_Click(object sender, RoutedEventArgs e)
        {
            if (chkCompleted.IsChecked == true)
            {
                PopulateAssignmentList(true);
            }
            else
            {
                PopulateAssignmentList(false);
            }
        }

        private void DGAssignmentList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Assignment assignment = (Assignment)DGAssignmentList.SelectedItem;

            var AssignmentForm = new frmAssignment(assignment, _assignmentManager,_userManager,_machineManager,_fieldManager);
            if (AssignmentForm.ShowDialog() == true)
            {
                PopulateAssignmentList(!(bool)chkActive.IsChecked);

            }
        }

        private void btnAddAssignment_Click(object sender, RoutedEventArgs e)
        {
            var addAssignmentForm = new frmAssignment(_assignmentManager,_userManager,_machineManager,_fieldManager);
            if (addAssignmentForm.ShowDialog() == true)
            {
                PopulateAssignmentList(!(bool)chkActive.IsChecked);
            }
            else
            {
                PopulateAssignmentList(false);
            }
        }

        private void DGFarmList_AutoGeneratedColumns(object sender, EventArgs e)
        {
            DGFarmList.Columns.RemoveAt(7);

        }
    }
}
