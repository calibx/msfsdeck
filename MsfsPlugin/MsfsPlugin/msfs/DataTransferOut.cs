namespace Loupedeck.MsfsPlugin.msfs
{
    using System;
    using System.Collections.Generic;

    using Loupedeck.MsfsPlugin.msfs.mobi;
    using Loupedeck.MsfsPlugin.tools;

    using static DataTransferTypes;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0049:Simplify Names", Justification = "<Pending>")]
    internal static class DataTransferOut
    {
        internal static Dictionary<String, Dictionary<String, Dictionary<String, List<Tuple<String, uint>>>>> MobiEvents;
        private const string STANDARD_EVENT_GROUP = "STANDARD";

        internal static void setPlugin(Plugin plugin) => pluginForKey = plugin;
        internal static void SendEvents(ISimConnectWrapper simConnect)
        {
            SendEvent(EVENTS.AILERON_TRIM_SET, MsfsData.Instance.bindings[BindingKeys.AILERON_TRIM], simConnect);
            SendEvent(EVENTS.AP_ALT_VAR_SET_ENGLISH, MsfsData.Instance.bindings[BindingKeys.AP_ALT], simConnect);
            SendEvent(EVENTS.KOHLSMAN_SET, MsfsData.Instance.bindings[BindingKeys.KOHLSMAN], simConnect);
            SendEvent(EVENTS.ELEVATOR_TRIM_SET, MsfsData.Instance.bindings[BindingKeys.ELEVATOR_TRIM], simConnect);
            SendEvent(EVENTS.FLAPS_SET, MsfsData.Instance.bindings[BindingKeys.FLAP], simConnect);
            SendEvent(EVENTS.HEADING_BUG_SET, MsfsData.Instance.bindings[BindingKeys.AP_HEADING], simConnect);
            SendEvent(EVENTS.AXIS_PROPELLER_SET, MsfsData.Instance.bindings[BindingKeys.PROPELLER], simConnect);
            SendEvent(EVENTS.RUDDER_TRIM_SET, MsfsData.Instance.bindings[BindingKeys.RUDDER_TRIM], simConnect);
            SendEvent(EVENTS.AP_SPD_VAR_SET, MsfsData.Instance.bindings[BindingKeys.AP_SPEED], simConnect);
            SendEvent(EVENTS.AXIS_SPOILER_SET, MsfsData.Instance.bindings[BindingKeys.SPOILER], simConnect);
            SendEvent(EVENTS.THROTTLE_SET, MsfsData.Instance.bindings[BindingKeys.THROTTLE], simConnect);
            SendEvent(EVENTS.AP_VS_VAR_SET_ENGLISH, MsfsData.Instance.bindings[BindingKeys.AP_VSPEED], simConnect);
            SendEvent(EVENTS.PARKING_BRAKES, MsfsData.Instance.bindings[BindingKeys.PARKING_BRAKES], simConnect);
            SendEvent(EVENTS.PITOT_HEAT_TOGGLE, MsfsData.Instance.bindings[BindingKeys.PITOT], simConnect, true);
            SendEvent(EVENTS.GEAR_TOGGLE, MsfsData.Instance.bindings[BindingKeys.GEAR_FRONT], simConnect);
            SendEvent(EVENTS.TOGGLE_NAV_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_NAV], simConnect, true);
            SendEvent(EVENTS.LANDING_LIGHTS_TOGGLE, MsfsData.Instance.bindings[BindingKeys.LIGHT_LANDING], simConnect, true);
            SendEvent(EVENTS.TOGGLE_BEACON_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_BEACON], simConnect, true);
            SendEvent(EVENTS.TOGGLE_TAXI_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_TAXI], simConnect, true);
            SendEvent(EVENTS.STROBES_TOGGLE, MsfsData.Instance.bindings[BindingKeys.LIGHT_STROBE], simConnect, true);
            SendEvent(EVENTS.PANEL_LIGHTS_TOGGLE, MsfsData.Instance.bindings[BindingKeys.LIGHT_INSTRUMENT], simConnect, true);
            SendEvent(EVENTS.TOGGLE_RECOGNITION_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_RECOG], simConnect, true);
            SendEvent(EVENTS.TOGGLE_WING_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_WING], simConnect, true);
            SendEvent(EVENTS.TOGGLE_LOGO_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_LOGO], simConnect, true);
            SendEvent(EVENTS.TOGGLE_CABIN_LIGHTS, MsfsData.Instance.bindings[BindingKeys.LIGHT_CABIN], simConnect, true);
            SendEvent(EVENTS.PEDESTRAL_LIGHTS_TOGGLE, MsfsData.Instance.bindings[BindingKeys.LIGHT_PEDESTAL], simConnect, true);
            SendEvent(EVENTS.GLARESHIELD_LIGHTS_TOGGLE, MsfsData.Instance.bindings[BindingKeys.LIGHT_GLARESHIELD], simConnect, true);
            SendEvent(EVENTS.ALL_LIGHTS_TOGGLE, MsfsData.Instance.bindings[BindingKeys.LIGHT_ALL_SWITCH], simConnect, true);

            SendEvent(EVENTS.AP_PANEL_ALTITUDE_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_ALT_SWITCH], simConnect);
            SendEvent(EVENTS.AP_PANEL_HEADING_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_HEAD_SWITCH], simConnect);
            SendEvent(EVENTS.AP_NAV1_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_NAV_SWITCH], simConnect);
            SendEvent(EVENTS.AP_PANEL_MACH_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_SPEED_SWITCH], simConnect);
            SendEvent(EVENTS.AP_MASTER, MsfsData.Instance.bindings[BindingKeys.AP_MASTER_SWITCH], simConnect);
            SendEvent(EVENTS.AP_N1_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_THROTTLE_SWITCH], simConnect);
            SendEvent(EVENTS.AP_PANEL_VS_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_VSPEED_SWITCH], simConnect);
            SendEvent(EVENTS.YAW_DAMPER_TOGGLE, MsfsData.Instance.bindings[BindingKeys.AP_YAW_DAMPER_SWITCH], simConnect);
            SendEvent(EVENTS.AP_BC_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_BC_SWITCH], simConnect);

            SendEvent(EVENTS.ATC_MENU_OPEN, MsfsData.Instance.bindings[BindingKeys.ATC_ATC_FOLDER], simConnect);
            SendEvent(EVENTS.ATC_MENU_0, MsfsData.Instance.bindings[BindingKeys.ATC_0_ATC_FOLDER], simConnect);
            SendEvent(EVENTS.ATC_MENU_1, MsfsData.Instance.bindings[BindingKeys.ATC_1_ATC_FOLDER], simConnect);
            SendEvent(EVENTS.ATC_MENU_2, MsfsData.Instance.bindings[BindingKeys.ATC_2_ATC_FOLDER], simConnect);
            SendEvent(EVENTS.ATC_MENU_3, MsfsData.Instance.bindings[BindingKeys.ATC_3_ATC_FOLDER], simConnect);
            SendEvent(EVENTS.ATC_MENU_4, MsfsData.Instance.bindings[BindingKeys.ATC_4_ATC_FOLDER], simConnect);
            SendEvent(EVENTS.ATC_MENU_5, MsfsData.Instance.bindings[BindingKeys.ATC_5_ATC_FOLDER], simConnect);
            SendEvent(EVENTS.ATC_MENU_6, MsfsData.Instance.bindings[BindingKeys.ATC_6_ATC_FOLDER], simConnect);
            SendEvent(EVENTS.ATC_MENU_7, MsfsData.Instance.bindings[BindingKeys.ATC_7_ATC_FOLDER], simConnect);
            SendEvent(EVENTS.ATC_MENU_8, MsfsData.Instance.bindings[BindingKeys.ATC_8_ATC_FOLDER], simConnect);
            SendEvent(EVENTS.ATC_MENU_9, MsfsData.Instance.bindings[BindingKeys.ATC_9_ATC_FOLDER], simConnect);

            SendEvent(EVENTS.TOGGLE_FLIGHT_DIRECTOR, MsfsData.Instance.bindings[BindingKeys.AP_FD_SWITCH], simConnect);
            SendEvent(EVENTS.FLIGHT_LEVEL_CHANGE, MsfsData.Instance.bindings[BindingKeys.AP_FLC_SWITCH], simConnect);
            SendEvent(EVENTS.AP_APR_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_APP_SWITCH], simConnect);
            SendEvent(EVENTS.AP_LOC_HOLD, MsfsData.Instance.bindings[BindingKeys.AP_LOC_SWITCH], simConnect);

            SendEvent(EVENTS.COM_STBY_RADIO_SET_HZ, MsfsData.Instance.bindings[BindingKeys.COM1_STBY], simConnect);
            SendEvent(EVENTS.COM1_RADIO_SWAP, MsfsData.Instance.bindings[BindingKeys.COM1_RADIO_SWAP], simConnect);
            SendEvent(EVENTS.COM2_STBY_RADIO_SET_HZ, MsfsData.Instance.bindings[BindingKeys.COM2_STBY], simConnect);
            SendEvent(EVENTS.COM2_RADIO_SWAP, MsfsData.Instance.bindings[BindingKeys.COM2_RADIO_SWAP], simConnect);
            SendEvent(EVENTS.FLASHLIGHT, MsfsData.Instance.bindings[BindingKeys.FLASHLIGHT], simConnect);
            SendEvent(EVENTS.SIM_RATE, MsfsData.Instance.bindings[BindingKeys.SIM_RATE], simConnect);
            SendEvent(EVENTS.SPOILERS_ARM_TOGGLE, MsfsData.Instance.bindings[BindingKeys.SPOILERS_ARM], simConnect);

            SendEvent(EVENTS.NAV1_STBY_SET_HZ, MsfsData.Instance.bindings[BindingKeys.NAV1_STBY_FREQUENCY], simConnect);
            SendEvent(EVENTS.NAV2_STBY_SET_HZ, MsfsData.Instance.bindings[BindingKeys.NAV2_STBY_FREQUENCY], simConnect);
            SendEvent(EVENTS.NAV1_RADIO_SWAP, MsfsData.Instance.bindings[BindingKeys.NAV1_RADIO_SWAP], simConnect);
            SendEvent(EVENTS.NAV2_RADIO_SWAP, MsfsData.Instance.bindings[BindingKeys.NAV2_RADIO_SWAP], simConnect);

            SendEvent(EVENTS.VOR1_SET, MsfsData.Instance.bindings[BindingKeys.VOR1_SET], simConnect);
            SendEvent(EVENTS.VOR2_SET, MsfsData.Instance.bindings[BindingKeys.VOR2_SET], simConnect);
            SendEvent(EVENTS.ADF1_RADIO_SWAP, MsfsData.Instance.bindings[BindingKeys.ADF_RADIO_SWAP], simConnect);
            SendEvent(EVENTS.ADF_COMPLETE_SET, MsfsData.Instance.bindings[BindingKeys.ADF_ACTIVE_FREQUENCY], simConnect);   // Used in planes where we can only manipulate the active frequency
            SendEvent(EVENTS.ADF_STBY_SET, MsfsData.Instance.bindings[BindingKeys.ADF_STBY_FREQUENCY], simConnect);

            //++ Insert appropriate SendEvent calls here. Use the new binding key and the new event "matching" it.

            if (MsfsData.Instance.bindings[BindingKeys.PUSHBACK_CONTROLLER].ControllerChanged)
            {
                switch (MsfsData.Instance.bindings[BindingKeys.PUSHBACK_CONTROLLER].ControllerValue)
                {
                    case 0:
                    case 3:
                        SendEvent(EVENTS.TOGGLE_PUSHBACK, MsfsData.Instance.bindings[BindingKeys.PUSHBACK_CONTROLLER], simConnect);
                        break;
                    case 1:
                    case 2:
                        SendEvent(EVENTS.KEY_TUG_HEADING, MsfsData.Instance.bindings[BindingKeys.PUSHBACK_CONTROLLER], simConnect);
                        break;
                }
                MsfsData.Instance.bindings[BindingKeys.PUSHBACK_CONTROLLER].MSFSChanged = true;
            }

            if (MsfsData.Instance.bindings[BindingKeys.ENGINE_AUTO].MsfsValue == 1)
            {
                SendEvent(EVENTS.ENGINE_AUTO_SHUTDOWN, MsfsData.Instance.bindings[BindingKeys.ENGINE_AUTO], simConnect);
            }
            else
            {
                SendEvent(EVENTS.ENGINE_AUTO_START, MsfsData.Instance.bindings[BindingKeys.ENGINE_AUTO], simConnect);
            }
            if (MsfsData.Instance.bindings[BindingKeys.PAUSE].ControllerChanged)
            {
                if (MsfsData.Instance.bindings[BindingKeys.PAUSE].MsfsValue == 1)
                {
                    SendEvent(EVENTS.PAUSE_OFF, MsfsData.Instance.bindings[BindingKeys.PAUSE], simConnect);
                    MsfsData.Instance.bindings[BindingKeys.PAUSE].SetMsfsValue(0);
                    MsfsData.Instance.bindings[BindingKeys.PAUSE].MSFSChanged = true;
                }
                else
                {
                    SendEvent(EVENTS.PAUSE_ON, MsfsData.Instance.bindings[BindingKeys.PAUSE], simConnect);
                    MsfsData.Instance.bindings[BindingKeys.PAUSE].SetMsfsValue(1);
                    MsfsData.Instance.bindings[BindingKeys.PAUSE].MSFSChanged = true;
                }
            }

            // TODO : WRITERQ TO FIX LATER
