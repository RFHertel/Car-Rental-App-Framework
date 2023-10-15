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
    public partial class ManageRentalRecords : Form
    {
        private readonly CarRentalEntities _db;
        public ManageRentalRecords()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            var addRentalRecord = new AddEditRentalRecord
            {
                MdiParent = this.MdiParent
            };
            addRentalRecord.Show();
        }

        private void btnEditRecord_Click(object sender, EventArgs e)
        {
            try
            {
                //Retrieve data to be edited
                //First we need to know the row that was selected

                //Get Id of the row
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;
                //query database for record
                var record = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);

                var addEditRentalRecord = new AddEditRentalRecord(record);
                addEditRentalRecord.MdiParent = this.MdiParent; //Main window is MDI Parent. But then there is another window to this one which is manage vehicle listing
                addEditRentalRecord.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            try
            {
                //Get Id of the row
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;
                //query database for record
                var record = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);
                //delete vehicle from table
                _db.CarRentalRecords.Remove(record);
                _db.SaveChanges();

                PopulateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ManageRentalRecords_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateGrid();
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"Error: {Ex.Message}");
            }
        }

        //New Function to PopulateGrid. Can be called anytime we need a grid refresh
        public void PopulateGrid()
        {
            // Select a custom model collection of cars from database
            var records = _db.CarRentalRecords
                .Select(q => new
                {
                    Customer = q.CustomerName,
                    DateOut = q.DateRented,
                    DateIn = q.DateReturned,
                    Id = q.id,
                    q.Cost,
                    Car = q.TypesOfCar.Make + " " + q.TypesOfCar.Model
                })
                .ToList();

            gvRecordList.DataSource = records;
            gvRecordList.Columns["DateIn"].HeaderText = "Date In";
            gvRecordList.Columns["DateOut"].HeaderText = "Date Out";
            //Hide the column for ID. Changed from the hard coded column value to the name, 
            // to make it more dynamic. 
            gvRecordList.Columns["Id"].Visible = false;
        }
    }
}
