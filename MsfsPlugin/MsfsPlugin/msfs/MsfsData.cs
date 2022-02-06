namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;

    class MsfsData
    {
        private readonly List<Notifiable> notifiables = new List<Notifiable>();
        private Boolean setToMSFS;

        // Used to know if we try to autoconnect to FSUIPC
        private Boolean valuesDisplayed;

        private static readonly Lazy<MsfsData> lazy = new Lazy<MsfsData>(() => new MsfsData());
        public static MsfsData Instance => lazy.Value;

        public Boolean Pause { get => this.PauseFromMSFS; set { this.PauseFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean PauseFromMSFS { get; set; }
        public Boolean DEBUG { get; set; }
        public Boolean Pushback { get; set; }
        public Boolean ATC { get; set; }
        public Boolean ATC0 { get; set; }
        public Boolean ATC1 { get; set; }
        public Boolean ATC2 { get; set; }
        public Boolean ATC3 { get; set; }
        public Boolean ATC4 { get; set; }
        public Boolean ATC5 { get; set; }
        public Boolean ATC6 { get; set; }
        public Boolean ATC7 { get; set; }
        public Boolean ATC8 { get; set; }
        public Boolean ATC9 { get; set; }

        public Int32 CurrentHeading { get; set; }
        public Int32 Rpm { get; set; }
        public String AircraftName { get; set; }
        public Int32 FuelFlow { get; set; }
        public Int32 FuelPercent { get; set; }
        public Int32 FuelTimeLeft { get; set; }
        public Int32 EngineType { get; set; }
        public Double E1N1 { get; set; }
        public Double E2N1 { get; set; }
        public Double E3N1 { get; set; }
        public Double E4N1 { get; set; }
        public Int32 NumberOfEngines { get; set; }
        public Int32 Fps { get; set; }
        public Int32 RefreshRate { get; set; }
        public String DebugValue1 { get; set; }
        public String DebugValue2 { get; set; }
        public String DebugValue3 { get; set; }
        public Int32 MaxFlap { get; set; }
        public Byte GearOverSpeed { get; set; }
        public Byte GearRetractable { get; set; }
        public Int32 CurrentGearHandle { get => this.CurrentGearHandleFromMSFS; set { this.CurrentGearHandleFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentGearHandleFromMSFS { get; set; }
        public Int32 GearFront { get; set; }
        public Int32 GearLeft { get; set; }
        public Int32 GearRight { get; set; }
        public Int32 CurrentAPHeading { get => this.CurrentAPHeadingFromMSFS; set { this.CurrentAPHeadingFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentAPHeadingFromMSFS { get; set; }
        public Boolean ApSwitch { get => this.ApSwitchFromMSFS; set { this.ApSwitchFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean ApSwitchFromMSFS { get; set; }
        public Boolean MasterSwitch { get => this.MasterSwitchFromMSFS; set { this.MasterSwitchFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean MasterSwitchFromMSFS { get; set; }
        public Boolean ApThrottleSwitch { get => this.ApThrottleSwitchFromMSFS; set { this.ApThrottleSwitchFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean ApThrottleSwitchFromMSFS { get; set; }
        public Boolean ApAltHoldSwitch { get => this.ApAltHoldSwitchFromMSFS; set { this.ApAltHoldSwitchFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean ApAltHoldSwitchFromMSFS { get; set; }
        public Boolean ApHeadHoldSwitch { get => this.ApHeadHoldSwitchFromMSFS; set { this.ApHeadHoldSwitchFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean ApHeadHoldSwitchFromMSFS { get; set; }
        public Boolean ApVSHoldSwitch { get => this.ApVSHoldSwitchFromMSFS; set { this.ApVSHoldSwitchFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean ApVSHoldSwitchFromMSFS { get; set; }
        public Boolean ApNavHoldSwitch { get => this.ApNavHoldSwitchFromMSFS; set { this.ApNavHoldSwitchFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean ApNavHoldSwitchFromMSFS { get; set; }
        public Boolean ApSpeedHoldSwitch { get => this.ApSpeedHoldSwitchFromMSFS; set { this.ApSpeedHoldSwitchFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean ApSpeedHoldSwitchFromMSFS { get; set; }
        public Int32 CurrentAltitude { get; set; }
        public Int32 CurrentAPAltitude { get => this.CurrentAPAltitudeFromMSFS; set { this.CurrentAPAltitudeFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentSpeed { get; set; }
        public Int32 CurrentAPSpeed { get => this.CurrentAPSpeedFromMSFS; set { this.CurrentAPSpeedFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentAPSpeedFromMSFS { get; set; }
        public Int32 CurrentAPAltitudeFromMSFS { get; set; }
        public Int32 CurrentVerticalSpeed { get; set; }
        public Int32 CurrentAPVerticalSpeed { get => this.CurrentAPVerticalSpeedFromMSFS; set { this.CurrentAPVerticalSpeedFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentAPVerticalSpeedFromMSFS { get; set; }
        public Int64 ApNextWPID { get; set; }
        public Int32 ApNextWPETE { get; set; }
        public Double ApNextWPDist { get; set; }
        public Double ApNextWPHeading { get; set; }
        public Boolean SetToMSFS { get => this.setToMSFS; set { this.setToMSFS = value; this.Changed(); } }
        public Boolean CurrentBrakes { get => this.CurrentBrakesFromMSFS; set { this.CurrentBrakesFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean CurrentBrakesFromMSFS { get; set; }
        public Int32 CurrentThrottle { get => this.CurrentThrottleFromMSFS; set { this.CurrentThrottleFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentThrottleFromMSFS { get; set; }
        public Int32 ThrottleLowerFromMSFS { get; set; }
        public Int32 CurrentSpoiler { get => this.CurrentSpoilerFromMSFS; set { this.CurrentSpoilerFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentSpoilerFromMSFS { get; set; }
        public Int32 CurrentPropeller { get => this.CurrentPropellerFromMSFS; set { this.CurrentPropellerFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentPropellerFromMSFS { get; set; }
        public Int32 CurrentAileronTrim { get => this.CurrentAileronTrimFromMSFS; set { this.CurrentAileronTrimFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentAileronTrimFromMSFS { get; set; }
        public Int32 CurrentRudderTrim { get => this.CurrentRudderTrimFromMSFS; set { this.CurrentRudderTrimFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentRudderTrimFromMSFS { get; set; }
        public Int32 CurrentElevatorTrim { get => this.CurrentElevatorTrimFromMSFS; set { this.CurrentElevatorTrimFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentElevatorTrimFromMSFS { get; set; }
        public Int32 CurrentZoom { get => this.CurrentZoomFromMSFS; set { this.CurrentZoomFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentZoomFromMSFS { get; set; }
        public Int32 CurrentMixture { get => this.CurrentMixtureFromMSFS; set { this.CurrentMixtureFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentMixtureFromMSFS { get; set; }
        public Int32 CurrentFlap { get => this.CurrentFlapFromMSFS; set { this.CurrentFlapFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentFlapFromMSFS { get; set; }
        public Boolean CurrentPitot { get => this.CurrentPitotFromMSFS; set { this.CurrentPitotFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean CurrentPitotFromMSFS { get; set; }
        public Boolean Connected { get; set; }
        public Boolean TryConnect { get; set; }
        public Boolean ValuesDisplayed { get => this.valuesDisplayed; set { this.valuesDisplayed = value; SimulatorDAO.Initialise(); } }
        public Boolean NavigationLight { get => this.NavigationLightFromMSFS; set { this.NavigationLightFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean NavigationLightFromMSFS { get; set; }
        public Boolean BeaconLight { get => this.BeaconLightFromMSFS; set { this.BeaconLightFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean BeaconLightFromMSFS { get; set; }
        public Boolean LandingLight { get => this.LandingLightFromMSFS; set { this.LandingLightFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean LandingLightFromMSFS { get; set; }
        public Boolean TaxiLight { get => this.TaxiLightFromMSFS; set { this.TaxiLightFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean TaxiLightFromMSFS { get; set; }
        public Boolean StrobesLight { get => this.StrobesLightFromMSFS; set { this.StrobesLightFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean StrobesLightFromMSFS { get; set; }
        public Boolean InstrumentsLight { get => this.InstrumentsLightFromMSFS; set { this.InstrumentsLightFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean InstrumentsLightFromMSFS { get; set; }
        public Boolean RecognitionLight { get => this.RecognitionLightFromMSFS; set { this.RecognitionLightFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean RecognitionLightFromMSFS { get; set; }
        public Boolean WingLight { get => this.WingLightFromMSFS; set { this.WingLightFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean WingLightFromMSFS { get; set; }
        public Boolean LogoLight { get => this.LogoLightFromMSFS; set { this.LogoLightFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean LogoLightFromMSFS { get; set; }
        public Boolean CabinLight { get => this.CabinLightFromMSFS; set { this.CabinLightFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean CabinLightFromMSFS { get; set; }
        private MsfsData()
        {
        }

        public void Register(Notifiable notif) => this.notifiables.Add(notif);

        public void Changed()
        {
            foreach (Notifiable notifiable in this.notifiables)
            {
                notifiable.Notify();
            }
        }
    }
}
