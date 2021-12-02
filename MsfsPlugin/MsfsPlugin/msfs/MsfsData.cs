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

        private Int32 currentAPHeading;
        private Int32 currentAPAltitude;
        private Int32 currentAPVerticalSpeed;

        private Int32 apSwitch;

        private Boolean state;
        private Boolean dirtyAP = false;

        private static readonly Lazy<MsfsData> lazy = new Lazy<MsfsData>(() => new MsfsData());

        public static MsfsData Instance { get { return lazy.Value; } }

        public Int32 CurrentHeading { get; set; }
        public Int32 Fps { get; set; }
        public Int32 CurrentAPHeading { get => this.currentAPHeading; set { this.currentAPHeading = value; this.DirtyAP = true; } }
        public Int32 ApSwitch { get => this.apSwitch; set { this.apSwitch = value; this.DirtyAP = true; } }
        public Int32 CurrentAltitude { get; set; }
        public Int32 CurrentAPAltitude { get => this.currentAPAltitude; set { this.currentAPAltitude = value; this.DirtyAP = true; } }
        public Int32 CurrentVerticalSpeed { get; set; }
        public Int32 CurrentAPVerticalSpeed { get => this.currentAPVerticalSpeed; set { this.currentAPVerticalSpeed = value; this.DirtyAP = true; } }
        public Boolean DirtyAP { get => this.dirtyAP; set { this.dirtyAP = value; this.changed(); } }
        public Boolean State { get => this.state; set { this.state = value; this.changed(); } }

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
