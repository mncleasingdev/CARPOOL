using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_CARPOOL_SYSTEM.Controllers.Registry
{
    public class SettlementDetailLinesDtlKey : BaseRegistryID
    {
        protected override void Init()
        {
            this.myID = 10022;
            this.myDefaultValue = (object)1;
        }
    }
}