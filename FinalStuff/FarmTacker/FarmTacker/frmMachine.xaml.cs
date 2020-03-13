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
    /// Interaction logic for frmMachine.xaml
    /// </summary>
    public partial class frmMachine : Window
    {
        private Machine _machine = null;
        private IMachineManager _machineManager = null;
        private bool _addMode = true;
        public frmMachine(Machine machine)
        {
            _machine = machine;
            InitializeComponent();
        }
        public frmMachine(IMachineManager machineManager)
        {
            InitializeComponent();
            _machineManager = machineManager;
        }

        public frmMachine(Machine machine, IMachineManager machineManager)
        {
            _machine = machine;
            _machineManager = machineManager;
            _addMode = false;
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addMode==false)
            {
                txtMachineID.Text = _machine.MachineID;
                txtMake.Text = _machine.Make;
                txtModel.Text = _machine.Model;
                cboMachineType.SelectedItem = _machine.MachineTypeID;
                cboMachineStatus.SelectedItem = _machine.MachineStatusID;
                txtHours.Text = _machine.Hours.ToString();
                chkActive.IsChecked = _machine.Active;

                txtMachineID.IsReadOnly = true;
                txtMake.IsReadOnly = true;
                txtModel.IsReadOnly = true;
                cboMachineType.IsEnabled = false;
                cboMachineStatus.IsEnabled = false;
                txtHours.IsReadOnly = true;
                chkActive.IsEnabled = false;

                PopulateMachineTypeList();
                PopulateMachineStatusList();

            }
            else
            {
                txtMachineID.Focus();
                chkActive.IsChecked = true;
                chkActive.IsEnabled = false;
                cboMachineType.IsEnabled = true;
                cboMachineStatus.IsEnabled = true;                
                btnEdit.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Visible;
                PopulateMachineTypeList();
                PopulateMachineStatusList();
            }
        }

        private void PopulateMachineTypeList()
        {
            cboMachineType.Items.Clear();
            List<string> types = new List<string>();
            types = _machineManager.RetreiveMachineTypes();

            foreach (string type in types)
            {
                cboMachineType.Items.Add(type);
            }


        }

        private void PopulateMachineStatusList()
        {
            cboMachineStatus.Items.Clear();
            List<string> statuss = new List<string>();
            statuss = _machineManager.RetreiveMachineStatus();

            foreach (string status in statuss)
            {
                cboMachineStatus.Items.Add(status);
            }


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            txtMachineID.IsReadOnly = false;
            txtMake.IsReadOnly = false;
            txtModel.IsReadOnly = false;
            cboMachineType.IsEnabled = true;
            cboMachineStatus.IsEnabled = true;
            txtHours.IsReadOnly = false;
            chkActive.IsEnabled = true;

            btnEdit.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
        }

        private void chkActive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string caption = (bool)chkActive.IsChecked ?
                    "Reactivate this Machine" : "Deactivate this Machine";
                if (MessageBox.Show("Are You Sure?", caption, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    chkActive.IsChecked = (bool)chkActive.IsChecked;
                    return;
                }
                _machineManager.SetMachineActiveState((bool)chkActive.IsChecked, _machine.MachineID);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (txtMachineID.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid MachineID!");
                txtMachineID.Focus();
                return;
            }
            if (txtMake.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid Make!");
                txtMake.Focus();
                return;
            }
            if (txtModel.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid Model!");
                txtModel.Focus();
                return;
            }
            if (cboMachineType.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid MachineType!");
                cboMachineType.Focus();
                return;
            }
            if (cboMachineStatus.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid MachineStatus!");
                cboMachineStatus.Focus();
                return;
            }
            if (txtHours.Text.ToString() == "")
            {
                MessageBox.Show("Please enter valid Hours!");
                txtHours.Focus();
                return;
            }            

            Machine machine = new Machine()
            {
                MachineID = txtMachineID.Text.ToString(),
                Make = txtMake.Text.ToString(),
                Model = txtModel.Text.ToString(),
                MachineTypeID = cboMachineType.Text.ToString(),
                MachineStatusID = cboMachineStatus.Text.ToString(),
                Hours = int.Parse(txtHours.Text.ToString())
            };
            if (_addMode)
            {
                try
                {
                    if (_machineManager.AddMachine(machine))
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
                    if (_machineManager.EditMachine(_machine, machine))
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
    }
}
