using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagers.Category_Manager
{
    public class ExceptionCategoryManager : Exception
    {
        public ExceptionCategoryManager(string message) : base(message) { }
    }
}
