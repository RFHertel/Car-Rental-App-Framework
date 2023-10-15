using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class Login : Form
    {
        private readonly CarRentalEntities _db;
        public Login()
        {
            InitializeComponent();
            _db = new CarRentalEntities();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //adding security to password - encryption - the hashed password is what should  be in the database - sha is our hashing algorithm.
                SHA256 sHA = SHA256.Create();

                var username = tbUserName.Text.Trim();
                var password = tbPassword.Text.Trim();

                //used the word password here for password which becomes a hashed number later that we put in the database

                var hashed_password = Utils.HashPassword(password);


                var user = _db.Users.FirstOrDefault(q => q.username == username && q.password == hashed_password);
                if (user == null) 
                {
                    MessageBox.Show("Please provide valid credentials");
                }
                else
                {
                    /*                    var role = user.UserRoles.FirstOrDefault(); //get me the user and the first in the list of roles
                                        var roleShortName = role.Role.shortname;
                                        var mainWindow = new MainWindow(this, roleShortName);*/

                    var mainWindow = new MainWindow(this, user);
                    mainWindow.Show();
                    Hide(); // Login Window gets hidden but not closed. This means the thread won't stop running until the application is shutdown
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Went Wrong. Please Try Again");
            }
        }
    }
}
