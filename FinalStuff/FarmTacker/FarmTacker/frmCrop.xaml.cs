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
    /// Interaction logic for frmCrop.xaml
    /// </summary>
    public partial class frmCrop : Window
    {
        private Crop _crop = null;
        private ICropManager _cropManager = null;
        private bool _addMode = true;
        public frmCrop(ICropManager cropManager)
        {
            _cropManager = cropManager;
            InitializeComponent();
        }
        public frmCrop(Crop crop)
        {
            _crop = crop;
            InitializeComponent();
        }
        public frmCrop(Crop crop, ICropManager cropManager)
        {
            _cropManager = cropManager;
            _crop = crop;
            _addMode = false;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_addMode == false)
            {
                btnDelete.Visibility = Visibility.Hidden;
                txtCropID.Text =_crop.CropID;
                txtSeedNum.Text = _crop.SeedNum;
                txtPricePerBag.Text = _crop.PricePerBag.ToString();
                txtDescription.Text = _crop.Description;

                txtCropID.IsReadOnly = true;
                txtSeedNum.IsReadOnly = true;
                txtPricePerBag.IsReadOnly =true;
                txtDescription.IsReadOnly = true;

            }
            else
            {
                txtCropID.Focus();
                btnEdit.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Visible;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnDelete.Visibility = Visibility.Visible;
            txtCropID.IsReadOnly = false;
            txtSeedNum.IsReadOnly = false;
            txtPricePerBag.IsReadOnly = false;
            txtDescription.IsReadOnly = false;

            btnEdit.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (txtCropID.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid CropID!");
                txtCropID.Focus();
                return;
            }
            if (txtSeedNum.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid SeedNumber!");
                txtSeedNum.Focus();
                return;
            }
            if (txtPricePerBag.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid Price!");
                txtSeedNum.Focus();
                return;
            }
            if (txtDescription.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid Description!");
                txtSeedNum.Focus();
                return;
            }

            Crop crop = new Crop()
            {
                CropID = txtCropID.Text.ToString(),
                SeedNum = txtSeedNum.Text.ToString(),
                PricePerBag = decimal.Parse(txtPricePerBag.Text),
                Description = txtDescription.Text.ToString()
            };
            if (_addMode)
            {
                try
                {
                    if (_cropManager.AddCrop(crop))
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
                    if (_cropManager.EditCrop(_crop, crop))
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string caption = "You want to delete this crop?";
                if (MessageBox.Show("Are You Sure?", caption, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
                _cropManager.DeleteCrop(_crop.CropID);
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
