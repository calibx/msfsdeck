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

        public Int32 currentHeading;
        public Int32 currentAPHeading;

        public Int32 currentAltitude;
        public Int32 currentAPAltitude;

        public Int32 currentVerticalSpeed;
        public Int32 currentAPVerticalSpeed;

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
