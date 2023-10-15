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
    public partial class ResetPassword : Form
    {
        private readonly CarRentalEntities _db;
        private User _user;
        public ResetPassword(User user)
        {
            InitializeComponent();
            _db = new CarRentalEntities();
            _user = user;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                var password = tbEnterNewPassword.Text;
                var confirm_password = tbConfirmPassword.Text;
                var user = _db.Users.FirstOrDefault(q => q.id == _user.id);

                if (password != confirm_password)
                {
                    MessageBox.Show("Passwords do not match. Please Try Again!");

                }
                user.password = Utils.HashPassword(password);
                _db.SaveChanges();
                MessageBox.Show("The password was reset succesfully");
            }
            catch (Exception)
            {
                MessageBox.Show("An Error has occured. Please Try Again");
            }
            //view1 new password is view1password. It's original password was Password@123 which is the DefaultHashedPassword when a user want to change a password. Please see Utils File
        }
    }
}
