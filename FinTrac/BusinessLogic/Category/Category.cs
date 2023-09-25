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
            bool isValid = true;
            if (string.IsNullOrEmpty(Name))
            {
                throw new ExceptionValidateCategory("ERROR ON NAME");
            }
            return isValid;
        }
    }
}
