using BusinessLogic;
using BusinessLogic.User_Components;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;


namespace TestProject1;

[TestClass]
public class GoalTests
{

    [TestInitialize]

    public void Init()
    {

    }

    [TestMethod]

    public void GivenCorrectName_ShouldBeSetted()
    {

        string goalTitle = "Less night";

        Goal myGoal = new Goal();
        myGoal.Title = goalTitle;

        Assert.AreEqual(goalTitle, myGoal.Title);
    }



}