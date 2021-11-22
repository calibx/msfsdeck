namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class MsfsData
    {
        public Int32 Heading;
        public String state;

        private static readonly Lazy<MsfsData> lazy =
            new Lazy<MsfsData>(() => new MsfsData());

        public static MsfsData Instance { get { return lazy.Value; } }

        private MsfsData()
        {
        }


    }
}
