using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Category
{
    public class Category
    {
        #region Properties
        private bool _categoryCreated = false;
        public string Name { get; set; } = "";
        public string CreationDate { get; } = DateTime.Now.ToString("dd/MM/yyyy");

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
            if (ValidateCategory())
            {
                //We keep it low profile in case we need it (future)
                _categoryCreated = true;
            }
        }
        #endregion

        #region Validating Category

        public bool ValidateCategory()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(Name))
            {
                throw new ExceptionValidateCategory("ERROR ON NAME");
            }
            return isValid;
        }
        #endregion

    }
}
