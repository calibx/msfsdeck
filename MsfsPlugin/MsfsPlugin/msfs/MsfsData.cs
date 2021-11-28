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

        public Int32 CurrentHeading { get => this.currentHeading; set => this.currentHeading = value; }
        public Int32 CurrentAPHeading { get => this.currentAPHeading; set => this.currentAPHeading = value; }
        public Int32 CurrentAltitude { get => this.currentAltitude; set => this.currentAltitude = value; }
        public Int32 CurrentAPAltitude { get => this.currentAPAltitude; set => this.currentAPAltitude = value; }
        public Int32 CurrentVerticalSpeed { get => this.currentVerticalSpeed; set => this.currentVerticalSpeed = value; }
        public Int32 CurrentAPVerticalSpeed { get => this.currentAPVerticalSpeed; set => this.currentAPVerticalSpeed = value; }

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
