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

        #region Initialize

        [TestInitialize]
        public void Initialize()
        {
            _goalDTO = new GoalDTO();
            genericCategory = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);
        }

        #endregion

        #region Title

        [TestMethod]
        public void GivenTitle_ShouldBeSetted()
        {
            string goalDTOTitle = "Less Party";
            _goalDTO.Title = goalDTOTitle;

            Assert.AreEqual(goalDTOTitle, _goalDTO.Title);
        }

        #endregion

        #region Max amount to spend
        [TestMethod]
        public void GivenMaxAmountToSpend_ShouldBeSetted()
        {
            int maxAmountToSpendForDTO = 100;
            _goalDTO.MaxAmountToSpend = maxAmountToSpendForDTO;

            Assert.AreEqual(maxAmountToSpendForDTO, _goalDTO.MaxAmountToSpend);
        }

        #endregion

        #region Currency of amount

        [TestMethod]
        public void GivenCurrencyOfAmount_ShoulBeSetted()
        {
            CurrencyEnum currencyForGoalDTO = CurrencyEnum.USA;
            _goalDTO.CurrencyOfAmount = currencyForGoalDTO;

            bool belongToEnum = Enum.IsDefined(typeof(CurrencyEnum), _goalDTO.CurrencyOfAmount);
            Assert.IsTrue(belongToEnum);
        }

        #endregion

        #region List Of Categories

        [TestMethod]
        public void GivenListOfCategories_ShoulBeSetted()
        {
            List<Category> myListOfCategories = new List<Category>();
            myListOfCategories.Add(genericCategory);

            _goalDTO.CategoriesOfGoalDTO = myListOfCategories;

            Assert.AreEqual(myListOfCategories, _goalDTO.CategoriesOfGoalDTO);
        }

        #endregion

        [TestMethod]
        public void GivenValues_ShouldBePossibleToCreateAGoalDTO()
        {
            List<Category> myListOfCategories = new List<Category>();
            myListOfCategories.Add(genericCategory);
            CurrencyEnum currencyForGoalDTO = CurrencyEnum.USA;
            int maxAmountToSpendForDTO = 100;
            string goalDTOTitle = "Less Party";
            GoalDTO myGoalDTO = new GoalDTO(goalDTOTitle, maxAmountToSpendForDTO, currencyForGoalDTO, myListOfCategories);
            Assert.AreEqual(goalDTOTitle, myGoalDTO.Title);
            Assert.AreEqual(maxAmountToSpendForDTO, myGoalDTO.MaxAmountToSpend);
            Assert.AreEqual(currencyForGoalDTO, myGoalDTO.CurrencyOfAmount);
            Assert.AreEqual(myListOfCategories, myGoalDTO.CategoriesOfGoalDTO);




        }



    }
}
