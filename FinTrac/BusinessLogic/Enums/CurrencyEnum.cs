using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Enums
{
    public enum CurrencyEnum
    {
        [Description("$")]
        UY = 1,

        [Description("US$")]
        USA = 2
    }
}
