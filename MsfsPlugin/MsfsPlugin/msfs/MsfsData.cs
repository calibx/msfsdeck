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
        private Int16 currentAPVerticalSpeed;
        private Int32 currentAPSpeed;

        private Int32 apSwitch;
        private Boolean masterSwitch;
        private Int32 apThrottleSwitch;
        private Int32 apAltHoldSwitch;
        private Int32 apHeadHoldSwitch;
        private Int32 apVSHoldSwitch;
        private Int32 apNavHoldSwitch;
        private Int32 apSpeedHoldSwitch;


        private Int32 currentBrakes;
        private Int16 currentTrottle;
        private Int32 currentFlap;

        private Int32 currentSpoiler;
        private Int16 currentAileronTrim;
        private Int16 currentRudderTrim;
        private Int16 currentElevatorTrim;
        private Int32 currentGearHandle;

        private Int32 currentZoom;
        private Int32 currentMixture;
        private Boolean currentPitot;

        private Boolean navigationLight;
        private Boolean beaconLight;
        private Boolean landingLight;
        private Boolean taxiLight;
        private Boolean strobesLight;
        private Boolean instrumentsLight;
        private Boolean recognitionLight;
        private Boolean wingLight;
        private Boolean logoLight;
        private Boolean cabinLight;

        private Boolean connected;
        private Boolean tryingToconnect;
        private Boolean setToMSFS;

        // Used to know if we try to autoconnect to FSUIPC
        private Boolean valuesDisplayed;

        private static readonly Lazy<MsfsData> lazy = new Lazy<MsfsData>(() => new MsfsData());
        public static MsfsData Instance => lazy.Value;
        public Int32 CurrentHeading { get; set; }
        public Int32 Rpm { get; set; }
        public Double E1N1 { get; set; }
        public Double E2N1 { get; set; }
        public Double E3N1 { get; set; }
        public Double E4N1 { get; set; }
        public Int32 Fps { get; set; }
        public Int32 RefreshRate { get; set; }
        public Int32 DebugValue { get; set; }
        public Int32 MaxFlap { get; set; }
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
        public Boolean MasterSwitch { get => this.masterSwitch; set { this.masterSwitch = value; this.SetToMSFS = true; } }
        public Boolean MasterSwitchFromMSFS { get => this.masterSwitch; set { this.masterSwitch = value; } }
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
        public Int16 CurrentAPVerticalSpeed { get => this.currentAPVerticalSpeed; set { this.currentAPVerticalSpeed = value; this.SetToMSFS = true; } }
        public Int16 CurrentAPVerticalSpeedFromMSFS { get => this.currentAPVerticalSpeed; set { this.currentAPVerticalSpeed = value; } }
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
        public Int16 CurrentAileronTrim { get => this.currentAileronTrim; set { this.currentAileronTrim = value; this.SetToMSFS = true; } }
        public Int16 CurrentAileronTrimFromMSFS { get => this.currentAileronTrim; set { this.currentAileronTrim = value; } }
        public Int16 CurrentRudderTrim { get => this.currentRudderTrim; set { this.currentRudderTrim = value; this.SetToMSFS = true; } }
        public Int16 CurrentRudderTrimFromMSFS { get => this.currentRudderTrim; set { this.currentRudderTrim = value; } }
        public Int16 CurrentElevatorTrim { get => this.currentElevatorTrim; set { this.currentElevatorTrim = value; this.SetToMSFS = true; } }
        public Int16 CurrentElevatorTrimFromMSFS { get => this.currentElevatorTrim; set { this.currentElevatorTrim = value; } }
        public Int32 CurrentZoom { get => this.currentZoom; set { this.currentZoom = value; this.SetToMSFS = true; } }
        public Int32 CurrentZoomFromMSFS { get => this.currentZoom; set { this.currentZoom = value; } }
        public Int32 CurrentMixture { get => this.currentMixture; set { this.currentMixture = value; this.SetToMSFS = true; } }
        public Int32 CurrentMixtureFromMSFS { get => this.currentMixture; set { this.currentMixture = value; } }
        public Int32 CurrentFlap { get => this.currentFlap; set { this.currentFlap = value; this.SetToMSFS = true; } }
        public Int32 CurrentFlapFromMSFS { get => this.currentFlap; set => this.currentFlap = value; }
        public Boolean CurrentPitot { get => this.currentPitot; set { this.currentPitot = value; this.SetToMSFS = true; } }
        public Boolean CurrentPitotFromMSFS { get => this.currentPitot; set { this.currentPitot = value; } }
        public Boolean Connected { get => this.connected; set { this.connected = value; } }
        public Boolean TryConnect { get => this.tryingToconnect; set { this.tryingToconnect = value; } }
        public Boolean ValuesDisplayed { get => this.valuesDisplayed; set { this.valuesDisplayed = value; SimulatorDAO.Initialise(); } }
        public Boolean NavigationLight { get => this.navigationLight; set { this.navigationLight = value; this.SetToMSFS = true; } }
        public Boolean NavigationLightFromMSFS { get => this.navigationLight; set { this.navigationLight = value; } }
        public Boolean BeaconLight { get => this.beaconLight; set { this.beaconLight = value; this.SetToMSFS = true; } }
        public Boolean BeaconLightFromMSFS { get => this.beaconLight; set { this.beaconLight = value; } }
        public Boolean LandingLight { get => this.landingLight; set { this.landingLight = value; this.SetToMSFS = true; } }
        public Boolean LandingLightFromMSFS { get => this.landingLight; set { this.landingLight = value; } }
        public Boolean TaxiLight { get => this.taxiLight; set { this.taxiLight = value; this.SetToMSFS = true; } }
        public Boolean TaxiLightFromMSFS { get => this.taxiLight; set { this.taxiLight = value; } }
        public Boolean StrobesLight { get => this.strobesLight; set { this.strobesLight = value; this.SetToMSFS = true; } }
        public Boolean StrobesLightFromMSFS { get => this.strobesLight; set { this.strobesLight = value; } }
        public Boolean InstrumentsLight { get => this.instrumentsLight; set { this.instrumentsLight = value; this.SetToMSFS = true; } }
        public Boolean InstrumentsLightFromMSFS { get => this.instrumentsLight; set { this.instrumentsLight = value; } }
        public Boolean RecognitionLight { get => this.recognitionLight; set { this.recognitionLight = value; this.SetToMSFS = true; } }
        public Boolean RecognitionLightFromMSFS { get => this.recognitionLight; set { this.recognitionLight = value; } }
        public Boolean WingLight { get => this.wingLight; set { this.wingLight = value; this.SetToMSFS = true; } }
        public Boolean WingLightFromMSFS { get => this.wingLight; set { this.wingLight = value; } }
        public Boolean LogoLight { get => this.logoLight; set { this.logoLight = value; this.SetToMSFS = true; } }
        public Boolean LogoLightFromMSFS { get => this.logoLight; set { this.logoLight = value; } }
        public Boolean CabinLight { get => this.cabinLight; set { this.cabinLight = value; this.SetToMSFS = true; } }
        public Boolean CabinLightFromMSFS { get => this.cabinLight; set { this.cabinLight = value; } }
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
