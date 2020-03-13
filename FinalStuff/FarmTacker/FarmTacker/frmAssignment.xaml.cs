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
    /// Interaction logic for frmAssignment.xaml
    /// </summary>
    public partial class frmAssignment : Window
    {
        public frmAssignment()
        {
            InitializeComponent();
        }
        private Assignment _assignment = null;
        private IAssignmentManager _assignmentManager = null;
        private IMachineManager _machineManager = null;
        private IFieldManager _fieldManager = null;
        private IUserManager _userManager = null;
        private bool _addMode = true;
        public frmAssignment(IAssignmentManager assignmentManager)
        {
            _assignmentManager = assignmentManager;
            InitializeComponent();
        }
        public frmAssignment(IAssignmentManager assignmentManager, IUserManager userManager,IMachineManager machineManager,IFieldManager fieldManager)
        {
            _assignmentManager = assignmentManager;
            _userManager = userManager;
            _fieldManager = fieldManager;
            _machineManager = machineManager;
            InitializeComponent();
        }
        public frmAssignment(IUserManager userManager)
        {
            _userManager = userManager;
            InitializeComponent();
        }
        public frmAssignment(Assignment assignment)
        {
            _assignment = assignment;
            InitializeComponent();
        }
        public frmAssignment(Assignment assignment, IAssignmentManager assignmentManager,IUserManager userManager,IMachineManager machineManager, IFieldManager fieldManager)
        {
            _userManager = userManager;
            _assignmentManager = assignmentManager;
            _machineManager = machineManager;
            _fieldManager = fieldManager;
            _assignment = assignment;
            _addMode = false;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addMode == false)
            {
                PopulateFarmList();
                PopulateUserList();
                PopulateUsageList();
                PopulateMachineList();
                txtMachineFieldUseID.Text = _assignment.MachineFieldUseID.ToString();
                cboFarmFieldID.SelectedItem = _assignment.FarmFieldID;
                cboUsageTypeID.SelectedItem = _assignment.UsageTypeID;
                cboMachineID.SelectedItem = _assignment.MachineID;
                cboUserID.SelectedItem = _assignment.UserID.ToString();
                txtDescription.Text = _assignment.Description;
                chkCompleted.IsChecked = _assignment.Completed;

                txtMachineFieldUseID.IsReadOnly = true;
                cboFarmFieldID.IsEnabled = false;
                cboUsageTypeID.IsEnabled = false;
                cboMachineID.IsEnabled = false;
                cboUserID.IsEnabled = false;
                txtDescription.IsReadOnly = true;
                chkCompleted.IsEnabled = false;
                
            }
            else 
            {
                PopulateUserList();
                PopulateUsageList();
                PopulateMachineList();
                cboFarmFieldID.IsEnabled = true;
                cboUsageTypeID.IsEnabled = true;
                cboMachineID.IsEnabled = true;
                cboUserID.IsEnabled = true;
                txtMachineFieldUseID.IsReadOnly = true;
                chkCompleted.IsChecked = false;
                chkCompleted.IsEnabled = false;
                cboFarmFieldID.Focus();
                btnEdit.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Visible;
                
            }

            
        }
        private void PopulateUserList()
        {
            cboUserID.Items.Clear();
            List<User> users = new List<User>();
            users = _userManager.RetreiveUserByRole("Employee");

            foreach (User user in users)
            {
                cboUserID.Items.Add(user.UserID.ToString());
            }


        }
        private void PopulateUsageList()
        {
            cboUsageTypeID.Items.Clear();
            List<string> statuss = new List<string>();
            statuss = _assignmentManager.RetreiveUsageTypes();

            foreach (string status in statuss)
            {
                cboUsageTypeID.Items.Add(status);
            }
        }

        private void PopulateMachineList()
        {
            cboMachineID.Items.Clear();
            List<Machine> machines = new List<Machine>();
            machines = _machineManager.GetMachineListByActive(true);

            foreach (Machine machine in machines)
            {
                cboMachineID.Items.Add(machine.MachineID.ToString());
            }
        }
        private void PopulateFarmList()
        {
            cboFarmFieldID.Items.Clear();
            List<string> fields = new List<string>();
            fields = _fieldManager.RetreiveAllFields();

            foreach (string field in fields)
            {
                cboFarmFieldID.Items.Add(field);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            txtMachineFieldUseID.IsReadOnly = true;
            cboFarmFieldID.IsEnabled = true;
            cboUsageTypeID.IsEnabled = true;
            cboMachineID.IsEnabled = true;
            cboUserID.IsEnabled = true;
            txtDescription.IsReadOnly = false;
            chkCompleted.IsEnabled = true;

            btnEdit.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            
            if (cboFarmFieldID.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid Make!");
                cboFarmFieldID.Focus();
                return;
            }
            if (cboUsageTypeID.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid Model!");
                cboUsageTypeID.Focus();
                return;
            }
            if (cboMachineID.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid MachineType!");
                cboMachineID.Focus();
                return;
            }
            if (cboUserID.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid MachineStatus!");
                cboUserID.Focus();
                return;
            }
            if (txtDescription.Text.ToString() == "")
            {
                MessageBox.Show("Please enter valid Hours!");
                txtDescription.Focus();
                return;
            }


            Assignment assignment = new Assignment()
            {
                
                FarmFieldID = cboFarmFieldID.Text.ToString(),
                UsageTypeID = cboUsageTypeID.Text.ToString(),
                MachineID = cboMachineID.Text.ToString(),
                UserID = int.Parse(cboUserID.Text.ToString()),
                Description = txtDescription.Text.ToString()
            };
            if (_addMode)
            {
                try
                {
                    if (_assignmentManager.AddAssignment(assignment))
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
                    if (_assignmentManager.EditAssignment(_assignment, assignment))
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

        private void chkCompleted_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string caption = (bool)chkCompleted.IsChecked ?
                    "Reopen this Machine" : "Complete this Machine";
                if (MessageBox.Show("Are You Sure?", caption, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    chkCompleted.IsChecked = (bool)chkCompleted.IsChecked;
                    return;
                }
                _assignmentManager.SetAssignmentCompletionState((bool)chkCompleted.IsChecked, _assignment.MachineFieldUseID);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }
    }
}
