using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class AddEditVehicle : Form
    {
        private bool isEditMode;
        private ManageVehicleListing _manageVehicleListing;
        private readonly CarRentalEntities _db;
        public AddEditVehicle(ManageVehicleListing manageVehicleListing = null)
        {
            InitializeComponent();
            lblTitle.Text = "Add New Vehicle";
            this.Text = lblTitle.Text;
            isEditMode = false;
            _db = new CarRentalEntities();
            _manageVehicleListing = manageVehicleListing;
        }

        public AddEditVehicle(TypesOfCar carToEdit, ManageVehicleListing manageVehicleListing = null) //Types of car is the data from the db
        {
            InitializeComponent();
            lblTitle.Text = "Edit Vehicle";
            this.Text = "Edit Vehicle";
            this.Text = lblTitle.Text;
            _manageVehicleListing = manageVehicleListing;
            if (carToEdit == null )
            {
                MessageBox.Show("Please Ensure that you selected a valid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                _db = new CarRentalEntities();
                PopulateFields(carToEdit);
            }
            
            
        }

        private void PopulateFields(TypesOfCar car)
        {
            lblId.Text = car.Id.ToString();
            tbMake.Text = car.Make;
            tbModel.Text = car.Model;
            tbVIN.Text = car.VIN;
            tbYear.Text = car.Year.ToString();
            tbLicenseNum.Text = car.LicensePlateNumber;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Added Validation for Make and Model:
                if (string.IsNullOrEmpty(tbMake.Text) || string.IsNullOrEmpty(tbModel.Text))
                {
                    MessageBox.Show("Please Ensure that you provide a make and model");
                }
                else
                {
                    if (isEditMode) // This section is for changes to an existing record
                    {
                        //Edit Code Here
                        var id = int.Parse(lblId.Text);
                        var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);
                        car.Model = tbModel.Text;
                        car.VIN = tbVIN.Text;
                        car.Make = tbMake.Text;
                        car.Model = tbModel.Text;
                        car.Year = int.Parse(tbYear.Text);
                        car.LicensePlateNumber = tbLicenseNum.Text;

/*                        _db.SaveChanges();
                        MessageBox.Show("Update Operation Completed. Refresh Grid to see Changes");
                        Close();*/
                    }
                    else // If we're adding a new car which means we are not in edit mode
                    {
                        //Add Code Here
                        var newCar = new TypesOfCar
                        {
                            LicensePlateNumber = tbLicenseNum.Text,
                            Make = tbMake.Text,
                            Model = tbModel.Text,
                            VIN = tbVIN.Text,
                            Year = int.Parse(tbYear.Text)
                        };
                        _db.TypesOfCars.Add(newCar);
/*                        _db.SaveChanges();
                        _manageVehicleListing.PopulateGrid();
                        MessageBox.Show("Insert Operation Completed. Refresh Grid to see Changes");
                        Close();*/

                    }
                    //works for either edit mode or not edit mode when just adding an item etc.
                    _db.SaveChanges();
                    _manageVehicleListing.PopulateGrid();
                    MessageBox.Show("Insert Operation Completed");
                    Close();
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show($"Error: {Ex.Message}");
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