/*            if (MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerChanged)
            {
                var writer = new Writers();
                writer.mixtureE1 = MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerValue;
                writer.mixtureE2 = MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerValue;
                writer.mixtureE3 = MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerValue;
                writer.mixtureE4 = MsfsData.Instance.bindings[BindingKeys.MIXTURE].ControllerValue;
                simConnect.SetDataOnSimObject(DEFINITIONS.Writers, SimConnect.SIMCONNECT_OBJECT_ID_USER, SIMCONNECT_DATA_SET_FLAG.DEFAULT, writer);
                MsfsData.Instance.bindings[BindingKeys.MIXTURE].ResetController();
            }*/
        }

        private static void SendEvent(EVENTS eventName, Binding binding, ISimConnectWrapper simConnect, Boolean enumerable = false)
        {
            if (binding.ControllerChanged)
            {
                UInt32 value;
                switch (eventName)
                {
                    case EVENTS.KOHLSMAN_SET:
                        value = ConvertTool.RoundToUint(binding.ControllerValue / 10000f * ConvertTool.inHg2mbar * 16);
                        break;
                    case EVENTS.ELEVATOR_TRIM_SET:
                        value = ConvertTool.RoundToUint(binding.ControllerValue / 100f * 16383);
                        break;
                    case EVENTS.FLAPS_SET:
                        value = ConvertTool.RoundToUint(binding.ControllerValue * 16383 / (MsfsData.Instance.bindings[BindingKeys.MAX_FLAP].ControllerValue == 0 ? 1 : MsfsData.Instance.bindings[BindingKeys.MAX_FLAP].ControllerValue));
                        break;
                    case EVENTS.AXIS_PROPELLER_SET:
                    case EVENTS.AXIS_SPOILER_SET:
                        value = ConvertTool.RoundToUint((binding.ControllerValue - 50) * 16383 / 50f);
                        break;
                    case EVENTS.THROTTLE_SET:
                        value = ConvertTool.RoundToUint(binding.ControllerValue / 100f * 16383);
                        break;
                    case EVENTS.TOGGLE_PUSHBACK:
                        value = ConvertTool.RoundToUint(binding.ControllerValue);
                        MsfsData.Instance.bindings[BindingKeys.PUSHBACK_STATE].MSFSChanged = true;
                        break;
                    case EVENTS.KEY_TUG_HEADING:
                        value = ConvertTool.RoundToUint(binding.ControllerValue == 1 ? TUG_ANGLE * -0.8f : TUG_ANGLE * 0.8f);
                        break;
                    case EVENTS.SIM_RATE:
                        value = 1;
                        simConnect.send(eventName, 1);
                        eventName = binding.ControllerValue < binding.MsfsValue ? EVENTS.MINUS : EVENTS.PLUS;
                        break;
                    case EVENTS.ATC_MENU_OPEN:
                        pluginForKey.KeyboardApi.SendShortcut(VirtualKeyCode.ScrollLock, ModifierKey.None);
                        value = 0;
                        break;
                    case EVENTS.FLASHLIGHT:
                        pluginForKey.KeyboardApi.SendShortcut(VirtualKeyCode.KeyL, ModifierKey.Alt);
                        value = 0;
                        break;
                    case EVENTS.ADF_COMPLETE_SET:
                    case EVENTS.ADF_STBY_SET:
                        value = BcdEncode(ConvertTool.RoundToUint(binding.ControllerValue / 10));
                        break;

                    //++ If the new binding cannot use the default way of sending, add a new case above.
                    default:
                        value = ConvertTool.RoundToUint(binding.ControllerValue);
                        break;
                }
                if (enumerable)
                {
                    for (UInt32 i = 1; i < 10; i++)
                    {
                        simConnect.send(eventName, i);
                    }
                }
                else
                {
                    simConnect.send(eventName, value);
                }

                binding.ResetController();
            }
        }

        

        private static uint BcdEncode(uint value)
        {
            var valText = $"0x{value}0000";
            return Convert.ToUInt32(valText, 16);
        }

        internal static void loadEvents()
        {
            if (MobiEvents == null)
            {
                MobiEvents = new Dictionary<string, Dictionary<string, Dictionary<string, List<Tuple<String, uint>>>>>();


                var lines = EmbeddedResources.ReadTextFile("Loupedeck.MsfsPlugin.Resources.msfs2020_eventids.cip").Split("\n");
                uint EventIdx = 0;

                var VendorKey = "DUMMY";
                var AircraftKey = "DUMMY";
                var ComponentKey = "DUMMY";
                foreach (var line in lines)
                {
                    if (line.StartsWith("//"))
                        continue;
                    var groups = line.Split(':');
                    if (groups.Length > 1) // group detection
                    {
                        var cols = line.Split('/');
                        VendorKey = cols[0];
                        AircraftKey = cols[1];
                        ComponentKey = cols[2].Split(":")[0];
                        if (!MobiEvents.ContainsKey(VendorKey))
                            MobiEvents[VendorKey] = new Dictionary<string, Dictionary<string, List<Tuple<String, uint>>>>();
                        if (!MobiEvents[VendorKey].ContainsKey(AircraftKey))
                            MobiEvents[VendorKey][AircraftKey] = new Dictionary<string, List<Tuple<String, uint>>>();
                        if (!MobiEvents[VendorKey][AircraftKey].ContainsKey(ComponentKey))
                            MobiEvents[VendorKey][AircraftKey][ComponentKey] = new List<Tuple<String, uint>>();
                    }
                    else
                    {
                        MobiEvents[VendorKey][AircraftKey][ComponentKey].Add(new Tuple<string, uint>(line, EventIdx++));
                    }
                }
            }
        }
        internal static void initEvents()
        {
            foreach (EVENTS evt in Enum.GetValues(typeof(EVENTS)))
            {
                SimConnectWrapper.Instance.register(evt, evt.ToString());
            }
            foreach (var GroupKey in MobiEvents.Keys)
            {
                foreach (var AircraftKey in MobiEvents[GroupKey].Keys)
                {
                    foreach (var ComponentKey in MobiEvents[GroupKey][AircraftKey].Keys)
                    {
                        foreach (Tuple<string, uint> eventItem in MobiEvents[GroupKey][AircraftKey][ComponentKey])
                        {
                            var prefix = "";
                            if (GroupKey != STANDARD_EVENT_GROUP)
                            {
                                prefix = "MobiFlight.";
                            }
                            SimConnectWrapper.Instance.register((MOBIFLIGHT_EVENTS)eventItem.Item2, prefix + eventItem.Item1);
                        }
                    }
                }
            }
        }

        private static Plugin pluginForKey;
        private const UInt32 TUG_ANGLE = 4294967295;
    }
}
