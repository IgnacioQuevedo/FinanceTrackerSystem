using BusinessLogic.Category_Components;
using BusinessLogic.Account_Components;
using BusinessLogic.Goal_Components;
using BusinessLogic.Transaction_Components;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using BusinessLogic.ExchangeHistory_Components;
using BusinessLogic.Exceptions;

namespace BusinessLogic.User_Components
{
    public class User
    {
        #region Properties

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string? Address { get; set; }
        public List<Category> MyCategories { get; set; }
        public List<Account> MyAccounts { get; set; }
        public List<Goal> MyGoals { get; set; }
        public List<ExchangeHistory> MyExchangesHistory { get; set; }

        #endregion

        #region Constructors

        public User()
        {
        }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
        
        public User(string firstName, string lastName, string email, string password, string? address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Address = address;
            if (ValidateUser())
            {
                MyCategories = new List<Category>();
                MyAccounts = new List<Account>();
                MyGoals = new List<Goal>();
                MyExchangesHistory = new List<ExchangeHistory>();
            }
        }

        #endregion

        #region User Management

        #region Validate User

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

        public bool ValidateFirstName(string possibleFirstName)
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

        public string RemoveAllUnsenseSpaces(string stringToCorrect)
        {
            return stringToCorrect.Trim();
        }

        #endregion

        #region ValidateLastName

