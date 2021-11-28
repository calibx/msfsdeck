namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class MsfsData
    {
        private readonly List<Notifiable> notifiables = new List<Notifiable>();

        private Int32 currentHeading;
        private Int32 currentAPHeading;

        private Int32 currentAltitude;
        private Int32 currentAPAltitude;

        private Int32 currentVerticalSpeed;
        private Int32 currentAPVerticalSpeed;

        public Int32 apSwitch;

        public Boolean state;
        public Int32 fps;

        public Boolean dirtyAP = false;


        private static readonly Lazy<MsfsData> lazy =
            new Lazy<MsfsData>(() => new MsfsData());

        public static MsfsData Instance { get { return lazy.Value; } }


        private MsfsData()
        {
        }

        public void register(Notifiable notif)
        {
            this.notifiables.Add(notif);
        }

        public void changed()
        {
            foreach (Notifiable notifiable in this.notifiables)
            {
                notifiable.Notify();
            }
        }
    }
}
