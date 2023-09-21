using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.User
{
    public class User
    {

        public string Password { get; set; } = "";




        public User() { }





        public static bool ValidatePassword(string posiblePassword)
        {
            int minLength = 10;
            int maxLength = 30;
            bool hasUpperCase = false;

            if(posiblePassword.Length < minLength || posiblePassword.Length > maxLength)
            {
                throw new ExceptionValidateUser("ERROR ON PASSWORD");
            }

            for (int i = 0; i < posiblePassword.Length; i++)
            {
                if (posiblePassword[i] >= 65 && posiblePassword[i] <= 90)
                {
                    hasUpperCase = true;
                }

            }

            if (!hasUpperCase) 
            {
                throw new ExceptionValidateUser("ERROR ON PASSWORD");
            }

            return true;
        }





    }
}
