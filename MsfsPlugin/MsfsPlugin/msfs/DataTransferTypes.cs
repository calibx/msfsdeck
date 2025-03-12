namespace Loupedeck.MsfsPlugin.msfs
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
        internal struct Writers
        {
            public Int64 mixtureE1;
            public Int64 mixtureE2;
            public Int64 mixtureE3;
            public Int64 mixtureE4;
        }


    }
}
