using System;
using System.Collections.Generic;
using System.Text;

namespace MillerInc.UI.ControlModels
{
    /// <summary>
    /// This class is for controlling the types of shutdown for applications, 
    /// which can help with loops and such
    /// </summary>
    public enum ExitControl
    {
        Continue = 1,
        Shutdown = 2,
        Restart = 3,
        Unexpected = 4
    }
}
