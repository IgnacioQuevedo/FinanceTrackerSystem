using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Category_Components
{
    public class Category
    {
        #region Properties
        public int CategoryId { get; set; } = -1;
        public string Name { get; set; } = "";
        public DateTime CreationDate { get; } = DateTime.Now.Date;
        public StatusEnum Status { get; set; }
        public TypeEnum Type { get; set; }
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
