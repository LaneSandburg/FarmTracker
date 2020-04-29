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
    /// Interaction logic for frmFarm.xaml
    /// </summary>
    public partial class frmFarm : Window
    {
        private Farm _Farm = null;        
        private IFarmManager _farmManager = null;
        private IFieldManager _fieldManager = null;
        private IUserManager _userManager = null;
        private ICropManager _cropManager = null;
        private bool _addMode = true;
        public frmFarm(Farm farm)
        {
            _Farm = farm;
            InitializeComponent();
        }        

        public frmFarm(IFarmManager farmManager)
        {
            InitializeComponent();
            _farmManager = farmManager;
        }
        public frmFarm(IFarmManager farmManager, IUserManager userManager)
        {
            InitializeComponent();
            _farmManager = farmManager;
            _userManager = userManager;
            _cropManager = new CropManager();
        }
        public frmFarm(IUserManager userManager)
        {
            InitializeComponent();
            _userManager = userManager;
        }

        public frmFarm(IFieldManager fieldManager)
        {
            InitializeComponent();
            _fieldManager = fieldManager;
        }
        public frmFarm(ICropManager cropManager)
        {
            InitializeComponent();
            _cropManager = cropManager;
        }
        public frmFarm(Farm farm, IFarmManager farmManager,IFieldManager fieldManager, IUserManager userManager, ICropManager cropManager)
        {
            _Farm = farm;
           
            _farmManager = farmManager;
            _fieldManager = fieldManager;
            _userManager = userManager;
            _cropManager = cropManager;

            _addMode = false;
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (_addMode == false)
            {
                DGFields.Visibility = Visibility.Visible;
                btnAddField.Visibility = Visibility.Visible;
                lblFields.Visibility = Visibility.Visible;
                txtFarmID.Text = _Farm.FarmID;
                cboLandOwner.SelectedItem = _Farm.UserID.ToString();
                txtAddress.Text = _Farm.Address;
                txtCity.Text = _Farm.City;
                txtState.Text = _Farm.State;
                txtZipcode.Text = _Farm.ZipCode;
                chkActive.IsChecked = _Farm.Active;

                txtFarmID.IsReadOnly = true;
                cboLandOwner.IsEnabled = false;
                txtAddress.IsReadOnly = true;
                txtCity.IsReadOnly = true;
                txtState.IsReadOnly = true;
                txtZipcode.IsReadOnly = true;
                chkActive.IsEnabled = false;

                
                PopulateFieldList();
                PopulateUserList();

            }

            else
            {
                chkActive.IsChecked = true;
                chkActive.IsEnabled = false;
                cboLandOwner.IsEnabled = true;
                txtFarmID.Focus();
                btnEdit.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Visible;
                DGFields.Visibility = Visibility.Hidden;
                btnAddField.Visibility = Visibility.Hidden;
                lblFields.Visibility = Visibility.Hidden;

                PopulateUserList();
            }
        }

        private void chkActive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string caption = (bool)chkActive.IsChecked ?
                    "Reactivate this Farm" : "Deactivate this Farm";
                if (MessageBox.Show("Are You Sure?", caption, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    chkActive.IsChecked = (bool)chkActive.IsChecked;
                    return;
                }
                _farmManager.SetFarmActiveState((bool)chkActive.IsChecked, _Farm.FarmID);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            cboLandOwner.IsReadOnly = false;
            txtAddress.IsReadOnly = false;
            txtCity.IsReadOnly = false;
            txtState.IsReadOnly = false;
            txtZipcode.IsReadOnly = false;
            chkActive.IsEnabled = true;
            cboLandOwner.IsEnabled = true;
            DGFields.IsEnabled = true;

            btnEdit.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtFarmID.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid FarmID!");
                txtFarmID.Focus();
                return;
            }
            if (cboLandOwner.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid LandOwnerID!");
                cboLandOwner.Focus();
                return;
            }
            if (txtAddress.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid Address!");
                txtAddress.Focus();
                return;
            }
            if (txtCity.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid City!");
                txtCity.Focus();
                return;
            }
            if (txtState.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid State!");
                txtState.Focus();
                return;
            }
            if (txtZipcode.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid Zipcode!");
                txtZipcode.Focus();
                return;
            }

            Farm farm = new Farm()
            {
                FarmID = txtFarmID.Text.ToString(),
               UserID = int.Parse(cboLandOwner.Text),
               Address = txtAddress.Text.ToString(),
               City = txtCity.Text.ToString(),
               State = txtState.Text.ToString(),
               ZipCode = txtZipcode.Text.ToString()
            };
            if (_addMode)
            {
                try
                {
                    if (_farmManager.AddFarm(farm))
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
                    if (_farmManager.EditFarm(_Farm.FarmID,farm))
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

        private void PopulateFieldList() 
        {
            try
            {
                DGFields.ItemsSource = _farmManager.RetreiveFarmFields(_Farm.FarmID);
                //DGFields.Columns[0].Header = "Field Number";
                //DGFields.Columns[1].Header = "Crop Planted";
                //DGFields.Columns[3].Header = "Acres";
                //DGFields.Columns[4].Header = "Past Yield";
                //DGFields.Columns[4].Header = "Current Yield";
                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void PopulateUserList()
        {
            cboLandOwner.Items.Clear();
            List<User> users = new List<User>();
            users = _userManager.RetreiveUserByRole("LandOwner");
            
            foreach (User user in users)
            {
                cboLandOwner.Items.Add(user.UserID.ToString());
            }
            
        
        }
        

        private void lstFields_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Field field = (Field)DGFields.SelectedItem;

            var FieldForm = new frmField(field, _fieldManager,_cropManager,_farmManager);
            if (FieldForm.ShowDialog() == true)
            {
                PopulateFieldList();
                cboLandOwner.SelectedItem = _Farm.UserID.ToString();
                PopulateUserList();

            }
        }

        private void DGFields_AutoGeneratedColumns(object sender, EventArgs e)
        {
            
            DGFields.Columns.RemoveAt(8);
            DGFields.Columns.RemoveAt(7);
            DGFields.Columns.RemoveAt(6);
            DGFields.Columns.RemoveAt(2);


        }

        private void btnAddField_Click(object sender, RoutedEventArgs e)
        {
            var addFieldForm = new frmField(_fieldManager,_cropManager,_farmManager);
            if (addFieldForm.ShowDialog() == true)
            {
                PopulateFieldList();
                PopulateUserList();

            }
        }
    }
}
