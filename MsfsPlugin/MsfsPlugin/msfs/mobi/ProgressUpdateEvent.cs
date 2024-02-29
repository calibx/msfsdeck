namespace Loupedeck.MsfsPlugin.msfs.mobi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProgressUpdateEvent
    {
        public int Total = 100;
        public int Current = 0;
        public string ProgressMessage = null;
    }
}
