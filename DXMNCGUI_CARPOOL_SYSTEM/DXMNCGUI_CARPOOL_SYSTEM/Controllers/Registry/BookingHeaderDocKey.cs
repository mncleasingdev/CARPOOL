using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_CARPOOL_SYSTEM.Controllers.Registry
{
    public class BookingHeaderDocKey : BaseRegistryID
    {
        protected override void Init()
        {
            this.myID = 13311;
            this.myDefaultValue = (object)1;
        }
    }
    public class BookingHeaderAdminDocKey : BaseRegistryID
    {
        protected override void Init()
        {
            this.myID = 13322;
            this.myDefaultValue = (object)1;
        }
    }
    public class BookingHeaderDriverDocKey : BaseRegistryID
    {
        protected override void Init()
        {
            this.myID = 13333;
            this.myDefaultValue = (object)1;
        }
    }
}