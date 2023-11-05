using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Dto_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Enums;
using BusinessLogic.Goal_Components;

namespace BusinessLogicTests.Dtos_Tests
{
    [TestClass]
    public class GoalDTO_Tests
    {
        private GoalDTO _goalDTO;
        private Category genericCategory;
        private List<Category> genericListOfCategories;
        private CurrencyEnum genericCurrencyEnum;
        private int genericMaxAmountOfGoal;
        private string genericTitleGoalDTO;

        #region Initialize

        [TestInitialize]
        public void Initialize()
        {
            _goalDTO = new GoalDTO();

            genericCategory = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);

            List<Category> myListOfCategories = new List<Category>();
            myListOfCategories.Add(genericCategory);

            genericCurrencyEnum = CurrencyEnum.USA;

            genericMaxAmountOfGoal = 100;

            genericTitleGoalDTO = "Less Party";
        }

        #endregion

        #region Title

        [TestMethod]
        public void GivenTitle_ShouldBeSetted()
        {
            _goalDTO.Title = genericTitleGoalDTO;

            Assert.AreEqual(genericTitleGoalDTO, _goalDTO.Title);
        }

        #endregion

        #region Max amount to spend
        [TestMethod]
        public void GivenMaxAmountToSpend_ShouldBeSetted()
        {
            _goalDTO.MaxAmountToSpend = genericMaxAmountOfGoal;

            Assert.AreEqual(genericMaxAmountOfGoal, _goalDTO.MaxAmountToSpend);
        }

        #endregion

        #region Currency of amount

        [TestMethod]
        public void GivenCurrencyOfAmount_ShoulBeSetted()
        {
            _goalDTO.CurrencyOfAmount = genericCurrencyEnum;

            bool belongToEnum = Enum.IsDefined(typeof(CurrencyEnum), _goalDTO.CurrencyOfAmount);
            Assert.IsTrue(belongToEnum);
        }

        #endregion

        #region List Of Categories

        [TestMethod]
        public void GivenListOfCategories_ShoulBeSetted()
        {
            _goalDTO.CategoriesOfGoalDTO = genericListOfCategories;

            Assert.AreEqual(genericListOfCategories, _goalDTO.CategoriesOfGoalDTO);
        }

        #endregion

        [TestMethod]
        public void GivenValues_ShouldBePossibleToCreateAGoalDTO()
        {
            GoalDTO myGoalDTO = new GoalDTO(genericTitleGoalDTO, genericMaxAmountOfGoal, genericCurrencyEnum, genericListOfCategories);

            Assert.AreEqual(genericTitleGoalDTO, myGoalDTO.Title);
            Assert.AreEqual(genericMaxAmountOfGoal, myGoalDTO.MaxAmountToSpend);
            Assert.AreEqual(genericCurrencyEnum, myGoalDTO.CurrencyOfAmount);
            Assert.AreEqual(genericListOfCategories, myGoalDTO.CategoriesOfGoalDTO);
        }



    }
}
