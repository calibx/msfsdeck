namespace SimConnectWrapper
{
    using System;
    using System.Runtime.InteropServices;


    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0049:Simplify Names", Justification = "<Pending>")]
    public static class DataTransferTypes
    {

        public enum DUMMY_EVENTS
        {
            DUMMY
        }
        internal enum DEFINITIONS
        {
            Readers,
            Writers,
        }

        internal enum EVENTS
        {
            GEAR_TOGGLE,
            PARKING_BRAKES,
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
            FLIGHT_LEVEL_CHANGE,
            AP_APR_HOLD,
            AP_LOC_HOLD,
            BRAKES,
            THROTTLE_REVERSE_THRUST_TOGGLE,
            COM_STBY_RADIO_SET_HZ,
            COM1_RADIO_SWAP,
            COM2_STBY_RADIO_SET_HZ,
            COM2_RADIO_SWAP,
            PEDESTRAL_LIGHTS_TOGGLE,
            GLARESHIELD_LIGHTS_TOGGLE,
            ALL_LIGHTS_TOGGLE,
            FLASHLIGHT,
            YAW_DAMPER_TOGGLE,
            AP_BC_HOLD,
            SIM_RATE_DECR,
            SIM_RATE_INC,
            SIM_RATE_SET,
            PLUS,
            MINUS,
            SIM_RATE,
            SPOILERS_ARM_TOGGLE,
            NAV1_RADIO_SWAP,
            NAV2_RADIO_SWAP,
            NAV1_STBY_SET_HZ,
            NAV2_STBY_SET_HZ,

            VOR1_SET,
            VOR2_SET,
            ADF1_RADIO_SWAP,
            ADF_COMPLETE_SET,
            ADF_STBY_SET,

            //++ Add new events here for data that is going to be sent from this plugin to SimConnect
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Readers
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
            public Double E1N1;
            public Double E2N1;
            public Double E3N1;
            public Double E4N1;
            public Double E1N2;
            public Double E2N2;
            public Double E3N2;
            public Double E4N2;
            public Int64 fuelCapacity;
            public Int64 fuelQuantity;
            public Double E1GPH;
            public Double E2GPH;
            public Double E3GPH;
            public Double E4GPH;
            public Double E1PPH;
            public Double E2PPH;
            public Double E3PPH;
            public Double E4PPH;
            public Int64 pushback;
            public Int64 ENG1N1RPM;
            public Int64 ENG2N1RPM;
            public Int64 ENG3N1RPM;
            public Int64 ENG4N1RPM;
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

            public Int64 COM1ActiveFreq;
            public Int64 COM1StbFreq;
            public Int64 COM1Available;
            public Int64 COM1Status;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            //public String COM1Type;

            public Int64 COM2ActiveFreq;
            public Int64 COM2StbFreq;
            public Int64 COM2Available;
            public Int64 COM2Status;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
            //public String COM2Type;
            public Int64 pedestralLight;
            public Int64 glareshieldLight;

            public Int64 apYawDamper;
            public Int64 apBackCourse;

            public Double simRate;
            public Int64 spoilerArm;

            public Int64 NAV1ActiveFreq;
            public Int64 NAV2ActiveFreq;
            public Int64 NAV1Available;
            public Int64 NAV2Available;
            public Int64 NAV1StbyFreq;
            public Int64 NAV2StbyFreq;

            public Int64 NAV1Obs;
            public Int64 NAV2Obs;

            public Double AirTemperature;
            public Int64 WindDirection;
            public Int64 WindSpeed;
            public Int64 Visibility;
            public Double SeaLevelPressure;

            public double ADFActiveFreq;
            public double ADFStbyFreq;
            public Int64 ADF1Available;
            public Int64 ADF2Available;

            //++ Add fields for new data here. Ensure that the type fits what is written in the data definition below.
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct Writers
        {
            public Int64 mixtureE1;
            public Int64 mixtureE2;
            public Int64 mixtureE3;
            public Int64 mixtureE4;
        }


    }
}
