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
            ValidatePasswordHasCorrectLength(posiblePassword);
            ValidatePasswordUppercase(posiblePassword);

            return true;
        }

        private static void ValidatePasswordHasCorrectLength(string posiblePassword)
        {
            int minLength = 10;
            int maxLength = 30;



            if (posiblePassword.Length < minLength || posiblePassword.Length > maxLength)
            {
                throw new ExceptionValidateUser("ERROR ON PASSWORD");
            }
        }

        private static void ValidatePasswordUppercase(string posiblePassword)
        {
            bool hasUpperCase = false;
            for (int i = 0; i < posiblePassword.Length && !hasUpperCase; i++)
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
        }
    }
}
