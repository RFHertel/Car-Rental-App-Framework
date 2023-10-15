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
    public partial class ManageUsers : Form
    {
        private readonly CarRentalEntities _db; //database context object
        public ManageUsers()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("AddUser"))
            {
                var addUser = new AddUser(this);
                addUser.MdiParent = this.MdiParent;
                addUser.Show();
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                //Retrieve data to be edited
                //First we need to know the row that was selected

                //Get Id of the row
                var id = (int)gvUserList.SelectedRows[0].Cells["id"].Value; //make sure in the User table the 'id' name is correct for the column
                
                //query database for record
                var user = _db.Users.FirstOrDefault(q => q.id == id);
                //var genericPassword = "Password@123"; //Communicate with the user that this is their new password and that they have to change the password. Security wise you would use something only the person can know with date of birth etc
                var hashed_password = Utils.DefaultHashedPassword(); 
                user.password = hashed_password;
                _db.SaveChanges();
                PopulateGrid();

                MessageBox.Show($"{user.username}'s Password has been reset!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnDeactivateUser_Click(object sender, EventArgs e)
        {
            try
            {
                //Retrieve data to be edited
                //First we need to know the row that was selected

                //Get Id of the row
                var id = (int)gvUserList.SelectedRows[0].Cells["id"].Value; //make sure in the User table the 'id' name is correct for the column

                //query database for record
                var user = _db.Users.FirstOrDefault(q => q.id == id);
                user.isActive = user.isActive == true ? false : true; //terniray operator
                _db.SaveChanges();
                PopulateGrid();

                MessageBox.Show($"{user.username}'s active status has changed!");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        public void PopulateGrid()
        {
            // Select a custom model collection of cars from database
            var users = _db.Users
                .Select(q => new
                {
                    q.id,
                    q.username,
                    q.UserRoles.FirstOrDefault().Role.name,
                    q.isActive
                })
                .ToList();

            gvUserList.DataSource = users;
            gvUserList.Columns["username"].HeaderText = "UserName";
            gvUserList.Columns["name"].HeaderText = "Role Name";
            gvUserList.Columns["isActive"].HeaderText = "Active";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGrid();
        }
    }
}
