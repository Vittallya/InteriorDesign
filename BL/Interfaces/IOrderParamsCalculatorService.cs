using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IOrderParamsCalculatorService
    {
        OrderPrice GetPrices(double serviceCost, OrderParams orderParams);

    }
}
