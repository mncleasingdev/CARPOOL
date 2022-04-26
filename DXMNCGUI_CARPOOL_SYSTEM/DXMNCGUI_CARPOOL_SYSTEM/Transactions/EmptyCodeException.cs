using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_CARPOOL_SYSTEM.Transactions
{
    public class EmptyCodeException : Exception
    {
        public EmptyCodeException()
            : base("Empty DocNo  is not allowed.")
        {
        }
    }
}