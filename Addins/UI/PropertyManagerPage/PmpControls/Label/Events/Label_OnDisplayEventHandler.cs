﻿using System.Runtime.InteropServices;

namespace Hymma.Solidworks.Addins
{
    /// <summary>
    /// event handler for <see cref="PmpLabel.OnDisplay"/>
    /// </summary>
    /// <param name="sender">is the PmpLabel that raised this event</param>
    /// <param name="evetnArgs">useful event arguments passed to you by SOLIDWORKS</param>
    [ComVisible(true)]
    public delegate void Label_OnDisplayEventHandler(PmpLabel sender, Label_OnDisplayEventArgs evetnArgs);
}
