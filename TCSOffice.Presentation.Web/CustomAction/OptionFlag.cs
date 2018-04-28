using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCSOffice.Presentation.Web.CustomAction
{
    class OptionFlag : Attribute
    {
        public string Name { get; private set; }

        public OptionFlag(string name)
        {
            Name = name;
        }
    }
}