using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CarRentalApp
{
    public partial class MainWindow : Form
    {
        private Login _login;
        public string _RoleName;
        public User _user;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(Login login, User user)
        {
            InitializeComponent();
            _login = login;
            _user = user;
            _RoleName = user.UserRoles.FirstOrDefault().Role.shortname; //This fetch happens accross 3 tables. Many-to-Many relationship. The first table
                                                                        // user calls the table userroles (which has two foreign keys: 1 to user table PK and 1 to role table PK)
                                                                        // userroles table has 1 entry - and 2 foreign keys
                                                                        // The user's id which is fetched from the name put in the user textbox 
                                                                        //is compared with the UserId foreign key in User roles and that is the same id
                                                                        //sent to the Roles table to fetch information about the user
                                                                        //For example: if you say users is 'user', after typing in the correct password: "password"(on the login page - login.cs),
                                                                        //the id here (2) is sent over to the roles table and gets id number 2, which
                                                                        //fetches back the position of 'clerk'.

                                                                        //The dependencies are in the userroles table when you click left click and "relationships" in the design space of the userroles table.
                                                                        //In SQL it's the equivalent of writing constraints
        }

        // To make this the window that houses all the forms need to go to properties and find isMDIcontainer and then all the windows will not just
        //pop up as their own windows. Do this in the [Design] Form:
        private void addRentalRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("AddEditRentalRecord"))
            {
                var addRentalRecord = new AddEditRentalRecord(); //to launch a window
                                                                 //when we are launching the window we need to tell it it's parent:
                addRentalRecord.ShowDialog(); //SHow dialog opens a window that can only stay open as 1 instance until it's closed
                addRentalRecord.MdiParent = this;
                //addRentalRecord.Show(); // This will pop up as another window  if do not have parent child relationship. It can come up as another screen that is not tethered to the main window. It keeps making windows. YOu can have 5 at the same time

            }

        }

        private void manageVehicleListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Check if there is any open form with the name that you want to launch and if there isn't then launch it
            var OpenForms = Application.OpenForms.Cast<Form>(); //THis gives a collection of the forms owned by the application. It's a list of the data type form of the open forms in the application
            var isOpen = OpenForms.Any(q => q.Name == "ManageVehicleListing"); //Name of the form is the class name
            if(!isOpen)
            {
                var vehicleLising = new ManageVehicleListing();
                vehicleLising.MdiParent = this;
                vehicleLising.Show();
            }

        }

        private void viewArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("ManageRentalRecords"))
            {
                var manageRentalRecords = new ManageRentalRecords();
                manageRentalRecords.MdiParent = this;
                manageRentalRecords.Show();
            }

        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //close login when main window closes also
            _login.Close();
        }

        private void dfdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var manageUsers = new ManageUsers();
            manageUsers.MdiParent = this;
            manageUsers.Show();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if(_user.password == Utils.DefaultHashedPassword())
            {
                var resetPassword = new ResetPassword(_user);
                resetPassword.ShowDialog();
            }

            var usermame = _user.username;
            tsiLoginText.Text = $"Logged In As: {usermame}";
            if(_RoleName != "admin") //hide admin tool strip button
            {
                dfdToolStripMenuItem.Visible = false;
            }
        }
    }
}
