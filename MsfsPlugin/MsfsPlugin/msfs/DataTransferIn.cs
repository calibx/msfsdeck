namespace Loupedeck.MsfsPlugin.msfs
{
    using System;
    using Loupedeck.MsfsPlugin.tools;

    using static DataTransferTypes;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0049:Simplify Names", Justification = "<Pending>")]
    internal static class DataTransferIn
    {

        public static void ReadMsfsValues(ISimConnectWrapper simConnectWrapper)
        {
            MsfsData.Instance.AircraftName = simConnectWrapper.getString("title");
            DebugTracing.Trace("parking brakes  " + simConnectWrapper.getLong("parkingBrake"));

            SetMsfsValue(BindingKeys.ENGINE_AUTO, simConnectWrapper.getLong("E1On"));
            SetMsfsValue(BindingKeys.AILERON_TRIM, (Int64)Math.Round(simConnectWrapper.getDouble("aileronTrim") * 100));
            SetMsfsValue(BindingKeys.AP_ALT, simConnectWrapper.getLong("apAltitude"));
            SetMsfsValue(BindingKeys.ALT, simConnectWrapper.getLong("planeAltitude"));
            SetMsfsValue(BindingKeys.KOHLSMAN, (Int64)Math.Round(simConnectWrapper.getDouble("kohlsmanInHb") * 10000));
            SetMsfsValue(BindingKeys.ELEVATOR_TRIM, (Int64)Math.Round(simConnectWrapper.getDouble("elevatorTrim") * 100));
            SetMsfsValue(BindingKeys.MAX_FLAP, simConnectWrapper.getLong("flapMax"));
            SetMsfsValue(BindingKeys.FLAP, simConnectWrapper.getLong("flapPosition"));
            SetMsfsValue(BindingKeys.AP_HEADING, simConnectWrapper.getLong("apHeading"));
            SetMsfsValue(BindingKeys.HEADING, (Int64)Math.Round(simConnectWrapper.getDouble("planeHeading")));
            SetMsfsValue(BindingKeys.MIXTURE, simConnectWrapper.getLong("mixtureE1"));
            SetMsfsValue(BindingKeys.PROPELLER, (Int64)Math.Round(simConnectWrapper.getDouble("propellerE1")));
            SetMsfsValue(BindingKeys.RUDDER_TRIM, (Int64)Math.Round(simConnectWrapper.getDouble("rudderTrim") * 100));
            SetMsfsValue(BindingKeys.AP_SPEED, simConnectWrapper.getLong("apSpeed"));
            SetMsfsValue(BindingKeys.SPEED, simConnectWrapper.getLong("planeSpeed"));
            SetMsfsValue(BindingKeys.SPOILER, (Int64)Math.Round(simConnectWrapper.getDouble("spoiler") * 100));
            SetMsfsValue(BindingKeys.THROTTLE, (Int64)Math.Round(simConnectWrapper.getDouble("throttle") * 100));
            SetMsfsValue(BindingKeys.AP_VSPEED, simConnectWrapper.getLong("apVSpeed"));
            SetMsfsValue(BindingKeys.VSPEED, (Int64)Math.Round(simConnectWrapper.getDouble("planeVSpeed") * 60));
            SetMsfsValue(BindingKeys.PARKING_BRAKES, simConnectWrapper.getLong("parkingBrake"));
            SetMsfsValue(BindingKeys.PITOT, simConnectWrapper.getLong("pitot"));
            SetMsfsValue(BindingKeys.GEAR_RETRACTABLE, simConnectWrapper.getLong("gearRetractable"));
            SetMsfsValue(BindingKeys.GEAR_FRONT, (Int64)Math.Round(simConnectWrapper.getDouble("gearCenterPos") * 10));
            SetMsfsValue(BindingKeys.GEAR_LEFT, (Int64)Math.Round(simConnectWrapper.getDouble("gearLeftPos") * 10));
            SetMsfsValue(BindingKeys.GEAR_RIGHT, (Int64)Math.Round(simConnectWrapper.getDouble("gearRightPos") * 10));
            SetMsfsValue(BindingKeys.FUEL_FLOW_GPH, (Int64)Math.Round(simConnectWrapper.getDouble("E1GPH")) + (Int64)Math.Round(simConnectWrapper.getDouble("E2GPH"))+ (Int64)Math.Round(simConnectWrapper.getDouble("E3GPH"))+ (Int64)Math.Round(simConnectWrapper.getDouble("E4GPH")));
            SetMsfsValue(BindingKeys.FUEL_FLOW_PPH, (Int64)Math.Round((simConnectWrapper.getDouble("E1PPH") + simConnectWrapper.getDouble("E1PPH") + simConnectWrapper.getDouble("E1PPH") + simConnectWrapper.getDouble("E1PPH")) * 100.0));
            SetMsfsValue(BindingKeys.FUEL_PERCENT, (Int64)Math.Round(simConnectWrapper.getLong("fuelQuantity") * 100.0 / simConnectWrapper.getLong("fuelCapacity")));
            SetMsfsValue(BindingKeys.FUEL_TIME_LEFT, (Int64)(simConnectWrapper.getLong("fuelQuantity") / (1+(Int64)Math.Round(simConnectWrapper.getDouble("E1GPH")) + (Int64)Math.Round(simConnectWrapper.getDouble("E2GPH")) + (Int64)Math.Round(simConnectWrapper.getDouble("E3GPH")) + (Int64)Math.Round(simConnectWrapper.getDouble("E4GPH"))) * 3600));
            SetMsfsValue(BindingKeys.AP_NEXT_WP_ID, simConnectWrapper.getLong("wpID"));
            SetMsfsValue(BindingKeys.AP_NEXT_WP_DIST, (Int64)Math.Round(simConnectWrapper.getLong("wpDistance") * 0.00053996f * 10, 1));
            SetMsfsValue(BindingKeys.AP_NEXT_WP_ETE, simConnectWrapper.getLong("wpETE"));
            SetMsfsValue(BindingKeys.AP_NEXT_WP_HEADING, simConnectWrapper.getLong("wpBearing"));
            SetMsfsValue(BindingKeys.ENGINE_TYPE, simConnectWrapper.getLong("engineType"));
            SetMsfsValue(BindingKeys.ENGINE_NUMBER, simConnectWrapper.getLong("engineNumber"));
            SetMsfsValue(BindingKeys.E1RPM, simConnectWrapper.getLong("ENG1N1RPM"));
            SetMsfsValue(BindingKeys.E2RPM, simConnectWrapper.getLong("ENG2N1RPM"));
            SetMsfsValue(BindingKeys.E3RPM, simConnectWrapper.getLong("ENG3N1RPM"));
            SetMsfsValue(BindingKeys.E4RPM, simConnectWrapper.getLong("ENG4N1RPM"));
            SetMsfsValue(BindingKeys.E1N1, PercentValue(simConnectWrapper.getDouble("E1N1")));
            SetMsfsValue(BindingKeys.E2N1, PercentValue(simConnectWrapper.getDouble("E2N1")));
            SetMsfsValue(BindingKeys.E3N1, PercentValue(simConnectWrapper.getDouble("E3N1")));
            SetMsfsValue(BindingKeys.E4N1, PercentValue(simConnectWrapper.getDouble("E4N1")));
            SetMsfsValue(BindingKeys.E1N2, PercentValue(simConnectWrapper.getDouble("E1N2")));
            SetMsfsValue(BindingKeys.E2N2, PercentValue(simConnectWrapper.getDouble("E2N2")));
            SetMsfsValue(BindingKeys.E3N2, PercentValue(simConnectWrapper.getDouble("E3N2")));
            SetMsfsValue(BindingKeys.E4N2, PercentValue(simConnectWrapper.getDouble("E4N2")));
            SetMsfsValue(BindingKeys.PUSHBACK_ATTACHED, (simConnectWrapper.getLong("pushbackAttached") == 1 && simConnectWrapper.getLong("wheelRPM") != 0) ? 1 : 0);
            SetMsfsValue(BindingKeys.PUSHBACK_STATE, simConnectWrapper.getLong("onGround"));
            //SetMsfsValue(BindingKeys.PUSHBACK_ANGLE, simConnectWrapper.getLong("pushback); // Can read but set so stay on the controller state

            SetMsfsValue(BindingKeys.LIGHT_NAV, simConnectWrapper.getLong("navLight"));
            SetMsfsValue(BindingKeys.LIGHT_BEACON, simConnectWrapper.getLong("beaconLight"));
            SetMsfsValue(BindingKeys.LIGHT_LANDING, simConnectWrapper.getLong("landingLight"));
            SetMsfsValue(BindingKeys.LIGHT_TAXI, simConnectWrapper.getLong("taxiLight"));
            SetMsfsValue(BindingKeys.LIGHT_STROBE, simConnectWrapper.getLong("strobeLight"));
            SetMsfsValue(BindingKeys.LIGHT_INSTRUMENT, simConnectWrapper.getLong("panelLight"));
            SetMsfsValue(BindingKeys.LIGHT_RECOG, simConnectWrapper.getLong("recognitionLight"));
            SetMsfsValue(BindingKeys.LIGHT_WING, simConnectWrapper.getLong("wingLight"));
            SetMsfsValue(BindingKeys.LIGHT_LOGO, simConnectWrapper.getLong("logoLight"));
            SetMsfsValue(BindingKeys.LIGHT_CABIN, simConnectWrapper.getLong("cabinLight"));
            SetMsfsValue(BindingKeys.LIGHT_PEDESTAL, simConnectWrapper.getLong("pedestralLight"));
            SetMsfsValue(BindingKeys.LIGHT_GLARESHIELD, simConnectWrapper.getLong("glareshieldLight"));

            SetMsfsValue(BindingKeys.AP_ALT_SWITCH, simConnectWrapper.getLong("apAltHold"));
            SetMsfsValue(BindingKeys.AP_HEAD_SWITCH, simConnectWrapper.getLong("apHeadingHold"));
            SetMsfsValue(BindingKeys.AP_NAV_SWITCH, simConnectWrapper.getLong("apNavHold"));
            SetMsfsValue(BindingKeys.AP_SPEED_SWITCH, simConnectWrapper.getLong("apSpeedHold"));
            SetMsfsValue(BindingKeys.AP_MASTER_SWITCH, simConnectWrapper.getLong("apMasterHold"));
            SetMsfsValue(BindingKeys.AP_THROTTLE_SWITCH, simConnectWrapper.getLong("apThrottleHold"));
            SetMsfsValue(BindingKeys.AP_VSPEED_SWITCH, simConnectWrapper.getLong("apVerticalSpeedHold"));
            SetMsfsValue(BindingKeys.AP_YAW_DAMPER_SWITCH, simConnectWrapper.getLong("apYawDamper"));
            SetMsfsValue(BindingKeys.AP_BC_SWITCH, simConnectWrapper.getLong("apBackCourse"));

            SetMsfsValue(BindingKeys.AP_FD_SWITCH, simConnectWrapper.getLong("apFD"));
            SetMsfsValue(BindingKeys.AP_FLC_SWITCH, simConnectWrapper.getLong("apFLC"));
            SetMsfsValue(BindingKeys.AP_APP_SWITCH, simConnectWrapper.getLong("apAPP"));
            SetMsfsValue(BindingKeys.AP_LOC_SWITCH, simConnectWrapper.getLong("apLOC"));

            SetMsfsValue(BindingKeys.COM1_ACTIVE_FREQUENCY, simConnectWrapper.getLong("COM1ActiveFreq"));
            SetMsfsValue(BindingKeys.COM1_STBY, simConnectWrapper.getLong("COM1StbFreq"));
            SetMsfsValue(BindingKeys.COM1_AVAILABLE, simConnectWrapper.getLong("COM1Available"));
            //SetMsfsValue(BindingKeys.COM1_STATUS, simConnectWrapper.getLong("COM1Status);
            //SetMsfsValue(BindingKeys.COM1_ACTIVE_FREQUENCY_TYPE, COMtypeToInt(simConnectWrapper.getLong("COM1Type));

            SetMsfsValue(BindingKeys.COM2_ACTIVE_FREQUENCY, simConnectWrapper.getLong("COM2ActiveFreq"));
            SetMsfsValue(BindingKeys.COM2_STBY, simConnectWrapper.getLong("COM2StbFreq"));
            SetMsfsValue(BindingKeys.COM2_AVAILABLE, simConnectWrapper.getLong("COM2Available"));
            //SetMsfsValue(BindingKeys.COM2_STATUS, simConnectWrapper.getLong("COM2Status);
            //SetMsfsValue(BindingKeys.COM2_ACTIVE_FREQUENCY_TYPE, this.COMtypeToInt(simConnectWrapper.getLong("COM2Type));

            SetMsfsValue(BindingKeys.SIM_RATE, (Int64)(simConnectWrapper.getDouble("simRate") * 100));
            SetMsfsValue(BindingKeys.SPOILERS_ARM, simConnectWrapper.getLong("spoilerArm"));

            SetMsfsValue(BindingKeys.NAV1_ACTIVE_FREQUENCY, simConnectWrapper.getLong("NAV1ActiveFreq"));
            SetMsfsValue(BindingKeys.NAV1_AVAILABLE, simConnectWrapper.getLong("NAV1Available"));
            SetMsfsValue(BindingKeys.NAV1_STBY_FREQUENCY, simConnectWrapper.getLong("NAV1StbyFreq"));
            SetMsfsValue(BindingKeys.NAV2_ACTIVE_FREQUENCY, simConnectWrapper.getLong("NAV2ActiveFreq"));
            SetMsfsValue(BindingKeys.NAV2_AVAILABLE, simConnectWrapper.getLong("NAV2Available"));
            SetMsfsValue(BindingKeys.NAV2_STBY_FREQUENCY, simConnectWrapper.getLong("NAV2StbyFreq"));

            SetMsfsValue(BindingKeys.VOR1_OBS, simConnectWrapper.getLong("NAV1Obs"));
            SetMsfsValue(BindingKeys.VOR2_OBS, simConnectWrapper.getLong("NAV2Obs"));

            SetMsfsValue(BindingKeys.AIR_TEMP, (long)Math.Round(simConnectWrapper.getDouble("AirTemperature") * 10));
            SetMsfsValue(BindingKeys.WIND_DIRECTION, simConnectWrapper.getLong("WindDirection"));
            SetMsfsValue(BindingKeys.WIND_SPEED, simConnectWrapper.getLong("WindSpeed"));
            SetMsfsValue(BindingKeys.VISIBILITY, simConnectWrapper.getLong("Visibility"));
            SetMsfsValue(BindingKeys.SEA_LEVEL_PRESSURE, (long)Math.Round(simConnectWrapper.getDouble("SeaLevelPressure") * 10));

            SetMsfsValue(BindingKeys.ADF_ACTIVE_FREQUENCY, (long)Math.Round(simConnectWrapper.getDouble("ADFActiveFreq") * 10));
            SetMsfsValue(BindingKeys.ADF_STBY_FREQUENCY, (long)Math.Round(simConnectWrapper.getDouble("ADFStbyFreq") * 10));
            SetMsfsValue(BindingKeys.ADF1_AVAILABLE, simConnectWrapper.getLong("ADF1Available"));
            SetMsfsValue(BindingKeys.ADF1_STBY_AVAILABLE, simConnectWrapper.getLong("ADF2Available"));

            //++ Insert appropriate SetMsfsValue calls here using the new binding keys and the new fields in simConnectWrapper.getLong("

            MsfsData.Instance.Changed(false);
        }

        // Percentages are rounded to nearest integer value:
        static Int64 PercentValue(Double value) => (Int64)Math.Round(value);

        static void SetMsfsValue(BindingKeys key, long value) => MsfsData.Instance.bindings[key].SetMsfsValue(value);

       
        
    }
}
