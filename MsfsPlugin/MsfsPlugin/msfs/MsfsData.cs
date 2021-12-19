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
        private Int32 currentAPSpeed;

        private Int32 apSwitch;
        private Int32 apThrottleSwitch;
        private Int32 apAltHoldSwitch;
        private Int32 apHeadHoldSwitch;
        private Int32 apVSHoldSwitch;
        private Int32 apNavHoldSwitch;
        private Int32 apSpeedHoldSwitch;

        private Boolean connected;
        private Int32 currentBrakes;
        private Int16 currentTrottle;
        private Int32 currentSpoiler;
        private Int32 currentGearHandle;
        private Boolean tryingToconnect;
        private Boolean setToMSFS;

        // Used to know if we try to autoconnect to FSUIPC
        private Boolean valuesDisplayed;

        private static readonly Lazy<MsfsData> lazy = new Lazy<MsfsData>(() => new MsfsData());

        public static MsfsData Instance { get { return lazy.Value; } }

        public Int32 CurrentHeading { get; set; }
        public Int32 Fps { get; set; }
        public Int32 DebugValue { get; set; }
        public Byte GearOverSpeed { get; set; }
        public Int32 CurrentGearHandle { get => this.currentGearHandle; set { this.currentGearHandle = value; this.SetToMSFS = true; } }
        public Int32 CurrentGearHandleFromMSFS { get => this.currentGearHandle; set { this.currentGearHandle = value; } }
        public Int32 GearFront { get; set; }
        public Int32 GearLeft { get; set; }
        public Int32 GearRight { get; set; }
        public Int32 CurrentAPHeading { get => this.currentAPHeading; set { this.currentAPHeading = value; this.SetToMSFS = true; } }
        public Int32 CurrentAPHeadingFromMSFS { get => this.currentAPHeading; set { this.currentAPHeading = value; } }
        public Int32 ApSwitch { get => this.apSwitch; set { this.apSwitch = value; this.SetToMSFS = true; } }
        public Int32 ApSwitchFromMSFS { get => this.apSwitch; set { this.apSwitch = value; } }
        public Int32 ApThrottleSwitch { get => this.apThrottleSwitch; set { this.apThrottleSwitch = value; this.SetToMSFS = true; } }
        public Int32 ApThrottleSwitchFromMSFS { get => this.apThrottleSwitch; set { this.apThrottleSwitch = value; } }
        public Int32 ApAltHoldSwitch { get => this.apAltHoldSwitch; set { this.apAltHoldSwitch = value; this.SetToMSFS = true; } }
        public Int32 ApAltHoldSwitchFromMSFS { get => this.apAltHoldSwitch; set { this.apAltHoldSwitch = value; } }
        public Int32 ApHeadHoldSwitch { get => this.apHeadHoldSwitch; set { this.apHeadHoldSwitch = value; this.SetToMSFS = true; } }
        public Int32 ApHeadHoldSwitchFromMSFS { get => this.apHeadHoldSwitch; set { this.apHeadHoldSwitch = value; } }
        public Int32 ApVSHoldSwitch { get => this.apVSHoldSwitch; set { this.apVSHoldSwitch = value; this.SetToMSFS = true; } }
        public Int32 ApVSHoldSwitchFromMSFS { get => this.apVSHoldSwitch; set { this.apVSHoldSwitch = value; } }
        public Int32 ApNavHoldSwitch { get => this.apNavHoldSwitch; set { this.apNavHoldSwitch = value; this.SetToMSFS = true; } }
        public Int32 ApNavHoldSwitchFromMSFS { get => this.apNavHoldSwitch; set { this.apNavHoldSwitch = value; } }
        public Int32 ApSpeedHoldSwitch { get => this.apSpeedHoldSwitch; set { this.apSpeedHoldSwitch = value; this.SetToMSFS = true; } }
        public Int32 ApSpeedHoldSwitchFromMSFS { get => this.apSpeedHoldSwitch; set { this.apSpeedHoldSwitch = value; } }
        public Int32 CurrentAltitude { get; set; }
        public Int32 CurrentAPAltitude { get => this.currentAPAltitude; set { this.currentAPAltitude = value; this.SetToMSFS = true; } }
        public Int32 CurrentSpeed { get; set; }
        public Int32 CurrentAPSpeed { get => this.currentAPSpeed; set { this.currentAPSpeed = value; this.SetToMSFS = true; } }
        public Int32 CurrentAPSpeedFromMSFS { get => this.currentAPSpeed; set { this.currentAPSpeed = value; } }
        public Int32 CurrentAPAltitudeFromMSFS { get => this.currentAPAltitude; set { this.currentAPAltitude = value; } }
        public Int32 CurrentVerticalSpeed { get; set; }
        public Int32 CurrentAPVerticalSpeed { get => this.currentAPVerticalSpeed; set { this.currentAPVerticalSpeed = value; this.SetToMSFS = true; } }
        public Int32 CurrentAPVerticalSpeedFromMSFS { get => this.currentAPVerticalSpeed; set { this.currentAPVerticalSpeed = value; } }
        public Int64 ApNextWPID { get; set; }
        public Int32 ApNextWPETE { get; set; }
        public Double ApNextWPDist { get; set; }
        public Double ApNextWPHeading { get; set; }
        public Boolean SetToMSFS { get => this.setToMSFS; set { this.setToMSFS = value; this.changed(); } }
        public Int32 CurrentBrakes { get => this.currentBrakes; set { this.currentBrakes = value; this.SetToMSFS = true; } }
        public Int32 CurrentBrakesFromMSFS { get => this.currentBrakes; set { this.currentBrakes = value; } }
        public Int16 CurrentThrottle { get => this.currentTrottle; set { this.currentTrottle = value; this.SetToMSFS = true; } }
        public Int16 CurrentThrottleFromMSFS { get => this.currentTrottle; set { this.currentTrottle = value; } }
        public Int32 ThrottleLowerFromMSFS { get; set; }
        public Int32 CurrentSpoiler { get => this.currentSpoiler; set { this.currentSpoiler = value; this.SetToMSFS = true; } }
        public Int32 CurrentSpoilerFromMSFS { get => this.currentSpoiler; set { this.currentSpoiler = value; } }
        public Boolean Connected { get => this.connected; set { this.connected = value; this.changed(); } }
        public Boolean TryConnect { get => this.tryingToconnect; set { this.tryingToconnect = value; this.changed(); } }
        public Boolean ValuesDisplayed { get => this.valuesDisplayed; set { this.valuesDisplayed = value; SimulatorDAO.Initialise(); } }

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
