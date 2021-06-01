﻿using SolidWorks.Interop.swconst;
using System.Collections.Generic;

namespace Hymma.SolidTools.SolidAddins
{
    public class SwComboBox : SwPMPConcreteControl
    {
        public SwComboBox():base(swPropertyManagerPageControlType_e.swControlType_Combobox)
        {

        }
        public IEnumerable<string> Items { get; set; }
        public short Height { get; set; }
    }
}
