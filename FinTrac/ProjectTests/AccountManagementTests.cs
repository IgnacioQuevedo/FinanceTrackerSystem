using BusinessLogic.Account_Components;
using BusinessLogic.User_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTests
{
    [TestClass]
    public class AccountManagementTests
    {

        #region initializingAspects
        private Account genericAccount;
        private User genericUser;

        [TestInitialize]
        public void TestInitialize()
        {
            genericAccount = new MonetaryAccount("Itau Saving Bank", 5000, CurrencyEnum.UY);
            string firstName = "Austin";
            string lastName = "Ford";
            string email = "austinFord@gmail.com";
            string password = "AustinF2003";
            string address = "NW 2nd Ave";
            genericUser = new User(firstName, lastName, email, password, address);
        }

        #endregion


        #region Add Monetary Account

        [TestMethod]
        public void GivenCorrectMonetaryAccountToAdd_ShouldAddIt()
        {
            int numberOfAccountsAddedBefore = genericUser.MyAccounts.Count;
            genericUser.AddAccount(genericAccount);
            
            Assert.AreEqual(numberOfAccountsAddedBefore + 1, genericUser.MyAccounts.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionAccountManagement))]

        public void GivenNameOfMonetaryAccountAlreadyAdded_ShouldThrowException()
        {
            genericUser.AddAccount(genericAccount);

            Account repitedNameAccount = new MonetaryAccount("Itau Saving Bank", 100, CurrencyEnum.UY);
            genericUser.AddAccount(repitedNameAccount);
        }




        #endregion


        #region Add Creadit card Account

        [TestMethod]
        public void GivenCorrectCreditCardAccount_ShouldBeAdded()
        {
            int numberOfAccountsAddedBefore = genericUser.MyAccounts.Count;
            string nameToBeSetted = "Prex";
            CurrencyEnum currencyToBeSetted = CurrencyEnum.UY;
            string issuingBankToBeSetted = "Santander";
            string last4DigitsToBeSetted = "1234";
            int availableCreditToBeSetted = 20000;
            DateTime closingDateToBeSetted = new DateTime(2024, 9, 30);

            Account genericCreditCardAccount = new CreditCardAccount(nameToBeSetted, currencyToBeSetted, issuingBankToBeSetted, last4DigitsToBeSetted, availableCreditToBeSetted, closingDateToBeSetted);

            genericUser.AddAccount(genericCreditCardAccount);

            Assert.AreEqual(numberOfAccountsAddedBefore, genericUser.MyAccounts.Count);
        }

        #endregion
    }
}