        public bool ValidateLastName(string possibleLastName)
        {
            string pattern = "^[A-Za-z ]+$";
            //Regex checks if a specified regular expression pattern matches the entire input string.
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

        public bool ValidateEmail(string possibleEmail)
        {
            string pattern = @"^[a-zA-Z0-9.]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool hasCorrectPattern = Regex.IsMatch(possibleEmail, pattern);

            for (int i = 0; i < possibleEmail.Length; i++)
            {
                if (possibleEmail[i].Equals('.') && possibleEmail[i + 1].Equals('.'))
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

        public bool ValidatePassword(string posiblePassword)
        {
            ValidatePasswordHasCorrectLength(posiblePassword);
            ValidatePasswordUppercase(posiblePassword);

            return true;
        }

        private void ValidatePasswordHasCorrectLength(string posiblePassword)
        {
            const int minLength = 10;
            const int maxLength = 30;


            if (posiblePassword.Length < minLength || posiblePassword.Length > maxLength)
            {
                throw new ExceptionValidateUser("ERROR ON PASSWORD");
            }
            
        }

        private void ValidatePasswordUppercase(string posiblePassword)
        {
            bool hasUpperCase = false;
            const int minAsciiUperCase = 65;
            const int maxAsciiUperCase = 90;

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

        #endregion

        #region Category Management

        #region Add Category

        public void AddCategory(Category categoryToAdd)
        {
            ValidateRegisteredCategory(categoryToAdd);
            MyCategories.Add(categoryToAdd);
        }
        private void ValidateRegisteredCategory(Category categoryToAdd)
        {
            foreach (var category in MyCategories.Where(t => t != null))
            {
                if (category.Name == categoryToAdd.Name)
                {
                    throw new ExceptionCategoryManagement(
                        "Category name already registered, impossible to create another Category.");
                }
            }
        }

        #endregion

        #region Get Categories

        public List<Category> GetCategories()
        {
            List<Category> listOfCategories = MyCategories;
            return listOfCategories;
        }

        #endregion

        #region Modify Category

        public void ModifyCategory(Category categoryToUpdate)
        {
            int lengthOfCategoryList = MyCategories.Count;
            bool flag = false;

            for (int i = 0; i < lengthOfCategoryList && !flag && MyCategories[i] != null; i++)
            {
                if (MyCategories[i].CategoryId == categoryToUpdate.CategoryId)
                {
                    
                    MyCategories[i].Name = categoryToUpdate.Name;
                    MyCategories[i].Status = categoryToUpdate.Status;
                    MyCategories[i].Type = categoryToUpdate.Type;
                    flag = true;
                }
            }
        }

        #endregion

        #region Delete Category

        public void DeleteCategory(Category categoryToDelete)
        {
            ValidateIfCategoryHasTransactions(categoryToDelete);
            MyCategories.Remove(categoryToDelete);
        }

        private void ValidateIfCategoryHasTransactions(Category categoryToDelete)
        {
            bool founded = false;
            foreach (Account account in MyAccounts)
            {
                foreach (Transaction transaction in account.MyTransactions)
                {
                    if (transaction.TransactionCategory == categoryToDelete)
                    {
                        founded = true;
                        break;
                    }
                }
            }

            if (founded)
            {
                throw new ExceptionCategoryManagement(
                    "Error: You can't delete this category because is being used in a transaction");
            }
        }

        #endregion

        #endregion

        #region Account Management

        #region AddAccount

        public void AddMonetaryAccount(MonetaryAccount accountToAdd)
        {
            for (int i = 0; i < MyAccounts.Count; i++)
            {
                if (MyAccounts[i] is MonetaryAccount)
                {
                    ValidateNameIsNotRegistered(accountToAdd, MyAccounts[i]);
                }
            }
            MyAccounts.Add(accountToAdd);
        }

        public void AddCreditAccount(CreditCardAccount accountToAdd)
        {
            for (int i = 0; i < MyAccounts.Count; i++)
            {
                if (MyAccounts[i] is CreditCardAccount)
                {
                    ValidateIssuingBankAnd4LastDigits(accountToAdd, (CreditCardAccount)MyAccounts[i]);
                }
            }
            MyAccounts.Add(accountToAdd);
        }

        #endregion

        #region GetAccounts

        public List<Account> GetAccounts()
        {
            return MyAccounts;
        }

        #endregion

        #region Modify Account

        public void ModifyMonetaryAccount(MonetaryAccount accountToUpdate)
        {
            int lengthOfAccounts = MyAccounts.Count;
            int indexOfUpdate = 0;

            for (int i = 0; i < lengthOfAccounts; i++)
            {
                if (MyAccounts[i] is MonetaryAccount)
                {
                    ValidateNameIsNotRegistered(accountToUpdate, MyAccounts[i]);
                }

                if (haveSameId(accountToUpdate, MyAccounts[i]))
                {
                    indexOfUpdate = i;
                }
            }

            UpdateValues(accountToUpdate, indexOfUpdate);
        }

        public void ModifyCreditAccount(CreditCardAccount accountToUpdate)
        {
            int lengthOfAccounts = MyAccounts.Count;
            int indexOfUpdate = 0;

            for (int i = 0; i < lengthOfAccounts; i++)
            {
                if (MyAccounts[i] is CreditCardAccount)
                {
                    ValidateIssuingBankAnd4LastDigits(accountToUpdate, (CreditCardAccount)MyAccounts[i]);
                }

                if (haveSameId(accountToUpdate, MyAccounts[i]))
                {
                    indexOfUpdate = i;
                }
            }

            UpdateValues(accountToUpdate, indexOfUpdate);
        }

        #region Auxiliary Methods for Modify

        private void ValidateNameIsNotRegistered(MonetaryAccount accountToUpdate, Account accountToCompare)
        {
            if (accountToCompare.Name.Equals(accountToUpdate.Name) && !haveSameId(accountToCompare, accountToUpdate))
            {
                throw new ExceptionAccountManagement("ERROR - Name already registered.");
            }
        }

        private bool haveSameId(Account account1, Account account2)
        {
            if (account1.AccountId == account2.AccountId)
            {
                return true;
            }

            return false;
        }

        private void UpdateValues(Account accountToUpdate, int index)
        {
            accountToUpdate.MyTransactions = MyAccounts[index].MyTransactions;
            MyAccounts[index] = accountToUpdate;
        }

        private void ValidateIssuingBankAnd4LastDigits(CreditCardAccount accountToUpdate,
            CreditCardAccount oneCreditCardAccount)
        {
            if (UsedIssuingNameAndLastDigits(accountToUpdate, oneCreditCardAccount)
                && !haveSameId(accountToUpdate, oneCreditCardAccount))
            {
                throw new ExceptionAccountManagement("Issuing name and last 4 digits already used, try others.");
            }
        }

        private bool UsedIssuingNameAndLastDigits(CreditCardAccount creditCardAccountToUpdate,
            CreditCardAccount oneCreditCardAccount)
        {
            return oneCreditCardAccount.IssuingBank.Equals(creditCardAccountToUpdate.IssuingBank)
                   && oneCreditCardAccount.Last4Digits.Equals(creditCardAccountToUpdate.Last4Digits);
        }

        #endregion

        #endregion

        #region DeleteAccount

        public void DeleteAccount(Account accountToDelete)
        {
            if (ThereIsNoTransactions(accountToDelete))
            {
                MyAccounts.Remove(accountToDelete);
                MyAccounts.Insert(accountToDelete.AccountId, null);
            }
            else
            {
                throw new ExceptionAccountManagement(
                    "Error: You can't delete your account because it has transactions");
            }
        }

        private static bool ThereIsNoTransactions(Account accountToDelete)
        {
            return accountToDelete.MyTransactions.All(x => x == null);
        }

        #endregion

        #endregion

        #region Goal Management

        #region AddGoal

        public void AddGoal(Goal goalToAdd)
        {
            MyGoals.Add(goalToAdd);
        }

        #endregion

        #region Return of Goals

        public List<Goal> GetGoals()
        {
            return MyGoals;
        }

        #endregion

        #endregion

        #region Exchange History Management

        #region Add Exchange History

        public void AddExchangeHistory(ExchangeHistory exchangeHistoryToAdd)
        {
            ChecksIfDateIsUsed(exchangeHistoryToAdd);

            MyExchangesHistory.Add(exchangeHistoryToAdd);
        }
        private void ChecksIfDateIsUsed(ExchangeHistory exchangeHistoryToAdd)
        {
            foreach (var oneExchange in MyExchangesHistory)
            {
                if (DateTime.Compare(oneExchange.ValueDate, exchangeHistoryToAdd.ValueDate) == 0)
                {
                    throw new ExceptionExchangeHistoryManagement("There already exists a exchange history " +
                                                                 "for this date, modify it instead.");
                }
            }
        }

        #endregion

        #region Get Exchanges History

        public List<ExchangeHistory> GetExchangesHistory()
        {
            return MyExchangesHistory;
        }

        #endregion

        #region Delete Exchange

        public void DeleteExchangeHistory(ExchangeHistory exchangeHistoryToDelete)
        {
            MyExchangesHistory.Remove(exchangeHistoryToDelete);
        }

        #endregion

        #region Modify Exchange

        public void ModifyExchangeHistory(ExchangeHistory exchangeHistoryToUpdate)
        {
            int idToUpdate = exchangeHistoryToUpdate.ExchangeHistoryId;
            bool updated = false;

            for (int i = 0; i < MyExchangesHistory.Count && !updated; i++)
            {
                if (idToUpdate == MyExchangesHistory[i].ExchangeHistoryId)
                {
                    MyExchangesHistory[i].Currency = exchangeHistoryToUpdate.Currency;
                    MyExchangesHistory[i].Value = exchangeHistoryToUpdate.Value;
                    MyExchangesHistory[i].ValueDate = exchangeHistoryToUpdate.ValueDate;
                    updated = true;
                }
            }

            if (!updated)
            {
                throw new ExceptionExchangeHistoryManagement("This exchange does not exist, impossible to modify");
            }
        }

        #endregion

        #endregion

        public static void EqualPasswords(object password1, object password2)
        {
            if (!password1.Equals(password2))
            {
                throw new ExceptionValidateUser("Passwords are not the same, try again.");
            }
        }
    }
}