using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace BusinessLogic.User
{
    public class User
    {

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Password { get; set; } = "";


        public User() { }



        #region ValidateFirstName

        public static bool ValidateFirstName(string possibleFirstName)
        {
            string pattern = "^[A-Za-z ]+$";
            bool hasNullOrSpaceOrEmpty = string.IsNullOrWhiteSpace(possibleFirstName);
            bool hasSpecialChar = !Regex.IsMatch(possibleFirstName, pattern);


            if (hasNullOrSpaceOrEmpty || hasSpecialChar)
            {
                throw new ExceptionValidateUser("ERROR ON FIRSTNAME");
            }

            return true;

        }

        public static string CorrectFirstName(string possibleFirstName)
        {
            return possibleFirstName.Trim();
        }

        #endregion


        #region ValidateLastName

        public static bool ValidateLastName(string possibleLastName)
        {
            string pattern = "^[A-Za-z ]+$";
            bool hasSpecialChar = !Regex.IsMatch(possibleLastName, pattern);
            bool hasNullOrEmptyOrSpace = string.IsNullOrWhiteSpace(possibleLastName);


            if (hasNullOrEmptyOrSpace || hasSpecialChar)
            {
                throw new ExceptionValidateUser("ERROR ON LASTNAME");
            }

            return true;
        }


        #endregion

        #region ValidatePassword
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
            int minAsciiUperCase = 65;
            int maxAsciiUperCase = 90;

            for (int i = 0; i < posiblePassword.Length && !hasUpperCase; i++)
            {
                if (posiblePassword[i] <= maxAsciiUperCase && posiblePassword[i] >= minAsciiUperCase)
                {
                    hasUpperCase = true;
                }
            }

            if (!hasUpperCase)
            {
                throw new ExceptionValidateUser("ERROR ON PASSWORD");
            }
        }
        #endregion
    }
}
