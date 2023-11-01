public class UserDTO_Tests
{

    [TestMethod]
    public void GivenFirstName_ShouldBeSetted()
    {
        string firstName = "Ignacio";
        UserDTO userDTO = new UserDTO();
        userDTO.FirstName = firstName;

        Assert.AreEqual(firstName,userDTO.FirstName);
        
    }
}