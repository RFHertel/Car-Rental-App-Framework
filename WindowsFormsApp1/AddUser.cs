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
    public partial class AddUser : Form
    {
        private readonly CarRentalEntities _db;
        private ManageUsers _manageUsers;
        public AddUser(ManageUsers manageUsers)
        {
            InitializeComponent();
            _db = new CarRentalEntities();
            _manageUsers = manageUsers;
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            var roles = _db.Roles.ToList(); //return list in TypesOfCars table: Honda, Toyoya, etc. When you get the list of cars they are all coming over with ids and names
            cbRoles.DataSource = roles; // Display member is the text that you want people to see under the "name" column from types of cars
            cbRoles.ValueMember = "id"; //here we want the Id of the item from the selection under name in TypesOfCars. This value member is what you intend to store
            cbRoles.DisplayMember = "name";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var username = tbUserName.Text;
                var RoleId = ((int)cbRoles.SelectedIndex) + 1;
                var password = Utils.DefaultHashedPassword();
                var user = new User
                {
                    username = username,
                    password = password,
                    isActive = true,
                };
                _db.Users.Add(user);
                _db.SaveChanges();

                // when I added a new user it came back with Id of 3 and that needs to be added to the userRoles tables now to show admin1's association with it's role. User Id 3 in my case (in video its 4) should be related to new roleId
                var userid = user.id;
                var userRole = new UserRole //after creating the new user Role object (userRole) need to add the record to the database
                {
                    roleid = RoleId, //the value from the combobox
                    userid = userid
                };
                _db.UserRoles.Add(userRole);
                _db.SaveChanges();

                MessageBox.Show("New User Added Succesfully");
                _manageUsers.PopulateGrid();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error has occured: " + ex);
            }
        }
    }
}
