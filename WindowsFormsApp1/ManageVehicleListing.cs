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
    public partial class ManageVehicleListing : Form
    {
        private readonly CarRentalEntities _db; 
        public ManageVehicleListing()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void ManageVehicleListing_Load(object sender, EventArgs e)
        {
            //Select * from TypesOfCars
            //var cars = _db.TypesOfCars.ToList();

            //Select Id as CarId, name as CarName from Typesofcars
            // var cars = _db.TypesOfCars //linq expression is need to get rid of a third columns and only use the 1st two columns that were originally made in the db
            //     .Select(q => new {CarId = q.Id, CarName = q.Make }) //this list of custom objects is being used as the datasource for the grid
            //     .ToList(); //This is building up the aliasing for select * as in sql query
            try
            {
                PopulateGrid();
            }
            catch (Exception Ex) 
            {
                MessageBox.Show($"Error: {Ex.Message}");
            }
            //Put this into function but this is also the explanation of the code:
/*            var cars = _db.TypesOfCars //linq expression is need to get rid of a third columns and only use the 1st two columns that were originally made in the db
                .Select(q => new 
                { Make = q.Make, 
                    Model = q.Model, 
                    VIN = q.VIN, 
                    Year = q.Year, 
                    LicensePlateNumber = q.LicensePlateNumber,
                    q.Id
                }) //this list of custom objects is being used as the datasource for the grid
                .ToList(); //This is building up the aliasing for select * as in sql query
            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "License Plate Number"; //write over for custom header text for 5th column
            gvVehicleList.Columns[5].Visible = false; // This is the column for Id
*//*            gvVehicleList.Columns[0].HeaderText = "ID";
            gvVehicleList.Columns[1].HeaderText = "NAME";*/

        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            try
            {
                AddEditVehicle addEditVehicle = new AddEditVehicle(this);
                addEditVehicle.MdiParent = this.MdiParent; //Main window is MDI Parent. But then there is another window to this one which is manage vehicle listing
                addEditVehicle.Show();
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error: {ex.Message}");            
            }

        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            try
            {
                //Retrieve data to be edited
                //First we need to know the row that was selected

                //Get Id of the row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;
                //query database for record
                var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);
                //launch AddEditVehicle window with data
                var addEditVehicle = new AddEditVehicle(car, this);
                addEditVehicle.MdiParent = this.MdiParent; //Main window is MDI Parent. But then there is another window to this one which is manage vehicle listing
                addEditVehicle.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }



        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                //Get Id of the row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;
                //query database for record
                var car = _db.TypesOfCars.FirstOrDefault(q => q.Id == id);

                DialogResult dr = MessageBox.Show("Are you sure you want to delete this record?","Delete", MessageBoxButtons.YesNoCancel);
                //delete vehicle from table

                if (dr == DialogResult.Yes)
                {
                    //delete the car from the table
                    _db.TypesOfCars.Remove(car);
                    _db.SaveChanges();
                    //gvVehicleList.Refresh();
                }
                PopulateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Simple Refresh Option
            PopulateGrid();

        }

        //New Function to PopulateGrid. Can be called anytime we need a grid refresh
        public void PopulateGrid()
        {
            // Select a custom model collection of cars from database
            var cars = _db.TypesOfCars
                .Select(q => new
                {
                    Make = q.Make,
                    Model = q.Model,
                    VIN = q.VIN,
                    Year = q.Year,
                    LicensePlateNumber = q.LicensePlateNumber,
                    q.Id
                })
                .ToList();
            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "License Plate Number";
            //Hide the column for ID. Changed from the hard coded column value to the name, 
            // to make it more dynamic. 
            gvVehicleList.Columns["Id"].Visible = false;
        }
    }
}
