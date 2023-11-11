using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos_Components
{
    public enum CurrencyEnumDTO
    {
        [Description("$")]
        UY = 1,

        [Description("US$")]
        USA = 2,

        [Description("€")]
        EUR = 3
    }
}
