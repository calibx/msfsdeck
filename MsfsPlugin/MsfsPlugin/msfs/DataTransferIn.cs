namespace Loupedeck.MsfsPlugin.msfs
{
    using System;

    using Microsoft.FlightSimulator.SimConnect;
    using SimType = Microsoft.FlightSimulator.SimConnect.SIMCONNECT_DATATYPE;

    using static DataTransferTypes;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0049:Simplify Names", Justification = "<Pending>")]
    internal static class DataTransferIn
    {
        internal static void ReadMsfsValues(Readers reader)
        {
            MsfsData.Instance.AircraftName = reader.title;

            SetMsfsValue(BindingKeys.ENGINE_AUTO, reader.E1On);
            SetMsfsValue(BindingKeys.AILERON_TRIM, (Int64)Math.Round(reader.aileronTrim * 100));
            SetMsfsValue(BindingKeys.AP_ALT, reader.apAltitude);
            SetMsfsValue(BindingKeys.ALT, reader.planeAltitude);
            SetMsfsValue(BindingKeys.AP_ALT_INPUT, reader.apAltitude);
            SetMsfsValue(BindingKeys.ALT_INPUT, reader.planeAltitude);
            SetMsfsValue(BindingKeys.KOHLSMAN, (Int64)Math.Round(reader.kohlsmanInHb * 100));
            SetMsfsValue(BindingKeys.ELEVATOR_TRIM, (Int64)Math.Round(reader.elevatorTrim * 100));
            SetMsfsValue(BindingKeys.MAX_FLAP, reader.flapMax);
            SetMsfsValue(BindingKeys.FLAP, reader.flapPosition);
            SetMsfsValue(BindingKeys.AP_HEADING, reader.apHeading);
            SetMsfsValue(BindingKeys.HEADING, (Int64)Math.Round(reader.planeHeading));
            SetMsfsValue(BindingKeys.AP_HEADING_INPUT, reader.apHeading);
            SetMsfsValue(BindingKeys.HEADING_INPUT, (Int64)Math.Round(reader.planeHeading));
            SetMsfsValue(BindingKeys.MIXTURE, reader.mixtureE1);
            SetMsfsValue(BindingKeys.PROPELLER, (Int64)Math.Round(reader.propellerE1));
            SetMsfsValue(BindingKeys.RUDDER_TRIM, (Int64)Math.Round(reader.rudderTrim * 100));
            SetMsfsValue(BindingKeys.AP_SPEED, reader.apSpeed);
            SetMsfsValue(BindingKeys.SPEED, reader.planeSpeed);
            SetMsfsValue(BindingKeys.AP_SPEED_INPUT, reader.apSpeed);
            SetMsfsValue(BindingKeys.SPEED_INPUT, reader.planeSpeed);
            SetMsfsValue(BindingKeys.SPOILER, (Int64)Math.Round(reader.spoiler * 100));
            SetMsfsValue(BindingKeys.THROTTLE, (Int64)Math.Round(reader.throttle * 100));
            SetMsfsValue(BindingKeys.AP_VSPEED, reader.apVSpeed);
            SetMsfsValue(BindingKeys.VSPEED, (Int64)Math.Round(reader.planeVSpeed * 60));
            SetMsfsValue(BindingKeys.AP_VSPEED_INPUT, reader.apVSpeed);
            SetMsfsValue(BindingKeys.VSPEED_INPUT, (Int64)Math.Round(reader.planeVSpeed * 60));
            SetMsfsValue(BindingKeys.PARKING_BRAKES, reader.parkingBrake);
            SetMsfsValue(BindingKeys.PITOT, reader.pitot);
            SetMsfsValue(BindingKeys.GEAR_RETRACTABLE, reader.gearRetractable);
            SetMsfsValue(BindingKeys.GEAR_FRONT, (Int64)Math.Round(reader.gearCenterPos * 10));
            SetMsfsValue(BindingKeys.GEAR_LEFT, (Int64)Math.Round(reader.gearLeftPos * 10));
            SetMsfsValue(BindingKeys.GEAR_RIGHT, (Int64)Math.Round(reader.gearRightPos * 10));
            SetMsfsValue(BindingKeys.FUEL_FLOW, reader.E1GPH + reader.E2GPH + reader.E3GPH + reader.E4GPH);
            SetMsfsValue(BindingKeys.FUEL_PERCENT, (Int64)Math.Round(reader.fuelQuantity * 100.0 / reader.fuelCapacity));
            SetMsfsValue(BindingKeys.FUEL_TIME_LEFT, (Int64)(reader.fuelQuantity / (Double)(reader.E1GPH + reader.E2GPH + reader.E3GPH + reader.E4GPH) * 3600));
            SetMsfsValue(BindingKeys.AP_NEXT_WP_ID, reader.wpID);
            SetMsfsValue(BindingKeys.AP_NEXT_WP_DIST, (Int64)Math.Round(reader.wpDistance * 0.00053996f, 1));
            SetMsfsValue(BindingKeys.AP_NEXT_WP_ETE, reader.wpETE);
            SetMsfsValue(BindingKeys.AP_NEXT_WP_HEADING, reader.wpBearing);
            SetMsfsValue(BindingKeys.ENGINE_TYPE, reader.engineType);
            SetMsfsValue(BindingKeys.ENGINE_NUMBER, reader.engineNumber);
            SetMsfsValue(BindingKeys.E1RPM, reader.ENG1N1RPM);
            SetMsfsValue(BindingKeys.E2RPM, reader.ENG2N1RPM);
            SetMsfsValue(BindingKeys.E3RPM, reader.ENG3N1RPM);
            SetMsfsValue(BindingKeys.E4RPM, reader.ENG4N1RPM);
            SetMsfsValue(BindingKeys.E1N1, PercentValue(reader.E1N1));
            SetMsfsValue(BindingKeys.E2N1, PercentValue(reader.E2N1));
            SetMsfsValue(BindingKeys.E3N1, PercentValue(reader.E3N1));
            SetMsfsValue(BindingKeys.E4N1, PercentValue(reader.E4N1));
            SetMsfsValue(BindingKeys.E1N2, PercentValue(reader.E1N2));
            SetMsfsValue(BindingKeys.E2N2, PercentValue(reader.E2N2));
            SetMsfsValue(BindingKeys.E3N2, PercentValue(reader.E3N2));
            SetMsfsValue(BindingKeys.E4N2, PercentValue(reader.E4N2));
            SetMsfsValue(BindingKeys.PUSHBACK_ATTACHED, (reader.pushbackAttached == 1 && reader.wheelRPM != 0) ? 1 : 0);
            SetMsfsValue(BindingKeys.PUSHBACK_STATE, reader.onGround);
            //SetMsfsValue(BindingKeys.PUSHBACK_ANGLE, reader.pushback); // Can read but set so stay on the controller state

            SetMsfsValue(BindingKeys.LIGHT_NAV, reader.navLight);
            SetMsfsValue(BindingKeys.LIGHT_BEACON, reader.beaconLight);
            SetMsfsValue(BindingKeys.LIGHT_LANDING, reader.landingLight);
            SetMsfsValue(BindingKeys.LIGHT_TAXI, reader.taxiLight);
            SetMsfsValue(BindingKeys.LIGHT_STROBE, reader.strobeLight);
            SetMsfsValue(BindingKeys.LIGHT_INSTRUMENT, reader.panelLight);
            SetMsfsValue(BindingKeys.LIGHT_RECOG, reader.recognitionLight);
            SetMsfsValue(BindingKeys.LIGHT_WING, reader.wingLight);
            SetMsfsValue(BindingKeys.LIGHT_LOGO, reader.logoLight);
            SetMsfsValue(BindingKeys.LIGHT_CABIN, reader.cabinLight);
            SetMsfsValue(BindingKeys.LIGHT_PEDESTAL, reader.pedestralLight);
            SetMsfsValue(BindingKeys.LIGHT_GLARESHIELD, reader.glareshieldLight);

            SetMsfsValue(BindingKeys.AP_ALT_AP_FOLDER, reader.apAltitude);
            SetMsfsValue(BindingKeys.ALT_AP_FOLDER, reader.planeAltitude);
            SetMsfsValue(BindingKeys.AP_HEADING_AP_FOLDER, reader.apHeading);
            SetMsfsValue(BindingKeys.HEADING_AP_FOLDER, (Int64)Math.Round(reader.planeHeading));
            SetMsfsValue(BindingKeys.AP_SPEED_AP_FOLDER, reader.apSpeed);
            SetMsfsValue(BindingKeys.SPEED_AP_FOLDER, reader.planeSpeed);
            SetMsfsValue(BindingKeys.AP_VSPEED_AP_FOLDER, reader.apVSpeed);
            SetMsfsValue(BindingKeys.VSPEED_AP_FOLDER, (Int64)Math.Round(reader.planeVSpeed * 60));
            SetMsfsValue(BindingKeys.AP_ALT_SWITCH_AP_FOLDER, reader.apAltHold);
            SetMsfsValue(BindingKeys.AP_HEAD_SWITCH_AP_FOLDER, reader.apHeadingHold);
            SetMsfsValue(BindingKeys.AP_NAV_SWITCH_AP_FOLDER, reader.apNavHold);
            SetMsfsValue(BindingKeys.AP_SPEED_SWITCH_AP_FOLDER, reader.apSpeedHold);
            SetMsfsValue(BindingKeys.AP_MASTER_SWITCH_AP_FOLDER, reader.apMasterHold);
            SetMsfsValue(BindingKeys.AP_THROTTLE_SWITCH_AP_FOLDER, reader.apThrottleHold);
            SetMsfsValue(BindingKeys.AP_VSPEED_SWITCH_AP_FOLDER, reader.apVerticalSpeedHold);
            SetMsfsValue(BindingKeys.AP_YAW_DAMPER_AP_FOLDER, reader.apYawDamper);
            SetMsfsValue(BindingKeys.AP_BC_AP_FOLDER, reader.apBackCourse);

            SetMsfsValue(BindingKeys.AP_ALT_SWITCH, reader.apAltHold);
            SetMsfsValue(BindingKeys.AP_HEAD_SWITCH, reader.apHeadingHold);
            SetMsfsValue(BindingKeys.AP_NAV_SWITCH, reader.apNavHold);
            SetMsfsValue(BindingKeys.AP_SPEED_SWITCH, reader.apSpeedHold);
            SetMsfsValue(BindingKeys.AP_MASTER_SWITCH, reader.apMasterHold);
            SetMsfsValue(BindingKeys.AP_THROTTLE_SWITCH, reader.apThrottleHold);
            SetMsfsValue(BindingKeys.AP_VSPEED_SWITCH, reader.apVerticalSpeedHold);
            SetMsfsValue(BindingKeys.AP_YAW_DAMPER_SWITCH, reader.apYawDamper);
            SetMsfsValue(BindingKeys.AP_BC_SWITCH, reader.apBackCourse);

            SetMsfsValue(BindingKeys.AP_ALT_AL_FOLDER, reader.apAltitude);
            SetMsfsValue(BindingKeys.ALT_AL_FOLDER, reader.planeAltitude);
            SetMsfsValue(BindingKeys.AP_HEADING_AL_FOLDER, reader.apHeading);
            SetMsfsValue(BindingKeys.HEADING_AL_FOLDER, (Int64)Math.Round(reader.planeHeading));
            SetMsfsValue(BindingKeys.AP_SPEED_AL_FOLDER, reader.apSpeed);
            SetMsfsValue(BindingKeys.SPEED_AL_FOLDER, reader.planeSpeed);
            SetMsfsValue(BindingKeys.AP_VSPEED_AL_FOLDER, reader.apVSpeed);
            SetMsfsValue(BindingKeys.VSPEED_AL_FOLDER, (Int64)Math.Round(reader.planeVSpeed * 60));
            SetMsfsValue(BindingKeys.AP_FD_SWITCH_AL_FOLDER, reader.apFD);
            SetMsfsValue(BindingKeys.AP_ALT_SWITCH_AL_FOLDER, reader.apAltHold);
            SetMsfsValue(BindingKeys.AP_SWITCH_AL_FOLDER, reader.apMasterHold);
            SetMsfsValue(BindingKeys.AP_GPS_SWITCH_AL_FOLDER, reader.apNavHold);
            SetMsfsValue(BindingKeys.AP_FLC_SWITCH_AL_FOLDER, reader.apFLC);
            SetMsfsValue(BindingKeys.AP_APP_SWITCH_AL_FOLDER, reader.apAPP);
            SetMsfsValue(BindingKeys.AP_LOC_SWITCH_AL_FOLDER, reader.apLOC);
            SetMsfsValue(BindingKeys.AP_SPEED_SWITCH_AL_FOLDER, reader.apSpeedHold);
            SetMsfsValue(BindingKeys.AP_HEAD_SWITCH_AL_FOLDER, reader.apHeadingHold);
            SetMsfsValue(BindingKeys.AP_THROTTLE_SWITCH_AL_FOLDER, reader.apThrottleHold);
            SetMsfsValue(BindingKeys.AP_VSPEED_SWITCH_AL_FOLDER, reader.apVerticalSpeedHold);
            SetMsfsValue(BindingKeys.AP_YAW_DAMPER_AL_FOLDER, reader.apYawDamper);
            SetMsfsValue(BindingKeys.AP_BC_AL_FOLDER, reader.apBackCourse);

            SetMsfsValue(BindingKeys.COM1_ACTIVE_FREQUENCY, reader.COM1ActiveFreq);
            SetMsfsValue(BindingKeys.COM1_STBY, reader.COM1StbFreq);
            SetMsfsValue(BindingKeys.COM1_AVAILABLE, reader.COM1Available);
            SetMsfsValue(BindingKeys.COM1_STATUS, reader.COM1Status);
            //SetMsfsValue(BindingKeys.COM1_ACTIVE_FREQUENCY_TYPE, COMtypeToInt(reader.COM1Type));

            SetMsfsValue(BindingKeys.COM2_ACTIVE_FREQUENCY, reader.COM2ActiveFreq);
            SetMsfsValue(BindingKeys.COM2_STBY, reader.COM2StbFreq);
            SetMsfsValue(BindingKeys.COM2_AVAILABLE, reader.COM2Available);
            SetMsfsValue(BindingKeys.COM2_STATUS, reader.COM2Status);
            //SetMsfsValue(BindingKeys.COM2_ACTIVE_FREQUENCY_TYPE, this.COMtypeToInt(reader.COM2Type));

            SetMsfsValue(BindingKeys.SIM_RATE, (Int64)(reader.simRate * 100));
            SetMsfsValue(BindingKeys.SPOILERS_ARM, reader.spoilerArm);

            SetMsfsValue(BindingKeys.NAV1_ACTIVE_FREQUENCY, reader.NAV1ActiveFreq);
            SetMsfsValue(BindingKeys.NAV1_AVAILABLE, reader.NAV1Available);
            SetMsfsValue(BindingKeys.NAV1_STBY_FREQUENCY, reader.NAV1StbyFreq);
            SetMsfsValue(BindingKeys.NAV2_ACTIVE_FREQUENCY, reader.NAV2ActiveFreq);
            SetMsfsValue(BindingKeys.NAV2_AVAILABLE, reader.NAV2Available);
            SetMsfsValue(BindingKeys.NAV2_STBY_FREQUENCY, reader.NAV2StbyFreq);

            SetMsfsValue(BindingKeys.VOR1_OBS, reader.NAV1Obs);
            SetMsfsValue(BindingKeys.VOR2_OBS, reader.NAV2Obs);

            SetMsfsValue(BindingKeys.AIR_TEMP, (long)Math.Round(reader.AirTemperature * 10));
            SetMsfsValue(BindingKeys.WIND_DIRECTION, reader.WindDirection);
            SetMsfsValue(BindingKeys.WIND_SPEED, reader.WindSpeed);
            SetMsfsValue(BindingKeys.VISIBILITY, reader.Visibility);
            SetMsfsValue(BindingKeys.SEA_LEVEL_PRESSURE, (long)Math.Round(reader.SeaLevelPressure * 10));

            SetMsfsValue(BindingKeys.ADF_ACTIVE_FREQUENCY, (long)Math.Round(reader.ADFActiveFreq * 10));
            SetMsfsValue(BindingKeys.ADF_STBY_FREQUENCY, (long)Math.Round(reader.ADFStbyFreq * 10));
            SetMsfsValue(BindingKeys.ADF1_AVAILABLE, reader.ADF1Available);
            SetMsfsValue(BindingKeys.ADF1_STBY_AVAILABLE, reader.ADF2Available);

            //++ Insert appropriate SetMsfsValue calls here using the new binding keys and the new fields in reader.

            MsfsData.Instance.Changed();
        }

        // Percentages are rounded to nearest integer value:
        static Int64 PercentValue(Double value) => (Int64)Math.Round(value);

        static void SetMsfsValue(BindingKeys key, long value) => MsfsData.Instance.bindings[key].SetMsfsValue(value);

        internal static void AddRequest(SimConnect simConnect)
        {
            void AddReaderDef(string name, string units, SimType type) => AddToDataDefinition(DEFINITIONS.Readers, name, units, type);

            void AddWriterDef(string name, string units, SimType type) => AddToDataDefinition(DEFINITIONS.Writers, name, units, type);

            void AddToDataDefinition(DEFINITIONS dataDef, string name, string units, SimType type) =>
                simConnect.AddToDataDefinition(dataDef, name, units, type, 0.0f, SimConnect.SIMCONNECT_UNUSED);

            AddReaderDef("TITLE", null, SimType.STRING256);
            AddReaderDef("Plane Latitude", "degrees", SimType.FLOAT64);
            AddReaderDef("Plane Longitude", "degrees", SimType.FLOAT64);
            AddReaderDef("Ground Altitude", "meters", SimType.FLOAT64);
            AddReaderDef("GEAR RIGHT POSITION", "Boolean", SimType.FLOAT64);
            AddReaderDef("GEAR LEFT POSITION", "Boolean", SimType.FLOAT64);
            AddReaderDef("GEAR CENTER POSITION", "Boolean", SimType.FLOAT64);
            AddReaderDef("IS GEAR RETRACTABLE", "Boolean", SimType.INT64);
            AddReaderDef("BRAKE PARKING POSITION", "Boolean", SimType.INT64);
            AddReaderDef("ENGINE TYPE", "Enum", SimType.INT64);
            AddReaderDef("TURB ENG N1:1", "Percent", SimType.FLOAT64);
            AddReaderDef("TURB ENG N1:2", "Percent", SimType.FLOAT64);
            AddReaderDef("TURB ENG N1:3", "Percent", SimType.FLOAT64);
            AddReaderDef("TURB ENG N1:4", "Percent", SimType.FLOAT64);
            AddReaderDef("TURB ENG N2:1", "Percent", SimType.FLOAT64);
            AddReaderDef("TURB ENG N2:2", "Percent", SimType.FLOAT64);
            AddReaderDef("TURB ENG N2:3", "Percent", SimType.FLOAT64);
            AddReaderDef("TURB ENG N2:4", "Percent", SimType.FLOAT64);
            AddReaderDef("FUEL TOTAL CAPACITY", "Gallon", SimType.INT64);
            AddReaderDef("FUEL TOTAL QUANTITY", "Gallon", SimType.INT64);
            AddReaderDef("ENG FUEL FLOW GPH:1", "Gallons per hour", SimType.INT64);
            AddReaderDef("ENG FUEL FLOW GPH:2", "Gallons per hour", SimType.INT64);
            AddReaderDef("ENG FUEL FLOW GPH:3", "Gallons per hour", SimType.INT64);
            AddReaderDef("ENG FUEL FLOW GPH:4", "Gallons per hour", SimType.INT64);
            AddReaderDef("PUSHBACK STATE:0", "Enum", SimType.INT64);
            AddReaderDef("PROP RPM:1", "RPM", SimType.INT64);
            AddReaderDef("PROP RPM:2", "RPM", SimType.INT64);
            AddReaderDef("PROP RPM:3", "RPM", SimType.INT64);
            AddReaderDef("PROP RPM:4", "RPM", SimType.INT64);
            AddReaderDef("NUMBER OF ENGINES", "Number", SimType.INT64);
            AddReaderDef("INDICATED ALTITUDE", "Feet", SimType.INT64);
            AddReaderDef("AUTOPILOT ALTITUDE LOCK VAR", "Feet", SimType.INT64);
            AddReaderDef("GPS FLIGHT PLAN WP INDEX", "Number", SimType.INT64);
            AddReaderDef("GPS WP DISTANCE", "Meters", SimType.INT64);
            AddReaderDef("GPS WP ETE", "Seconds", SimType.INT64);
            AddReaderDef("GPS WP BEARING", "Radians", SimType.INT64);
            AddReaderDef("GPS FLIGHT PLAN WP COUNT", "Number", SimType.INT64);
            AddReaderDef("AUTOPILOT HEADING LOCK DIR", "degrees", SimType.INT64);
            AddReaderDef("PLANE HEADING DEGREES MAGNETIC", "degrees", SimType.FLOAT64);
            AddReaderDef("AIRSPEED INDICATED", "Knots", SimType.INT64);
            AddReaderDef("VERTICAL SPEED", "feet/second", SimType.FLOAT64);
            AddReaderDef("AUTOPILOT VERTICAL HOLD VAR", "Feet per minute", SimType.INT64);
            AddReaderDef("AUTOPILOT AIRSPEED HOLD VAR", "Knots", SimType.INT64);
            AddReaderDef("ENG COMBUSTION:1", "Boolean", SimType.INT64);

            AddReaderDef("LIGHT NAV", "Boolean", SimType.INT64);
            AddReaderDef("LIGHT BEACON", "Boolean", SimType.INT64);
            AddReaderDef("LIGHT LANDING", "Boolean", SimType.INT64);
            AddReaderDef("LIGHT TAXI", "Boolean", SimType.INT64);
            AddReaderDef("LIGHT STROBE", "Boolean", SimType.INT64);
            AddReaderDef("LIGHT PANEL", "Boolean", SimType.INT64);
            AddReaderDef("LIGHT RECOGNITION", "Boolean", SimType.INT64);
            AddReaderDef("LIGHT WING", "Boolean", SimType.INT64);
            AddReaderDef("LIGHT LOGO", "Boolean", SimType.INT64);
            AddReaderDef("LIGHT CABIN", "Boolean", SimType.INT64);

            AddReaderDef("AUTOPILOT ALTITUDE LOCK", "Boolean", SimType.INT64);
            AddReaderDef("AUTOPILOT HEADING LOCK", "Boolean", SimType.INT64);
            AddReaderDef("AUTOPILOT MACH HOLD", "Boolean", SimType.INT64);
            AddReaderDef("AUTOPILOT MANAGED THROTTLE ACTIVE", "Boolean", SimType.INT64);
            AddReaderDef("AUTOPILOT MASTER", "Boolean", SimType.INT64);
            AddReaderDef("AUTOPILOT NAV1 LOCK", "Boolean", SimType.INT64);
            AddReaderDef("AUTOPILOT VERTICAL HOLD", "Boolean", SimType.INT64);
            AddReaderDef("KOHLSMAN SETTING HG:1", "inHg", SimType.FLOAT64);
            AddReaderDef("AILERON TRIM PCT", "Number", SimType.FLOAT64);
            AddReaderDef("ELEVATOR TRIM PCT", "Percent Over 100", SimType.FLOAT64);
            AddReaderDef("FLAPS NUM HANDLE POSITIONS", "Number", SimType.INT64);
            AddReaderDef("FLAPS HANDLE INDEX", "Number", SimType.INT64);
            AddReaderDef("GENERAL ENG MIXTURE LEVER POSITION:1", "Percent", SimType.INT64);
            AddReaderDef("GENERAL ENG PROPELLER LEVER POSITION:1", "Percent", SimType.FLOAT64);
            AddReaderDef("RUDDER TRIM PCT", "Percent Over 100", SimType.FLOAT64);
            AddReaderDef("SPOILERS HANDLE POSITION", "Percent Over 100", SimType.FLOAT64);
            AddReaderDef("GENERAL ENG THROTTLE LEVER POSITION:1", "Percent Over 100", SimType.FLOAT64);
            AddReaderDef("PITOT HEAT SWITCH:1", "Boolean", SimType.INT64);
            AddReaderDef("CENTER WHEEL RPM", "RPM", SimType.INT64);
            AddReaderDef("AUTOPILOT FLIGHT DIRECTOR ACTIVE:1", "Boolean", SimType.INT64);
            AddReaderDef("AUTOPILOT FLIGHT LEVEL CHANGE", "Boolean", SimType.INT64);
            AddReaderDef("AUTOPILOT APPROACH HOLD", "Boolean", SimType.INT64);
            AddReaderDef("AUTOPILOT APPROACH IS LOCALIZER", "Boolean", SimType.INT64);
            AddReaderDef("SIM ON GROUND", "Boolean", SimType.INT64);
            AddReaderDef("GROUND VELOCITY", "Knots", SimType.INT64);
            AddReaderDef("PUSHBACK ATTACHED", "Boolean", SimType.INT64);
            AddReaderDef("COM ACTIVE FREQUENCY:1", "Hz", SimType.INT64);
            AddReaderDef("COM STANDBY FREQUENCY:1", "Hz", SimType.INT64);
            AddReaderDef("COM AVAILABLE:1", "Boolean", SimType.INT64);
            AddReaderDef("COM STATUS:1", "Enum", SimType.INT64);
            //this.simConnect.AddToDataDefinition(DEFINITIONS.Readers, "COM ACTIVE FREQ TYPE:1", null, SimType.STRINGV, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            AddReaderDef("COM ACTIVE FREQUENCY:2", "Hz", SimType.INT64);
            AddReaderDef("COM STANDBY FREQUENCY:2", "Hz", SimType.INT64);
            AddReaderDef("COM AVAILABLE:2", "Boolean", SimType.INT64);
            AddReaderDef("COM STATUS:2", "Enum", SimType.INT64);
            //this.simConnect.AddToDataDefinition(DEFINITIONS.Readers, "COM ACTIVE FREQ TYPE:2", null, SimType.STRINGV, 0.0f, SimConnect.SIMCONNECT_UNUSED);
            AddReaderDef("LIGHT PEDESTRAL", "Boolean", SimType.INT64);
            AddReaderDef("LIGHT GLARESHIELD", "Boolean", SimType.INT64);
            AddReaderDef("AUTOPILOT YAW DAMPER", "Boolean", SimType.INT64);
            AddReaderDef("AUTOPILOT BACKCOURSE HOLD", "Boolean", SimType.INT64);
            AddReaderDef("SIMULATION RATE", "Number", SimType.FLOAT64);
            AddReaderDef("SPOILERS ARMED", "Boolean", SimType.INT64);

            AddReaderDef("NAV ACTIVE FREQUENCY:1", "Hz", SimType.INT64);
            AddReaderDef("NAV ACTIVE FREQUENCY:2", "Hz", SimType.INT64);
            AddReaderDef("NAV AVAILABLE:1", "Boolean", SimType.INT64);
            AddReaderDef("NAV AVAILABLE:2", "Boolean", SimType.INT64);
            AddReaderDef("NAV STANDBY FREQUENCY:1", "Hz", SimType.INT64);
            AddReaderDef("NAV STANDBY FREQUENCY:2", "Hz", SimType.INT64);

            AddReaderDef("NAV OBS:1", "degrees", SimType.INT64);
            AddReaderDef("NAV OBS:2", "degrees", SimType.INT64);

            AddReaderDef("TOTAL AIR TEMPERATURE", "Celsius", SimType.FLOAT64);
            AddReaderDef("AMBIENT WIND DIRECTION", "Degrees", SimType.INT64);
            AddReaderDef("AMBIENT WIND VELOCITY", "Knots", SimType.INT64);
            AddReaderDef("AMBIENT VISIBILITY", "Meters", SimType.INT64);
            AddReaderDef("SEA LEVEL PRESSURE", "Millibars", SimType.FLOAT64);

            AddReaderDef("ADF ACTIVE FREQUENCY:1", "KHz", SimType.FLOAT64);
            AddReaderDef("ADF STANDBY FREQUENCY:1", "KHz", SimType.FLOAT64);
            AddReaderDef("ADF AVAILABLE:1", "Boolean", SimType.INT64);
            AddReaderDef("ADF STANDBY AVAILABLE:1", "Boolean", SimType.INT64);

            //++ Make new data definitions here using a type that fits SimConnect variable if it needs to be read from the Sim

            AddWriterDef("GENERAL ENG MIXTURE LEVER POSITION:1", "Percent", SimType.INT64);
            AddWriterDef("GENERAL ENG MIXTURE LEVER POSITION:2", "Percent", SimType.INT64);
            AddWriterDef("GENERAL ENG MIXTURE LEVER POSITION:3", "Percent", SimType.INT64);
            AddWriterDef("GENERAL ENG MIXTURE LEVER POSITION:4", "Percent", SimType.INT64);

            foreach (EVENTS evt in Enum.GetValues(typeof(EVENTS)))
            {
                simConnect.MapClientEventToSimEvent(evt, evt.ToString());
            }

            simConnect.RegisterDataDefineStruct<Readers>(DEFINITIONS.Readers);
            simConnect.RegisterDataDefineStruct<Writers>(DEFINITIONS.Writers);
        }
    }
}
