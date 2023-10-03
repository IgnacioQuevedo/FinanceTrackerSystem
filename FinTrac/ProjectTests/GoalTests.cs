using BusinessLogic;
using BusinessLogic.User_Components;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Goal_Components;

namespace TestProject1;

[TestClass]
public class GoalTests
{

    private string goalTitle;
    private int maxAmmountToSpend;
    private Goal myGoal;

    [TestInitialize]

    public void Init()
    {
        myGoal = new Goal();

        goalTitle = "Less night";
        maxAmmountToSpend = 0;

        myGoal.Title = goalTitle;
    }

    #region Goal Title
    [TestMethod]

    public void GivenCorrectTitle_ShouldBeSetted() 
    { 
        Assert.AreEqual(goalTitle, myGoal.Title);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateGoal))]

    public void GivenEmptyTitle_ShouldThrownException()
    {
        goalTitle = "";
        myGoal.Title = goalTitle;
        myGoal.ValidateGoal();
    }

    #endregion


    #region Max Ammount To Spent

    [TestMethod]
    public void GivenAMaxAmmountToSpend_ShouldBeSetted()
    {
        int maxAmmount = 1000;
        myGoal.maxAmmountToSpend = maxAmmount;

        Assert.AreEqual(maxAmmount, myGoal.maxAmmountToSpend);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateGoal))]
    public void SettingAMaxAmmountToSpendNegative_ShouldThrowException()
    {
        maxAmmountToSpend = -999;
        myGoal.maxAmmountToSpend = maxAmmountToSpend;
        myGoal.ValidateGoal();
    }

    #endregion
}