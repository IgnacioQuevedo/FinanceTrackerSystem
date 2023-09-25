using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Category
{
    public class Category
    {
        public string Name { get; set; } = "";



        public Category()
        {
        }


        public bool ValidateCategory()
        {
            if (string.IsNullOrEmpty(Name)) { 
                throw new ExceptionValidateCategory("ERROR ON NAME");
            }
            bool isValid = true;
            return isValid;
        }
    }
}
