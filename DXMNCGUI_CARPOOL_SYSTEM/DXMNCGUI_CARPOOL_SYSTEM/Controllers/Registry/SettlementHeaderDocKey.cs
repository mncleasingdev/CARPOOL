using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_CARPOOL_SYSTEM.Controllers.Registry
{
    public class SettlementHeaderDocKey : BaseRegistryID
    {
        protected override void Init()
        {
            this.myID = 10011;
            this.myDefaultValue = (object)1;
        }
    }
}