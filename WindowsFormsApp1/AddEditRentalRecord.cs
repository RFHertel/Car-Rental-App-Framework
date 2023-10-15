using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class AddEditRentalRecord : Form
    {
        //Look for the .edmx file for this:
        private readonly CarRentalEntities _db; //this represents the entire space in the CarRentalDB.edmx diagram. GIves you access to every table / entity in the model
        private bool isEditMode;
        public AddEditRentalRecord()
        {
            InitializeComponent();
            _db = new CarRentalEntities(); //initialize retrieval of tables in db as new instance. Initialize object of type of the entiry model
            lblTitle.Text = "Add New Rental Record";
            this.Text = "Add New Rental Record";
            isEditMode = false;
        }

        public AddEditRentalRecord(CarRentalRecord recordToEdit)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Rental Record";
            this.Text = "Edit Rental Record";
            if (recordToEdit != null)
            {
                MessageBox.Show("Please Ensure that you selected a valid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                _db = new CarRentalEntities();
                PopulateFields(recordToEdit);
            }

        }

        private void PopulateFields(CarRentalRecord recordToEdit)
        {
            tbCustomerName.Text = recordToEdit.CustomerName;
            dtRented.Value = (DateTime)recordToEdit.DateReturned; //It was nullable and needed to be converted to non-nullable
            dtReturned.Value = (DateTime)recordToEdit.DateRented;
            tbCost.Text = recordToEdit.Cost.ToString();
            lblRecordId.Text = recordToEdit.id.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string customerName = tbCustomerName.Text;
                var dateOut = dtRented.Value;
                var dateIn = dtReturned.Value;

                var carType = cbTypeOfCar.Text;
                var isValid = true;
                double cost = Convert.ToDouble(tbCost.Text);
                var errorMessage = "";

                if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(carType))
                {
                    //MessageBox.Show("Please Enter Missing Data");
                    isValid = false;
                    errorMessage += "Error: Please Enter Missing Data.\n\r";
                }

                if (dateOut >= dateIn)
                {
                    //MessageBox.Show("Illegal Date Selection");
                    isValid = false;
                    errorMessage += "Error: Illegal Date Selection.\n\r";
                }

                if (isValid)
                {
                    var rentalRecord = new CarRentalRecord();
                    if (isEditMode) //if is in edit mode we are not adding which is in the else. 
                    {
                        //so in edit mode need to grab the id
                        var id = int.Parse(lblRecordId.Text);
                        rentalRecord = _db.CarRentalRecords.FirstOrDefault(q => q.id == id); //retrieved record
                    }
                    rentalRecord.CustomerName = customerName;
                    rentalRecord.DateRented = dateOut;
                    rentalRecord.DateReturned = dateIn;
                    rentalRecord.Cost = (decimal)cost; //it's a decimal in the db and is a double in C# so need to cast
                    rentalRecord.TypeOfCarId = (int)cbTypeOfCar.SelectedValue;
                    
                    if (!isEditMode) //if is in edit mode we are not adding which is in the else. 
                    {
                        _db.CarRentalRecords.Add(rentalRecord);
                    }

                    _db.SaveChanges();

                    MessageBox.Show($"Customer Name: {customerName}\n\r" +
                    $"Date Rented Name: {dateOut}\n\r" +
                    $"Date Returned: {dateIn}\n\r" +
                    $"Cost: {cost}\n\r" +
                    $"Car Type: {carType}\n\r" +
                    "Thank you for your business");


                    Close();
                }
                else
                {
                    MessageBox.Show(errorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw - ends the program
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //This is how you set up a combo box to databind a list of objects to it from a database
            //select * from TypesOfCars
            var cars = _db.TypesOfCars
                .Select(q => new {Id = q.Id, Name = q.Make + " " + q.Model})
                .ToList(); //return list in TypesOfCars table: Honda, Toyoya, etc. When you get the list of cars they are all coming over with ids and names
            cbTypeOfCar.DisplayMember = "Name"; // Display member is the text that you want people to see under the "name" column from types of cars
            cbTypeOfCar.ValueMember = "Id"; //here we want the Id of the item from the selection under name in TypesOfCars. This value member is what you intend to store
            cbTypeOfCar.DataSource = cars;
        }

/*        private void button2_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow(); // to go to another form
            mainWindow.Show(); How to reference another instance of a window from a click event.
        }*/
    }
}
