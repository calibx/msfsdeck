namespace Loupedeck.MsfsPlugin
{
    using System;
    using System.Collections.Generic;
    class MsfsData
    {
        private readonly List<Notifiable> notifiables = new List<Notifiable>();

        public Dictionary<BindingKeys, Binding> bindings = new Dictionary<BindingKeys, Binding>();

        private static readonly Lazy<MsfsData> lazy = new Lazy<MsfsData>(() => new MsfsData());
        public static MsfsData Instance => lazy.Value;
        public Boolean folderDisplayed { get; set; }
        public Boolean DEBUG { get; set; }
        public Int16 refreshLimiter { get; set; }
        public Int16 PushbackFromMSFS { get; set; }
        public Int16 PushbackClick { get; set; }
        public Int16 Pushback { get => this.PushbackFromMSFS; }
        public Int16 PushbackLeft { get; set; }
        public Int16 PushbackRight { get; set; }
        public Boolean LandingLightState { get; set; }
        public Int16 AutoTaxi { get; set; }
        public Int16 AutoTaxiSwitch { get; set; }
        public Boolean ATC { get; set; }
        public Boolean ATCClose { get; set; }
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
        public Int32 E1Rpm { get; set; }
        public Int32 E2Rpm { get; set; }
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
        public String DebugValue1 { get; set; }
        public String DebugValue2 { get; set; }
        public String DebugValue3 { get; set; }
        public Byte GearRetractable { get; set; }
        public Int32 CurrentGearHandle { get => this.CurrentGearHandleFromMSFS; set { this.CurrentGearHandleFromMSFS = value; this.SetToMSFS = true; } }
        public Int32 CurrentGearHandleFromMSFS { get; set; }
        public Double GearFront { get; set; }
        public Double GearLeft { get; set; }
        public Double GearRight { get; set; }
        public Int32 CurrentAPHeading { get; set; }
        public Int32 CurrentAPHeadingState { get; set; }
        public Boolean ApSwitch { get; set; }
        public Boolean ApSwitchState { get; set; }
        public Boolean APPSwitch { get => this.APPSwitchFromMSFS; set { this.APPSwitchFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean APPSwitchFromMSFS { get; set; }
        public Boolean FDSwitch { get => this.FDSwitchFromMSFS; set { this.FDSwitchFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean FDSwitchFromMSFS { get; set; }
        public Boolean LOCSwitch { get => this.LOCSwitchFromMSFS; set { this.LOCSwitchFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean LOCSwitchFromMSFS { get; set; }
        public Boolean FLCSwitch { get => this.FLCSwitchFromMSFS; set { this.FLCSwitchFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean FLCSwitchFromMSFS { get; set; }
        public Boolean MasterSwitch { get => this.MasterSwitchFromMSFS; set { this.MasterSwitchFromMSFS = value; this.SetToMSFS = true; } }
        public Boolean MasterSwitchFromMSFS { get; set; }
        public Boolean ApThrottleSwitch { get; set; }
        public Boolean ApThrottleSwitchState { get; set; }
        public Boolean ApAltHoldSwitch { get; set; }
        public Boolean ApAltHoldSwitchState { get; set; }
        public Boolean ApHeadHoldSwitch { get; set; }
        public Boolean ApHeadHoldSwitchState { get; set; }
        public Boolean ApVSHoldSwitch { get; set; }
        public Boolean ApVSHoldSwitchState { get; set; }
        public Boolean ApNavHoldSwitch { get; set; }
        public Boolean ApNavHoldSwitchState { get; set; }
        public Boolean ApSpeedHoldSwitch { get; set; }
        public Boolean ApSpeedHoldSwitchState { get; set; }
        public Int32 CurrentAltitude { get; set; }
        public Int32 CurrentAPAltitude { get; set; }
        public Int32 CurrentSpeed { get; set; }
        public Int32 CurrentAPSpeed { get; set; }
        public Int32 CurrentAPSpeedState { get; set; }
        public Int32 CurrentAPAltitudeState { get; set; }
        public Int32 CurrentVerticalSpeed { get; set; }
        public Int32 CurrentAPVerticalSpeed { get; set; }
        public Int32 CurrentAPVerticalSpeedState { get; set; }
        public Int64 ApNextWPID { get; set; }
        public Int32 ApNextWPETE { get; set; }
        public Double ApNextWPDist { get; set; }
        public Double ApNextWPHeading { get; set; }
        public Boolean SetToMSFS { get; set; }
        public Boolean NavigationLight { get; set; }
        public Boolean BeaconLight { get; set; }
        public Boolean LandingLight { get; set; }
        public Boolean TaxiLight { get; set; }
        public Boolean StrobesLight { get; set; }
        public Boolean InstrumentsLight { get; set; }
        public Boolean RecognitionLight { get; set; }
        public Boolean WingLight { get; set; }
        public Boolean LogoLight { get; set; }
        public Boolean CabinLight { get; set; }
        public Boolean NavigationLightState { get; set; }
        public Boolean BeaconLightState { get; set; }
        public Boolean TaxiLightState { get; set; }
        public Boolean StrobesLightState { get; set; }
        public Boolean InstrumentsLightState { get; set; }
        public Boolean RecognitionLightState { get; set; }
        public Boolean WingLightState { get; set; }
        public Boolean LogoLightState { get; set; }
        public Boolean CabinLightState { get; set; }
        private MsfsData()
        {
        }

        public void Register(Notifiable notif) => this.notifiables.Add(notif);

        public Binding Register(Binding binding)
        {
            if (!this.bindings.ContainsKey(binding.Key))
            {
                this.bindings.Add(binding.Key, binding);
            }
            return this.bindings[binding.Key];
        } 

        public void Changed()
        {
            lock (this)
            {
                foreach (Notifiable notifiable in this.notifiables)
                {
                    notifiable.Notify();
                }
                this.refreshLimiter = 0;
            }
        }
    }
}

