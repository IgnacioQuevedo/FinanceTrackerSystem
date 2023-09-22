using BusinessLogic.User;
using DataManagers;

namespace DataManagersTests
{
    [TestClass]
    public class UnitTest1
    {

        #region AddUser

        [TestMethod]
        public void GivenUserToAddToRepositoryAccounts_ShouldReturnTrue()
        {
            string firstName = "Austin";
            string lastName = "Ford";
            string email = "austinFord@gmail.com";
            string password = "AustinF2003";
            string address = "NW 2nd Ave";

            User myUser = new User(firstName, lastName, email, password, address);

            Assert.AreEqual(true, UserManager.ValidateAddUser(myUser));


        }
        #endregion
    }
}