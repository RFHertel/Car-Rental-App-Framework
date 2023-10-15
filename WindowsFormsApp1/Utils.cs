using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public static class Utils
    {
        public static bool FormIsOpen(string name)
        {
            //Check is window already open
            var OpenForms = Application.OpenForms.Cast<Form>();
            var isOpen = OpenForms.Any(q => q.Name == name);
            return isOpen;
        }

        public static string HashPassword(string password)
        {
            //adding security to password - encryption - the hashed password is what should  be in the database - sha is our hashing algorithm.
            SHA256 sHA = SHA256.Create();

            //convert the input sting to a byte array to compute a hash
            byte[] data = sHA.ComputeHash(Encoding.UTF8.GetBytes(password));

            //Create a new Stringbuilder to collect the bytes
            //and create a string.

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static string DefaultHashedPassword()
        {
            //adding security to password - encryption - the hashed password is what should  be in the database - sha is our hashing algorithm.
            SHA256 sHA = SHA256.Create();

            //convert the input sting to a byte array to compute a hash
            byte[] data = sHA.ComputeHash(Encoding.UTF8.GetBytes("Password@123"));

            //Create a new Stringbuilder to collect the bytes
            //and create a string.

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        //view1 new password is view1password. It's original password was Password@123 which is the DefaultHashedPassword when a user want to change a password. Please see ReserPassword


    }
}
