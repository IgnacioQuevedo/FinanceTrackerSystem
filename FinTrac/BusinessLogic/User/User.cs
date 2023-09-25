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
        private static bool _userCreated = false;

        #region Properties
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string? Address { get; set; }
        #endregion

        #region Constructors


        public User() { }

        public User(string firstName, string lastName, string email, string password, string? address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Address = address;
            if (ValidateUser())
            {
                //Maybe in the future is necessary, we keep it low profile in case we need it.
                _userCreated = true;
            }

        }

        private bool ValidateUser()
        {
            bool emailValidated = ValidateEmail(Email);
            bool passwordValidated = ValidatePassword(Password);
            bool firstNameValidated = ValidateFirstName(FirstName);
            bool lastNameValidated = ValidateLastName(LastName);
            return emailValidated && passwordValidated && firstNameValidated && lastNameValidated;
        }

        #endregion




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

        public static string RemoveAllUnsenseSpaces(string stringToCorrect)
        {
            return stringToCorrect.Trim();
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

        #region ValidateEmail

        public static bool ValidateEmail(string possibleEmail)
        {
            string pattern = @"^[a-zA-Z0-9.]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool hasCorrectPattern = Regex.IsMatch(possibleEmail, pattern);

            for (int i = 0; i < possibleEmail.Length; i++)
            {
                if (possibleEmail[i].Equals('.') && possibleEmail[i+1].Equals('.'))
                {
                    hasCorrectPattern = false;
                }
            }
            
            if (!hasCorrectPattern)
            {
                throw new ExceptionValidateUser("ERROR ON EMAIL");
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
 
        #region ValidateAddress
        //If necessary validations or implement methods.
        #endregion
    }
}
