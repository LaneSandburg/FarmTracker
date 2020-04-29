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
    /// Interaction logic for frmField.xaml
    /// </summary>
    public partial class frmField : Window
    {
        private Field _field = null;
        private IFieldManager _fieldManager = null;
        private IFarmManager _farmManager = null;
        private ICropManager _cropManager = null;
        private bool _addMode = true;

        public frmField() 
        {
            _field = new Field();
            _fieldManager = new FieldManager();
            _farmManager = new FarmManager();
            _cropManager = new CropManager();
            InitializeComponent();
        }
        public frmField(Field field)
        {
            _field = field;
            InitializeComponent();
        }
        public frmField(IFieldManager fieldManager)
        {
            InitializeComponent();
            _fieldManager = fieldManager;
        }
        public frmField(IFarmManager farmManager)
        {
            InitializeComponent();
            _farmManager = farmManager;
        }
        public frmField(ICropManager cropManager)
        {
            InitializeComponent();
            _cropManager = cropManager;
        }
        public frmField(IFieldManager fieldManager, ICropManager cropManager)
        {
            InitializeComponent();
            _fieldManager = fieldManager;
            _cropManager = cropManager;
        }
        public frmField(IFieldManager fieldManager, ICropManager cropManager, IFarmManager farmManager)
        {
            InitializeComponent();
            _fieldManager = fieldManager;
            _farmManager = farmManager;
            _cropManager = cropManager;
        }

        public frmField(Field field, IFieldManager fieldManager, ICropManager cropManager, IFarmManager farmManager)
        {
            _field = field;
            _fieldManager = fieldManager;
            _farmManager = farmManager;
            _cropManager = cropManager;
            _addMode = false;
            InitializeComponent();

        }
        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addMode == false)
            {
                txtFarmFieldID.Text = _field.FarmFieldID.ToString();
                cboCropID.SelectedItem = _field.CropID;
                cboFarmID.SelectedItem = _field.FarmID;
                txtAcres.Text = _field.Acres.ToString();
                txtPastYield.Text = _field.PastYield.ToString(); ;
                txtCurrentYield.Text = _field.CurrentYield.ToString();
                txtPlantedOn.Text = _field.PlantOnDate.ToString();
                txtHarvestDate.Text = _field.HarvestDate.ToString();
                txtLastSprayedOn.Text = _field.LastSprayedOn.ToString();

                txtFarmFieldID.IsReadOnly = true;
                cboCropID.IsEnabled = false;
                cboFarmID.IsEnabled = false;
                txtAcres.IsReadOnly = true;
                txtPastYield.IsReadOnly = true;
                txtCurrentYield.IsReadOnly = true;
                txtPlantedOn.IsEnabled = false;
                txtHarvestDate.IsEnabled = false;
                txtLastSprayedOn.IsEnabled = false;

                PopulateCropList();
                PopulateFarmList();



            }

            else
            {
                cboCropID.IsEnabled = true;
                cboFarmID.IsEnabled = true;
                txtFarmFieldID.Focus();                
                btnEdit.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Visible;

                PopulateCropList();
                PopulateFarmList();
            }
        }
        private void PopulateCropList()
        {
            cboCropID.Items.Clear();
            List<Crop> crops = new List<Crop>();
            crops = _cropManager.GetCropList();

            foreach (Crop crop in crops)
            {
                cboCropID.Items.Add(crop.CropID);
            }


        }

        private void PopulateFarmList()
        {
            cboFarmID.Items.Clear();
            List<Farm> farms = new List<Farm>();
            farms = _farmManager.GetFarmListByActive(true);

            foreach (Farm farm in farms)
            {
                cboFarmID.Items.Add(farm.FarmID);
            }


        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            txtFarmFieldID.IsReadOnly = false;
            cboCropID.IsEnabled = true;
            cboFarmID.IsEnabled = true;
            txtAcres.IsReadOnly = false;
            txtPastYield.IsReadOnly = false;
            txtCurrentYield.IsReadOnly = false;
            txtPlantedOn.IsEnabled = true;
            txtHarvestDate.IsEnabled = true;
            txtLastSprayedOn.IsEnabled = true;

            cboCropID.Focus();
            btnEdit.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Field field = new Field();

            if (txtFarmFieldID.Text.ToString().Length < 10 || txtFarmFieldID.Text.ToString().Contains(" "))
            {
                MessageBox.Show("you must enter a valid FarmFieldID");
                txtFarmFieldID.Focus();
                return;
            }
            else
            {
                field.FarmFieldID = txtFarmFieldID.Text;
            }

            if (cboCropID.SelectedItem == null) 
            {
                field.CropID = "";
            }
            else { field.CropID = cboCropID.Text; }
            
            field.FarmID = cboFarmID.Text.ToString();

            string acres = "";
            foreach (char c in txtAcres.Text.ToString())
            {
                if (c < '0' || c > '9')
                {
                    MessageBox.Show("you must enter a integer");
                    txtAcres.Focus();
                    return;

                }
                else
                {

                    acres += c;
                }
            }            
            field.Acres = int.Parse(txtAcres.Text);


            if (txtPastYield.Text == "" || txtCurrentYield.Text == "" ||
                txtPlantedOn.Text == "" || txtHarvestDate.Text == "" || txtLastSprayedOn.Text == "")
            {

                field.PastYield = 0;
                field.CurrentYield = 0;
                field.PlantOnDate = DateTime.Now;
                field.HarvestDate = DateTime.Now;
                field.LastSprayedOn = DateTime.Now;

            }
            else 
            {
                string pastYield = "";
                foreach (char c in txtPastYield.Text.ToString())
                {
                    if (c < '0' || c > '9')
                    {
                        MessageBox.Show("you must enter a integer");
                        txtPastYield.Focus();
                        return;

                    }
                    else {
                        
                        pastYield+=c;
                    }                    
                }
                field.PastYield = int.Parse(pastYield);

                string currentYield = "";
                foreach (char c in txtCurrentYield.Text.ToString())
                {
                    if (c < '0' || c > '9')
                    {
                        MessageBox.Show("you must enter a integer");
                        txtCurrentYield.Focus();
                        return;

                    }
                    else
                    {

                        currentYield += c;
                    }
                }
                field.CurrentYield = int.Parse(currentYield);
                field.PlantOnDate = DateTime.Parse(txtPlantedOn.SelectedDate.ToString());
                field.HarvestDate = DateTime.Parse(txtPlantedOn.SelectedDate.ToString());
                field.LastSprayedOn = DateTime.Parse(txtPlantedOn.SelectedDate.ToString());
            }
            



            if (_addMode)
            {
                try
                {
                    if (_fieldManager.AddField(field))
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
                    if (_fieldManager.UpdateField(_field.FarmID, field))
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
