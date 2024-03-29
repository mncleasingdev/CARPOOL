﻿using System;

namespace DXMNCGUI_CARPOOL_SYSTEM.Controllers.Localization
{
    [AttributeUsage(AttributeTargets.Enum)]
    [Obsolete("Use LocalizableStringAttribute instead.", false)]
    public sealed class StringIdAttribute : Attribute
    {
        private string myBaseName;

        public string BaseName
        {
            get
            {
                return this.myBaseName;
            }
        }

        public StringIdAttribute(string baseName)
        {
            if (baseName == null)
                throw new ArgumentNullException(nameof(baseName));
            this.myBaseName = baseName;
        }
    }
}