using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using BusinessLogic.User_Components;
using BusinessLogic.Goal_Components;

namespace BusinessLogic.Category_Components
{
    public class Category
    {
        #region Properties

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        public string Name { get; set; } = "";
        public DateTime CreationDate { get; } = DateTime.Now.Date;
        public StatusEnum Status { get; set; }
        public TypeEnum Type { get; set; }
        
        public int UserId { get; set; }
        public User CategoryUser { get; set; }
        public List<Goal> CategoryGoals { get; set; }

        #endregion

        #region Constructors
        public Category()
        {
        }

        public Category(string name, StatusEnum status, TypeEnum type)
        {
            Name = name;
            Status = status;
            Type = type;
            ValidateCategory();

        }
        #endregion

        #region Validating Category

        public bool ValidateCategory()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new ExceptionValidateCategory("ERROR ON NAME");
            }
            return true;
        }
        #endregion
    }
}
