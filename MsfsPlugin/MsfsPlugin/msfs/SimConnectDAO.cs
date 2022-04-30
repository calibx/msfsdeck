namespace Loupedeck.MsfsPlugin.msfs
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using Microsoft.FlightSimulator.SimConnect;

    public class SimConnectDAO
    {
        private static readonly Lazy<SimConnectDAO> lazy = new Lazy<SimConnectDAO>(() => new SimConnectDAO());
        public static SimConnectDAO Instance => lazy.Value;

        public const Int32 WM_USER_SIMCONNECT = 0x0402;
        private const UInt32 TUG_ANGLE = 4294967295;

        private SimConnect m_oSimConnect = null;

        private Plugin pluginForKey;

        private static readonly System.Timers.Timer timer = new System.Timers.Timer();
        private enum DATA_REQUESTS
        {
            REQUEST_1
        }
        enum EVENTS
        {
            GEAR_TOGGLE,
            PARKING_BRAKE,
            ENGINE_AUTO_START,
            ENGINE_AUTO_SHUTDOWN,
            PAUSE_ON,
            PAUSE_OFF,
            PITOT_HEAT_TOGGLE,
            TOGGLE_PUSHBACK,
            KEY_TUG_HEADING,
            TUG_DISABLE,
            NAV_LIGHTS_SET,
            LANDING_LIGHTS_SET,
            BEACON_LIGHTS_SET,
            TAXI_LIGHTS_SET,
            STROBES_SET,
            PANEL_LIGHTS_SET,
            RECOGNITION_LIGHTS_SET,
            WING_LIGHTS_SET,
            LOGO_LIGHTS_SET,
            CABIN_LIGHTS_SET,
            ATC_MENU_OPEN,
            ATC_MENU_CLOSE,
            ATC_MENU_0,
            ATC_MENU_1,
            ATC_MENU_2,
            ATC_MENU_3,
            ATC_MENU_4,
            ATC_MENU_5,
            ATC_MENU_6,
            ATC_MENU_7,
            ATC_MENU_8,
            ATC_MENU_9,
            AP_PANEL_MACH_HOLD,
            AP_PANEL_ALTITUDE_HOLD,
            AP_PANEL_HEADING_HOLD,
            AP_MASTER,
            AP_NAV1_HOLD,
            AP_N1_HOLD,
            AP_PANEL_VS_HOLD,
            AP_ALT_VAR_SET_ENGLISH,
            HEADING_BUG_SET,
            AP_SPD_VAR_SET,
            AP_VS_VAR_SET_ENGLISH,
            KOHLSMAN_SET,
            AILERON_TRIM_SET,
            ELEVATOR_TRIM_SET,
            FLAPS_SET,
            AXIS_PROPELLER_SET,
            RUDDER_TRIM_SET,
            AXIS_SPOILER_SET,
            THROTTLE_SET,
        };
        private enum DEFINITIONS
        {
            Readers,
            Writers,
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct Readers
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            public String title;
            public Double latitude;
            public Double longitude;
            public Double groundaltitude;
            public Double gearRightPos;
            public Double gearLeftPos;
            public Double gearCenterPos;
            public Int64 gearRetractable;
            public Int64 parkingBrake;
            public Int64 engineType;
            public Int64 E1N1;
            public Int64 E2N1;
            public Int64 E3N1;
            public Int64 E4N1;
            public Int64 fuelCapacity;
            public Int64 fuelQuantity;
            public Int64 E1GPH;
            public Int64 E2GPH;
            public Int64 E3GPH;
            public Int64 E4GPH;
            public Int64 pushback;
            public Int64 ENG1N1RPM;
            public Int64 ENG2N1RPM;
            public Int64 engineNumber;
            public Int64 planeAltitude;
            public Int64 apAltitude;
            public Int64 wpID;
            public Int64 wpDistance;
            public Int64 wpETE;
            public Int64 wpBearing;
            public Int64 wpCount;
            public Int64 apHeading;
            public Double planeHeading;
            public Int64 planeSpeed;
            public Double planeVSpeed;
            public Int64 apVSpeed;
            public Int64 apSpeed;
            public Int64 E1On;

            public Int64 navLight;
            public Int64 beaconLight;
            public Int64 landingLight;
            public Int64 taxiLight;
            public Int64 strobeLight;
            public Int64 panelLight;
            public Int64 recognitionLight;
            public Int64 wingLight;
            public Int64 logoLight;
            public Int64 cabinLight;

            public Int64 apAltHold;
            public Int64 apHeadingHold;
            public Int64 apSpeedHold;
            public Int64 apThrottleHold;
            public Int64 apMasterHold;
            public Int64 apNavHold;
            public Int64 apVerticalSpeedHold;

            public Double kohlsmanInHb;

            public Double aileronTrim;
            public Double elevatorTrim;

            public Int64 flapMax;
            public Int64 flapPosition;

            public Int64 mixtureE1;
            public Double propellerE1;

            public Double rudderTrim;
            public Double spoiler;
            public Double throttle;
            public Int64 pitot;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct Writers
        {
            public Int64 mixtureE1;
            public Int64 mixtureE2;
            public Int64 mixtureE3;
            public Int64 mixtureE4;
        }

        public enum hSimconnect : int
        {
            group1
        }

        private SimConnectDAO() { }
        public static void Refresh(Object source, EventArgs e) => Instance.OnTick();
        public void setPlugin(Plugin plugin) => this.pluginForKey = plugin;

        public void Connect()
        {
            if (MsfsData.Instance.bindings[BindingKeys.CONNECTION].MsfsValue == 0)
            {
                Debug.WriteLine("Trying cnx");
                MsfsData.Instance.bindings[BindingKeys.CONNECTION].SetMsfsValue(2);
                try
                {
                    this.m_oSimConnect = new SimConnect("MSFS Plugin", new IntPtr(0), WM_USER_SIMCONNECT, null, 0);
                    this.m_oSimConnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(this.SimConnect_OnRecvOpen);
                    this.m_oSimConnect.OnRecvSimobjectDataBytype += new SimConnect.RecvSimobjectDataBytypeEventHandler(this.SimConnect_OnRecvSimobjectDataBytype);
                    MsfsData.Instance.Changed();
                    this.AddRequest();
                    lock (timer)
                    {
                        timer.Interval = 200;
                        timer.Elapsed += Refresh;
                        timer.Enabled = true;
                    }
                }
                catch (COMException ex)
                {
                    Debug.WriteLine(ex);
                    MsfsData.Instance.bindings[BindingKeys.CONNECTION].SetMsfsValue(0);
                }
            }
        }
        public void Disconnect()
        {
            timer.Enabled = false;
            if (this.m_oSimConnect != null)
            {
                this.m_oSimConnect.Dispose();
                this.m_oSimConnect = null;
            }

            MsfsData.Instance.bindings[BindingKeys.CONNECTION].SetMsfsValue(0);
            MsfsData.Instance.Changed();
        }
        private void SimConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            Debug.WriteLine("Cnx opened");
            MsfsData.Instance.bindings[BindingKeys.CONNECTION].SetMsfsValue(1);
            timer.Interval = 200;
        }
        private void SimConnect_OnRecvSimobjectDataBytype(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE data)
        {
            Debug.WriteLine("Received Data");
            var delay = true;
            var reader = (Readers)data.dwData[0];
            MsfsData.Instance.AircraftName = reader.title;
            MsfsData.Instance.EngineType = (Int32)reader.engineType;

            MsfsData.Instance.bindings[BindingKeys.ENGINE_AUTO].SetMsfsValue(reader.E1On);
            MsfsData.Instance.bindings[BindingKeys.AILERON_TRIM].SetMsfsValue((Int64)Math.Round(reader.aileronTrim * 100));
            MsfsData.Instance.bindings[BindingKeys.AP_ALT].SetMsfsValue(reader.apAltitude);
            MsfsData.Instance.bindings[BindingKeys.ALT].SetMsfsValue(reader.planeAltitude);
            MsfsData.Instance.bindings[BindingKeys.AP_ALT_INPUT].SetMsfsValue(reader.apAltitude);
            MsfsData.Instance.bindings[BindingKeys.ALT_INPUT].SetMsfsValue(reader.planeAltitude);
            MsfsData.Instance.bindings[BindingKeys.KOHLSMAN].SetMsfsValue((Int64)Math.Round(reader.kohlsmanInHb * 100));
            MsfsData.Instance.bindings[BindingKeys.ELEVATOR_TRIM].SetMsfsValue((Int64)Math.Round(reader.elevatorTrim * 100));
            MsfsData.Instance.bindings[BindingKeys.MAX_FLAP].SetMsfsValue(reader.flapMax);
            MsfsData.Instance.bindings[BindingKeys.FLAP].SetMsfsValue(reader.flapPosition);
            MsfsData.Instance.bindings[BindingKeys.AP_HEADING].SetMsfsValue(reader.apHeading);
            MsfsData.Instance.bindings[BindingKeys.HEADING].SetMsfsValue((Int64)Math.Round(reader.planeHeading));
            MsfsData.Instance.bindings[BindingKeys.AP_HEADING_INPUT].SetMsfsValue(reader.apHeading);
            MsfsData.Instance.bindings[BindingKeys.HEADING_INPUT].SetMsfsValue((Int64)Math.Round(reader.planeHeading));
            MsfsData.Instance.bindings[BindingKeys.MIXTURE].SetMsfsValue(reader.mixtureE1);
            MsfsData.Instance.bindings[BindingKeys.PROPELLER].SetMsfsValue((Int64)Math.Round(reader.propellerE1));
            MsfsData.Instance.bindings[BindingKeys.RUDDER_TRIM].SetMsfsValue((Int64)Math.Round(reader.rudderTrim * 100));
            MsfsData.Instance.bindings[BindingKeys.AP_SPEED].SetMsfsValue(reader.apSpeed);
            MsfsData.Instance.bindings[BindingKeys.SPEED].SetMsfsValue(reader.planeSpeed);
            MsfsData.Instance.bindings[BindingKeys.AP_SPEED_INPUT].SetMsfsValue(reader.apSpeed);
            MsfsData.Instance.bindings[BindingKeys.SPEED_INPUT].SetMsfsValue(reader.planeSpeed);
            MsfsData.Instance.bindings[BindingKeys.SPOILER].SetMsfsValue((Int64)Math.Round(reader.spoiler * 100));
            MsfsData.Instance.bindings[BindingKeys.THROTTLE].SetMsfsValue((Int64)Math.Round(reader.throttle * 100));
            MsfsData.Instance.bindings[BindingKeys.AP_VSPEED].SetMsfsValue(reader.apVSpeed);
            MsfsData.Instance.bindings[BindingKeys.VSPEED].SetMsfsValue((Int64)Math.Round(reader.planeVSpeed * 60));
            MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_INPUT].SetMsfsValue(reader.apVSpeed);
            MsfsData.Instance.bindings[BindingKeys.VSPEED_INPUT].SetMsfsValue((Int64)Math.Round(reader.planeVSpeed * 60));
            MsfsData.Instance.bindings[BindingKeys.PARKING_BRAKES].SetMsfsValue(reader.parkingBrake);
            MsfsData.Instance.bindings[BindingKeys.PITOT].SetMsfsValue(reader.pitot);
            MsfsData.Instance.bindings[BindingKeys.GEAR_RETRACTABLE].SetMsfsValue(reader.gearRetractable);
            MsfsData.Instance.bindings[BindingKeys.GEAR_FRONT].SetMsfsValue((Int64)Math.Round(reader.gearCenterPos * 10));
            MsfsData.Instance.bindings[BindingKeys.GEAR_LEFT].SetMsfsValue((Int64)Math.Round(reader.gearLeftPos * 10));
            MsfsData.Instance.bindings[BindingKeys.GEAR_RIGHT].SetMsfsValue((Int64)Math.Round(reader.gearRightPos * 10));
            MsfsData.Instance.bindings[BindingKeys.FUEL_FLOW].SetMsfsValue((Int64)(reader.E1GPH + reader.E2GPH + reader.E3GPH + reader.E4GPH));
            MsfsData.Instance.bindings[BindingKeys.FUEL_PERCENT].SetMsfsValue((Int64)(reader.fuelQuantity * 100 / reader.fuelCapacity));
            MsfsData.Instance.bindings[BindingKeys.FUEL_TIME_LEFT].SetMsfsValue((Int64)(reader.fuelQuantity / (Double)(reader.E1GPH + reader.E2GPH + reader.E3GPH + reader.E4GPH) * 3600));

            MsfsData.Instance.E1N1 = (Int32)reader.E1N1;
            MsfsData.Instance.E2N1 = (Int32)reader.E2N1;
            MsfsData.Instance.E3N1 = (Int32)reader.E3N1;
            MsfsData.Instance.E4N1 = (Int32)reader.E4N1;
            MsfsData.Instance.NumberOfEngines = (Int32)reader.engineNumber;
            MsfsData.Instance.E1Rpm = (Int32)reader.ENG1N1RPM;
            MsfsData.Instance.E2Rpm = (Int32)reader.ENG2N1RPM;


            MsfsData.Instance.PushbackFromMSFS = (Int16)reader.pushback;
            MsfsData.Instance.ApNextWPDist = reader.wpDistance * 0.00053996f;
            MsfsData.Instance.ApNextWPETE = (Int32)reader.wpETE;
            MsfsData.Instance.ApNextWPHeading = reader.wpBearing;
            MsfsData.Instance.ApNextWPID = reader.wpID;
            MsfsData.Instance.ApAltHoldSwitchState = reader.apAltHold == 1;
            MsfsData.Instance.ApHeadHoldSwitchState = reader.apHeadingHold == 1;
            MsfsData.Instance.ApSpeedHoldSwitchState = reader.apSpeedHold == 1;
            MsfsData.Instance.ApThrottleSwitchState = reader.apThrottleHold == 1;
            MsfsData.Instance.ApSwitchState = reader.apMasterHold == 1;
            MsfsData.Instance.ApNavHoldSwitchState = reader.apNavHold == 1;
            MsfsData.Instance.ApVSHoldSwitchState = reader.apVerticalSpeedHold == 1;

            MsfsData.Instance.NavigationLightState = reader.navLight == 1;
            MsfsData.Instance.LandingLightState = reader.landingLight == 1;
            MsfsData.Instance.BeaconLightState = reader.beaconLight == 1;
            MsfsData.Instance.TaxiLightState = reader.taxiLight == 1;
            MsfsData.Instance.StrobesLightState = reader.strobeLight == 1;
            MsfsData.Instance.InstrumentsLightState = reader.panelLight == 1;
            MsfsData.Instance.RecognitionLightState = reader.recognitionLight == 1;
            MsfsData.Instance.WingLightState = reader.wingLight == 1;
            MsfsData.Instance.LogoLightState = reader.logoLight == 1;
            MsfsData.Instance.CabinLightState = reader.cabinLight == 1;

            var pushChanged = false;
            UInt32 tug_angle = 0;
            if (MsfsData.Instance.PushbackLeft == 1)
            {
                tug_angle = (UInt32)(TUG_ANGLE * 0.8);
                this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.KEY_TUG_HEADING, (UInt32)tug_angle, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                MsfsData.Instance.PushbackLeft = 0;
            }
            if (MsfsData.Instance.PushbackRight == 1)
            {
                tug_angle = TUG_ANGLE / 8;
                this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.KEY_TUG_HEADING, (UInt32)tug_angle, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                MsfsData.Instance.PushbackRight = 0;
            }
            if (MsfsData.Instance.PushbackClick == 1)
            {
                if (MsfsData.Instance.Pushback == 0)
                {
                    this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.TUG_DISABLE, 0, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                }
                else
                {
                    tug_angle = 0;
                    pushChanged = MsfsData.Instance.Pushback == 3;
                    this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.KEY_TUG_HEADING, 0, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                    if (pushChanged)
                        this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.TOGGLE_PUSHBACK, 0, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                }
                MsfsData.Instance.PushbackClick = 0;
            }

            if (MsfsData.Instance.SetToMSFS)
            {
                MsfsData.Instance.SetToMSFS = false;
                delay = true;
            }
            else
            {
                if (!delay)
                {
                    MsfsData.Instance.CurrentAPHeadingState = (Int32)reader.apHeading;
                    MsfsData.Instance.CurrentAPSpeedState = (Int32)reader.apSpeed;
                    MsfsData.Instance.CurrentAPVerticalSpeedState = (Int32)reader.apVSpeed;
                    MsfsData.Instance.CurrentAPAltitude = MsfsData.Instance.CurrentAPAltitudeState;
                    MsfsData.Instance.CurrentAPHeading = MsfsData.Instance.CurrentAPHeadingState;
                    MsfsData.Instance.CurrentAPSpeed = MsfsData.Instance.CurrentAPSpeedState;
                    MsfsData.Instance.CurrentAPVerticalSpeed = MsfsData.Instance.CurrentAPVerticalSpeedState;
                }
                delay = false;
            }

            this.SendEvent(MsfsData.Instance.NavigationLight, EVENTS.NAV_LIGHTS_SET, MsfsData.Instance.NavigationLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.LandingLight, EVENTS.LANDING_LIGHTS_SET, MsfsData.Instance.LandingLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.BeaconLight, EVENTS.BEACON_LIGHTS_SET, MsfsData.Instance.BeaconLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.TaxiLight, EVENTS.TAXI_LIGHTS_SET, MsfsData.Instance.TaxiLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.StrobesLight, EVENTS.STROBES_SET, MsfsData.Instance.StrobesLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.InstrumentsLight, EVENTS.PANEL_LIGHTS_SET, MsfsData.Instance.InstrumentsLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.RecognitionLight, EVENTS.RECOGNITION_LIGHTS_SET, MsfsData.Instance.RecognitionLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.WingLight, EVENTS.WING_LIGHTS_SET, MsfsData.Instance.WingLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.LogoLight, EVENTS.LOGO_LIGHTS_SET, MsfsData.Instance.LogoLightState ? 0 : 1);
            this.SendEvent(MsfsData.Instance.CabinLight, EVENTS.CABIN_LIGHTS_SET, MsfsData.Instance.CabinLightState ? 0 : 1);
            //this.SendEvent(MsfsData.Instance.ATC, EVENTS.ATC_MENU_OPEN, 0); // => with key waiting for simconnect inclusion
            //this.SendEvent(MsfsData.Instance.ATCClose, EVENTS.ATC_MENU_CLOSE, 0);
            this.SendEvent(MsfsData.Instance.ATC0, EVENTS.ATC_MENU_0, 0);
            this.SendEvent(MsfsData.Instance.ATC1, EVENTS.ATC_MENU_1, 0);
            this.SendEvent(MsfsData.Instance.ATC2, EVENTS.ATC_MENU_2, 0);
            this.SendEvent(MsfsData.Instance.ATC3, EVENTS.ATC_MENU_3, 0);
            this.SendEvent(MsfsData.Instance.ATC4, EVENTS.ATC_MENU_4, 0);
            this.SendEvent(MsfsData.Instance.ATC5, EVENTS.ATC_MENU_5, 0);
            this.SendEvent(MsfsData.Instance.ATC6, EVENTS.ATC_MENU_6, 0);
            this.SendEvent(MsfsData.Instance.ATC7, EVENTS.ATC_MENU_7, 0);
            this.SendEvent(MsfsData.Instance.ATC8, EVENTS.ATC_MENU_8, 0);
            this.SendEvent(MsfsData.Instance.ATC9, EVENTS.ATC_MENU_9, 0);

            this.SendEvent(MsfsData.Instance.ApSpeedHoldSwitch, EVENTS.AP_PANEL_MACH_HOLD, 0);
            this.SendEvent(MsfsData.Instance.ApAltHoldSwitch, EVENTS.AP_PANEL_ALTITUDE_HOLD, 0);
            this.SendEvent(MsfsData.Instance.ApHeadHoldSwitch, EVENTS.AP_PANEL_HEADING_HOLD, 0);
            this.SendEvent(MsfsData.Instance.ApSwitch, EVENTS.AP_MASTER, 0);
            this.SendEvent(MsfsData.Instance.ApNavHoldSwitch, EVENTS.AP_NAV1_HOLD, 0);
            this.SendEvent(MsfsData.Instance.ApThrottleSwitch, EVENTS.AP_N1_HOLD, 0);
            this.SendEvent(MsfsData.Instance.ApVSHoldSwitch, EVENTS.AP_PANEL_VS_HOLD, 0);

            this.SendEvent(EVENTS.AILERON_TRIM_SET, MsfsData.Instance.bindings[BindingKeys.AILERON_TRIM]);
            this.SendEvent(EVENTS.AP_ALT_VAR_SET_ENGLISH, MsfsData.Instance.bindings[BindingKeys.AP_ALT]);
            this.SendEvent(EVENTS.AP_ALT_VAR_SET_ENGLISH, MsfsData.Instance.bindings[BindingKeys.AP_ALT_INPUT]);
            this.SendEvent(EVENTS.KOHLSMAN_SET, MsfsData.Instance.bindings[BindingKeys.KOHLSMAN]);
            this.SendEvent(EVENTS.ELEVATOR_TRIM_SET, MsfsData.Instance.bindings[BindingKeys.ELEVATOR_TRIM]);
            this.SendEvent(EVENTS.FLAPS_SET, MsfsData.Instance.bindings[BindingKeys.FLAP]);
            this.SendEvent(EVENTS.HEADING_BUG_SET, MsfsData.Instance.bindings[BindingKeys.AP_HEADING]);
            this.SendEvent(EVENTS.HEADING_BUG_SET, MsfsData.Instance.bindings[BindingKeys.AP_HEADING_INPUT]);
            this.SendEvent(EVENTS.AXIS_PROPELLER_SET, MsfsData.Instance.bindings[BindingKeys.PROPELLER]);
            this.SendEvent(EVENTS.RUDDER_TRIM_SET, MsfsData.Instance.bindings[BindingKeys.RUDDER_TRIM]);
            this.SendEvent(EVENTS.AP_SPD_VAR_SET, MsfsData.Instance.bindings[BindingKeys.AP_SPEED]);
            this.SendEvent(EVENTS.AP_SPD_VAR_SET, MsfsData.Instance.bindings[BindingKeys.AP_SPEED_INPUT]);
            this.SendEvent(EVENTS.AXIS_SPOILER_SET, MsfsData.Instance.bindings[BindingKeys.SPOILER]);
            this.SendEvent(EVENTS.THROTTLE_SET, MsfsData.Instance.bindings[BindingKeys.THROTTLE]);
            this.SendEvent(EVENTS.AP_VS_VAR_SET_ENGLISH, MsfsData.Instance.bindings[BindingKeys.AP_VSPEED]);
            this.SendEvent(EVENTS.AP_VS_VAR_SET_ENGLISH, MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_INPUT]);
            this.SendEvent(EVENTS.PARKING_BRAKE, MsfsData.Instance.bindings[BindingKeys.PARKING_BRAKES]);
            this.SendEvent(EVENTS.PITOT_HEAT_TOGGLE, MsfsData.Instance.bindings[BindingKeys.PITOT]);
            this.SendEvent(EVENTS.GEAR_TOGGLE, MsfsData.Instance.bindings[BindingKeys.GEAR_FRONT]);

            if (MsfsData.Instance.bindings[BindingKeys.ENGINE_AUTO].MsfsValue == 1)
            {
                this.SendEvent(EVENTS.ENGINE_AUTO_SHUTDOWN, MsfsData.Instance.bindings[BindingKeys.ENGINE_AUTO]);
            }
            else
            {
                this.SendEvent(EVENTS.ENGINE_AUTO_START, MsfsData.Instance.bindings[BindingKeys.ENGINE_AUTO]);
            }
            if (MsfsData.Instance.bindings[BindingKeys.PAUSE].ControllerChanged)
            { 
                if (MsfsData.Instance.bindings[BindingKeys.PAUSE].MsfsValue == 1)
                {
                    this.SendEvent(EVENTS.PAUSE_OFF, MsfsData.Instance.bindings[BindingKeys.PAUSE]);
                    MsfsData.Instance.bindings[BindingKeys.PAUSE].SetMsfsValue(0);
                    MsfsData.Instance.bindings[BindingKeys.PAUSE].MSFSChanged = true;
                }
                else
                {
                    this.SendEvent(EVENTS.PAUSE_ON, MsfsData.Instance.bindings[BindingKeys.PAUSE]);
                    MsfsData.Instance.bindings[BindingKeys.PAUSE].SetMsfsValue(1);
                    MsfsData.Instance.bindings[BindingKeys.PAUSE].MSFSChanged = true;
                }
            }
            var writer = new Writers();
            if (MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerChanged)
            {
                writer.mixtureE1 = MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerValue;
                writer.mixtureE2 = MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerValue;
                writer.mixtureE3 = MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerValue;
                writer.mixtureE4 = MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerValue;
                this.m_oSimConnect.SetDataOnSimObject(DEFINITIONS.Writers, SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_DATA_SET_FLAG.DEFAULT, writer);
                MsfsData.Instance.bindings[BindingKeys.MIXTURE].ResetController();
            }

            if (MsfsData.Instance.ATC)
            {
                this.pluginForKey.ClientApplication.SendKeyboardShortcut((VirtualKeyCode)0x91);
            }


            this.ResetEvents();
            MsfsData.Instance.Changed();
        }



        private void SendEvent(EVENTS eventName, Binding binding)
        {
            if (binding.ControllerChanged)
            {
                UInt32 value = 0;
                Debug.WriteLine("Send " + eventName);
                switch (eventName)
                {
                    case EVENTS.KOHLSMAN_SET:
                        value = (UInt32)(binding.ControllerValue / 100f * 33.8639 * 16);
                        break;
                    case EVENTS.AILERON_TRIM_SET:
                        value = (UInt32)binding.ControllerValue;
                        break;
                    case EVENTS.AP_ALT_VAR_SET_ENGLISH:
                        value = (UInt32)binding.ControllerValue;
                        break;
                    case EVENTS.ELEVATOR_TRIM_SET:
                        value = (UInt32)(binding.ControllerValue / 100f * 16383);
                        break;
                    case EVENTS.FLAPS_SET:
                        value = (UInt32)(binding.ControllerValue * 16383 / (MsfsData.Instance.bindings[BindingKeys.MAX_FLAP].ControllerValue == 0 ? 1 : MsfsData.Instance.bindings[BindingKeys.MAX_FLAP].ControllerValue));
                        break;
                    case EVENTS.HEADING_BUG_SET:
                        value = (UInt32)binding.ControllerValue;
                        break;
                    case EVENTS.AXIS_PROPELLER_SET:
                        value = (UInt32)Math.Round((binding.ControllerValue - 50) * 16383 / 50f);
                        break;
                    case EVENTS.RUDDER_TRIM_SET:
                        value = (UInt32)binding.ControllerValue;
                        break;
                    case EVENTS.AP_SPD_VAR_SET:
                        value = (UInt32)binding.ControllerValue;
                        break;
                    case EVENTS.AXIS_SPOILER_SET:
                        value = (UInt32)Math.Round((binding.ControllerValue - 50) * 16383 / 50f);
                        break;
                    case EVENTS.THROTTLE_SET:
                        value = (UInt32)(binding.ControllerValue / 100f * 16383);
                        break;
                    case EVENTS.AP_VS_VAR_SET_ENGLISH:
                        value = (UInt32)binding.ControllerValue;
                        break;
                    case EVENTS.ENGINE_AUTO_SHUTDOWN:
                        value = (UInt32)binding.ControllerValue;
                        break;
                    case EVENTS.ENGINE_AUTO_START:
                        value = (UInt32)binding.ControllerValue;
                        break;
                    case EVENTS.PARKING_BRAKE:
                        value = (UInt32)binding.ControllerValue;
                        break;
                    case EVENTS.PITOT_HEAT_TOGGLE:
                        value = (UInt32)binding.ControllerValue;
                        break;
                    case EVENTS.GEAR_TOGGLE:
                        value = (UInt32)binding.ControllerValue;
                        break;
                }
                this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, eventName, value, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                binding.ResetController();
            }
        }

        private void SendEvent(Boolean inputKey, EVENTS eventName, Int64 value)
        {
            if (inputKey)
            {
                Debug.WriteLine("Send " + eventName);
                this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, eventName, (UInt32)value, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
            }
        }

        private void ResetEvents()
        {
            MsfsData.Instance.NavigationLight = false;
            MsfsData.Instance.LandingLight = false;
            MsfsData.Instance.BeaconLight = false;
            MsfsData.Instance.TaxiLight = false;
            MsfsData.Instance.StrobesLight = false;
            MsfsData.Instance.InstrumentsLight = false;
            MsfsData.Instance.RecognitionLight = false;
            MsfsData.Instance.WingLight = false;
            MsfsData.Instance.LogoLight = false;
            MsfsData.Instance.CabinLight = false;
            MsfsData.Instance.ATC = false;
            MsfsData.Instance.ATCClose = false;
            MsfsData.Instance.ATC0 = false;
            MsfsData.Instance.ATC1 = false;
            MsfsData.Instance.ATC2 = false;
            MsfsData.Instance.ATC3 = false;
            MsfsData.Instance.ATC4 = false;
            MsfsData.Instance.ATC5 = false;
            MsfsData.Instance.ATC6 = false;
            MsfsData.Instance.ATC7 = false;
            MsfsData.Instance.ATC8 = false;
            MsfsData.Instance.ATC9 = false;

            MsfsData.Instance.ApSpeedHoldSwitch = false;
            MsfsData.Instance.ApAltHoldSwitch = false;
            MsfsData.Instance.ApHeadHoldSwitch = false;
            MsfsData.Instance.ApSwitch = false;
            MsfsData.Instance.ApNavHoldSwitch = false;
            MsfsData.Instance.ApThrottleSwitch = false;
            MsfsData.Instance.ApVSHoldSwitch = false;
        }

        private void OnTick()
        {
            try
            { 
                if (this.m_oSimConnect != null)
                { 
                    this.m_oSimConnect.RequestDataOnSimObjectType(DATA_REQUESTS.REQUEST_1, DEFINITIONS.Readers, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                    this.m_oSimConnect.ReceiveMessage();
                }
            }
            catch (COMException exception)
            {
                Debug.Write(exception.ToString());
                this.Disconnect();
            }
        }

        private void AddRequest()
        {
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "TITLE", null, SIMCONNECT_DATATYPE.STRING256, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "Plane Latitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "Plane Longitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "Ground Altitude", "meters", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "GEAR RIGHT POSITION", "Boolean", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "GEAR LEFT POSITION", "Boolean", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "GEAR CENTER POSITION", "Boolean", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "IS GEAR RETRACTABLE", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "BRAKE PARKING POSITION", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "ENGINE TYPE", "Enum", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "TURB ENG N1:1", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "TURB ENG N1:2", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "TURB ENG N1:3", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "TURB ENG N1:4", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "FUEL TOTAL CAPACITY", "Gallon", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "FUEL TOTAL QUANTITY", "Gallon", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "ENG FUEL FLOW GPH:1", "Gallons per hour", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "ENG FUEL FLOW GPH:2", "Gallons per hour", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "ENG FUEL FLOW GPH:3", "Gallons per hour", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "ENG FUEL FLOW GPH:4", "Gallons per hour", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "PUSHBACK STATE:0", "Enum", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "PROP RPM:1", "RPM", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "PROP RPM:2", "RPM", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "NUMBER OF ENGINES", "Number", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "INDICATED ALTITUDE", "Feet", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT ALTITUDE LOCK VAR", "Feet", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "GPS FLIGHT PLAN WP INDEX", "Number", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "GPS WP DISTANCE", "Meters", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "GPS WP ETE", "Seconds", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "GPS WP BEARING", "Radians", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "GPS FLIGHT PLAN WP COUNT", "Number", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT HEADING LOCK DIR", "degrees", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "PLANE HEADING DEGREES MAGNETIC", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AIRSPEED INDICATED", "Knots", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "VERTICAL SPEED", "feet/second", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT VERTICAL HOLD VAR", "Feet per minute", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT AIRSPEED HOLD VAR", "Knots", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "ENG COMBUSTION:1", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT NAV", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT BEACON", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT LANDING", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT TAXI", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT STROBE", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT PANEL", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT RECOGNITION", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT WING", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT LOGO", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "LIGHT CABIN", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT ALTITUDE LOCK", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT HEADING LOCK", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT MACH HOLD", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT MANAGED THROTTLE ACTIVE", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT MASTER", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT NAV1 LOCK", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT VERTICAL HOLD", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "KOHLSMAN SETTING HG:1", "inHg", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AILERON TRIM PCT", "Number", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "ELEVATOR TRIM PCT", "Percent Over 100", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "FLAPS NUM HANDLE POSITIONS", "Number", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "FLAPS HANDLE INDEX", "Number", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "GENERAL ENG MIXTURE LEVER POSITION:1", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "GENERAL ENG PROPELLER LEVER POSITION:1", "Percent", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "RUDDER TRIM PCT", "Percent Over 100", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "SPOILERS HANDLE POSITION", "Percent Over 100", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "GENERAL ENG THROTTLE LEVER POSITION:1", "Percent Over 100", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "PITOT HEAT SWITCH:1", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Writers, "GENERAL ENG MIXTURE LEVER POSITION:1", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Writers, "GENERAL ENG MIXTURE LEVER POSITION:2", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Writers, "GENERAL ENG MIXTURE LEVER POSITION:3", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Writers, "GENERAL ENG MIXTURE LEVER POSITION:4", "Percent", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.GEAR_TOGGLE, "GEAR_TOGGLE");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.PARKING_BRAKE, "PARKING_BRAKES");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ENGINE_AUTO_SHUTDOWN, "ENGINE_AUTO_SHUTDOWN");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ENGINE_AUTO_START, "ENGINE_AUTO_START");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.PAUSE_ON, "PAUSE_ON");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.PAUSE_OFF, "PAUSE_OFF");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.PITOT_HEAT_TOGGLE, "PITOT_HEAT_TOGGLE");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.TOGGLE_PUSHBACK, "TOGGLE_PUSHBACK");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.KEY_TUG_HEADING, "KEY_TUG_HEADING");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.TUG_DISABLE, "TUG_DISABLE");

            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.NAV_LIGHTS_SET, "NAV_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.LANDING_LIGHTS_SET, "LANDING_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.BEACON_LIGHTS_SET, "BEACON_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.TAXI_LIGHTS_SET, "TAXI_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.STROBES_SET, "STROBES_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.PANEL_LIGHTS_SET, "PANEL_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.RECOGNITION_LIGHTS_SET, "RECOGNITION_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.WING_LIGHTS_SET, "WING_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.LOGO_LIGHTS_SET, "LOGO_LIGHTS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.CABIN_LIGHTS_SET, "CABIN_LIGHTS_SET");

            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_OPEN, "ATC_MENU_OPEN");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_CLOSE, "SIMUI_WINDOW_HIDESHOW");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_0, "ATC_MENU_0");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_1, "ATC_MENU_1");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_2, "ATC_MENU_2");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_3, "ATC_MENU_3");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_4, "ATC_MENU_4");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_5, "ATC_MENU_5");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_6, "ATC_MENU_6");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_7, "ATC_MENU_7");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_8, "ATC_MENU_8");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ATC_MENU_9, "ATC_MENU_9");

            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AP_PANEL_MACH_HOLD, "AP_PANEL_MACH_HOLD");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AP_PANEL_ALTITUDE_HOLD, "AP_PANEL_ALTITUDE_HOLD");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AP_PANEL_HEADING_HOLD, "AP_PANEL_HEADING_HOLD");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AP_MASTER, "AP_MASTER");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AP_NAV1_HOLD, "AP_NAV1_HOLD");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AP_N1_HOLD, "AP_N1_HOLD");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AP_PANEL_VS_HOLD, "AP_PANEL_VS_HOLD");

            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AP_ALT_VAR_SET_ENGLISH, "AP_ALT_VAR_SET_ENGLISH");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.HEADING_BUG_SET, "HEADING_BUG_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AP_SPD_VAR_SET, "AP_SPD_VAR_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AP_VS_VAR_SET_ENGLISH, "AP_VS_VAR_SET_ENGLISH");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.KOHLSMAN_SET, "KOHLSMAN_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AILERON_TRIM_SET, "AILERON_TRIM_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.ELEVATOR_TRIM_SET, "ELEVATOR_TRIM_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.FLAPS_SET, "FLAPS_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AXIS_PROPELLER_SET, "AXIS_PROPELLER_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.RUDDER_TRIM_SET, "RUDDER_TRIM_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AXIS_SPOILER_SET, "AXIS_SPOILER_SET");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.THROTTLE_SET, "THROTTLE_SET");

            this.m_oSimConnect.RegisterDataDefineStruct<Readers>(DEFINITIONS.Readers);
            this.m_oSimConnect.RegisterDataDefineStruct<Readers>(DEFINITIONS.Writers);
        }
    }
}



