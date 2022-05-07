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
            TUG_DISABLE,
            TOGGLE_NAV_LIGHTS,
            LANDING_LIGHTS_TOGGLE,
            TOGGLE_BEACON_LIGHTS,
            TOGGLE_TAXI_LIGHTS,
            STROBES_TOGGLE,
            PANEL_LIGHTS_TOGGLE,
            TOGGLE_RECOGNITION_LIGHTS,
            TOGGLE_WING_LIGHTS,
            TOGGLE_LOGO_LIGHTS,
            TOGGLE_CABIN_LIGHTS,
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
            KEY_TUG_HEADING,
            TOGGLE_FLIGHT_DIRECTOR,
            AP_FLIGHT_LEVEL_CHANGE,
            AP_APR_HOLD,
            AP_LOC_HOLD,
            BRAKES,
            THROTTLE_REVERSE_THRUST_TOGGLE,
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
            public Int64 wheelRPM;
            public Int64 apFD;
            public Int64 apFLC;
            public Int64 apAPP;
            public Int64 apLOC;
            public Int64 onGround;
            public Int64 groundSpeed;
            public Int64 pushbackAttached;
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
            MsfsData.Instance.bindings[BindingKeys.AP_NEXT_WP_ID].SetMsfsValue(reader.wpID);
            MsfsData.Instance.bindings[BindingKeys.AP_NEXT_WP_DIST].SetMsfsValue((Int64)Math.Round(reader.wpDistance * 0.00053996f, 1));
            MsfsData.Instance.bindings[BindingKeys.AP_NEXT_WP_ETE].SetMsfsValue(reader.wpETE);
            MsfsData.Instance.bindings[BindingKeys.AP_NEXT_WP_HEADING].SetMsfsValue(reader.wpBearing);
            MsfsData.Instance.bindings[BindingKeys.ENGINE_TYPE].SetMsfsValue(reader.engineType);
            MsfsData.Instance.bindings[BindingKeys.ENGINE_NUMBER].SetMsfsValue(reader.engineNumber);
            MsfsData.Instance.bindings[BindingKeys.E1RPM].SetMsfsValue(reader.ENG1N1RPM);
            MsfsData.Instance.bindings[BindingKeys.E2RPM].SetMsfsValue(reader.ENG2N1RPM);
            MsfsData.Instance.bindings[BindingKeys.E1N1].SetMsfsValue(reader.E1N1);
            MsfsData.Instance.bindings[BindingKeys.E2N1].SetMsfsValue(reader.E2N1);
            MsfsData.Instance.bindings[BindingKeys.E3N1].SetMsfsValue(reader.E3N1);
            MsfsData.Instance.bindings[BindingKeys.E4N1].SetMsfsValue(reader.E4N1);
            MsfsData.Instance.bindings[BindingKeys.PUSHBACK_ATTACHED].SetMsfsValue((reader.pushbackAttached == 1 && reader.wheelRPM != 0) ? 1 : 0);
            MsfsData.Instance.bindings[BindingKeys.PUSHBACK_STATE].SetMsfsValue(reader.onGround);
            //MsfsData.Instance.bindings[BindingKeys.PUSHBACK_ANGLE].SetMsfsValue(reader.pushback); // Can read but set so stay on the controller state
            MsfsData.Instance.bindings[BindingKeys.LIGHT_NAV_MULTI].SetMsfsValue(reader.navLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_BEACON_MULTI].SetMsfsValue(reader.beaconLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_LANDING_MULTI].SetMsfsValue(reader.landingLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_TAXI_MULTI].SetMsfsValue(reader.taxiLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_STROBE_MULTI].SetMsfsValue(reader.strobeLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_INSTRUMENT_MULTI].SetMsfsValue(reader.panelLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_RECOG_MULTI].SetMsfsValue(reader.recognitionLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_WING_MULTI].SetMsfsValue(reader.wingLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_LOGO_MULTI].SetMsfsValue(reader.logoLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_CABIN_MULTI].SetMsfsValue(reader.cabinLight);
            
            MsfsData.Instance.bindings[BindingKeys.LIGHT_NAV_FOLDER].SetMsfsValue(reader.navLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_BEACON_FOLDER].SetMsfsValue(reader.beaconLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_LANDING_FOLDER].SetMsfsValue(reader.landingLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_TAXI_FOLDER].SetMsfsValue(reader.taxiLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_STROBE_FOLDER].SetMsfsValue(reader.strobeLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_INSTRUMENT_FOLDER].SetMsfsValue(reader.panelLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_RECOG_FOLDER].SetMsfsValue(reader.recognitionLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_WING_FOLDER].SetMsfsValue(reader.wingLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_LOGO_FOLDER].SetMsfsValue(reader.logoLight);
            MsfsData.Instance.bindings[BindingKeys.LIGHT_CABIN_FOLDER].SetMsfsValue(reader.cabinLight);

            MsfsData.Instance.bindings[BindingKeys.AP_ALT_AP_FOLDER].SetMsfsValue(reader.apAltitude);
            MsfsData.Instance.bindings[BindingKeys.ALT_AP_FOLDER].SetMsfsValue(reader.planeAltitude);
            MsfsData.Instance.bindings[BindingKeys.AP_HEADING_AP_FOLDER].SetMsfsValue(reader.apHeading);
            MsfsData.Instance.bindings[BindingKeys.HEADING_AP_FOLDER].SetMsfsValue((Int64)Math.Round(reader.planeHeading));
            MsfsData.Instance.bindings[BindingKeys.AP_SPEED_AP_FOLDER].SetMsfsValue(reader.apSpeed);
            MsfsData.Instance.bindings[BindingKeys.SPEED_AP_FOLDER].SetMsfsValue(reader.planeSpeed);
            MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_AP_FOLDER].SetMsfsValue(reader.apVSpeed);
            MsfsData.Instance.bindings[BindingKeys.VSPEED_AP_FOLDER].SetMsfsValue((Int64)Math.Round(reader.planeVSpeed * 60));
            MsfsData.Instance.bindings[BindingKeys.AP_ALT_SWITCH_AP_FOLDER].SetMsfsValue(reader.apAltHold);
            MsfsData.Instance.bindings[BindingKeys.AP_HEAD_SWITCH_AP_FOLDER].SetMsfsValue(reader.apHeadingHold);
            MsfsData.Instance.bindings[BindingKeys.AP_NAV_SWITCH_AP_FOLDER].SetMsfsValue(reader.apNavHold);
            MsfsData.Instance.bindings[BindingKeys.AP_SPEED_SWITCH_AP_FOLDER].SetMsfsValue(reader.apSpeedHold);
            MsfsData.Instance.bindings[BindingKeys.AP_MASTER_SWITCH_AP_FOLDER].SetMsfsValue(reader.apMasterHold);
            MsfsData.Instance.bindings[BindingKeys.AP_THROTTLE_SWITCH_AP_FOLDER].SetMsfsValue(reader.apThrottleHold);
            MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_SWITCH_AP_FOLDER].SetMsfsValue(reader.apVerticalSpeedHold);

            MsfsData.Instance.bindings[BindingKeys.AP_ALT_AL_FOLDER].SetMsfsValue(reader.apAltitude);
            MsfsData.Instance.bindings[BindingKeys.ALT_AL_FOLDER].SetMsfsValue(reader.planeAltitude);
            MsfsData.Instance.bindings[BindingKeys.AP_HEADING_AL_FOLDER].SetMsfsValue(reader.apHeading);
            MsfsData.Instance.bindings[BindingKeys.HEADING_AL_FOLDER].SetMsfsValue((Int64)Math.Round(reader.planeHeading));
            MsfsData.Instance.bindings[BindingKeys.AP_SPEED_AL_FOLDER].SetMsfsValue(reader.apSpeed);
            MsfsData.Instance.bindings[BindingKeys.SPEED_AL_FOLDER].SetMsfsValue(reader.planeSpeed);
            MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_AL_FOLDER].SetMsfsValue(reader.apVSpeed);
            MsfsData.Instance.bindings[BindingKeys.VSPEED_AL_FOLDER].SetMsfsValue((Int64)Math.Round(reader.planeVSpeed * 60));
            MsfsData.Instance.bindings[BindingKeys.AP_FD_SWITCH_AL_FOLDER].SetMsfsValue(reader.apFD);
            MsfsData.Instance.bindings[BindingKeys.AP_ALT_SWITCH_AL_FOLDER].SetMsfsValue(reader.apAltHold);
            MsfsData.Instance.bindings[BindingKeys.AP_SWITCH_AL_FOLDER].SetMsfsValue(reader.apMasterHold);
            MsfsData.Instance.bindings[BindingKeys.AP_GPS_SWITCH_AL_FOLDER].SetMsfsValue(reader.apNavHold);
            MsfsData.Instance.bindings[BindingKeys.AP_FLC_SWITCH_AL_FOLDER].SetMsfsValue(reader.apFLC);
            MsfsData.Instance.bindings[BindingKeys.AP_APP_SWITCH_AL_FOLDER].SetMsfsValue(reader.apAPP);
            MsfsData.Instance.bindings[BindingKeys.AP_LOC_SWITCH_AL_FOLDER].SetMsfsValue(reader.apLOC);
            MsfsData.Instance.bindings[BindingKeys.AP_SPEED_SWITCH_AL_FOLDER].SetMsfsValue(reader.apSpeedHold);
            MsfsData.Instance.bindings[BindingKeys.AP_HEAD_SWITCH_AL_FOLDER].SetMsfsValue(reader.apHeadingHold);
            MsfsData.Instance.bindings[BindingKeys.AP_THROTTLE_SWITCH_AL_FOLDER].SetMsfsValue(reader.apThrottleHold);
            MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_SWITCH_AL_FOLDER].SetMsfsValue(reader.apVerticalSpeedHold);

            this.SendEvent(EVENTS.AILERON_TRIM_SET, MsfsData.Instance.bindings[BindingKeys.AILERON_TRIM]);
            this.SendEvent(EVENTS.AP_ALT_VAR_SET_ENGLISH, MsfsData.Instance.bindings[BindingKeys.AP_ALT]);
            this.SendEvent(EVENTS.AP_ALT_VAR_SET_ENGLISH, MsfsData.Instance.bindings[BindingKeys.AP_ALT_INPUT]);
            this.SendEvent(EVENTS.AP_ALT_VAR_SET_ENGLISH, MsfsData.Instance.bindings[BindingKeys.AP_ALT_AP_FOLDER]);
            this.SendEvent(EVENTS.AP_ALT_VAR_SET_ENGLISH, MsfsData.Instance.bindings[BindingKeys.AP_ALT_AL_FOLDER]);
            this.SendEvent(EVENTS.KOHLSMAN_SET, MsfsData.Instance.bindings[BindingKeys.KOHLSMAN]);
            this.SendEvent(EVENTS.ELEVATOR_TRIM_SET, MsfsData.Instance.bindings[BindingKeys.ELEVATOR_TRIM]);
            this.SendEvent(EVENTS.FLAPS_SET, MsfsData.Instance.bindings[BindingKeys.FLAP]);
            this.SendEvent(EVENTS.HEADING_BUG_SET, MsfsData.Instance.bindings[BindingKeys.AP_HEADING]);
            this.SendEvent(EVENTS.HEADING_BUG_SET, MsfsData.Instance.bindings[BindingKeys.AP_HEADING_INPUT]);
            this.SendEvent(EVENTS.HEADING_BUG_SET, MsfsData.Instance.bindings[BindingKeys.AP_HEADING_AP_FOLDER]);
            this.SendEvent(EVENTS.HEADING_BUG_SET, MsfsData.Instance.bindings[BindingKeys.AP_HEADING_AL_FOLDER]);
            this.SendEvent(EVENTS.AXIS_PROPELLER_SET, MsfsData.Instance.bindings[BindingKeys.PROPELLER]);
            this.SendEvent(EVENTS.RUDDER_TRIM_SET, MsfsData.Instance.bindings[BindingKeys.RUDDER_TRIM]);
            this.SendEvent(EVENTS.AP_SPD_VAR_SET, MsfsData.Instance.bindings[BindingKeys.AP_SPEED]);
            this.SendEvent(EVENTS.AP_SPD_VAR_SET, MsfsData.Instance.bindings[BindingKeys.AP_SPEED_INPUT]);
            this.SendEvent(EVENTS.AP_SPD_VAR_SET, MsfsData.Instance.bindings[BindingKeys.AP_SPEED_AP_FOLDER]);
            this.SendEvent(EVENTS.AP_SPD_VAR_SET, MsfsData.Instance.bindings[BindingKeys.AP_SPEED_AL_FOLDER]);
            this.SendEvent(EVENTS.AXIS_SPOILER_SET, MsfsData.Instance.bindings[BindingKeys.SPOILER]);
            this.SendEvent(EVENTS.THROTTLE_SET, MsfsData.Instance.bindings[BindingKeys.THROTTLE]);
            this.SendEvent(EVENTS.AP_VS_VAR_SET_ENGLISH, MsfsData.Instance.bindings[BindingKeys.AP_VSPEED]);
            this.SendEvent(EVENTS.AP_VS_VAR_SET_ENGLISH, MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_INPUT]);
            this.SendEvent(EVENTS.AP_VS_VAR_SET_ENGLISH, MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_AP_FOLDER]);
            this.SendEvent(EVENTS.AP_VS_VAR_SET_ENGLISH, MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_AL_FOLDER]);
            this.SendEvent(EVENTS.PARKING_BRAKE, MsfsData.Instance.bindings[BindingKeys.PARKING_BRAKES]);
            this.SendEvent(EVENTS.PITOT_HEAT_TOGGLE, MsfsData.Instance.bindings[BindingKeys.PITOT]);
            this.SendEvent(EVENTS.GEAR_TOGGLE, MsfsData.Instance.bindings[BindingKeys.GEAR_FRONT]);
            this.SendEvent(EVENTS.TOGGLE_NAV_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_NAV_MULTI]);
            this.SendEvent(EVENTS.LANDING_LIGHTS_TOGGLE, MsfsData.Instance.bindings[BindingKeys.LIGHT_LANDING_MULTI]);
            this.SendEvent(EVENTS.TOGGLE_BEACON_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_BEACON_MULTI]);
            this.SendEvent(EVENTS.TOGGLE_TAXI_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_TAXI_MULTI]);
            this.SendEvent(EVENTS.STROBES_TOGGLE, MsfsData.Instance.bindings[BindingKeys.LIGHT_STROBE_MULTI]);
            this.SendEvent(EVENTS.PANEL_LIGHTS_TOGGLE, MsfsData.Instance.bindings[BindingKeys.LIGHT_INSTRUMENT_MULTI]);
            this.SendEvent(EVENTS.TOGGLE_RECOGNITION_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_RECOG_MULTI]);
            this.SendEvent(EVENTS.TOGGLE_WING_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_WING_MULTI]);
            this.SendEvent(EVENTS.TOGGLE_LOGO_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_LOGO_MULTI]);
            this.SendEvent(EVENTS.TOGGLE_CABIN_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_CABIN_MULTI]);
            this.SendEvent(EVENTS.TOGGLE_NAV_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_NAV_FOLDER]);
            this.SendEvent(EVENTS.LANDING_LIGHTS_TOGGLE, MsfsData.Instance.bindings[BindingKeys.LIGHT_LANDING_FOLDER]);
            this.SendEvent(EVENTS.TOGGLE_BEACON_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_BEACON_FOLDER]);
            this.SendEvent(EVENTS.TOGGLE_TAXI_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_TAXI_FOLDER]);
            this.SendEvent(EVENTS.STROBES_TOGGLE, MsfsData.Instance.bindings[BindingKeys.LIGHT_STROBE_FOLDER]);
            this.SendEvent(EVENTS.PANEL_LIGHTS_TOGGLE, MsfsData.Instance.bindings[BindingKeys.LIGHT_INSTRUMENT_FOLDER]);
            this.SendEvent(EVENTS.TOGGLE_RECOGNITION_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_RECOG_FOLDER]);
            this.SendEvent(EVENTS.TOGGLE_WING_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_WING_FOLDER]);
            this.SendEvent(EVENTS.TOGGLE_LOGO_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_LOGO_FOLDER]);
            this.SendEvent(EVENTS.TOGGLE_CABIN_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_CABIN_FOLDER]);
            this.SendEvent(EVENTS.AP_PANEL_ALTITUDE_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_ALT_SWITCH_AP_FOLDER]);
            this.SendEvent(EVENTS.AP_PANEL_HEADING_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_HEAD_SWITCH_AP_FOLDER]);
            this.SendEvent(EVENTS.AP_NAV1_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_NAV_SWITCH_AP_FOLDER]);
            this.SendEvent(EVENTS.AP_PANEL_MACH_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_SPEED_SWITCH_AP_FOLDER]);
            this.SendEvent(EVENTS.AP_MASTER, MsfsData.Instance.bindings[BindingKeys.AP_MASTER_SWITCH_AP_FOLDER]);
            this.SendEvent(EVENTS.AP_N1_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_THROTTLE_SWITCH_AP_FOLDER]);
            this.SendEvent(EVENTS.AP_PANEL_VS_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_SWITCH_AP_FOLDER]);
            this.SendEvent(EVENTS.ATC_MENU_OPEN, MsfsData.Instance.bindings[BindingKeys.ATC_ATC_FOLDER]);
            this.SendEvent(EVENTS.ATC_MENU_0, MsfsData.Instance.bindings[BindingKeys.ATC_0_ATC_FOLDER]);
            this.SendEvent(EVENTS.ATC_MENU_1, MsfsData.Instance.bindings[BindingKeys.ATC_1_ATC_FOLDER]);
            this.SendEvent(EVENTS.ATC_MENU_2, MsfsData.Instance.bindings[BindingKeys.ATC_2_ATC_FOLDER]);
            this.SendEvent(EVENTS.ATC_MENU_3, MsfsData.Instance.bindings[BindingKeys.ATC_3_ATC_FOLDER]);
            this.SendEvent(EVENTS.ATC_MENU_4, MsfsData.Instance.bindings[BindingKeys.ATC_4_ATC_FOLDER]);
            this.SendEvent(EVENTS.ATC_MENU_5, MsfsData.Instance.bindings[BindingKeys.ATC_5_ATC_FOLDER]);
            this.SendEvent(EVENTS.ATC_MENU_6, MsfsData.Instance.bindings[BindingKeys.ATC_6_ATC_FOLDER]);
            this.SendEvent(EVENTS.ATC_MENU_7, MsfsData.Instance.bindings[BindingKeys.ATC_7_ATC_FOLDER]);
            this.SendEvent(EVENTS.ATC_MENU_8, MsfsData.Instance.bindings[BindingKeys.ATC_8_ATC_FOLDER]);
            this.SendEvent(EVENTS.ATC_MENU_9, MsfsData.Instance.bindings[BindingKeys.ATC_9_ATC_FOLDER]);

            this.SendEvent(EVENTS.AP_PANEL_ALTITUDE_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_ALT_SWITCH_AL_FOLDER]);
            this.SendEvent(EVENTS.AP_PANEL_HEADING_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_HEAD_SWITCH_AL_FOLDER]);
            this.SendEvent(EVENTS.AP_NAV1_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_GPS_SWITCH_AL_FOLDER]);
            this.SendEvent(EVENTS.AP_PANEL_MACH_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_SPEED_SWITCH_AL_FOLDER]);
            this.SendEvent(EVENTS.AP_MASTER, MsfsData.Instance.bindings[BindingKeys.AP_SWITCH_AL_FOLDER]);
            this.SendEvent(EVENTS.AP_N1_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_THROTTLE_SWITCH_AL_FOLDER]);
            this.SendEvent(EVENTS.AP_PANEL_VS_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_SWITCH_AL_FOLDER]);
            this.SendEvent(EVENTS.TOGGLE_FLIGHT_DIRECTOR, MsfsData.Instance.bindings[BindingKeys.AP_FD_SWITCH_AL_FOLDER]);
            this.SendEvent(EVENTS.AP_FLIGHT_LEVEL_CHANGE, MsfsData.Instance.bindings[BindingKeys.AP_FLC_SWITCH_AL_FOLDER]);
            this.SendEvent(EVENTS.AP_APR_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_APP_SWITCH_AL_FOLDER]);
            this.SendEvent(EVENTS.AP_LOC_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_LOC_SWITCH_AL_FOLDER]);

            if (MsfsData.Instance.bindings[BindingKeys.PUSHBACK_CONTROLLER].ControllerChanged)
            {
                switch (MsfsData.Instance.bindings[BindingKeys.PUSHBACK_CONTROLLER].ControllerValue)
                {
                    case 0:
                        this.SendEvent(EVENTS.TOGGLE_PUSHBACK, MsfsData.Instance.bindings[BindingKeys.PUSHBACK_CONTROLLER]);
                        break;
                    case 1:
                        this.SendEvent(EVENTS.KEY_TUG_HEADING, MsfsData.Instance.bindings[BindingKeys.PUSHBACK_CONTROLLER]);
                        break;
                    case 2:
                        this.SendEvent(EVENTS.KEY_TUG_HEADING, MsfsData.Instance.bindings[BindingKeys.PUSHBACK_CONTROLLER]);
                        break;
                    case 3:
                        this.SendEvent(EVENTS.TOGGLE_PUSHBACK, MsfsData.Instance.bindings[BindingKeys.PUSHBACK_CONTROLLER]);
                        break;
                }
                MsfsData.Instance.bindings[BindingKeys.PUSHBACK_CONTROLLER].MSFSChanged = true;
            }

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

            if (MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerChanged)
            {
                var writer = new Writers();
                writer.mixtureE1 = MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerValue;
                writer.mixtureE2 = MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerValue;
                writer.mixtureE3 = MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerValue;
                writer.mixtureE4 = MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerValue;
                this.m_oSimConnect.SetDataOnSimObject(DEFINITIONS.Writers, SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_DATA_SET_FLAG.DEFAULT, writer);
                MsfsData.Instance.bindings[BindingKeys.MIXTURE].ResetController();
            }
           this.AutoTaxiInput(reader);
           MsfsData.Instance.Changed();
        }



        private void SendEvent(EVENTS eventName, Binding binding)
        {
            if (binding.ControllerChanged)
            {
                Debug.WriteLine("Send " + eventName);
                UInt32 value;
                switch (eventName)
                {
                    case EVENTS.KOHLSMAN_SET:
                        value = (UInt32)(binding.ControllerValue / 100f * 33.8639 * 16);
                        break;
                    case EVENTS.ELEVATOR_TRIM_SET:
                        value = (UInt32)(binding.ControllerValue / 100f * 16383);
                        break;
                    case EVENTS.FLAPS_SET:
                        value = (UInt32)(binding.ControllerValue * 16383 / (MsfsData.Instance.bindings[BindingKeys.MAX_FLAP].ControllerValue == 0 ? 1 : MsfsData.Instance.bindings[BindingKeys.MAX_FLAP].ControllerValue));
                        break;
                    case EVENTS.AXIS_PROPELLER_SET:
                        value = (UInt32)Math.Round((binding.ControllerValue - 50) * 16383 / 50f);
                        break;
                    case EVENTS.AXIS_SPOILER_SET:
                        value = (UInt32)Math.Round((binding.ControllerValue - 50) * 16383 / 50f);
                        break;
                    case EVENTS.THROTTLE_SET:
                        value = (UInt32)(binding.ControllerValue / 100f * 16383);
                        break;
                    case EVENTS.TOGGLE_PUSHBACK:
                        value = (UInt32)binding.ControllerValue;
                        MsfsData.Instance.bindings[BindingKeys.PUSHBACK_STATE].MSFSChanged = true;
                        break;
                    case EVENTS.KEY_TUG_HEADING:
                        value = (UInt32)(binding.ControllerValue == 1 ? TUG_ANGLE * -0.8f : TUG_ANGLE * 0.8f);
                        break;
                    case EVENTS.ATC_MENU_OPEN:
                         this.pluginForKey.ClientApplication.SendKeyboardShortcut((VirtualKeyCode)0x91);
                        value = 0;
                        break;
                    default:
                        value = (UInt32)binding.ControllerValue;
                        break;
                }
                this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, eventName, value, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                binding.ResetController();
            }
        }
        private void OnTick()
        {
            lock (this)
            {
                try
                {
                    if (this.m_oSimConnect != null)
                    {

                        if (!MsfsData.Instance.overflow)
                        {
                            MsfsData.Instance.refreshLimiter = 0;
                            this.m_oSimConnect.RequestDataOnSimObjectType(DATA_REQUESTS.REQUEST_1, DEFINITIONS.Readers, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
                            this.m_oSimConnect.ReceiveMessage();
                        }
                        else
                        {
                            MsfsData.Instance.refreshLimiter = 0;
                            MsfsData.Instance.Changed();
                            Debug.WriteLine("Overflow handling " + MsfsData.Instance.refreshLimiter);
                            MsfsData.Instance.overflow = MsfsData.Instance.refreshLimiter >= 10;
                        }
                    }
                }
                catch (COMException exception)
                {
                    Debug.Write(exception.ToString());
                    this.Disconnect();
                }
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
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "CENTER WHEEL RPM", "RPM", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT FLIGHT DIRECTOR ACTIVE", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT FLIGHT LEVEL CHANGE", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT APPROACH HOLD", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "AUTOPILOT APPROACH IS LOCALIZER", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "SIM ON GROUND", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "GROUND VELOCITY", "Knots", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            this.m_oSimConnect.AddToDataDefinition(DEFINITIONS.Readers, "PUSHBACK ATTACHED", "Boolean", SIMCONNECT_DATATYPE.INT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            



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
            
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.TOGGLE_NAV_LIGHTS, "TOGGLE_NAV_LIGHTS");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.LANDING_LIGHTS_TOGGLE, "LANDING_LIGHTS_TOGGLE");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.TOGGLE_BEACON_LIGHTS, "TOGGLE_BEACON_LIGHTS");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.TOGGLE_TAXI_LIGHTS, "TOGGLE_TAXI_LIGHTS");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.STROBES_TOGGLE, "STROBES_TOGGLE");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.PANEL_LIGHTS_TOGGLE, "PANEL_LIGHTS_TOGGLE");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.TOGGLE_RECOGNITION_LIGHTS, "TOGGLE_RECOGNITION_LIGHTS");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.TOGGLE_WING_LIGHTS, "TOGGLE_WING_LIGHTS");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.TOGGLE_LOGO_LIGHTS, "TOGGLE_LOGO_LIGHTS");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.TOGGLE_CABIN_LIGHTS, "TOGGLE_CABIN_LIGHTS");

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
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.TOGGLE_FLIGHT_DIRECTOR, "TOGGLE_FLIGHT_DIRECTOR");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AP_FLIGHT_LEVEL_CHANGE, "AP_FLIGHT_LEVEL_CHANGE");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AP_APR_HOLD, "AP_APR_HOLD");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.AP_LOC_HOLD, "AP_LOC_HOLD");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.BRAKES, "BRAKES");
            this.m_oSimConnect.MapClientEventToSimEvent(EVENTS.THROTTLE_REVERSE_THRUST_TOGGLE, "THROTTLE_REVERSE_THRUST_TOGGLE");
            
            this.m_oSimConnect.RegisterDataDefineStruct<Readers>(DEFINITIONS.Readers);
            this.m_oSimConnect.RegisterDataDefineStruct<Writers>(DEFINITIONS.Writers);
        }

        private void AutoTaxiInput(Readers reader)
        {
            if (reader.onGround == 1)
            {
                if (MsfsData.Instance.bindings[BindingKeys.AUTO_TAXI].ControllerValue >= 2)
                {
                    if (reader.groundSpeed > 19)
                    {
                        MsfsData.Instance.bindings[BindingKeys.AUTO_TAXI].SetMsfsValue(3);
                        this.m_oSimConnect.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, EVENTS.BRAKES, 1, hSimconnect.group1, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
                    } else
                    {
                        MsfsData.Instance.bindings[BindingKeys.AUTO_TAXI].SetMsfsValue(2);
                    }
                } else
                {
                    MsfsData.Instance.bindings[BindingKeys.AUTO_TAXI].SetMsfsValue(1);
                }
            }
            else
            {
                MsfsData.Instance.bindings[BindingKeys.AUTO_TAXI].SetMsfsValue(0);
            }
        }
    }
}



